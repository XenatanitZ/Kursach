using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public Customers()
        {
            InitializeComponent();
            CustomersLoading();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu());
        }
        public void CustomersLoading()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand Command = new SqlCommand("SELECT first_name, second_name, middle_name, mobile_number, email FROM dbo.Customers", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataTable dataTable = new DataTable("List");
                dataAdapter.Fill(dataTable);
                DG.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
