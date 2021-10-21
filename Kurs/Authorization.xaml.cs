using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SqlClient;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static int ID { get; set; }
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public Authorization()
        {
            InitializeComponent();
            
            Auth.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Login.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Password.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            Enter.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBoxPassword.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            if (LoginCheck() == true && PasswordCheck() == true)
            {
                MessageBox.Show("Пользователь авторизован!");
                IDAdding();
                NavigationService?.Navigate(new MainMenu());
            }


        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Registration());
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool LoginCheck()
        {
            string LogComm = "SELECT count(*) FROM Users WHERE login = @log";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand Command = new SqlCommand(LogComm, conn);
                Command.Parameters.AddWithValue("@log", TextBoxLogin.Text);
                int LC = (int)Command.ExecuteScalar();
                if (LC > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    TextBoxLogin.Text = String.Empty;
                    PasswordBoxPassword.Password = String.Empty;
                    MessageBox.Show("Неверный логин или пароль!");
                }
                conn.Close();
                return false;
            }
        }

        private bool PasswordCheck()
        {
            string PassComm = "SELECT count(*) FROM Users WHERE login = @log AND password = @pass";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand Command = new SqlCommand(PassComm, conn);
                Command.Parameters.AddWithValue("@log", TextBoxLogin.Text);
                Command.Parameters.AddWithValue("@pass", PasswordBoxPassword.Password);
                int PC = (int)Command.ExecuteScalar();
                if (PC > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    TextBoxLogin.Text = String.Empty;
                    PasswordBoxPassword.Password = String.Empty;
                    MessageBox.Show("Неверный логин или пароль!");
                }
                return false;
            }
        }

        public int IDAdding()
        {
            string PassComm = "SELECT * FROM Users WHERE login = @log AND password = @pass";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand Command = new SqlCommand(PassComm, conn);
                Command.Parameters.AddWithValue("@log", TextBoxLogin.Text);
                Command.Parameters.AddWithValue("@pass", PasswordBoxPassword.Password);
                SqlDataReader UsID = Command.ExecuteReader();
                UsID.Read();
                ID = (int)UsID["user_id"];
                UsID.Close();
                conn.Close();
                return ID;              
            }
        }      
    }
}
