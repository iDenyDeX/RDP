using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace RDP
{
    public partial class LoginForm : Form
    {
        string setting_con = "server=127.0.0.1;uid=iDeny;pwd=12345;database=cmdlogin;SslMode=none";
        MySqlConnection conn;
        string myConnMySQL;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            StatusTick.Enabled = true;
            KeyPreview = true;
            //KeyUp += new KeyEventHandler(LoginForm_KeyUp);
            //login_btn.Click += new EventHandler(Login_btn_Click);

        }
        private void LoginForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) login_btn.PerformClick();
        }
        private void Login_btn_Click(object sender, EventArgs e)
        {
            string cmdMySQL = "select * from cmdlogin.login where username = '" + username_txt.Text + "' and password = '" + password_txt.Text + "'";
            MySqlConnection MySQL_Connection1 = new MySqlConnection(setting_con);
            MySqlCommand mySqlCommand = new MySqlCommand(cmdMySQL, MySQL_Connection1);
            MySqlDataAdapter sda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            MySQL_Connection1.Open();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MessageBox.Show("You Login as " + dt.Rows[i][2] + "!");

                    if (dt.Rows[i][2].ToString() == "admin")
                    {
                        RDP frdp = new RDP();
                        frdp.Show();
                        Visible = false;
                    }
                    else if (dt.Rows[i][2].ToString() == "user")
                    {
                        RDP frdp = new RDP();
                        frdp.Show();
                        Visible = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Login error, check your username and password!");
            }
            MySQL_Connection1.Close();
        }

        private void Exit_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoginForm_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Status_Tick(object sender, EventArgs e)
        {
            bool status = false;
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 3306);
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            status2_lbl.Invoke((MethodInvoker)delegate
            {
                if (status)
                {
                    status2_lbl.Text = "Online";
                    status2_lbl.ForeColor = Color.Green;
                    username_txt.Enabled = true;
                    password_txt.Enabled = true;
                    login_btn.Enabled = true;
                }
                else
                {
                    status2_lbl.Text = "Offline";
                    status2_lbl.ForeColor = Color.Red;
                    username_txt.Enabled = false;
                    password_txt.Enabled = false;
                    login_btn.Enabled = false;
                }
            });

        }
    }
}
