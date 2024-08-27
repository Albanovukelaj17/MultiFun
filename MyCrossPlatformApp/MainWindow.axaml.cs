using Avalonia.Controls;
using Avalonia.Interactivity;
using MyCrossPlatformApp.Calculator;  // Korrekte Referenzierung des Namespaces

namespace MyCrossPlatformApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculator_Click(object sender, RoutedEventArgs e)
        {
            var calculatorWindow = new CalculatorWindow();
            calculatorWindow.Show();
        }
    }
}