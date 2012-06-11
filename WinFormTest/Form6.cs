using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test.Model.Dao;
using System.Threading;
using Test.Model;
using System.Collections;

namespace WinFormTest
{
    public partial class Form6 : Form
    {
        DateTime on;
        private int t = 0;
        MobileDao mobileDaoCheckBalance;
        System.Threading.Timer timer2;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "模拟通话";
            tabPage2.Text = "新增业务";
            this.timer1.Enabled = false;
            this.timer1.Interval = 1;
            mobileDaoCheckBalance = new MobileDao();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            on = DateTime.Now;
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入要拨打的电话号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入用户自己的电话号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //判断该手机号码是否存在
            MobileDao mobileDao = new MobileDao();
            if (mobileDao.checknumexists(Int64.Parse(textBox1.Text)))
            {
                MessageBox.Show("你要拨打的号码不存在,请再次输入.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (mobileDao.checknumexists(Int64.Parse(textBox2.Text)))
            {
                MessageBox.Show("请准确输入你的手机号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (mobileDao.checkState(Int64.Parse(textBox1.Text)))
            {
                MessageBox.Show("你要拨打的号码已经停机或者欠费", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (mobileDao.checkState(Int64.Parse(textBox2.Text)))
            {
                MessageBox.Show("你的手机已经处于停机或者欠费状态", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //拨打电话前先检查用户手机余额(>=0.2)
            if (!mobileDao.checkBalance(Int64.Parse(textBox2.Text), 0.2f))
            {
                MessageBox.Show("用户余额不足0.2元是没有办法通信的", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.timer1.Enabled = true;
            
            //拨打电话每分钟检查一次用户手机的余额
            TimerCallback timerDelegate = new TimerCallback(proxy);
            Mobile mobile = new Mobile();
            timer2 = new System.Threading.Timer(timerDelegate, mobile, 0, 60000);
        }
        //代理方法,负责定时检查用户手机余额,如果用户余额不够，则挂机.并且登记通话记录.
        private void proxy(Object obj)
        {
            Mobile p = (Mobile)obj;
            p.Mobilenumber = Int64.Parse(textBox2.Text);
            //简单测试,每分钟扣费0.2元
            if (!mobileDaoCheckBalance.checkBalance(Int64.Parse(textBox2.Text), 0f,0.2f))
            {
                //停止计时
                
                this.timer2.Dispose();
                this.timer1.Enabled = false;
                //记录通话信息
                CallRecord callRecord = new CallRecord();
                callRecord.FPhoneNumber = Int64.Parse(textBox2.Text);
                callRecord.TPhoneNumber = Int64.Parse(textBox1.Text);
                string record = on + "-" + DateTime.Now + " time:" + this.label2.Text;
                CallRecordDao dao = new CallRecordDao();
                callRecord.T_from = on;
                callRecord.T_to = DateTime.Now;
                dao.saveRecord(callRecord);
                MessageBox.Show("你的余额不足,已经挂机.", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //停止计时
            this.timer1.Enabled = false;
            this.timer2.Dispose();
            //记录通话信息
            CallRecord callRecord = new CallRecord();
            callRecord.FPhoneNumber = Int64.Parse(textBox2.Text);
            callRecord.TPhoneNumber = Int64.Parse(textBox1.Text);
            string record = on + "-" + DateTime.Now + " time:" + this.label2.Text;
            CallRecordDao dao = new CallRecordDao();
            callRecord.T_from = on;
            callRecord.T_to = DateTime.Now;
            dao.saveRecord(callRecord);
        }

        //计时,显示用户打电话时间.
        public string GetAllTime(int time)
        {
            string hh, mm, ss, fff; int f = time % 100;
            // 毫秒 
            int s = time / 100;
            // 转化为秒 
            int m = s / 60;
            // 分 
            int h = m / 60;
            // 时 
            s = s % 60;
            // 秒 //毫秒格式00 

            if (f < 10)
            {
                fff = "0" + f.ToString();
            }
            else
            {
                fff = f.ToString();
            }
            //秒格式00 
            if (s < 10)
            {
                ss = "0" + s.ToString();
            }
            else
            {
                ss = s.ToString();
            } //分格式00 
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            } //时格式00 
            if (h < 10)
            {
                hh = "0" + h.ToString();
            }
            else
            {
                hh = h.ToString();
            }
            //返回 hh:mm:ss.ff 
            return hh + ":" + mm + ":" + ss;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            t = t + 1;//得到总的毫秒数 
            this.label4.Text = GetAllTime(t);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (textBox3.Text == "") { MessageBox.Show("请输入你的手机号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            CallRecordDao callRecallDao = new CallRecordDao();
            IEnumerable<CallRecord> list = callRecallDao.listCallRecord(Int64.Parse(textBox3.Text));
            foreach(CallRecord record in list)
            {
                richTextBox1.AppendText(record.FPhoneNumber.ToString()+"\t");
                richTextBox1.AppendText(record.TPhoneNumber.ToString()+"\t");
                richTextBox1.AppendText(record.T_from.ToString() + "\t");
                richTextBox1.AppendText(record.T_to.ToString()+"\n");
                
            }
        }
        //高级查询
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            CallRecordDao callRecordDao = new CallRecordDao();
            if (textBox3.Text == "") { MessageBox.Show("请输入你的手机号码", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (textBox4.Text == "") { MessageBox.Show("请输入开始月份", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (textBox5.Text == "") { MessageBox.Show("请输入结束月份", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            ArrayList list = callRecordDao.findByMonth(textBox4.Text, textBox5.Text);
            bool flag = false;
            foreach (CallRecord record1 in list)
            {
                richTextBox1.AppendText(record1.FPhoneNumber.ToString() + "\t");
                richTextBox1.AppendText(record1.TPhoneNumber.ToString() + "\t");
                richTextBox1.AppendText(record1.T_from.ToString() + "\t");
                richTextBox1.AppendText(record1.T_to.ToString() + "\n");
                flag = true;
            }
            if (!flag) richTextBox1.AppendText("无记录");
        }
    }
}
