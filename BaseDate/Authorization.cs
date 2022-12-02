using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using MaterialSkin.Controls;

namespace BaseDate
{
    public partial class Authorization : MaterialForm
    {
        public bool IsLogin
        {
            get
            {
                bool been = false;
                string loginUser = textBoxLogin1.Text;
                string passwordUser = textBoxPass.Text;

                DataBaseManager _databaseManager = new DataBaseManager();
                DataTable _dataTable = new DataTable();
                SQLiteDataAdapter _SQLiteDataAdapter = new SQLiteDataAdapter();
                SQLiteCommand _SQLiteCommand = new SQLiteCommand("SELECT * FROM `users` WHERE `Login` = @UserLogin", _databaseManager.GetConnection);

                _SQLiteCommand.Parameters.Add("@UserLogin", (DbType)SqlDbType.VarChar).Value = loginUser;

                _SQLiteDataAdapter.SelectCommand = _SQLiteCommand;
                _SQLiteDataAdapter.Fill(_dataTable);

                if (_dataTable.Rows.Count > 0)
                {
                    been = true;
                }
                else
                    been = false;

                return been;
            }
        }
        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            
            string loginUser = textBoxLogin1.Text;
            string passwordUser = textBoxPass.Text;

            DataBaseManager _databaseManager = new DataBaseManager();
            DataTable _dataTable = new DataTable();
            SQLiteDataAdapter _SQLiteDataAdapter = new SQLiteDataAdapter();
            SQLiteCommand _SQLiteCommand = new SQLiteCommand(
                "SELECT * FROM `users` WHERE `Login` = @UserLogin AND `Pass` = @UserPass", _databaseManager.GetConnection);

            try
            {
                _SQLiteCommand.Parameters.Add("@UserLogin", (DbType)SqlDbType.VarChar).Value = loginUser;
                _SQLiteCommand.Parameters.Add("@UserPass", (DbType)SqlDbType.VarChar).Value = passwordUser;

                _databaseManager.OpenConnection();

                _SQLiteDataAdapter.SelectCommand = _SQLiteCommand;
                _SQLiteDataAdapter.Fill(_dataTable);
                if (_dataTable.Rows.Count > 0)
                {
                    Congratulations form = new Congratulations();
                    this.Hide();
                    form.Show();


                    User user = new User(loginUser);
                }
                else
                {
                    if (IsLogin)
                    {
                        MessageBox.Show("Неправильный пароль.");
                    }
                    else
                    {
                        if (MessageBox.Show("Необходимо зарегистрироваться.\nЗарегистрироваться сейчас?",
                            "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Registration form = new Registration();
                            this.Hide();
                            form.Show();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка БД!", "Ошибка");
            }
            finally
            {
                _databaseManager.CloseConnection();
            }
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
