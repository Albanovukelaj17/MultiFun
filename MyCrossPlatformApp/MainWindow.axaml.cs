using Avalonia.Controls;
using Avalonia.Interactivity;
using MyCrossPlatformApp.Calculator; 

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
        private void RPSGame_Click(object sender, RoutedEventArgs e)
        {
            var rpsWindow = new RockPaperScissors.RPSWindow();
            rpsWindow.Show();
        }
        private void TicTacToeGame_Click(object sender, RoutedEventArgs e)
        {
            var ttt_Window = new TicTacToe.ttt_Window();
            ttt_Window.Show();
        }
        
        private void HangManGame_Click(object sender, RoutedEventArgs e)
        {
            var hangman_Window = new HangMan.hangman_Window();
            hangman_Window.Show();
        }
   } 
}