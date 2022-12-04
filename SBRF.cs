using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace SBRF_Soft
{
    public partial class SBRF : Form
    {
        enum code
        {
            /* Оплата         */
            ePay = 1,
            /* Возврат        */
            eRefund = 3,
            /* Закрытие смены */
            eCloseShift = 7
        }

        // Execute code
        const string cProcessCMD = "cmd.exe";
        const string cLoadParam = "/Cloadparm.exe {0:D} {1:D}";

        const string cPay = "[Смена {0} - {1}] Оплата на сумму: {2:F} руб.";
        const string cPaySuccess = "Оплата прошла успешно!";

        const string cRefund = "[Смена {0} - {1}] Возврат на сумму: {2:F} руб.";
        const string cRefundSuccess = "Возврат пройден успешно!";

        const string cCloseShift = "Смена закрыта - {0} на сумму: {1:F}";

        private double sum;
        private int activeShift;

        public SBRF()
        {
            // create
            InitializeComponent();

            ClearNmSum();

            // Сумма взноса
            sum = 0;

            // Устанавливаем начальную смену
            activeShift = 1;
        }

        private void ClearNmSum()
        {
            nmSum.Controls[0].Visible = false;
            nmSum.Value = 0;
            nmSum.Controls[1].Text = "";
            nmSum.Focus();
        }

        private void DoChangeDeposited(double value)
        {
            if (rbPay.Checked)
            {
                sum += value;
            }
            else if (rbRefund.Checked)
            {
                sum -= value;
            }
            amountDeposited.Text = String.Format("{0} руб.", sum);
        }

        private void AddMsgFireLog(string msg, double value)
        {
            if (cCloseShift == msg)
            {
                FireLog.Items.Insert(0, String.Format(msg, DateTime.Now, value));
            }
            else
            {
                FireLog.Items.Insert(0, String.Format(msg, activeShift, DateTime.Now, value));
            }
        }

        private void AddMsgStatus(string msg)
        {
            statuslabel.Text = msg;
        }

        private void NewShift()
        {
            // Обнуляем результат
            sum = 0;
            amountDeposited.Text = "0 руб.";

            // Открываем след. смену
            activeShift++;
        }

        private void DoPerformOperation(Process AProc)
        {
            try
            {
                double sumval = Convert.ToDouble(nmSum.Value);

                // Преобразование по требованию заказчика
                long value = Convert.ToInt64(sumval * 100);

                if (rbPay.Checked) // Оплата
                {
                    // Указываем параметры для загрузки
                    AProc.StartInfo.Arguments = String.Format(cLoadParam, code.ePay, value);
                    // Вносим запись в лог (смена - дата - сумма)
                    AddMsgFireLog(cPay, sumval);
                    // Добавляем запись в статус-бар
                    AddMsgStatus(cPaySuccess);
                }
                else if (rbRefund.Checked) // Возврат
                {
                    // Указываем параметры для загрузки
                    AProc.StartInfo.Arguments = String.Format(cLoadParam, code.eRefund, value);
                    // Вносим запись в лог (смена - дата - сумма)
                    AddMsgFireLog(cRefund, sumval);
                    // Добавляем запись в статус-бар
                    AddMsgStatus(cRefundSuccess);
                }

                // Изменить сумму депозита
                DoChangeDeposited(sumval);
            }
            catch (OverflowException)
            {
                if (MessageBox.Show("Взнос не может быть больше 5e9", "Переполнение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        /* ---------- Main ---------- */
        private void Execute()
        {
            nmSum.Value = Math.Round(nmSum.Value, 2);

            if (nmSum.Value <= 0)
            {
                AddMsgStatus("Значение не может быть меньше или равно нулю!");
                return;
            }

            // Создаем процесс
            Process proc = new Process();

            try
            {
                // Без использования оболочки
                proc.StartInfo.UseShellExecute = false;

                // Имя процесса
                proc.StartInfo.FileName = cProcessCMD;

                // Выполнить бизнес-логику
                DoPerformOperation(proc);

                // Без создания окна
                proc.StartInfo.CreateNoWindow = true;

                // Запускаем процесс
                proc.Start();
            }
            finally
            {
                // Закрываем процесс
                proc.Close();

                // Очистить строку и значения
                ClearNmSum();
            }
        }

        /* ---------- Events ---------- */
        private void MenuItem_CloseShift_Click(object sender, EventArgs e)
        {
            // Создаем процесс
            Process proc = new Process();
            try
            {
                if (MessageBox.Show("Вы действительно хотите закрыть смену?", "Закрыть смену", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
                
                // Добавляем сообщение в статус
                AddMsgStatus(String.Format(cCloseShift, DateTime.Now, sum));

                // Добавляем сообщение в лог (сообщение - сумма)
                AddMsgFireLog(cCloseShift, sum);

                // Без использования оболочки
                proc.StartInfo.UseShellExecute = false;

                // Имя процесса
                proc.StartInfo.FileName = cProcessCMD;

                // Ввод команды
                proc.StartInfo.Arguments = String.Format(cLoadParam, code.eCloseShift, null);

                // Без создания окна
                proc.StartInfo.CreateNoWindow = true;
                
                // Запускаем процесс
                proc.Start();
            }
            finally
            {
                // Открываем новую смену
                NewShift();

                // Закрываем процесс
                proc.Close();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Execute();
        }

        void KeyUp_Excute(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Execute();
            }
        }

        private void NmSum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void MenuItem_ClearLog_Click(object sender, EventArgs e)
        {
            if(FireLog.Items.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите очистить журнал сообщений?",
                "Очистка журнал", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            FireLog.Items.Clear();
        }

        private void MenuItem_HideJournal_Click(object sender, EventArgs e)
        {
            if (FireLog.Visible)
            {
                FireLog.Visible = false;
                if(Form.ActiveForm != null)
                {
                    Form.ActiveForm.Height = Form.ActiveForm.MinimumSize.Height;
                } 
            } 
            else
            {
                FireLog.Visible = true;
                if(Form.ActiveForm != null)
                {
                    if(Form.ActiveForm.Height < 500)
                    {
                        Form.ActiveForm.Height = 500;
                    }
                }               
            }
        }

        private void FireLog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                FireLog.Items.Remove(FireLog.SelectedItem);
            }
        }
    }
}
