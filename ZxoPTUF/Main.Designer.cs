namespace ZxoPTUF
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.filedialog = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.outputDialog = new System.Windows.Forms.SaveFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDataFile = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tick = new System.Windows.Forms.Timer(this.components);
            this.progressLabel = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filedialog
            // 
            this.filedialog.FileName = "filedialog";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Locate data.win";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(139, 36);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.PlaceholderText = "Example: spr_player_";
            this.tbFilter.Size = new System.Drawing.Size(162, 23);
            this.tbFilter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sprites that start with:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output Folder:";
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(102, 100);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(217, 23);
            this.tbOutput.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Locate output";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(204, 129);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Data File:";
            // 
            // tbDataFile
            // 
            this.tbDataFile.Location = new System.Drawing.Point(102, 71);
            this.tbDataFile.Name = "tbDataFile";
            this.tbDataFile.Size = new System.Drawing.Size(217, 23);
            this.tbDataFile.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(142, 7);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(345, 23);
            this.progressBar.TabIndex = 9;
            // 
            // tick
            // 
            this.tick.Enabled = true;
            this.tick.Interval = 33;
            this.tick.Tick += new System.EventHandler(this.tick_Tick);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(12, 15);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(89, 15);
            this.progressLabel.TabIndex = 10;
            this.progressLabel.Text = "%PROGRESST%";
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Location = new System.Drawing.Point(12, 133);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(69, 15);
            this.lblCredit.TabIndex = 11;
            this.lblCredit.Text = "By Zxo Pink";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 154);
            this.Controls.Add(this.lblCredit);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDataFile);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "PTUF Extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenFileDialog filedialog;
        private Button button1;
        private TextBox tbFilter;
        private Label label1;
        private Label label2;
        private TextBox tbOutput;
        private Button button2;
        private SaveFileDialog outputDialog;
        private Button button3;
        private Label label3;
        private TextBox tbDataFile;
        private ProgressBar progressBar;
        private System.Windows.Forms.Timer tick;
        private Label progressLabel;
        private Label lblCredit;
    }
}