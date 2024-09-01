using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TicTacToe
{
    public partial class ttt_Window : Window
    {
        private int _round_number = 0;
        private int _player_x = 1;
        private int _player_o = 0;
        private string[,] board = new string[3, 3];
        
        public ttt_Window()
        {
            InitializeComponent();
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {  
            var button = sender as Button;

            
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            
            if (board[row, col] == null)
            {
                string currentPlayer;
                if (_round_number % 2 == _player_x)
                {
                    currentPlayer = "X";
                    board[row, col] = "X";
                    button.Content = "X";
                }
                else
                {
                    currentPlayer = "O";
                    board[row, col] = "O";
                    button.Content = "O";
                }

                _round_number++;

                if (CheckWin(currentPlayer))
                {
                    StatusTextBlock.Text = $"Player {currentPlayer} wins!";
                }
            }
        }

        private void OnResetGameClick(object sender, RoutedEventArgs e)
        {
            ResetGame();  
        }

        private bool CheckTie()
        {
            foreach (var cell in board)
            {
                if (cell == null)
                {
                    return false; 
                }
            }
            return true;
        }

        private bool CheckWin(string player)
        {
            // Horizontale Checks
            if ((board[0, 0] == player && board[0, 1] == player && board[0, 2] == player) ||  // Erste Reihe
                (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player) ||  // Zweite Reihe
                (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player))    // Dritte Reihe
            {
                return true;
            }
        
            // Vertikale Checks
            if ((board[0, 0] == player && board[1, 0] == player && board[2, 0] == player) ||  // Erste Spalte
                (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player) ||  // Zweite Spalte
                (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player))    // Dritte Spalte
            {
                return true;
            }
        
            // Diagonale Checks
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||  // Hauptdiagonale
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))    // Nebendiagonale
            {
                return true;
            }
        
            return false;
        }
        private void ResetGame()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = null; 
                }
            }

            foreach (var control in this.Find<Grid>("MainGrid").Children)
            {
                if (control is Button button)
                {
                    button.Content = null; 
                }
            }

            _round_number = 0;
            StatusTextBlock.Text = string.Empty; 
        }



    }
}
