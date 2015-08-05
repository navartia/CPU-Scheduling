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
    public partial class ProcessResultForm : Form
    {
        private DataTable eventData;

        public ProcessResultForm()
        {
            InitializeComponent();
        }

        public ProcessResultForm(DataTable processData, DataTable eventData)
        {
            InitializeComponent();

            dataGridView1.DataSource = processData;
            this.eventData = eventData;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Brush main = new SolidBrush(Color.White);
            Pen border = new Pen(Color.Black);

            //Main Bar
            e.Graphics.DrawRectangle(border, new Rectangle(16, 24, 751, 61));
            e.Graphics.FillRectangle(main, new Rectangle(17, 25, 750, 60));

            //Color randomColor;
            Random randomGen = new Random();
        }
    }
}
