using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test.Model.Dao;
using Test.Model;

namespace WinFormTest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            time=time.AddMonths(-1);
            Test.Model.TimeShedule t = new TimeShedule();
            //Int32 i=t.isPayTime(time);
            //MessageBox.Show(i.ToString());
        }
    }
}
