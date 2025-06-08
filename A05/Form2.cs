using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace A05
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Aktivnost.Statistika();
            DataTable grafikon = new DataTable();
            grafikon.Columns.Add("kolona1");
            grafikon.Columns.Add("kolona2");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int prvaKolona = 0;
                int drugaKolona = 1;
                grafikon.Rows.Add(row.Cells[prvaKolona].Value, row.Cells[drugaKolona].Value);
            }
            chart1.DataSource = grafikon;
            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            chart1.Series["Series1"].XValueMember = "kolona1";
            chart1.Series["Series1"].YValueMembers = "kolona2";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
