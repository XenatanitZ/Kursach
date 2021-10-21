using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SqlClient;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public Registration()
        {
            InitializeComponent();
            Reg.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Login.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Password.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Create.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBoxPassword.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            else
            {
                AddUser();
                NavigationService?.Navigate(new Authorization());
            }
                
            
        }

        private bool AddUser()
        {
            string UpdCommand = "INSERT INTO Users (login, password) VALUES (@log, @pass)";
            string LogCheck = "SELECT Count(*) FROM Users WHERE login = @log";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand LogCheckCommand = new SqlCommand(LogCheck, conn);
                LogCheckCommand.Parameters.AddWithValue("@log", TextBoxLogin.Text);
                int LogCounter = (int)LogCheckCommand.ExecuteScalar();
                if (LogCounter > 0)
                {
                    TextBoxLogin.Text = "";
                    TextBoxLogin.Foreground = Brushes.Red;
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован!");
                    return false;
                }
                else
                {
                    SqlCommand Command = new SqlCommand(UpdCommand, conn);
                    Command.Parameters.AddWithValue("@log", TextBoxLogin.Text);
                    Command.Parameters.AddWithValue("@pass", PasswordBoxPassword.Password);
                    Command.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Authorization());
        }
    }
}
