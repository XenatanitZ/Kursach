using System;
using System.Windows;
using System.Windows.Controls;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {
        public Payment()
        {
            InitializeComponent();
        }

        public void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CategoryChoosing());
        }

        public void AcceptButton(object sender, RoutedEventArgs e)
        {
            AcceptFunc();
        }

        private bool AcceptFunc()
        {
            string cardNumber = CardNumber.Text;
            string firstNumber = FirstNumber.Text;
            string secondNumber = SecondNumber.Text;
            string cvv = CVV.Text;

            int res_CardNumber;
            int res_FirstNumber;
            int res_SecondNumber;
            int res_CVV;
            
            bool isInt_CardNumber = Int32.TryParse(cardNumber, out res_CardNumber);
            bool isInt_FirstNumber = Int32.TryParse(firstNumber, out res_FirstNumber);
            bool isInt_SecondNumber = Int32.TryParse(secondNumber, out res_SecondNumber);
            bool isInt_CVV = Int32.TryParse(cvv, out res_CVV);
            

            if (isInt_FirstNumber == false)
            {
                FirstNumber.Text = "";
                MessageBox.Show("В поле 'Месяц' введено не число!");
                return false;
            }

            if (isInt_SecondNumber == false)
            {
                SecondNumber.Text = "";
                MessageBox.Show("В поле 'Год' введено не число!");
                return false;
            }

            if (res_FirstNumber > 12)
            {
                FirstNumber.Text = "";
                MessageBox.Show("Значение 'Месяц' не может превышать 12!");
                return false;
            }

            if (isInt_CVV == false)
            {
                CVV.Text = "";
                MessageBox.Show("В поле 'CVV' введено не число!");
                return false;
            }

            else
            {
                Random rnd = new Random();
                int value = rnd.Next(100000000, 999999999);
                MessageBox.Show("Оплата произведена, запишите ваш номер заказа");
                MessageBox.Show(value.ToString());
                NavigationService?.Navigate(new MainMenu());
                return true;
            }

        }
        
    }
}
