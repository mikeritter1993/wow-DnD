namespace DnD
{
    partial class InputMessageBox
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
            this.messageBoxTxt = new System.Windows.Forms.RichTextBox();
            this.enteredTextTxt = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageBoxTxt
            // 
            this.messageBoxTxt.Location = new System.Drawing.Point(12, 12);
            this.messageBoxTxt.Name = "messageBoxTxt";
            this.messageBoxTxt.ReadOnly = true;
            this.messageBoxTxt.Size = new System.Drawing.Size(166, 107);
            this.messageBoxTxt.TabIndex = 0;
            this.messageBoxTxt.Text = "Loooooooooooooooooooooooooooooooooooooooooooooooooong SAMMMMMMMMMMMMMMMMMMMMMMMMM" +
    "MMMMMMMMMMple Text";
            // 
            // enteredTextTxt
            // 
            this.enteredTextTxt.Location = new System.Drawing.Point(12, 125);
            this.enteredTextTxt.Name = "enteredTextTxt";
            this.enteredTextTxt.Size = new System.Drawing.Size(166, 20);
            this.enteredTextTxt.TabIndex = 1;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(12, 151);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(103, 151);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // InputMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 185);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.enteredTextTxt);
            this.Controls.Add(this.messageBoxTxt);
            this.Name = "InputMessageBox";
            this.Text = "InputMessageBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox messageBoxTxt;
        private System.Windows.Forms.TextBox enteredTextTxt;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}