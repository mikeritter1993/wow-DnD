namespace DnD
{
    partial class JoinOrHost
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
            this.joinGameBtn = new System.Windows.Forms.Button();
            this.hostGameBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // joinGameBtn
            // 
            this.joinGameBtn.Location = new System.Drawing.Point(100, 54);
            this.joinGameBtn.Name = "joinGameBtn";
            this.joinGameBtn.Size = new System.Drawing.Size(75, 23);
            this.joinGameBtn.TabIndex = 1;
            this.joinGameBtn.Text = "Join Game";
            this.joinGameBtn.UseVisualStyleBackColor = true;
            // 
            // hostGameBtn
            // 
            this.hostGameBtn.Location = new System.Drawing.Point(100, 124);
            this.hostGameBtn.Name = "hostGameBtn";
            this.hostGameBtn.Size = new System.Drawing.Size(75, 23);
            this.hostGameBtn.TabIndex = 2;
            this.hostGameBtn.Text = "Host Game";
            this.hostGameBtn.UseVisualStyleBackColor = true;
            // 
            // JoinOrHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.hostGameBtn);
            this.Controls.Add(this.joinGameBtn);
            this.Name = "JoinOrHost";
            this.Text = "JoinOrHost";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button joinGameBtn;
        private System.Windows.Forms.Button hostGameBtn;
    }
}