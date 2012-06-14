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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GradeDao gradeDao = new GradeDao();
            label2.Text = gradeDao.getLastGrade().ToString() + "分";
            label4.Text = gradeDao.getMonthGrade().ToString() + "分";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            ComplainDao complainDao = new ComplainDao();
            IEnumerable<Complain> ie = complainDao.getMessage();
            foreach (Complain c in ie)
            {
                richTextBox1.AppendText("1" + "\t");
                richTextBox1.AppendText(c.Dtime.ToString() + "\t");
                richTextBox1.AppendText(c.Message + "\n");
                richTextBox1.AppendText("--------------------------------------------------------------------------\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            button3.Enabled = true;
            if (textBox1.Text == "")
            {
                MessageBox.Show("请你输入你的电话号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int64 num = Int64.Parse(textBox1.Text);
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
                //查找该用户已经开通的套餐
                RuleDao ruleDao = new RuleDao();
                IEnumerable<Test.Model.Rule> ie = ruleDao.getRule(num);
                foreach (Test.Model.Rule r in ie)
                {
                    if (r.Chargeid == 4) checkBox1.Checked = true;
                    else if (r.Chargeid == 5) checkBox2.Checked = true;
                    else continue;
                }
                if (checkBox1.Checked) MessageBox.Show("你已经开通了GPRS服务");
                else checkBox1.Enabled = false;
                if (checkBox2.Checked) MessageBox.Show("你已经开通了铃声服务");
                else checkBox2.Enabled = false;
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    MessageBox.Show("你没有开通以下服务");
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    button3.Enabled = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int64 num = Int64.Parse(textBox1.Text);
            if (checkBox1.Checked)
            {
                RuleDao rule = new RuleDao();
                if (rule.quxiaotaocan(num, 4))
                {
                    MessageBox.Show("已经取消了GPRS套餐");
                    checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                }
                else
                    MessageBox.Show("系统出现点小故障,现在没办法为你服务.");
            }
            if (checkBox2.Checked)
            {
                RuleDao rule = new RuleDao();
                if (rule.quxiaotaocan(num, 4))
                {
                    MessageBox.Show("已经取消了铃声套餐");
                    checkBox2.Checked = false;
                    checkBox2.Enabled = false;
                }
                else
                    MessageBox.Show("系统出现点小故障,现在没办法为你服务.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请你输入你的电话号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int64 num = Int64.Parse(textBox1.Text);
            MobileDao dao = new MobileDao();
            if (dao.checknumexists(num))
            {
                MessageBox.Show("此号码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(dao.checkState(num))
            {
                MessageBox.Show("你的号码处于停机或者欠费状态,无法进行该业务.");
                return;
            }
            //修改基本套餐
            else
            {
                RuleDao ruleDao = new RuleDao();
                //id表示用户基本套餐类型,1表示10元每月,2表示20月每月,3表示30元每月
                Int32 id = ruleDao.getId(num);
                if (id == 0)
                {
                    MessageBox.Show("系统出错");
                    return;
                }
                //MessageBox.Show(id.ToString());
                if (!radioButton4.Checked && !radioButton5.Checked && !radioButton4.Checked)
                {
                    MessageBox.Show("请选择套餐类型");
                    return;
                }
                if (radioButton4.Checked && id == 1)
                {
                    MessageBox.Show("你原来的套餐就是这个,不能重复选择.");
                    return;
                }
                if (radioButton5.Checked && id == 2)
                {
                    MessageBox.Show("你原来的套餐就是这个,不能重复选择.");
                    return;
                }
                if (radioButton6.Checked && id == 3)
                {
                    MessageBox.Show("你原来的套餐就是这个,不能重复选择.");
                    return;
                }
                //如果radioButton4被选择了，表示用户选择的套餐为10元每月的
                if (radioButton4.Checked)
                {
                    if (id == 2)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 10))
                        {
                            MessageBox.Show("你卡上的余额不足10块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num,1);
                            //扣费和修改日期
                            dao.koufei(num, 10,1);
                        }
                    }
                    else if (id == 3)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 10))
                        {
                            MessageBox.Show("你卡上的余额不足10块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num, 1);
                            //扣费和修改日期
                            dao.koufei(num, 10, 1);
                        }
                    }
                }

                //如果radioButton5被选择了,表示用户选择的套餐为20月每月的
                if (radioButton5.Checked)
                {
                    if (id == 1)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 20))
                        {
                            MessageBox.Show("你卡上的余额不足20块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num, 2);
                            //扣费和修改日期
                            dao.koufei(num, 20, 1);
                        }
                    }
                    else if (id == 3)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 20))
                        {
                            MessageBox.Show("你卡上的余额不足20块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num, 2);
                            //扣费和修改日期
                            dao.koufei(num, 20, 1);
                        }
                    }
                }
                //radioButton6表示选择的套餐为30元每月的
                if (radioButton6.Checked)
                {
                    if (id == 1)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 30))
                        {
                            MessageBox.Show("你卡上的余额不足30块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num, 3);
                            //扣费和修改日期
                            dao.koufei(num, 30, 1);
                        }
                    }
                    else if (id == 2)
                    {
                        //查询余额
                        if (!dao.checkBalance(num, 30))
                        {
                            MessageBox.Show("你卡上的余额不足30块钱,不能修改成该业务.");
                            return;
                        }
                        //修改基本套餐类型,马上进行扣费，并且修改扣费日期
                        else
                        {
                            //更改为10元/月套餐
                            RuleDao d = new RuleDao();
                            d.update(num, 3);
                            //扣费和修改日期
                            dao.koufei(num, 30, 1);
                        }
                    }
                }
            }
        }
    }
}
