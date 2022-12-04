using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Windows.Forms.Design;

namespace SBRF_Soft
{
    public partial class SBRF : Form
    {       
        public SBRF()
        {
            InitializeComponent();
            ClearNmSum();
        }

        private void ClearNmSum()
        {
            nmSum.Controls[0].Visible = false;
            nmSum.Controls[1].Text = "";
            nmSum.Focus();
        }

        static int GetDecimalDigitsCount(double number)
        {
            string[] str = number.ToString(
                new System.Globalization.NumberFormatInfo()
                { NumberDecimalSeparator = "." }).Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }

        private void Execute()
        {
            nmSum.Value = Math.Round(nmSum.Value, 2);
            
            if (nmSum.Value <= 0)
            {
                statuslabel.Text = String.Format("Значение не может быть меньше или равно нулю!");
                return;
            }

            // Создаем процесс
            Process proc = new Process();

            try
            {
                // Без использования оболочки
                proc.StartInfo.UseShellExecute = false;

                // Имя процесса
                proc.StartInfo.FileName = "cmd.exe";

                int value = Convert.ToInt32(Convert.ToDouble(nmSum.Value) * 100);

                if (rbPay.Checked) // Оплата
                {
                    proc.StartInfo.Arguments = "/C" + "loadparm.exe 1 " + value;
                    FireLog.Items.Insert(0, String.Format("[{0}] Оплата на сумму: {1} руб.",
                        DateTime.Now, Convert.ToDouble(nmSum.Value)));
                    statuslabel.Text = "Оплата прошла успешно!";
                }
                else if (rbRefund.Checked) // Возврат
                {
                    proc.StartInfo.Arguments = "/C" + "loadparm.exe 3 " + value;
                    FireLog.Items.Insert(0, String.Format("[{0}] Возврат на сумму: {1} руб.",
                        DateTime.Now, Convert.ToDouble(nmSum.Value)));
                    statuslabel.Text = "Возврат пройден успешно!";
                }

                // Без создания окна
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
            }
            finally
            {
                proc.Close();
                ClearNmSum();
            }
        }

        private void menuItem_CloseShift_Click(object sender, EventArgs e)
        {
            // Создаем процесс
            Process proc = new Process();
            try
            {
                if (MessageBox.Show("Вы действительно хотите закрыть смену?", 
                    "Закрыть смену", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                statuslabel.Text = "Смена закрыта: " + DateTime.Now;

                // Без использования оболочки
                proc.StartInfo.UseShellExecute = false;

                // Имя процесса
                proc.StartInfo.FileName = "cmd.exe";

                proc.StartInfo.Arguments = "/C" + "loadparm.exe 7";

                // Без создания окна
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
            }
            finally
            {
                proc.Close();
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
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

        private void nmSum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void menuItem_ClearLog_Click(object sender, EventArgs e)
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
