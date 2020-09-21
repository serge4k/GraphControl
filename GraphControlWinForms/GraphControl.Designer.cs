namespace GraphControlWinForms
{
    partial class GraphControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFitToScreenByTime = new System.Windows.Forms.Button();
            this.btnFitToSreenByValue = new System.Windows.Forms.Button();
            this.cbFitToScreenAlways = new System.Windows.Forms.CheckBox();
            this.lblDivByX = new System.Windows.Forms.Label();
            this.lblDivX = new System.Windows.Forms.Label();
            this.lblDivByY = new System.Windows.Forms.Label();
            this.lblDivY = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFitToScreenByTime
            // 
            this.btnFitToScreenByTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFitToScreenByTime.Location = new System.Drawing.Point(4, 606);
            this.btnFitToScreenByTime.Margin = new System.Windows.Forms.Padding(4);
            this.btnFitToScreenByTime.Name = "btnFitToScreenByTime";
            this.btnFitToScreenByTime.Size = new System.Drawing.Size(184, 38);
            this.btnFitToScreenByTime.TabIndex = 0;
            this.btnFitToScreenByTime.Text = "Fit to screen by Time";
            this.btnFitToScreenByTime.UseVisualStyleBackColor = true;
            this.btnFitToScreenByTime.Click += new System.EventHandler(this.btnFitToScreenByTime_Click);
            // 
            // btnFitToSreenByValue
            // 
            this.btnFitToSreenByValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFitToSreenByValue.Location = new System.Drawing.Point(196, 606);
            this.btnFitToSreenByValue.Margin = new System.Windows.Forms.Padding(4);
            this.btnFitToSreenByValue.Name = "btnFitToSreenByValue";
            this.btnFitToSreenByValue.Size = new System.Drawing.Size(184, 38);
            this.btnFitToSreenByValue.TabIndex = 1;
            this.btnFitToSreenByValue.Text = "Fit to screen by Value";
            this.btnFitToSreenByValue.UseVisualStyleBackColor = true;
            this.btnFitToSreenByValue.Click += new System.EventHandler(this.btnFitToSreenByValue_Click);
            // 
            // cbFitToScreenAlways
            // 
            this.cbFitToScreenAlways.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFitToScreenAlways.AutoSize = true;
            this.cbFitToScreenAlways.Location = new System.Drawing.Point(407, 616);
            this.cbFitToScreenAlways.Margin = new System.Windows.Forms.Padding(4);
            this.cbFitToScreenAlways.Name = "cbFitToScreenAlways";
            this.cbFitToScreenAlways.Size = new System.Drawing.Size(154, 21);
            this.cbFitToScreenAlways.TabIndex = 2;
            this.cbFitToScreenAlways.Text = "Fit to screen always";
            this.cbFitToScreenAlways.UseVisualStyleBackColor = true;
            this.cbFitToScreenAlways.CheckedChanged += new System.EventHandler(this.cbFitToScreenAlways_CheckedChanged);
            // 
            // lblDivByX
            // 
            this.lblDivByX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDivByX.AutoSize = true;
            this.lblDivByX.Location = new System.Drawing.Point(585, 617);
            this.lblDivByX.Name = "lblDivByX";
            this.lblDivByX.Size = new System.Drawing.Size(131, 17);
            this.lblDivByX.TabIndex = 3;
            this.lblDivByX.Text = "Division value by X:";
            // 
            // lblDivX
            // 
            this.lblDivX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDivX.AutoSize = true;
            this.lblDivX.Location = new System.Drawing.Point(722, 617);
            this.lblDivX.Name = "lblDivX";
            this.lblDivX.Size = new System.Drawing.Size(16, 17);
            this.lblDivX.TabIndex = 4;
            this.lblDivX.Text = "1";
            // 
            // lblDivByY
            // 
            this.lblDivByY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDivByY.AutoSize = true;
            this.lblDivByY.Location = new System.Drawing.Point(877, 617);
            this.lblDivByY.Name = "lblDivByY";
            this.lblDivByY.Size = new System.Drawing.Size(40, 17);
            this.lblDivByY.TabIndex = 5;
            this.lblDivByY.Text = "by Y:";
            // 
            // lblDivY
            // 
            this.lblDivY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDivY.AutoSize = true;
            this.lblDivY.Location = new System.Drawing.Point(923, 617);
            this.lblDivY.Name = "lblDivY";
            this.lblDivY.Size = new System.Drawing.Size(16, 17);
            this.lblDivY.TabIndex = 6;
            this.lblDivY.Text = "1";
            // 
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDivY);
            this.Controls.Add(this.lblDivByY);
            this.Controls.Add(this.lblDivX);
            this.Controls.Add(this.lblDivByX);
            this.Controls.Add(this.cbFitToScreenAlways);
            this.Controls.Add(this.btnFitToSreenByValue);
            this.Controls.Add(this.btnFitToScreenByTime);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GraphControl";
            this.Size = new System.Drawing.Size(1081, 648);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFitToScreenByTime;
        private System.Windows.Forms.Button btnFitToSreenByValue;
        private System.Windows.Forms.CheckBox cbFitToScreenAlways;
        private System.Windows.Forms.Label lblDivByX;
        private System.Windows.Forms.Label lblDivX;
        private System.Windows.Forms.Label lblDivByY;
        private System.Windows.Forms.Label lblDivY;
    }
}
