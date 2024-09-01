using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using Avalonia.Threading;
using System; 

namespace TicTacToe
{
    public partial class ttt_Window : Window
    {
        private int _round_number = 0;
        private string[,] board = new string[3, 3];
        private bool gameWon = false;
        private GameStats gameStats = new GameStats();

        public ttt_Window()
        {
            InitializeComponent();
            gameStats.Load(); // load saved stats
            gameStats.Display(); 
            //if minmax  always draw,if random dumm , cpu AI ?
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {  
            if (gameWon) return;

            var button = sender as Button;
            if (button == null) return;

            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == null)
            {
                string currentPlayer;
                if (_round_number % 2 == 0)
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
                    gameWon = true;

                    if (currentPlayer == "X")
                    {
                        gameStats.Wins++;
                    }
                    else
                    {
                        gameStats.Losses++;
                    }

                    Task.Delay(2000).ContinueWith(_ => Dispatcher.UIThread.InvokeAsync(() => ResetGame()));
                }
                else if (CheckTie())
                {
                    StatusTextBlock.Text = "It's a tie!";
                    gameStats.Ties++;
                    Task.Delay(2000).ContinueWith(_ => Dispatcher.UIThread.InvokeAsync(() => ResetGame()));
                }

                gameStats.Save();  // save game , so that new game same stats, if not  0 0 0
            }
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
            if ((board[0, 0] == player && board[0, 1] == player && board[0, 2] == player) ||
                (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player) ||
                (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player))
            {
                return true;
            }
        
            // Vertikale Checks
            if ((board[0, 0] == player && board[1, 0] == player && board[2, 0] == player) ||
                (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player) ||
                (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player))
            {
                return true;
            }
        
            // Diagonale Checks
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            {
                return true;
            }
        
            return false;
        }
        private void OnResetGameClick(object sender, RoutedEventArgs e)
        {
            ResetGame();  
        }


        private void UpdateGameStatsDisplay()
        {
            WinsTextBlock.Text = $"Wins: {gameStats.Wins}";
            LossesTextBlock.Text = $"Losses: {gameStats.Losses}";
            TiesTextBlock.Text = $"Ties: {gameStats.Ties}";
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

            var grid = this.FindControl<Grid>("MainGrid");
            if (grid != null)
            {
                foreach (var control in grid.Children)
                {
                    if (control is Button button)
                    {
                        button.Content = null;
                    }
                }
            }

            _round_number = 0;
            gameWon = false;

            if (StatusTextBlock != null)
            {
                StatusTextBlock.Text = string.Empty;
            }

            UpdateGameStatsDisplay();  
        }

    }
}
