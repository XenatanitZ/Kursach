using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для AddComponentMenu.xaml
    /// </summary>
    public partial class AddComponentMenu : Page
    {
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public AddComponentMenu()
        {
            InitializeComponent();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu());
        }

        private void AddComponentButton(object sender, RoutedEventArgs e)
        {
            AddComponent();

        }

        private bool AddComponent()
        {
            string UpdCommand = "INSERT INTO product (category, company, name, description, price_ex_vat) VALUES (@cat, @comp, @name, @desc, @price)";
            string NameCheck = "SELECT Count(*) FROM product WHERE name = @name";
            string Price = PriceText.Text;
            int res;
            bool isInt = Int32.TryParse(Price, out res);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand NameCheckCommand = new SqlCommand(NameCheck, conn);
                NameCheckCommand.Parameters.AddWithValue("@name", NameText.Text);
                int NameCounter = (int)NameCheckCommand.ExecuteScalar();
                if (NameCounter > 0)
                {
                    NameText.Text = "";
                    MessageBox.Show("Товар с таким названием уже существует!");
                    return false;
                }
                
                
                if (isInt == false)
                {
                    PriceText.Text = "";
                    MessageBox.Show("В поле 'Цена' введено не число!");
                    return false;
                }

                else
                {
                    SqlCommand Command = new SqlCommand(UpdCommand, conn);
                    Command.Parameters.AddWithValue("@cat", ComboBox.Text);
                    Command.Parameters.AddWithValue("@comp", CompanyText.Text);
                    Command.Parameters.AddWithValue("@name", NameText.Text);
                    Command.Parameters.AddWithValue("@desc", DescriptionText.Text);
                    Command.Parameters.AddWithValue("@price", PriceText.Text);
                    Command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Товар успешно добавлен!");
                    NavigationService?.Navigate(new MainMenu());
                    return true;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
