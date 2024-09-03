using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;

namespace HangMan
{
    public partial class hangman_Window : Window
    {
        private string[] words = { "apple", "banana", "cherry", "date", "fig", "grape" };
        private string chosenWord;
        private char[] guessedWord;
        private int lives;
        private List<char> incorrectGuesses;

        public hangman_Window()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            Random random = new Random();
            chosenWord = words[random.Next(words.Length)];
            guessedWord = new string('_', chosenWord.Length).ToCharArray();
            lives = 6;
            incorrectGuesses = new List<char>();

            UpdateUI();
        }

        private void OnGuessButtonClick(object sender, RoutedEventArgs e)
        {
            string input = GuessTextBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(input) || input.Length != 1)
            {
                MessageTextBlock.Text = "Please enter a single letter.";
                return;
            }

            char guess = input[0];
            GuessTextBox.Text = string.Empty;

            if (chosenWord.Contains(guess))
            {
                for (int i = 0; i < chosenWord.Length; i++)
                {
                    if (chosenWord[i] == guess)
                    {
                        guessedWord[i] = guess;
                    }
                }
            }
            else
            {
                if (!incorrectGuesses.Contains(guess))
                {
                    incorrectGuesses.Add(guess);
                    lives--;
                }
            }

            UpdateUI();
            CheckGameOver();
        }

        private void UpdateUI()
        {
            WordTextBlock.Text = new string(guessedWord);
            LivesTextBlock.Text = $"Lives: {lives}";
            IncorrectGuessesTextBlock.Text = $"Incorrect Guesses: {string.Join(", ", incorrectGuesses)}";
            MessageTextBlock.Text = string.Empty;
        }

        private void CheckGameOver()
        {
            if (new string(guessedWord) == chosenWord)
            {
                MessageTextBlock.Text = $"Congratulations! You guessed the word \"{chosenWord}\".";
            }
            else if (lives <= 0)
            {
                MessageTextBlock.Text = $"Game Over! The word was \"{chosenWord}\".";
            }
        }
    }
}
