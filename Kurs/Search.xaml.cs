using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public Search()
        {
            InitializeComponent();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu());
        }

        private void SearchButton(object sender, RoutedEventArgs e)
        {
            SearchProduct();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            DeleteProduct();
        }

        private bool SearchProduct()
        {
            string NameCheck = "SELECT name FROM product WHERE name = @name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand NameCheckCommand = new SqlCommand(NameCheck, conn);
                NameCheckCommand.Parameters.AddWithValue("@name", NameText.Text);
                if (NameCheck == null)
                {
                    NameText.Text = "";
                    MessageBox.Show("Продукта с таким названием не существует!");                                     
                    return false;
                }

                else
                {
                    SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE name = @name", conn);
                    Command.Parameters.AddWithValue("@name", NameText.Text);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                    DataTable dataTable = new DataTable("List");
                    dataAdapter.Fill(dataTable);
                    DG.ItemsSource = dataTable.DefaultView;
                    return true;                   
                }
            }
        }

        private bool DeleteProduct()
        {
            string NameCheck = "SELECT [name] FROM product WHERE [name] = @name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand NameCheckCommand = new SqlCommand(NameCheck, conn);
                NameCheckCommand.Parameters.AddWithValue("@name", NameText.Text);
                if (NameCheck == null)
                {
                    NameText.Text = "";
                    MessageBox.Show("Продукта с таким названием не существует!");
                    return false;
                }

                else
                {
                    SqlCommand Command = new SqlCommand("DELETE FROM product WHERE [name] = @name", conn);
                    Command.Parameters.AddWithValue("@name", NameText.Text);
                    Command.ExecuteNonQuery();
                    MessageBox.Show("Товар с данным названием удален!");
                    NavigationService?.Navigate(new Search());
                    return true;
                }
            }
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
