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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "用户信息";
            tabPage2.Text = "开户";
        }
        //开户
        private void button1_Click(object sender, EventArgs e)
        {
            Int64 num=Int64.Parse(textBox1.Text);
            string pwd = textBox2.Text;
            float balance = float.Parse(textBox3.Text);
            string type="";
            if (radioButton1.Checked) type = "world";
            if (radioButton2.Checked) type = "music";
            if (radioButton3.Checked) type = "travel";

            string name = textBox4.Text;
            string address = textBox5.Text;

            Int32 charge=0;
            if (radioButton4.Checked) charge = 10;
            if (radioButton5.Checked) charge = 20;
            if (radioButton6.Checked) charge = 30;

            //封装mobile类
            Mobile mobile = new Mobile();
            mobile.Mobilenumber = num;
            mobile.DateTimeOfMakeCard = DateTime.Now;
            mobile.LastTimePayFor = DateTime.Now;
            mobile.Mobiletype = type;
            mobile.Balance = balance;
            mobile.State = "on";
            mobile.Password = pwd;

            //封装customer类
            Customer customer = new Customer();
            customer.Name = name;
            customer.Address = address;

            //封装Charge类
            ChargeDao chargedao = new ChargeDao();
            Int32 chargeid = chargedao.getId("monthlypay", charge);
            if (chargeid == -1) { MessageBox.Show("保存Charge失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            Test.Model.Rule rule = new Test.Model.Rule();
            rule.Chargeid = chargeid;
            rule.Mobilenumber = num;

            MainDao dao = new MainDao();
            string message=dao.save(mobile, customer,rule);
            MessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        //检测号码是否被占用了
        private void button6_Click(object sender, EventArgs e)
        {
            Int64 num = Int64.Parse(textBox1.Text);
            MobileDao dao = new MobileDao();
            if (!dao.checknumexists(num)) MessageBox.Show("此号码已经被占用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else MessageBox.Show("这个号码可以使用", "正确", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int64 num = Int64.Parse(textBox6.Text);
            MobileDao dao = new MobileDao();
            if (dao.checknumexists(num)) MessageBox.Show("此号码不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                AccountDao accountDao = new AccountDao();
                Int32 customerId = accountDao.getCustomerId(num);
                CustomerDao customerDao = new CustomerDao();
                label9.Text = customerDao.getName(customerId);
                MobileDao mobileDao = new MobileDao();
                Mobile mobile = mobileDao.getMobile(num);
                label11.Text = num.ToString();
                if (mobile.Mobiletype.Equals("world")) label13.Text = "全球通";
                else if (mobile.Mobiletype.Equals("music")) label13.Text = "动感地带";
                else label13.Text = "神州行";
                label15.Text = mobile.Balance.ToString() + "元";
                label18.Text = mobile.LastTimePayFor.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int64 num = Int64.Parse(textBox6.Text);
            string name;
            Int32 chargepermonth;
            if (checkBox1.Checked)
            {
                name = "gprs";
                chargepermonth = 10;
            }
            else if (checkBox2.Checked)
            {
                name = "music";
                chargepermonth = 2;
            }
            else
            {

                MessageBox.Show("请选择要开通的业务类型", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ChargeDao chargedao = new ChargeDao();
            Int32 chargeid = chargedao.getId(name, chargepermonth, 2);
            Test.Model.Rule rule = new Test.Model.Rule();
            rule.Chargeid = chargeid;
            rule.Mobilenumber = num;
            RuleDao ruleDao = new RuleDao();
            //首先判断是否已经开通了该业务
            if (!ruleDao.check(num,chargeid)) { MessageBox.Show("你已经开通同该业务,无需重复操作", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            MobileDao mobileDao = new MobileDao();
            //然后判断用户的余额是否足够
            if (!mobileDao.checkBalance(num,chargepermonth)) { MessageBox.Show("你的余额不足,无法开通该业务", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            ruleDao.save(rule);
            //进行扣费
            mobileDao.koufei(num,chargepermonth);
            MessageBox.Show("新业务开通成功,从即日起每个月进行相关扣费");

        }
        //停机办理
        private void button4_Click(object sender, EventArgs e)
        {
            Int64 num = Int64.Parse(textBox6.Text);
            MobileDao mobileDao = new MobileDao();
            if (textBox7.Text == "")
            {
                MessageBox.Show("输入你的密码才能办理停机业务", "wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox7.Text == mobileDao.getPassword(num))
            {
                if(mobileDao.checkState(num))
                {
                    MessageBox.Show("已经处于停机状态,不用重复该操作","wrong",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                //MessageBox.Show("你输入的密码是正确的");
                if(!mobileDao.checkBalance(num,5))
                {
                    MessageBox.Show("你的卡需要预存至少5块钱才能进行停机业务办理");
                    return;
                }
                mobileDao.changeState(num);
                mobileDao.tingjikoufei(num);
                MessageBox.Show("你已经办理了停机业务，我们每个月会收取你5块钱的服务费.");
            }
            else
                MessageBox.Show("你输入的密码是错误的");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Int64 num=Int64.Parse(textBox6.Text);
            MobileDao dao = new MobileDao();
            //判断用户是否是停机用户或者是欠费用户
            if (dao.checkState(num))
            {
                MessageBox.Show("你是停机或者欠费用户,我们不再对你进行月租扣费.");
                return;
            }

            Mobile mobile=dao.getMobile(num);
            TimeShedule timeShedule=new TimeShedule();
            if (timeShedule.isPayTime(mobile.LastTimePayFor))
            {
                RuleDao rd = new RuleDao();
                Int32 chargeid=rd.getId(num);
                ChargeDao chargeDao = new ChargeDao();
                Int32 chargePermonth=chargeDao.getCharge(chargeid).Chargepermonth;
                dao.koufei(num, chargePermonth, 1);
                MessageBox.Show("扣费成功");
            }
            else
            {
                MessageBox.Show("你上次扣费时间是:" + mobile.LastTimePayFor.ToString() + ",今天的日期是" + DateTime.Now.ToString() + ",所以扣费不成功.");

            }
        }
    }
}
