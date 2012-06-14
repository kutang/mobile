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
        Random rand = new Random();
        Int32 randNum;
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
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("请检查你是否已经输入投诉内容了.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string message=richTextBox1.Text;
            ComplainDao complainDao = new ComplainDao();
            if (complainDao.saveMessage(message)) MessageBox.Show("感谢你的留言,我们会认真处理相关问题的.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.None);
            else MessageBox.Show("留言失败，很抱歉.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("请输入手机号码");
                return;
            }
            if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("请输入完整的三个号码");
                return;
            }
            //三个不同的电话号码
            if (textBox3.Text.Equals(textBox4.Text)||textBox3.Text.Equals(textBox5.Text)||textBox4.Text.Equals(textBox5.Text))
            {
                MessageBox.Show("请输入不同的电话号码");
                return;
            }
            Int64 mobile1=Int64.Parse(textBox3.Text);
            Int64 mobile2=Int64.Parse(textBox4.Text);
            Int64 mobile3=Int64.Parse(textBox5.Text);
            Int64 num = Int64.Parse(textBox6.Text);
            MobileDao dao = new MobileDao();
            if (dao.checknumexists(num))
            {
                MessageBox.Show("此号码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (dao.checkState(num))
                {
                    MessageBox.Show("已经停机或者出于欠费阶段,你是无法进行下一步操作的.非常抱歉.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CallRecordDao d = new CallRecordDao();
                if (d.checkMobile(num, mobile1) == 0)
                {
                    MessageBox.Show("你输入的号码1不正确");
                    return;
                }
                if (d.checkMobile(num, mobile2) == 0)
                {
                    MessageBox.Show("你输入的号码2不正确");
                    return;
                }
                if (d.checkMobile(num, mobile3) == 0)
                {
                    MessageBox.Show("你输入的号码3不正确");
                    return;
                }
                randNum= rand.Next(10);
                MessageBox.Show(randNum.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("请输入验证码");
                return;
            }
            if (Int32.Parse(textBox7.Text) == randNum)
            {
                MessageBox.Show("你输入的验证码是正确的");
            }
            else
            {
                MessageBox.Show("你输入的验证码不正确的");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(textBox2.Text))
            {
                MessageBox.Show("你两次输入的密码不一样");
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
            MobileDao dao = new MobileDao();
            dao.modifyPassword(Int64.Parse(textBox6.Text), textBox1.Text);
            MessageBox.Show("修改密码成功");
        }
    }
}
