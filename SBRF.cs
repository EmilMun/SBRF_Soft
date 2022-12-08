using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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
            eCloseShift = 7,
            /* Без операции */
            eNull = -1
        }

        // Execute code
        const string cProcessCMD = "cmd.exe";
        const string cLoadParam = "/Cloadparm.exe {0:D} {1:D}";

        const string cFrmtPay = "[Смена {0} - {1}] Оплата на сумму: {2:F} руб.";
        const string cMsgPaySuccess = "Оплата прошла успешно!";

        const string cFrmtRefund = "[Смена {0} - {1}] Возврат на сумму: {2:F} руб.";
        const string cMsgRefundSuccess = "Возврат пройден успешно!";

        const string cFrmtCloseShift = "Смена закрыта - {0} на сумму: {1:F}"; 
        const string cFrmtDeposite = "{0} руб.";

        private struct MsgItem
        {
            public string msg;
            public double value;
            public code opertaion;

            public void Create(string msg, double value, code operation)
            {
                this.msg = msg;
                this.value = value;
                this.opertaion = operation;
            }
        }

        // Сумма взноса
        private double gDeposited = 0;
        // Начальная смена
        private int gActiveShift = 1;
        // Связанный список с формой логов
        private readonly List<MsgItem> gLstItmFrLog = new List<MsgItem>();

        public SBRF()
        {
            // create
            InitializeComponent();

            ClearNumericField();
        }
        
        private void ClearSum()
        {
            // Обнуляем результат
            gDeposited = 0;
            amountDeposited.Text = "0 руб.";
        }

        private void ClearJournal()
        {
            FireLog.Items.Clear();
            gLstItmFrLog.Clear();
            ClearSum();
        }

        private void ClearNumericField()
        {
            nmSum.Controls[0].Visible = false;
            nmSum.Value = 0;
            nmSum.Controls[1].Text = "";
            nmSum.Focus();
        }

        private void DoChangeDeposited(double AValue, code ACode = code.eNull)
        {
            switch (ACode)
            {
                case code.eNull:
                    if (rbPay.Checked)
                    {
                        gDeposited += AValue;
                    }
                    else if (rbRefund.Checked)
                    {
                        gDeposited -= AValue;
                    }
                    break;

                case code.ePay:
                    gDeposited += AValue;
                    break;

                case code.eRefund:
                    gDeposited -= AValue;
                    break;
            }
            amountDeposited.Text = String.Format(cFrmtDeposite, gDeposited);
        }

        private void AddMsgFireLog(string msg, double value)
        {
            if (msg == null)
            {
                msg = "";
            }
            
            MsgItem item = new MsgItem();
            
            switch (msg)
            {
                case cFrmtCloseShift:
                    item.Create(String.Format(msg, DateTime.Now, value), value, code.eCloseShift);
                    break;

                case cFrmtRefund:
                    item.Create(String.Format(msg, gActiveShift, DateTime.Now, value), value, code.ePay);
                    break;

                case cFrmtPay:
                    item.Create(String.Format(msg, gActiveShift, DateTime.Now, value), value, code.eRefund);
                    break;
            }

            FireLog.Items.Insert(0, item.msg);
            
            gLstItmFrLog.Insert(0, item);
        }

        private void AddMsgStatus(string msg)
        {
            statuslabel.Text = msg ?? "";
        }

        private void NewShift()
        {
            ClearSum();

            // Очищаем зависимости
            gLstItmFrLog.Clear();
            
            // Открываем след. смену
            gActiveShift++;
        }

        private void DoPerformOperation(Process AProc)
        {
            try
            {
                if (AProc == null)
                {
                    return;
                }
                
                double sumval = Convert.ToDouble(nmSum.Value);

                // Преобразование по требованию заказчика
                long value = Convert.ToInt64(sumval * 100);

                if (rbPay.Checked) // Оплата
                {
                    // Указываем параметры для загрузки
                    AProc.StartInfo.Arguments = String.Format(cLoadParam, code.ePay, value);
                    // Вносим запись в лог (смена - дата - сумма)
                    AddMsgFireLog(cFrmtPay, sumval);
                    // Добавляем запись в статус-бар
                    AddMsgStatus(cMsgPaySuccess);
                }
                else if (rbRefund.Checked) // Возврат
                {
                    // Указываем параметры для загрузки
                    AProc.StartInfo.Arguments = String.Format(cLoadParam, code.eRefund, value);
                    // Вносим запись в лог (смена - дата - сумма)
                    AddMsgFireLog(cFrmtRefund, sumval);
                    // Добавляем запись в статус-бар
                    AddMsgStatus(cMsgRefundSuccess);
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
                ClearNumericField();
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
                AddMsgStatus(String.Format(cFrmtCloseShift, DateTime.Now, gDeposited));

                // Добавляем сообщение в лог (сообщение - сумма)
                AddMsgFireLog(cFrmtCloseShift, gDeposited);

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

            ClearJournal();
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
                try
                {
                    if(gLstItmFrLog.Count == 0) 
                    {
                        return;
                    }

                    if ((FireLog.SelectedIndex > gLstItmFrLog.Count - 1) || (FireLog.SelectedIndex < 0))
                    {
                        return;
                    }

                    // Обновляем запись
                    DoChangeDeposited(gLstItmFrLog[FireLog.SelectedIndex].value, gLstItmFrLog[FireLog.SelectedIndex].opertaion);

                    // Удаляем объект из списка
                    gLstItmFrLog.RemoveAt(FireLog.SelectedIndex);
                }
                finally
                {
                    // Удаляем сообщение из лога
                    FireLog.Items.Remove(FireLog.SelectedItem);
                }
            }
        }
        private void menuItem_About_Click(object sender, EventArgs e) {
            MessageBox.Show(
                // Отображаемый текст на форме
                "Программа SBRF\n\n" +
                "Автор: Мунасыпов Эмиль Радикович\n\n" +
                "Почта: munasypov.emil@radikovich.ru",
                // Подпись на форме
                "О программе", 
                // Параметры формы
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); 
        }
    }
}
