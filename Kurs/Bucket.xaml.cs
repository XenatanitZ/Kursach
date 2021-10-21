using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Bucket.xaml
    /// </summary>
    public partial class Bucket : Page
    {
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public Bucket()
        {
            InitializeComponent();
            BucketCheck();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu());
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            EmptyBucketCheck();
        }

        private void ClearBucketButton(object sender, RoutedEventArgs e)
        {
            ClearBucket();
        }

        private void BucketCheck()
        {
            int id = Authorization.ID;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand Command = new SqlCommand("SELECT dbo.[Order].[user_id], dbo.[Order].[product_id], dbo.product.[name], dbo.product.price_ex_vat FROM dbo.Customers RIGHT OUTER JOIN dbo.[Order] ON dbo.Customers.customer_id = dbo.[Order].customer_id FULL OUTER JOIN dbo.product ON dbo.[Order].product_id = dbo.product.product_id FULL OUTER JOIN dbo.Users ON dbo.[Order].[user_id] = dbo.Users.[user_id] WHERE dbo.Users.[user_id] = @id", conn);

                Command.Parameters.AddWithValue("@id", id);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataTable dataTable = new DataTable("List");
                dataAdapter.Fill(dataTable);
                DG.ItemsSource = dataTable.DefaultView;
            }
        }

        private bool ClearBucket()
        {
            int id = Authorization.ID;
          
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand Command = new SqlCommand("DELETE FROM [Order] WHERE [user_id] = @id", conn);
                Command.Parameters.AddWithValue("@id", id);
                Command.ExecuteNonQuery();
                MessageBox.Show("Корзина успешно очищена!");
                NavigationService?.Navigate(new Bucket());
                return true;
                
            }
        }

        private bool EmptyBucketCheck()
        {
            int id = Authorization.ID;
            string IdCheck = "SELECT Count(*) FROM [Order] WHERE user_id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand IdCheckCommand = new SqlCommand(IdCheck, conn);
                IdCheckCommand.Parameters.AddWithValue("@id", id);
                int IdCounter = (int)IdCheckCommand.ExecuteScalar();
                if (IdCounter > 0)
                {
                    NavigationService?.Navigate(new Payment());
                    return true;

                }

                else
                {
                    MessageBox.Show("Сначала добавьте товар в корзину!");
                    return false;
                }
            }
        }
    }
}
