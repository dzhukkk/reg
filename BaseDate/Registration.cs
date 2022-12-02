using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Generic;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BaseDate
{
    public partial class Registration : MaterialForm
    {
        public bool IsLogin
        {
            get
            {
                bool been = false;
                string loginUser = textBoxLogin.Text;
                string passwordUser = textBoxPass.Text;

                DataBaseManager _databaseManager = new DataBaseManager();
                DataTable _dataTable = new DataTable();
                SQLiteDataAdapter _myDataAdapter = new SQLiteDataAdapter();
                SQLiteCommand _myCommand = new SQLiteCommand("SELECT * FROM `Users` WHERE `Login` = @UserLogin AND `Pass` = @UserPass", _databaseManager.GetConnection);


                _myCommand.Parameters.Add("@UserLogin", (DbType)SqlDbType.VarChar).Value = loginUser;
                _myCommand.Parameters.Add("@UserPass", (DbType)SqlDbType.VarChar).Value = passwordUser;

                _myDataAdapter.SelectCommand = _myCommand;
                _myDataAdapter.Fill(_dataTable);

                if (_dataTable.Rows.Count > 0)
                {
                    been = true;
                    MessageBox.Show("Такой логин уже есть!\nПопробуйте другой логин!", "Внимание!");
                }
                else
                    been = false;

                return been;
            }
        }

        //проверка пользователя
       public bool IsUser
        {
            get
            {
                bool been = false;

                string loginUser = textBoxLogin.Text;
                string nameUser = textBoxName.Text;
                string surnameUser = textBoxFullName.Text;

                DataBaseManager _databaseManager = new DataBaseManager();
                DataTable _dataTable = new DataTable();
                SQLiteDataAdapter _myDataAdapter = new SQLiteDataAdapter();
                SQLiteCommand _myCommand = new SQLiteCommand("SELECT * FROM Users WHERE Login = @UserLogin AND Name = @UserName AND FullName = @UserFullName", _databaseManager.GetConnection);//выбираем все записи из таблички user где логин = введеному логину и пароль = введеному паролю


                _myCommand.Parameters.AddWithValue("@UserLogin", loginUser);
                _myCommand.Parameters.AddWithValue("@UserName", nameUser);
                _myCommand.Parameters.AddWithValue("@UserFullName", surnameUser);

                _myDataAdapter.SelectCommand = _myCommand;
                _myDataAdapter.Fill(_dataTable);

                
                if (_dataTable.Rows.Count > 0)
                {
                    been = true;
                    if (MessageBox.Show("Такой пользователь уже есть!\nПерейти на вкладку входа?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                      
                        Authorization form = new Authorization();
                        this.Hide();
                        form.Show();
                    }
                }
                else
                    been = false;


                return been;
            }
        }
        public Registration()
        {
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            //прверка на уникальность всех данных
            if (!IsUser)
            {
                //проверка на уникальность логина
                if (!IsLogin)
                {

                    DataBaseManager _databaseManager = new DataBaseManager();
                    SQLiteCommand _SQLiteCommand = new SQLiteCommand("INSERT INTO Users(Login, Pass, Name, FullName)VALUES (@Login,@Pass,@Name,@UserFullName)", _databaseManager.GetConnection);//формируем запрос

                    try
                    {

                        _SQLiteCommand.Parameters.AddWithValue("@Login", textBoxLogin.Text);
                        _SQLiteCommand.Parameters.AddWithValue("@Pass", textBoxPass.Text);
                        _SQLiteCommand.Parameters.AddWithValue("@Name", textBoxName.Text);
                        _SQLiteCommand.Parameters.AddWithValue("@UserFullName", textBoxFullName.Text);


                        _databaseManager.OpenConnection();

                        if (_SQLiteCommand.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Аккаунт создан!", "Внимание!");

                            Congratulations form = new Congratulations();
                            this.Hide();
                            form.Show();

                            //запоним кто вошел
                            User user = new User(textBoxLogin.Text);
                        }
                        else
                            MessageBox.Show("Ошибка создания аккаунта!", "Ошибка!");
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка работы с базой данных!", "Ошибка");
                    }
                    finally
                    {
                        _databaseManager.CloseConnection();//закрываем соеденение
                    }
                }
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Authorization form = new Authorization();
            this.Hide();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFullName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
