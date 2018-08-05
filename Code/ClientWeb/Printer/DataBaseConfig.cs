using System;
using System.Windows.Forms;
using Utility;

namespace Printer
{
    public partial class DataBaseConfig : Form
    {
        public DataBaseConfig()
        {
            InitializeComponent();
        }

        private string dbConnectionString;
        public string DbConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }

        private void DataBaseConfig_Load(object sender, EventArgs e)
        {
            setConStrBoxV();
        }

        private void setConStrBoxV()
        {
            dbConnectionString = Function.GetConnectionStringsConfig("ConnectionString");
            txtServerName.Text = GetServerNameByConStr();
            txtDataBaseName.Text = GetDataBaseNameByConStr();
            txtAccount.Text = GetAccountByConStr();
            txtPwd.Text = GetPwdByConStr();
        }

        #region 连接字符串相关
        private string GetServerNameByConStr()
        {
            return Function.GetSpanStr("Data Source=", ";", dbConnectionString);
        }

        private string GetDataBaseNameByConStr()
        {
            return Function.GetSpanStr("Initial Catalog=", ";", dbConnectionString);
        }

        private string GetAccountByConStr()
        {
            return Function.GetSpanStr("User ID=", ";", dbConnectionString);
        }

        private string GetPwdByConStr()
        {
            return Function.GetSpanStr("Password=", ";", dbConnectionString);
        }
        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtDataBaseName.Text.ToLower() == "master")
            {
                MessageBox.Show("不可以使用系统数据库名，请输入一个新的数据库名！");
            }
            else
            {
                string conStr = getInputConStr();
                if (!DBHelper.ConnectionTest(conStr))
                {
                    MessageBox.Show("测试连接失败,请输入正确的配置信息！", "提示");
                }
                else
                {
                    dbConnectionString = conStr;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private string getInputConStr()
        {
            string conStr = "Data Source=" + txtServerName.Text.TrimEnd() + ";Initial Catalog=" + txtDataBaseName.Text.TrimEnd() + ";User ID=" + txtAccount.Text.TrimEnd() + ";Password=" + txtPwd.Text.TrimEnd() + ";";
            return conStr;
        }

        private void btnTestCon_Click(object sender, EventArgs e)
        {
            if (txtDataBaseName.Text.ToLower() == "master")
            {
                MessageBox.Show("不可以使用系统数据库名，请输入一个新的数据库名！");
            }
            else
            {
                string conStr = getInputConStr();
                if (DBHelper.ConnectionTest(conStr))
                {
                    MessageBox.Show("连接成功！");
                }
                else
                {
                    MessageBox.Show("连接失败！");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
