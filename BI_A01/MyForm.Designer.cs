namespace BI_A01
{
    partial class MyForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GraphChoiceCB = new System.Windows.Forms.ComboBox();
            this.MyDataGridView = new System.Windows.Forms.DataGridView();
            this.UpdateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MyChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MyChart
            // 
            chartArea1.Name = "ChartArea1";
            this.MyChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.MyChart.Legends.Add(legend1);
            this.MyChart.Location = new System.Drawing.Point(421, 12);
            this.MyChart.Name = "MyChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.MyChart.Series.Add(series1);
            this.MyChart.Size = new System.Drawing.Size(743, 540);
            this.MyChart.TabIndex = 0;
            this.MyChart.Text = "chart1";
            // 
            // GraphChoiceCB
            // 
            this.GraphChoiceCB.FormattingEnabled = true;
            this.GraphChoiceCB.Location = new System.Drawing.Point(12, 12);
            this.GraphChoiceCB.Name = "GraphChoiceCB";
            this.GraphChoiceCB.Size = new System.Drawing.Size(403, 24);
            this.GraphChoiceCB.TabIndex = 1;
            this.GraphChoiceCB.Text = "Select a Chart...";
            this.GraphChoiceCB.SelectedIndexChanged += new System.EventHandler(this.GraphChoiceCB_SelectedIndexChanged);
            // 
            // MyDataGridView
            // 
            this.MyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MyDataGridView.Location = new System.Drawing.Point(12, 42);
            this.MyDataGridView.Name = "MyDataGridView";
            this.MyDataGridView.RowTemplate.Height = 24;
            this.MyDataGridView.Size = new System.Drawing.Size(403, 403);
            this.MyDataGridView.TabIndex = 2;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.Location = new System.Drawing.Point(12, 451);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(403, 101);
            this.UpdateButton.TabIndex = 3;
            this.UpdateButton.Text = "UPDATE";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 564);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.MyDataGridView);
            this.Controls.Add(this.GraphChoiceCB);
            this.Controls.Add(this.MyChart);
            this.Name = "MyForm";
            this.Text = "BI A01";
            ((System.ComponentModel.ISupportInitialize)(this.MyChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart MyChart;
        private System.Windows.Forms.ComboBox GraphChoiceCB;
        private System.Windows.Forms.DataGridView MyDataGridView;
        private System.Windows.Forms.Button UpdateButton;
    }
}

