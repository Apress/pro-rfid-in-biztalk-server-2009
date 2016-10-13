namespace HelloMobileRfid
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReadTag = new System.Windows.Forms.Button();
            this.tbTag = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(16, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(72, 20);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open RF";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(154, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 20);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close RF";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReadTag
            // 
            this.btnReadTag.Location = new System.Drawing.Point(76, 75);
            this.btnReadTag.Name = "btnReadTag";
            this.btnReadTag.Size = new System.Drawing.Size(72, 62);
            this.btnReadTag.TabIndex = 2;
            this.btnReadTag.Text = "Read Tag";
            this.btnReadTag.Click += new System.EventHandler(this.btnReadTag_Click);
            // 
            // tbTag
            // 
            this.tbTag.Location = new System.Drawing.Point(16, 153);
            this.tbTag.Name = "tbTag";
            this.tbTag.Size = new System.Drawing.Size(210, 21);
            this.tbTag.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbTag);
            this.Controls.Add(this.btnReadTag);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Read Tag";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReadTag;
        private System.Windows.Forms.TextBox tbTag;
    }
}

