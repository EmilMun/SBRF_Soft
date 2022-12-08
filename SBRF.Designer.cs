namespace SBRF_Soft
{
    partial class SBRF
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.rbPay = new System.Windows.Forms.RadioButton();
            this.rbRefund = new System.Windows.Forms.RadioButton();
            this.nmSum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_CloseShift = new System.Windows.Forms.ToolStripMenuItem();
            this.сообщенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_HideJournal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.FireLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.amountDeposited = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmSum)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnOK.Location = new System.Drawing.Point(12, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(297, 38);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.btnOK.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp_Excute);
            // 
            // rbPay
            // 
            this.rbPay.AutoSize = true;
            this.rbPay.Checked = true;
            this.rbPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.rbPay.Location = new System.Drawing.Point(12, 37);
            this.rbPay.Name = "rbPay";
            this.rbPay.Size = new System.Drawing.Size(106, 30);
            this.rbPay.TabIndex = 2;
            this.rbPay.TabStop = true;
            this.rbPay.Text = "Оплата";
            this.rbPay.UseVisualStyleBackColor = true;
            // 
            // rbRefund
            // 
            this.rbRefund.AutoSize = true;
            this.rbRefund.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.rbRefund.Location = new System.Drawing.Point(124, 37);
            this.rbRefund.Name = "rbRefund";
            this.rbRefund.Size = new System.Drawing.Size(114, 30);
            this.rbRefund.TabIndex = 3;
            this.rbRefund.Text = "Возврат";
            this.rbRefund.UseVisualStyleBackColor = true;
            // 
            // nmSum
            // 
            this.nmSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nmSum.DecimalPlaces = 2;
            this.nmSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.nmSum.Location = new System.Drawing.Point(102, 73);
            this.nmSum.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
            this.nmSum.Name = "nmSum";
            this.nmSum.Size = new System.Drawing.Size(207, 32);
            this.nmSum.TabIndex = 0;
            this.nmSum.ThousandsSeparator = true;
            this.nmSum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NmSum_KeyDown);
            this.nmSum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp_Excute);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(7, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Сумма:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 180);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(321, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Window,
            this.сообщенияToolStripMenuItem,
            this.menuItem_About});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(321, 33);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem_Window
            // 
            this.menuItem_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_CloseShift});
            this.menuItem_Window.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.menuItem_Window.Name = "menuItem_Window";
            this.menuItem_Window.Size = new System.Drawing.Size(80, 29);
            this.menuItem_Window.Text = "Смена";
            // 
            // menuItem_CloseShift
            // 
            this.menuItem_CloseShift.Name = "menuItem_CloseShift";
            this.menuItem_CloseShift.Size = new System.Drawing.Size(212, 30);
            this.menuItem_CloseShift.Text = "Закрыть смену";
            this.menuItem_CloseShift.Click += new System.EventHandler(this.MenuItem_CloseShift_Click);
            // 
            // сообщенияToolStripMenuItem
            // 
            this.сообщенияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_ClearLog,
            this.MenuItem_HideJournal});
            this.сообщенияToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.сообщенияToolStripMenuItem.Name = "сообщенияToolStripMenuItem";
            this.сообщенияToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.сообщенияToolStripMenuItem.Text = "Журнал";
            // 
            // menuItem_ClearLog
            // 
            this.menuItem_ClearLog.Name = "menuItem_ClearLog";
            this.menuItem_ClearLog.Size = new System.Drawing.Size(342, 30);
            this.menuItem_ClearLog.Text = "Очистить журнал сообщений";
            this.menuItem_ClearLog.Click += new System.EventHandler(this.MenuItem_ClearLog_Click);
            // 
            // MenuItem_HideJournal
            // 
            this.MenuItem_HideJournal.Name = "MenuItem_HideJournal";
            this.MenuItem_HideJournal.Size = new System.Drawing.Size(342, 30);
            this.MenuItem_HideJournal.Text = "Скрыть\\показать журнал";
            this.MenuItem_HideJournal.Click += new System.EventHandler(this.MenuItem_HideJournal_Click);
            // 
            // menuItem_About
            // 
            this.menuItem_About.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.menuItem_About.Name = "menuItem_About";
            this.menuItem_About.Size = new System.Drawing.Size(140, 29);
            this.menuItem_About.Text = "О программе";
            this.menuItem_About.Click += new System.EventHandler(this.menuItem_About_Click);
            // 
            // FireLog
            // 
            this.FireLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FireLog.FormattingEnabled = true;
            this.FireLog.Location = new System.Drawing.Point(12, 181);
            this.FireLog.Name = "FireLog";
            this.FireLog.Size = new System.Drawing.Size(297, 4);
            this.FireLog.TabIndex = 7;
            this.FireLog.Visible = false;
            this.FireLog.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FireLog_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(7, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Внесено:";
            // 
            // amountDeposited
            // 
            this.amountDeposited.AutoSize = true;
            this.amountDeposited.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.amountDeposited.Location = new System.Drawing.Point(117, 108);
            this.amountDeposited.Name = "amountDeposited";
            this.amountDeposited.Size = new System.Drawing.Size(72, 26);
            this.amountDeposited.TabIndex = 9;
            this.amountDeposited.Text = "0 руб.";
            // 
            // SBRF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 202);
            this.Controls.Add(this.amountDeposited);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.FireLog);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmSum);
            this.Controls.Add(this.rbRefund);
            this.Controls.Add(this.rbPay);
            this.Controls.Add(this.btnOK);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(337, 241);
            this.Name = "SBRF";
            this.Text = "SBRF";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp_Excute);
            ((System.ComponentModel.ISupportInitialize)(this.nmSum)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbPay;
        private System.Windows.Forms.RadioButton rbRefund;
        private System.Windows.Forms.NumericUpDown nmSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Window;
        private System.Windows.Forms.ToolStripMenuItem menuItem_CloseShift;
        private System.Windows.Forms.ListBox FireLog;
        private System.Windows.Forms.ToolStripMenuItem сообщенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ClearLog;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_HideJournal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label amountDeposited;
        private System.Windows.Forms.ToolStripMenuItem menuItem_About;
    }
}

