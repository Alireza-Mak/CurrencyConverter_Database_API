using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyConverter_Database_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setCurrentYear();
        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
        }
        public void Convert_Click(object sender, RoutedEventArgs e)
        {
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
        }
        public void OnInputTextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public void Swap_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void setCurrentYear()
        {
            int year = System.DateTime.Now.Year;
            currentYear.Text = year + " ";
        }
    }
}
