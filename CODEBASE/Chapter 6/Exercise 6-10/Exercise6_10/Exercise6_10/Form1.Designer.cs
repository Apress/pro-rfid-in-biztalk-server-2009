namespace Exercise6_11
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbLogicalDevices = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProcessList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numTagCount = new System.Windows.Forms.NumericUpDown();
            this.rbTagRead = new System.Windows.Forms.RadioButton();
            this.rbTagList = new System.Windows.Forms.RadioButton();
            this.rtbStatus = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSendTags = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCount)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.cbLogicalDevices);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbProcessList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processes and Devices";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(6, 83);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbLogicalDevices
            // 
            this.cbLogicalDevices.FormattingEnabled = true;
            this.cbLogicalDevices.Location = new System.Drawing.Point(110, 51);
            this.cbLogicalDevices.Name = "cbLogicalDevices";
            this.cbLogicalDevices.Size = new System.Drawing.Size(210, 21);
            this.cbLogicalDevices.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Logical Devices:";
            // 
            // cbProcessList
            // 
            this.cbProcessList.FormattingEnabled = true;
            this.cbProcessList.Location = new System.Drawing.Point(110, 24);
            this.cbProcessList.Name = "cbProcessList";
            this.cbProcessList.Size = new System.Drawing.Size(210, 21);
            this.cbProcessList.TabIndex = 1;
            this.cbProcessList.SelectedIndexChanged += new System.EventHandler(this.cbProcessList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Processes:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numTagCount);
            this.groupBox2.Controls.Add(this.rbTagRead);
            this.groupBox2.Controls.Add(this.rbTagList);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 105);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Number of Tags";
            // 
            // numTagCount
            // 
            this.numTagCount.Location = new System.Drawing.Point(9, 65);
            this.numTagCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTagCount.Name = "numTagCount";
            this.numTagCount.Size = new System.Drawing.Size(120, 20);
            this.numTagCount.TabIndex = 2;
            // 
            // rbTagRead
            // 
            this.rbTagRead.AutoSize = true;
            this.rbTagRead.Location = new System.Drawing.Point(9, 42);
            this.rbTagRead.Name = "rbTagRead";
            this.rbTagRead.Size = new System.Drawing.Size(163, 17);
            this.rbTagRead.TabIndex = 1;
            this.rbTagRead.TabStop = true;
            this.rbTagRead.Text = "Send tags as TagReadEvent";
            this.rbTagRead.UseVisualStyleBackColor = true;
            // 
            // rbTagList
            // 
            this.rbTagList.AutoSize = true;
            this.rbTagList.Checked = true;
            this.rbTagList.Location = new System.Drawing.Point(9, 19);
            this.rbTagList.Name = "rbTagList";
            this.rbTagList.Size = new System.Drawing.Size(153, 17);
            this.rbTagList.TabIndex = 0;
            this.rbTagList.TabStop = true;
            this.rbTagList.Text = "Send tags as TagListEvent";
            this.rbTagList.UseVisualStyleBackColor = true;
            // 
            // rtbStatus
            // 
            this.rtbStatus.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbStatus.Location = new System.Drawing.Point(344, 12);
            this.rtbStatus.Name = "rtbStatus";
            this.rtbStatus.Size = new System.Drawing.Size(263, 223);
            this.rtbStatus.TabIndex = 2;
            this.rtbStatus.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(102, 26);
            // 
            // miClear
            // 
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(101, 22);
            this.miClear.Text = "Clear";
            // 
            // btnSendTags
            // 
            this.btnSendTags.Location = new System.Drawing.Point(12, 241);
            this.btnSendTags.Name = "btnSendTags";
            this.btnSendTags.Size = new System.Drawing.Size(75, 23);
            this.btnSendTags.TabIndex = 3;
            this.btnSendTags.Text = "Send Tags";
            this.btnSendTags.UseVisualStyleBackColor = true;
            this.btnSendTags.Click += new System.EventHandler(this.btnSendTags_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 276);
            this.Controls.Add(this.btnSendTags);
            this.Controls.Add(this.rtbStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Tag Read Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCount)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbLogicalDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProcessList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbTagList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numTagCount;
        private System.Windows.Forms.RadioButton rbTagRead;
        private System.Windows.Forms.RichTextBox rtbStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.Button btnSendTags;
    }
}

