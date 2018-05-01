/*
 * FILENAME     : myForm.cs
 * PROJECT      : BI_A01
 * PROGRAMMER   : Jody Markic
 * FIRST VERSION: 2017-09-21
 * DESCRIPTION  : This file contain the code behind of the MyForm class, a interactive GUI for
 *                the BI_A01 project. BI_A01 displays a graph, a dataGridView, an Update button, and
 *                a comboBox. Select a chart from the combobox, manipulate values in the dataGridView,
 *                hit update and watch the graph update on the fly.
 */

//namespaces
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BI_A01
{
    // Class        : MyForm : Form
    // Description  : This class acts as GUI for an end user to interact with the project BI_A01.
    //                MyForm contains displays a graph, a dataGridView, an Update button, and combobox.
    //                It holds event handles for some of these objects and chart and grid data is seeded
    //                as class level variables.
    //
    public partial class MyForm : Form
    {
        //class variables

        //just grouped data to be used in seeding the 4 charts in arrays
        private enum WhichChart { PieChart = 0, LineChart, ControlChart, ParetoDiagram };
        private enum WhichMonth { Jan = 0, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };

        const int PieLabelMax = 3;
        const int MonthsInTheYear = 12;

        private string[] chartTypes = { "Pie Chart", "Line Chart", "Control Chart", "Pareto Diagram" };

        private int[] pieChartValues = { 52, 18, 30 };
        private string[] pieChartLabels = { "EMEA", "Asia Pacific", "Americas" };

        private int[] lineChartExpected = { 182, 193, 185, 179, 198, 195, 174, 165, 185, 149, 169, 180 };
        private int[] lineChartActual = { 145, 109, 105, 100, 145, 109, 130, 140, 150, 193, 185, 171 };
        private string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };    

        private double[] controlChartData =
        { 97, 98, 102, 100, 103,
         99, 96, 100.5, 103, 102,
         101, 94.5, 98, 100, 104.5,
         99, 104, 102, 100, 101.5
       };

        private double[] controlLines = { 100, 105, 95, 104, 96 };

        StripLine CL = new StripLine();
        StripLine UCL = new StripLine();
        StripLine LCL = new StripLine();
        StripLine UWL = new StripLine();
        StripLine LWL = new StripLine();

        private string[] defects = { "Holes", "Poor Mix", "Stains", "Not Enough Component", "Torn", "Others" };
        private int[] defectsValues = { 27, 11, 8, 7, 5, 2 };

        private DataTable dataTable = new DataTable();

        //
        // METHOD       : MyForm()
        // DESCRIPTION  : Constructor
        // PARAMETERS   : Initializes the form, seeds the combobox with graph options
        //                and connects the datagrid with a data table.
        // RETURN       : N/A
        //
        public MyForm()
        {
            InitializeComponent();

            SeedCombobox();
            MyDataGridView.DataSource = dataTable;
        }

        //
        // METHOD       : UpdateButton_Click()
        // DESCRIPTION  : Event handle for update, determines which chart is being loaded
        // PARAMETERS   : object sender, EventArgs e
        // RETURN       : void
        //
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            int selectedIndex;
            selectedIndex = GraphChoiceCB.SelectedIndex;

            switch (selectedIndex)
            {
                case (int)WhichChart.PieChart:
                case (int)WhichChart.LineChart:
                    updatePieOrLine();
                    break;
                case (int)WhichChart.ControlChart:
                    updateControl();
                    break;
                case (int)WhichChart.ParetoDiagram:
                    updatePareto();
                    break;
                default:
                    break;
            }
        }

        //
        // METHOD       : GraphChoiceCB_SelectedIndexChanged()
        // DESCRIPTION  : Event handle for combobox selection, loads the chart which corresponds to the index
        // PARAMETERS   : object sender, EventArgs e
        // RETURN       : void
        //
        private void GraphChoiceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex;

            SelectedIndex = GraphChoiceCB.SelectedIndex;
            LoadAChart(SelectedIndex);
        }

        //
        // METHOD       : updatePieOrLine()
        // DESCRIPTION  : Update points in of the pie chart based off of cell values in the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void updatePieOrLine()
        {
            double buffer;

            //loop for as many series there are currently connected to the chart (A Series gets a column in the GridView)
            for (int j = 0; j < MyChart.Series.Count; j++)
            {
                //clear existing datapoints
                MyChart.Series[j].Points.Clear();
                //loop down a column for all it's rows
                for (int i = 0; i < MyDataGridView.Rows.Count; i++)
                {
                    //check that each cell in the column were going to update to ensure its not null
                    if (MyDataGridView.Rows[i].Cells[j + 1].Value != null)
                    {
                        //ensure that each cell in the column were going through is a number
                        if (Double.TryParse(MyDataGridView.Rows[i].Cells[j + 1].Value.ToString(), out buffer))
                        {
                            MyChart.Series[j].Points.AddY(buffer);
                        }
                    }
                }
            }

            // just something extra for the Pie Chart to label proportions with region names
            if (GraphChoiceCB.SelectedIndex == (int)WhichChart.PieChart)
            {
                for (int i = 0; i < pieChartLabels.Length; i++)
                {
                    MyChart.Series["MySeries"].Points[i].Label = pieChartLabels[i];
                }
            }
        }

        //
        // METHOD       : updateControl()
        // DESCRIPTION  : Update points in of the control chart based off of cell values in the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void updateControl()
        {
            double buffer = 0;
            //update column one like usual

            int whatisit = MyDataGridView.Rows.Count;
            //update column one like usual
            MyChart.Series[0].Points.Clear();

            //loop for every piece of control chart sample data
            for (int i = 0; i < controlChartData.Length; i++)
            {
                //ensure that each cell in the column were going through is a number
                if (Double.TryParse(MyDataGridView.Rows[i].Cells[0].Value.ToString(), out buffer))
                {
                    //add the data to the graph
                    MyChart.Series[0].Points.AddY(buffer);
                    MyChart.Series[0].Points[i].MarkerStyle = MarkerStyle.Circle;
                    MyChart.Series[0].Points[i].MarkerColor = Color.DodgerBlue;
                    MyChart.Series[0].Points[i].MarkerSize = 15;
                }
            }

            //read values from the datagridview and set control lines
            if (Double.TryParse(MyDataGridView.Rows[0].Cells[1].Value.ToString(), out buffer))
            {
                CL.IntervalOffset = buffer;
            }
            
            if (Double.TryParse(MyDataGridView.Rows[0].Cells[2].Value.ToString(), out buffer))
            {
                UCL.IntervalOffset = buffer;
            }

            if (Double.TryParse(MyDataGridView.Rows[0].Cells[3].Value.ToString(), out buffer))
            {
                LCL.IntervalOffset = buffer;
            }

            if (Double.TryParse(MyDataGridView.Rows[0].Cells[4].Value.ToString(), out buffer))
            {
                UWL.IntervalOffset = buffer;
            }

            if (Double.TryParse(MyDataGridView.Rows[0].Cells[5].Value.ToString(), out buffer))
            {
                LWL.IntervalOffset = buffer;
            }
        }

        //
        // METHOD       : updatePareto()
        // DESCRIPTION  : Update points in of the pareto diagram based off of cell values in the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void updatePareto()
        {
            double buffer;
            //clear points of all series from graph
            MyChart.Series[0].Points.Clear();
            MyChart.Series[1].Points.Clear();

            //loop for every piece of pareto chart sample data
            for (int i = 0; i < defectsValues.Length; i++)
            {
                //check that each cell in the column were going to update to ensure its not null
                if (MyDataGridView.Rows[i].Cells[1].Value != null)
                {
                    //ensure that each cell in the column were going through is a number
                    if (Double.TryParse(MyDataGridView.Rows[i].Cells[1].Value.ToString(), out buffer))
                    {
                        //update chart values
                        MyChart.Series[0].Points.AddY(buffer);
                    }
                }
            }

            //variables for calculating total defect percentages
            double totalDefects = 0;
            double defectPercentage = 0;
            double cumulativeDefectsPercentage = 0;

            //loop for each point on the chart
            for (int i = 0; i < MyChart.Series[0].Points.Count; i++)
            {
                //tally total amount of defects
                totalDefects += MyChart.Series[0].Points[i].YValues[0];
            }

            //loop for each point on the chart
            for (int i = 0; i < MyChart.Series[0].Points.Count; i++)
            {
                //calculate the current accumluated total defect percentage
                defectPercentage = (MyChart.Series[0].Points[i].YValues[0] * 100) / totalDefects;
                cumulativeDefectsPercentage += defectPercentage;
                //chart the total defect percentage line to the pareto diagram
                MyChart.Series[1].Points.AddY(cumulativeDefectsPercentage);
                MyChart.Series[1].Points[i].MarkerStyle = MarkerStyle.Circle;
                MyChart.Series[1].Points[i].MarkerColor = Color.Red;
                MyChart.Series[1].Points[i].MarkerSize = 5;
            }
        }

        //
        // METHOD       : ClearCurrentChart()
        // DESCRIPTION  : Clears values on the the chart
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void ClearCurrentChart()
        {
            MyChart.Series.Clear();
            MyChart.Titles.Clear();
            MyChart.ChartAreas.Clear();
        }

        //
        // METHOD       : ClearCurrentDataGrid()
        // DESCRIPTION  : Clears values in the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void ClearCurrentDataGrid()
        {
            dataTable.Clear();
            dataTable.Columns.Clear();
        }

        //
        // METHOD       : SeedCombobox()
        // DESCRIPTION  : Seed combobox for graph options
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void SeedCombobox()
        {
            foreach (string chartName in chartTypes)
            {
                GraphChoiceCB.Items.Add(chartName);
            }
        }

        //
        // METHOD       : AddControlLines()
        // DESCRIPTION  : Add Striplines to the control chart
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void AddControlLines()
        {
            //set fields for stripline objects
            CL.Interval = 0;
            CL.IntervalOffset = controlLines[0];
            CL.StripWidth = 0.3;
            CL.BackColor = Color.Black;

            UCL.Interval = 0;
            UCL.IntervalOffset = controlLines[1];
            UCL.StripWidth = 0.3;
            UCL.BackColor = Color.Red;

            LCL.Interval = 0;
            LCL.IntervalOffset = controlLines[2];
            LCL.StripWidth = 0.3;
            LCL.BackColor = Color.Red;

            UWL.Interval = 0;
            UWL.IntervalOffset = controlLines[3];
            UWL.StripWidth = 0.3;
            UWL.BackColor = Color.Yellow;

            LWL.Interval = 0;
            LWL.IntervalOffset = controlLines[4];
            LWL.StripWidth = 0.3;
            LWL.BackColor = Color.Yellow;

            //add striplines to control chart
            MyChart.ChartAreas["MyChartArea"].AxisY.StripLines.Add(CL);
            MyChart.ChartAreas["MyChartArea"].AxisY.StripLines.Add(UCL);
            MyChart.ChartAreas["MyChartArea"].AxisY.StripLines.Add(LCL);
            MyChart.ChartAreas["MyChartArea"].AxisY.StripLines.Add(UWL);
            MyChart.ChartAreas["MyChartArea"].AxisY.StripLines.Add(LWL);

            //label each stripline
            CL.Text = "CL";
            UCL.Text = "UCL";
            LCL.Text = "LCL";
            UWL.Text = "UWL";
            LWL.Text = "LWL";

        }

        //
        // METHOD       : LoadPieChart()
        // DESCRIPTION  : Load the PieChart and its seeded values into the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void LoadPieChart()
        {
            //clear chart of data
            ClearCurrentChart();

            //add a chart area, series , and title to chart
            MyChart.ChartAreas.Add("MyChartArea");
            MyChart.Series.Add("MySeries");
            MyChart.Titles.Add("Sweet Pie Chart");

            //create pie chart
            MyChart.Series["MySeries"].ChartType = SeriesChartType.Pie;

            //add data to the chart
            foreach (int datapoint in pieChartValues)
            {
                MyChart.Series["MySeries"].Points.AddY(datapoint);
            }

            //label chart data
            for (int i = 0; i < pieChartLabels.Length; i++)
            {
                MyChart.Series["MySeries"].Points[i].Label = pieChartLabels[i];
            }

            //clear grid data
            ClearCurrentDataGrid();

            //create and add columns to grid view
            DataColumn dataColumnName = new DataColumn("Name");
            DataColumn dataColumnValue = new DataColumn("Value");

            dataTable.Columns.Add(dataColumnName);
            dataTable.Columns.Add(dataColumnValue);

            //add values to the grid that corrispond to points on the graph
            for (int i = 0; i < pieChartValues.Length; i++)
            {
                dataTable.Rows.Add(pieChartLabels[i], pieChartValues[i]);
            }

            //set first column to read only.
            MyDataGridView.Columns[0].ReadOnly = true;
        }

        //
        // METHOD       : LoadLineChart()
        // DESCRIPTION  :  Load the LineChart and its seeded values into the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void LoadLineChart()
        {
            //clear chart of data
            ClearCurrentChart();

            //configure chart area axis intervals
            MyChart.ChartAreas.Add("MyChartArea");

            MyChart.ChartAreas["MyChartArea"].AxisY.Maximum = 250;
            MyChart.ChartAreas["MyChartArea"].AxisY.Minimum = 0;

            MyChart.ChartAreas["MyChartArea"].AxisX.Interval = 1;
            MyChart.ChartAreas["MyChartArea"].AxisX.Maximum = 12;
            MyChart.ChartAreas["MyChartArea"].AxisY.Minimum = 0;

            //add axis titles
            MyChart.ChartAreas["MyChartArea"].Axes[0].Title = "Month";
            MyChart.ChartAreas["MyChartArea"].Axes[1].Title = "Number Of Units Produced";

            //add series
            MyChart.Series.Add("Expected");
            MyChart.Series.Add("Actual");

            //create line charts
            MyChart.Series["Expected"].ChartType = SeriesChartType.Line;
            MyChart.Series["Actual"].ChartType = SeriesChartType.Line;

            // add chart title
            MyChart.Titles.Add("Sweet Line Chart");

            //add data to series expected
            foreach (int datapoint in lineChartExpected)
            {
                MyChart.Series["Expected"].Points.AddY(datapoint);
            }


            //add data to series actual
            for (int i = 0; i < lineChartExpected.Length; i++)
            {
                MyChart.Series["Actual"].Points.AddY(lineChartActual[i]);
                MyChart.Series["Actual"].Points[i].AxisLabel = months[i];
            }

            //clear grid data
            ClearCurrentDataGrid();

            //create and add columns to the grid
            DataColumn dataColumnName = new DataColumn("Defects");
            DataColumn dataColumnValue = new DataColumn("Count");
            DataColumn dataColumnValue1 = new DataColumn("Actual");

            dataTable.Columns.Add(dataColumnName);
            dataTable.Columns.Add(dataColumnValue);
            dataTable.Columns.Add(dataColumnValue1);

            //seed grid with data that corrisponds to points on the graph
            for (int i = 0; i < MonthsInTheYear; i++)
            {
                dataTable.Rows.Add(months[i], lineChartExpected[i], lineChartActual[i]);
            }

            //set the first column in the grid to read only
            MyDataGridView.Columns[0].ReadOnly = true;
        }

        //
        // METHOD       : LoadControlChart()
        // DESCRIPTION  : Load the ControlChart and its seeded values into the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void LoadControlChart()
        {
            //clear chart of data
            ClearCurrentChart();

            //configure chart axises
            MyChart.ChartAreas.Add("MyChartArea");

            MyChart.ChartAreas["MyChartArea"].AxisY.Maximum = 110;
            MyChart.ChartAreas["MyChartArea"].AxisY.Minimum = 90;

            //add title
            MyChart.Titles.Add("Sweet Control Chart");

            //add series
            MyChart.Series.Add("Sample");
            //add chart type
            MyChart.Series["Sample"].ChartType = SeriesChartType.Line;

            //add data to the graph
            for (int i = 0; i < controlChartData.Length; i++)
            {
                MyChart.Series["Sample"].Points.AddY(controlChartData[i]);
                MyChart.Series["Sample"].Points[i].MarkerStyle = MarkerStyle.Circle;
                MyChart.Series["Sample"].Points[i].MarkerColor = Color.DodgerBlue;
                MyChart.Series["Sample"].Points[i].MarkerSize = 15;
            }

            //add striplines
            AddControlLines();

            //clear grid data
            ClearCurrentDataGrid();

            //Here is where i'll connect the chart data to the DataGridView

            //create and add columns to the grid
            DataColumn columnOne = new DataColumn("Sample");
            DataColumn columnTwo = new DataColumn("CL");
            DataColumn columnThree = new DataColumn("UCL");
            DataColumn columnFour = new DataColumn("LCL");
            DataColumn columnFive = new DataColumn("UWL");
            DataColumn columnSix = new DataColumn("LWL");

            dataTable.Columns.Add(columnOne);
            dataTable.Columns.Add(columnTwo);
            dataTable.Columns.Add(columnThree);
            dataTable.Columns.Add(columnFour);
            dataTable.Columns.Add(columnFive);
            dataTable.Columns.Add(columnSix);

            //add control line values to the grid
            dataTable.Rows.Add(controlChartData[0], controlLines[0], controlLines[1],
                controlLines[2], controlLines[3], controlLines[4]);

            //add data to the grid that corrisponds with points on the graph
            for (int i = 1; i < controlChartData.Length; i++)
            {
                dataTable.Rows.Add(controlChartData[i]);
            }
        }

        //
        // METHOD       : LoadParetoDiagram()
        // DESCRIPTION  : Load the ParetoDiagram and its seeded values into the DataGridView
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void LoadParetoDiagram()
        {
            //clear chart data
            ClearCurrentChart();

            //add a chart area
            MyChart.ChartAreas.Add("MyChartArea");

            //add series
            MyChart.Series.Add("Defects");
            MyChart.Series.Add("Scrap Percentage");

            //give series a chart type
            MyChart.Series["Defects"].ChartType = SeriesChartType.Column;
            MyChart.Series["Scrap Percentage"].ChartType = SeriesChartType.Line;
            MyChart.Series["Scrap Percentage"].YAxisType = AxisType.Secondary;

            //add title
            MyChart.Titles.Add("Sweet Pareto Diagram");

            //configure chart axises
            MyChart.ChartAreas["MyChartArea"].AxisY.Title = "Freq.";
            MyChart.ChartAreas["MyChartArea"].AxisY2.Title = "Cum. %";
            MyChart.ChartAreas["MyChartArea"].AxisX.Title = "Defect Catergory";

            MyChart.ChartAreas["MyChartArea"].AxisY2.Enabled = AxisEnabled.True;
            MyChart.ChartAreas["MyChartArea"].AxisY2.Maximum = 100;
            MyChart.ChartAreas["MyChartArea"].AxisY2.Minimum = 0;

            //add data to the chart
            for (int i = 0; i < defects.Length; i++)
            {
                MyChart.Series["Defects"].Points.AddY(defectsValues[i]);
                MyChart.Series["Defects"].Points[i].AxisLabel = defects[i];
                MyChart.Series["Defects"].Points[i].IsValueShownAsLabel = true;
            }

            //load percentage line to chart
            LoadPercentageLine();

            //c;ear grid data
            ClearCurrentDataGrid();

            //create and add columns to grid
            DataColumn columnsOne = new DataColumn("Defects");
            DataColumn columnsTwo = new DataColumn("Count");

            dataTable.Columns.Add(columnsOne);
            dataTable.Columns.Add(columnsTwo);

            //add data to grid that corrisponds to points on the graph
            for (int i = 0; i < defectsValues.Length; i++)
            {
                dataTable.Rows.Add(defects[i], defectsValues[i]);
            }

            //set first column of the grid to readonly.
            MyDataGridView.Columns[0].ReadOnly = true;

        }

        //
        // METHOD       : LoadPercentageLine()
        // DESCRIPTION  : Loads the total percentage line chart into paretoDiagram
        // PARAMETERS   : N/A
        // RETURN       : void
        //
        private void LoadPercentageLine()
        {
            double totalDefects = 0;
            double defectPercentage = 0;
            double cumulativeDefectsPercentage = 0;

            //total up number of defects
            for (int i = 0; i < defectsValues.Length; i++)
            {
                totalDefects += defectsValues[i];
            }

            //calculate current accumulated total of defects parts as a percentage
            for (int i = 0; i < defectsValues.Length; i++)
            {
                defectPercentage = (defectsValues[i] * 100) / totalDefects;
                cumulativeDefectsPercentage += defectPercentage;
                //add line to the chart
                MyChart.Series["Scrap Percentage"].Points.AddY(cumulativeDefectsPercentage);
                MyChart.Series["Scrap Percentage"].Points[i].MarkerStyle = MarkerStyle.Circle;
                MyChart.Series["Scrap Percentage"].Points[i].MarkerColor = Color.Red;
                MyChart.Series["Scrap Percentage"].Points[i].MarkerSize = 5;
            }
        }

        //
        // METHOD       : LoadAChart()
        // DESCRIPTION  : Determine which chart to load
        // PARAMETERS   : int newChart
        // RETURN       : void
        //
        private void LoadAChart(int newChart)
        {
            switch (newChart)
            {
                case (int)WhichChart.PieChart:
                    LoadPieChart();
                    break;
                case (int)WhichChart.LineChart:
                    LoadLineChart();
                    break;
                case (int)WhichChart.ControlChart:
                    LoadControlChart();
                    break;
                case (int)WhichChart.ParetoDiagram:
                    LoadParetoDiagram();
                    break;
                default:
                    break;
            }
        }
    }
}
