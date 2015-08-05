using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_Scheduling
{
    public partial class MainForm : Form
    {
        private int processNumber;

        public MainForm()
        {
            InitializeComponent();

            processNumber = 0;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            processNumber = Convert.ToInt32(textBox1.Text);

            for (int i = 0; i < processNumber; i++)
            {
                String[] data = {"Process " + (i + 1).ToString(), "", ""};

                dataGridView1.Rows.Add(data);
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            int length = dataGridView1.Rows.Count - 1;
            Process[] processArray = new Process[length];
            for(int i = 0; i < length; i ++)
            {
                String name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                int arrivalTime = Convert.ToInt32( dataGridView1.Rows[i].Cells[1].Value.ToString());
                int cpuBurst = Convert.ToInt32( dataGridView1.Rows[i].Cells[2].Value.ToString());

                processArray[i] = new Process(name, arrivalTime, cpuBurst);
            }

            FirstComeFirstServed fcfs = new FirstComeFirstServed(processArray);
            fcfs.Run();

            DataTable processData = fcfs.GetProcessData();
            DataTable eventData = fcfs.GetEventData();

            this.Hide();
            ProcessResultForm prf = new ProcessResultForm(processData, eventData);
            prf.ShowDialog();

            this.Show();
        }
    }
}
