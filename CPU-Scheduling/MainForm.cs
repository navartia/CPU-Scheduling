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
        private int mode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int processNumber;

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
            int length = dataGridView1.Rows.Count;
            Process[] processArray = new Process[length];
            for(int i = 0; i < length; i ++)
            {
                String name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                int arrivalTime = Convert.ToInt32( dataGridView1.Rows[i].Cells[1].Value.ToString());
                int cpuBurst = Convert.ToInt32( dataGridView1.Rows[i].Cells[2].Value.ToString());

                processArray[i] = new Process(name, arrivalTime, cpuBurst);
            }

            SchedulingAlgorithm scheduler = new FirstComeFirstServed(processArray);
            switch (mode)
            {
                case 0:
                    scheduler = new FirstComeFirstServed(processArray);
                    MessageBox.Show("FCFS");
                    break;
                case 1:
                    scheduler = new ShortestTimeFirst(processArray);
                    MessageBox.Show("STF");
                    break;
            }

            scheduler.Run();
            DataTable processData = scheduler.GetProcessData();
            DataTable eventData = scheduler.GetEventData();

            this.Hide();

            ProcessResultForm prf = new ProcessResultForm(processData, eventData);
            prf.ShowDialog();

            this.Show();
        }

        private void radioButtonFCFS_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFCFS.Checked)
                mode = 0;
        }

        private void radioButtonSTF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSTF.Checked)
                mode = 1;
        }
    }
}
