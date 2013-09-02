namespace Resizer
{
    partial class ResizeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.resizeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dimensionBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.qualityScale = new System.Windows.Forms.NumericUpDown();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.metaDataCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.setCurrentDirButton = new System.Windows.Forms.Button();
            this.maxRadio = new System.Windows.Forms.RadioButton();
            this.minRadio = new System.Windows.Forms.RadioButton();
            this.verticalRadio = new System.Windows.Forms.RadioButton();
            this.horizontalRadio = new System.Windows.Forms.RadioButton();
            this.cancelButton = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.qualityScale)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url:";
            // 
            // urlBox
            // 
            this.urlBox.Location = new System.Drawing.Point(35, 9);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(328, 20);
            this.urlBox.TabIndex = 1;
            // 
            // resizeButton
            // 
            this.resizeButton.Location = new System.Drawing.Point(35, 35);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(55, 23);
            this.resizeButton.TabIndex = 2;
            this.resizeButton.Text = "Resize";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dimension:";
            // 
            // dimensionBox
            // 
            this.dimensionBox.Location = new System.Drawing.Point(115, 63);
            this.dimensionBox.Name = "dimensionBox";
            this.dimensionBox.Size = new System.Drawing.Size(41, 20);
            this.dimensionBox.TabIndex = 4;
            this.dimensionBox.Text = "1200";
            this.dimensionBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "px";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "JPG Quality:";
            // 
            // qualityScale
            // 
            this.qualityScale.Location = new System.Drawing.Point(347, 64);
            this.qualityScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.qualityScale.Name = "qualityScale";
            this.qualityScale.Size = new System.Drawing.Size(44, 20);
            this.qualityScale.TabIndex = 7;
            this.qualityScale.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(157, 35);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(234, 23);
            this.progressBar.TabIndex = 8;
            // 
            // metaDataCheck
            // 
            this.metaDataCheck.AutoSize = true;
            this.metaDataCheck.Checked = true;
            this.metaDataCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metaDataCheck.Location = new System.Drawing.Point(115, 94);
            this.metaDataCheck.Name = "metaDataCheck";
            this.metaDataCheck.Size = new System.Drawing.Size(15, 14);
            this.metaDataCheck.TabIndex = 9;
            this.metaDataCheck.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Preserve metadata:";
            // 
            // setCurrentDirButton
            // 
            this.setCurrentDirButton.Location = new System.Drawing.Point(369, 9);
            this.setCurrentDirButton.Name = "setCurrentDirButton";
            this.setCurrentDirButton.Size = new System.Drawing.Size(22, 20);
            this.setCurrentDirButton.TabIndex = 11;
            this.setCurrentDirButton.Text = "/";
            this.setCurrentDirButton.UseVisualStyleBackColor = true;
            this.setCurrentDirButton.Click += new System.EventHandler(this.setCurrentDirButton_Click);
            // 
            // maxRadio
            // 
            this.maxRadio.AutoSize = true;
            this.maxRadio.Checked = true;
            this.maxRadio.Location = new System.Drawing.Point(154, 92);
            this.maxRadio.Name = "maxRadio";
            this.maxRadio.Size = new System.Drawing.Size(45, 17);
            this.maxRadio.TabIndex = 12;
            this.maxRadio.TabStop = true;
            this.maxRadio.Text = "Max";
            this.maxRadio.UseVisualStyleBackColor = true;
            // 
            // minRadio
            // 
            this.minRadio.AutoSize = true;
            this.minRadio.Location = new System.Drawing.Point(205, 92);
            this.minRadio.Name = "minRadio";
            this.minRadio.Size = new System.Drawing.Size(42, 17);
            this.minRadio.TabIndex = 13;
            this.minRadio.Text = "Min";
            this.minRadio.UseVisualStyleBackColor = true;
            // 
            // verticalRadio
            // 
            this.verticalRadio.AutoSize = true;
            this.verticalRadio.Location = new System.Drawing.Point(331, 92);
            this.verticalRadio.Name = "verticalRadio";
            this.verticalRadio.Size = new System.Drawing.Size(60, 17);
            this.verticalRadio.TabIndex = 14;
            this.verticalRadio.Text = "Vertical";
            this.verticalRadio.UseVisualStyleBackColor = true;
            // 
            // horizontalRadio
            // 
            this.horizontalRadio.AutoSize = true;
            this.horizontalRadio.Location = new System.Drawing.Point(253, 92);
            this.horizontalRadio.Name = "horizontalRadio";
            this.horizontalRadio.Size = new System.Drawing.Size(72, 17);
            this.horizontalRadio.TabIndex = 15;
            this.horizontalRadio.Text = "Horizontal";
            this.horizontalRadio.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(96, 35);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            // 
            // ResizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 122);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.horizontalRadio);
            this.Controls.Add(this.verticalRadio);
            this.Controls.Add(this.minRadio);
            this.Controls.Add(this.maxRadio);
            this.Controls.Add(this.setCurrentDirButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.metaDataCheck);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.qualityScale);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dimensionBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.urlBox);
            this.Controls.Add(this.label1);
            this.Name = "ResizeForm";
            this.Text = "Resizer";
            ((System.ComponentModel.ISupportInitialize)(this.qualityScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dimensionBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown qualityScale;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox metaDataCheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button setCurrentDirButton;
        private System.Windows.Forms.RadioButton maxRadio;
        private System.Windows.Forms.RadioButton minRadio;
        private System.Windows.Forms.RadioButton verticalRadio;
        private System.Windows.Forms.RadioButton horizontalRadio;
        private System.Windows.Forms.Button cancelButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

