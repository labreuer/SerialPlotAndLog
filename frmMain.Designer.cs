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
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.alsoResetChart = new System.Windows.Forms.CheckBox();
            this.resetArduino = new System.Windows.Forms.Button();
            this.yAxisNonZero = new System.Windows.Forms.CheckBox();
            this.resetChart = new System.Windows.Forms.Button();
            this.lastData = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.baud = new System.Windows.Forms.ComboBox();
            this.port = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
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
            this.panel1.Controls.Add(this.alsoResetChart);
            this.panel1.Controls.Add(this.resetArduino);
            this.panel1.Controls.Add(this.yAxisNonZero);
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
            // alsoResetChart
            // 
            this.alsoResetChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.alsoResetChart.AutoSize = true;
            this.alsoResetChart.Checked = true;
            this.alsoResetChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alsoResetChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alsoResetChart.Location = new System.Drawing.Point(1223, 41);
            this.alsoResetChart.Name = "alsoResetChart";
            this.alsoResetChart.Size = new System.Drawing.Size(170, 29);
            this.alsoResetChart.TabIndex = 12;
            this.alsoResetChart.Text = "also reset chart";
            this.alsoResetChart.UseVisualStyleBackColor = true;
            // 
            // resetArduino
            // 
            this.resetArduino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetArduino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetArduino.Location = new System.Drawing.Point(1399, 39);
            this.resetArduino.Name = "resetArduino";
            this.resetArduino.Size = new System.Drawing.Size(156, 34);
            this.resetArduino.TabIndex = 11;
            this.resetArduino.Text = "Reset Arduino";
            this.resetArduino.UseVisualStyleBackColor = true;
            // 
            // yAxisNonZero
            // 
            this.yAxisNonZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yAxisNonZero.AutoSize = true;
            this.yAxisNonZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yAxisNonZero.Location = new System.Drawing.Point(908, 40);
            this.yAxisNonZero.Name = "yAxisNonZero";
            this.yAxisNonZero.Size = new System.Drawing.Size(166, 29);
            this.yAxisNonZero.TabIndex = 10;
            this.yAxisNonZero.Text = "y-axis nonzero";
            this.yAxisNonZero.UseVisualStyleBackColor = true;
            // 
            // resetChart
            // 
            this.resetChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetChart.Location = new System.Drawing.Point(1080, 39);
            this.resetChart.Name = "resetChart";
            this.resetChart.Size = new System.Drawing.Size(137, 34);
            this.resetChart.TabIndex = 5;
            this.resetChart.Text = "Reset Chart";
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 826);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart);
            this.Name = "frmMain";
            this.Text = "SerialPlotAndLog";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}

