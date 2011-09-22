namespace WinMoss
{
    partial class AutoRunDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRunNow = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRunningIn = new System.Windows.Forms.Label();
            this.tmrAutoRun = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnRunNow
            // 
            this.btnRunNow.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRunNow.Location = new System.Drawing.Point(28, 52);
            this.btnRunNow.Name = "btnRunNow";
            this.btnRunNow.Size = new System.Drawing.Size(75, 23);
            this.btnRunNow.TabIndex = 0;
            this.btnRunNow.Text = "Run Now";
            this.btnRunNow.UseVisualStyleBackColor = true;
            this.btnRunNow.Click += new System.EventHandler(this.btnRunNow_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(116, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRunningIn
            // 
            this.lblRunningIn.AutoSize = true;
            this.lblRunningIn.Location = new System.Drawing.Point(26, 19);
            this.lblRunningIn.Name = "lblRunningIn";
            this.lblRunningIn.Size = new System.Drawing.Size(166, 13);
            this.lblRunningIn.TabIndex = 2;
            this.lblRunningIn.Text = "MOSS AutoRunning in 0 seconds";
            // 
            // tmrAutoRun
            // 
            this.tmrAutoRun.Enabled = true;
            this.tmrAutoRun.Interval = 50;
            this.tmrAutoRun.Tick += new System.EventHandler(this.tmrAutoRun_Tick);
            // 
            // AutoRunDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 91);
            this.Controls.Add(this.lblRunningIn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRunNow);
            this.Name = "AutoRunDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MOSS AutoRun";
            this.Load += new System.EventHandler(this.AutoRunDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunNow;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRunningIn;
        private System.Windows.Forms.Timer tmrAutoRun;
    }
}