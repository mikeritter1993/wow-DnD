namespace DnD
{
    partial class JoinGame
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
            this.ipAddressLbl = new System.Windows.Forms.Label();
            this.ipAddressTxt = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipAddressLbl
            // 
            this.ipAddressLbl.AutoSize = true;
            this.ipAddressLbl.Location = new System.Drawing.Point(95, 92);
            this.ipAddressLbl.Name = "ipAddressLbl";
            this.ipAddressLbl.Size = new System.Drawing.Size(86, 13);
            this.ipAddressLbl.TabIndex = 0;
            this.ipAddressLbl.Text = "Enter IP Address";
            // 
            // ipAddressTxt
            // 
            this.ipAddressTxt.Location = new System.Drawing.Point(78, 58);
            this.ipAddressTxt.Name = "ipAddressTxt";
            this.ipAddressTxt.Size = new System.Drawing.Size(124, 20);
            this.ipAddressTxt.TabIndex = 1;
            this.ipAddressTxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(98, 131);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            // 
            // JoinGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.ipAddressTxt);
            this.Controls.Add(this.ipAddressLbl);
            this.Name = "JoinGame";
            this.Text = "JoinGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipAddressLbl;
        private System.Windows.Forms.TextBox ipAddressTxt;
        private System.Windows.Forms.Button connectBtn;
    }
}