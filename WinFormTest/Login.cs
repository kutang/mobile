using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Test.Model.util;

namespace WinFormTest
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("请输入密码!","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {
                if (textBox1.Text == "root")
                {
                    SessionTransport.phonenumber=this.textBox1.Text;
                    //MainFrame mainFrame = new MainFrame();
                    //this.Visible = false;
                    //mainFrame.Show();
                }
                else
                {
                    MessageBox.Show("密码错误!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
