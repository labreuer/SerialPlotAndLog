namespace SerialPlotAndLog
{
	partial class frmMain
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showOptions = new System.Windows.Forms.Button();
            this.resetArduino = new System.Windows.Forms.Button();
            this.resetChart = new System.Windows.Forms.Button();
            this.lastData = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.baud = new System.Windows.Forms.ComboBox();
            this.port = new System.Windows.Forms.ComboBox();
            this.alsoResetChart = new System.Windows.Forms.CheckBox();
            this.yAxisNonZero = new System.Windows.Forms.CheckBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.saveAndCloseOptions = new System.Windows.Forms.Button();
            this.askFilenameOnReset = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1558, 746);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.showOptions);
            this.panel1.Controls.Add(this.resetArduino);
            this.panel1.Controls.Add(this.resetChart);
            this.panel1.Controls.Add(this.lastData);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.error);
            this.panel1.Controls.Add(this.baud);
            this.panel1.Controls.Add(this.port);
            this.panel1.Location = new System.Drawing.Point(0, 749);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1558, 77);
            this.panel1.TabIndex = 4;
            // 
            // showOptions
            // 
            this.showOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.showOptions.AutoSize = true;
            this.showOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showOptions.Location = new System.Drawing.Point(1104, 38);
            this.showOptions.Name = "showOptions";
            this.showOptions.Size = new System.Drawing.Size(137, 35);
            this.showOptions.TabIndex = 12;
            this.showOptions.Text = "Options";
            this.showOptions.UseVisualStyleBackColor = true;
            // 
            // resetArduino
            // 
            this.resetArduino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetArduino.AutoSize = true;
            this.resetArduino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetArduino.Location = new System.Drawing.Point(1390, 38);
            this.resetArduino.Name = "resetArduino";
            this.resetArduino.Size = new System.Drawing.Size(165, 35);
            this.resetArduino.TabIndex = 11;
            this.resetArduino.Text = "Reset Machine";
            this.resetArduino.UseVisualStyleBackColor = true;
            // 
            // resetChart
            // 
            this.resetChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetChart.AutoSize = true;
            this.resetChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetChart.Location = new System.Drawing.Point(1247, 38);
            this.resetChart.Name = "resetChart";
            this.resetChart.Size = new System.Drawing.Size(137, 35);
            this.resetChart.TabIndex = 5;
            this.resetChart.Text = "Reset Plot";
            this.resetChart.UseVisualStyleBackColor = true;
            // 
            // lastData
            // 
            this.lastData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lastData.Location = new System.Drawing.Point(3, 0);
            this.lastData.Name = "lastData";
            this.lastData.Size = new System.Drawing.Size(1552, 28);
            this.lastData.TabIndex = 9;
            this.lastData.Text = "Last Data";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(337, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Baud:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port:";
            // 
            // error
            // 
            this.error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.error.AutoSize = true;
            this.error.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(528, 44);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(57, 25);
            this.error.TabIndex = 6;
            this.error.Text = "error";
            // 
            // baud
            // 
            this.baud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baud.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baud.FormattingEnabled = true;
            this.baud.Location = new System.Drawing.Point(412, 41);
            this.baud.Name = "baud";
            this.baud.Size = new System.Drawing.Size(110, 33);
            this.baud.TabIndex = 5;
            // 
            // port
            // 
            this.port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.port.FormattingEnabled = true;
            this.port.Location = new System.Drawing.Point(67, 41);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(264, 33);
            this.port.TabIndex = 4;
            // 
            // alsoResetChart
            // 
            this.alsoResetChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.alsoResetChart.AutoSize = true;
            this.alsoResetChart.Checked = true;
            this.alsoResetChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alsoResetChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alsoResetChart.Location = new System.Drawing.Point(6, 46);
            this.alsoResetChart.Name = "alsoResetChart";
            this.alsoResetChart.Size = new System.Drawing.Size(295, 29);
            this.alsoResetChart.TabIndex = 12;
            this.alsoResetChart.Text = "reset machine also resets plot";
            this.alsoResetChart.UseVisualStyleBackColor = true;
            // 
            // yAxisNonZero
            // 
            this.yAxisNonZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yAxisNonZero.AutoSize = true;
            this.yAxisNonZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yAxisNonZero.Location = new System.Drawing.Point(6, 81);
            this.yAxisNonZero.Name = "yAxisNonZero";
            this.yAxisNonZero.Size = new System.Drawing.Size(166, 29);
            this.yAxisNonZero.TabIndex = 10;
            this.yAxisNonZero.Text = "y-axis nonzero";
            this.yAxisNonZero.UseVisualStyleBackColor = true;
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.saveAndCloseOptions);
            this.grpOptions.Controls.Add(this.askFilenameOnReset);
            this.grpOptions.Controls.Add(this.alsoResetChart);
            this.grpOptions.Controls.Add(this.yAxisNonZero);
            this.grpOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOptions.Location = new System.Drawing.Point(1014, 578);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(329, 203);
            this.grpOptions.TabIndex = 14;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // saveAndCloseOptions
            // 
            this.saveAndCloseOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAndCloseOptions.AutoSize = true;
            this.saveAndCloseOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAndCloseOptions.Location = new System.Drawing.Point(147, 162);
            this.saveAndCloseOptions.Name = "saveAndCloseOptions";
            this.saveAndCloseOptions.Size = new System.Drawing.Size(176, 35);
            this.saveAndCloseOptions.TabIndex = 13;
            this.saveAndCloseOptions.Text = "Save and Close";
            this.saveAndCloseOptions.UseVisualStyleBackColor = true;
            // 
            // askFilenameOnReset
            // 
            this.askFilenameOnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.askFilenameOnReset.AutoSize = true;
            this.askFilenameOnReset.Checked = true;
            this.askFilenameOnReset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.askFilenameOnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.askFilenameOnReset.Location = new System.Drawing.Point(6, 116);
            this.askFilenameOnReset.Name = "askFilenameOnReset";
            this.askFilenameOnReset.Size = new System.Drawing.Size(249, 29);
            this.askFilenameOnReset.TabIndex = 13;
            this.askFilenameOnReset.Text = "ask for filename on reset";
            this.askFilenameOnReset.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 826);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart);
            this.Name = "frmMain";
            this.Text = "SerialPlotAndLog";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.ComboBox baud;
        private System.Windows.Forms.ComboBox port;
        private System.Windows.Forms.Label lastData;
        private System.Windows.Forms.Button resetChart;
        private System.Windows.Forms.CheckBox yAxisNonZero;
        private System.Windows.Forms.Button resetArduino;
        private System.Windows.Forms.CheckBox alsoResetChart;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox askFilenameOnReset;
        private System.Windows.Forms.Button showOptions;
        private System.Windows.Forms.Button saveAndCloseOptions;
    }
}

