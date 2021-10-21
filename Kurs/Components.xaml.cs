using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Components.xaml
    /// </summary>
    public partial class Components : Page
    {  
        private string connectionString = "Server=DESKTOP-GCODLLE\\SQLEXPRESS;DataBase=Kursach;Integrated Security=True";
        public int comp;
        public Components(int CatNumber)
        {
            InitializeComponent();
            comp = CatNumber;
            ComponentsLoading();
        }

        public void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CategoryChoosing());
        }

        private void BucketAdding(object sender, RoutedEventArgs e)
        {
            AddInBucket();
        }

        public void ComponentsLoading()
        {
            switch(comp)
            {
                case 1:
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE category = 'Видеокарты'", conn);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                        DataTable dataTable = new DataTable("List");
                        dataAdapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                    break;
                case 2:
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE category = 'Процессоры'", conn);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                        DataTable dataTable = new DataTable("List");
                        dataAdapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                    break;
                case 3:
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE category = 'Материнские платы'", conn);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                        DataTable dataTable = new DataTable("List");
                        dataAdapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                    break;
                case 4:
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE category = 'Оперативная память'", conn);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                        DataTable dataTable = new DataTable("List");
                        dataAdapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                    break;
                case 5:
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand Command = new SqlCommand("SELECT product_id, category, company, name, price_ex_vat FROM dbo.product WHERE category = 'Блоки питания'", conn);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                        DataTable dataTable = new DataTable("List");
                        dataAdapter.Fill(dataTable);
                        DG.ItemsSource = dataTable.DefaultView;
                    }
                    break;
            }
        }

        public void AddInBucket()
        {          
            int id = Authorization.ID;
            string UpdCommand = "INSERT INTO [Order] (product_id, user_id) VALUES (@prod, @us)";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand Command = new SqlCommand(UpdCommand, conn);
                
                Command.Parameters.AddWithValue("@prod", int.Parse(IDchoosing.Text));
                
                Command.Parameters.AddWithValue("@us", id);
                Command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Товар добавлен в корзину!");
                NavigationService?.Navigate(new Bucket());               
            }
        }

    }
}
