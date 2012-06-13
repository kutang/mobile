using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test.Model.Dao;

namespace WinFormTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int level=0;
            if (radioButton1.Checked) level = 5;
            else if (radioButton2.Checked) level = 3;
            else if (radioButton3.Checked) level = 1;
            else if (radioButton4.Checked) level = -2;
            else level = 0;
            GradeDao gradeDao = new GradeDao();
            if (gradeDao.addGrade(level)) MessageBox.Show("评分成功,感谢你真诚的评价", "right", MessageBoxButtons.OK, MessageBoxIcon.None);
            else MessageBox.Show("评分失败,非常抱歉.我们正在努力改正错误.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message=richTextBox1.Text;
            ComplainDao complainDao = new ComplainDao();
            if (complainDao.saveMessage(message)) MessageBox.Show("感谢你的留言,我们会认真处理相关问题的.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.None);
            else MessageBox.Show("留言失败，很抱歉.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
