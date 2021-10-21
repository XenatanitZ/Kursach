using System.Windows;
using System.Windows.Controls;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для CategoryChoosing.xaml
    /// </summary>
    public partial class CategoryChoosing : Page
    {
        public CategoryChoosing()
        {
            InitializeComponent();
        }

        private void GraphicsCardsButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Components(1));
        }

        private void ProcessorsButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Components(2));
        }

        private void MotherboardsButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Components(3));
        }

        private void RAMButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Components(4));
        }

        private void PowerSuppliesButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Components(5));
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu());
        }

    }
}
