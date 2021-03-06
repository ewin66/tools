﻿using System;
using System.IO;
using System.Windows.Forms;

namespace 数据库配置工具
{
    public partial class DBForm : Form
    {

        public DBForm()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.ini"))
            {
                //Provider=SQLOLEDB.1;Password=aza@lea@123;Persist Security Info=True;User ID=sa;Initial Catalog=fkgl;Data Source=ftp.smartpioneer.cn
                Tools.IniHelper.SetFilePath(System.Environment.CurrentDirectory + @"\config.ini");
                tbservername.Text = Tools.IniHelper.ReadIniData("数据库配置", "DataSource", "");
                tbdbname.Text = Tools.IniHelper.ReadIniData("数据库配置", "Database", "");
                tbusername.Text = Tools.IniHelper.ReadIniData("数据库配置", "UserID", "");
                string s = Tools.IniHelper.ReadIniData("数据库配置", "Password", "").Trim();
                if (!string.IsNullOrWhiteSpace(s))
                {
                    tbpassword.Text = Tools.EncyptHelper.MD5Decrypt(s);
                }
            }
            else
            {
                tbservername.Text = "";
                tbdbname.Text = "";
                tbusername.Text = "";
                tbpassword.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if (ck.Checked)
            {
                tbpassword.PasswordChar = '\0';
            }
            else
            {
                tbpassword.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tools.IniHelper.SetFilePath(System.Environment.CurrentDirectory + @"\config.ini");
            Tools.IniHelper.IniWriteValue("数据库配置", "DataSource", tbservername.Text);
            Tools.IniHelper.IniWriteValue("数据库配置", "Database", tbdbname.Text);
            Tools.IniHelper.IniWriteValue("数据库配置", "UserID", tbusername.Text);
            Tools.IniHelper.IniWriteValue("数据库配置", "Password", Tools.EncyptHelper.MD5Encrypt(tbpassword.Text.Trim()));
            MessageBox.Show("保存完成");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBClass dbClass = new DBClass(tbservername.Text, tbdbname.Text, tbusername.Text, tbpassword.Text);
            if (dbClass.OpenTest())
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }
    }
}
