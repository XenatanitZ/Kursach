using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public int user_id;
        public MainMenu()
        {
            InitializeComponent();
         
            MainMenuTitle.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            CustomersButton.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
            ComponentsButton.FontFamily = new FontFamily("Exo_2.0_Semi_Bold_Italic");
        }

        private void CustomersOpen(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate (new Customers());
        }

        private void ComponentsOpen(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CategoryChoosing());
        }

        private void BucketOpen(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Bucket());
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ComponentsAdding(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddComponentMenu());
        }

        private void SearchOpen(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Search());
        }

    }
}
