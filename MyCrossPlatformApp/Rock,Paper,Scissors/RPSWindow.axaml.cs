using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace MyCrossPlatformApp.RockPaperScissors
{
    public partial class RPSWindow : Window
    {
        private Random _random = new Random();
        private int _wins = 0;
        private int _losses = 0;

        public RPSWindow()
        {
            InitializeComponent();
        }
        //step 2 : load images for rock,paper,scissors?
        

        private void OnRockClick(object sender, RoutedEventArgs e)
        {
            PlayGame("Rock");
        }

        private void OnPaperClick(object sender, RoutedEventArgs e)
        {
            PlayGame("Paper");
        }

        private void OnScissorsClick(object sender, RoutedEventArgs e)
        {
            PlayGame("Scissors");
        }

        private void PlayGame(string userChoice)
        {
            string[] choices = { "Rock", "Paper", "Scissors" };
            string computerChoice = choices[_random.Next(choices.Length)];

            string result;

            if (userChoice == computerChoice)
            {
                result = "It's a tie!";
            }
            else if ((userChoice == "Rock" && computerChoice == "Scissors") ||
                     (userChoice == "Paper" && computerChoice == "Rock") ||
                     (userChoice == "Scissors" && computerChoice == "Paper"))
            {
                result = "You win!";
                _wins++;
               
            }
            else
            {
                result = "You lose!";
                _losses++;
            }

            ResultTextBlock.Text = $"You chose {userChoice}, Computer chose {computerChoice}. {result}";
            UpdateScoreboard();
        }
        private void UpdateScoreboard()
        {
            WinsTextBlock.Text = $"Wins: {_wins}";
            LossesTextBlock.Text = $"Losses: {_losses}";
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            _wins = 0;
            _losses = 0;
            UpdateScoreboard(); 
        }
    }
}