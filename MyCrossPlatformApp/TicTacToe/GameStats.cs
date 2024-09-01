using System;
using System.IO;


namespace TicTacToe
{
    public class GameStats
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        private readonly string filePath = "gamestats.txt";

        public void Load()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                Wins = int.Parse(lines[0]);
                Losses = int.Parse(lines[1]);
                Ties = int.Parse(lines[2]);
            }
        }

        public void Save()
        {
            string[] lines = {
                Wins.ToString(),
                Losses.ToString(),
                Ties.ToString()
            };
            File.WriteAllLines(filePath, lines);
        }

        public void Display()
        {
            Console.WriteLine($"Wins: {Wins}, Losses: {Losses}, Ties: {Ties}");
        }
    }
}