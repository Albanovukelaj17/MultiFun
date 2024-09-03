using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using Avalonia.Threading;

namespace HangMan
{
    public partial class hangman_Window : Window
    {
private string[] words = 
{
    "apple", "banana", "cherry", "date", "fig", "grape", "kiwi", "lemon", "lime", "mango", 
    "orange", "peach", "pear", "plum", "berry", "melon", "grapefruit", "blueberry", "strawberry", 
    "raspberry", "blackberry", "papaya", "pineapple", "coconut", "apricot", "nectarine", "pomegranate", 
    "watermelon", "cantaloupe", "honeydew", "passionfruit", "dragonfruit", "lychee", "persimmon", 
    "tangerine", "mandarin", "clementine", "kumquat", "guava", "jackfruit", "durian", "rambutan", 
    "plantain", "quince", "starfruit", "olive", "avocado", "tomato", "cucumber", "pepper", 
    "zucchini", "pumpkin", "squash", "eggplant", "carrot", "broccoli", "cauliflower", "spinach", 
    "lettuce", "kale", "arugula", "cabbage", "brusselsprout", "collard", "chard", "beet", "turnip", 
    "radish", "parsnip", "rutabaga", "potato", "sweetpotato", "yam", "onion", "garlic", "shallot", 
    "leek", "scallion", "celery", "fennel", "parsley", "cilantro", "basil", "thyme", "rosemary", 
    "sage", "oregano", "dill", "mint", "chive", "ginger", "turmeric", "cinnamon", "nutmeg", 
    "clove", "allspice", "paprika", "cayenne", "peppercorn", "mustard", "wasabi", "horseradish", 
    "cardamom", "saffron", "vanilla", "chocolate", "coffee", "tea", "sugar", "honey", "maple", 
    "molasses", "caramel", "butterscotch", "peanut", "almond", "walnut", "pecan", "hazelnut", 
    "cashew", "pistachio", "macadamia", "chestnut", "brazilnut", "pine", "acorn", "sunflower", 
    "sesame", "pumpkinseed", "flaxseed", "chia", "quinoa", "rice", "wheat", "oat", "barley", 
    "corn", "rye", "millet", "sorghum", "buckwheat", "amaranth", "spelt", "farro", "kamut", 
    "bulgur", "couscous", "polenta", "grits", "pasta", "noodle", "bread", "biscuit", "cracker", 
    "pretzel", "bagel", "muffin", "cupcake", "cake", "brownie", "cookie", "pie", "tart", 
    "pudding", "custard", "jelly", "jam", "preserve", "butter", "cheese", "yogurt", "cream", 
    "milk", "icecream", "sorbet", "gelato", "whiskey", "beer", "wine", "vodka", "rum", 
    "gin", "tequila", "bourbon", "brandy", "cognac", "liqueur", "champagne", "sherry", 
    "port", "cider", "sake", "vermouth", "absinthe", "bitters", "tonic", "soda", 
    "cola", "lemonade", "juice", "smoothie", "shake", "water", "sparklingwater"
};
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
            lives = 7;
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
                
                System.Threading.Tasks.Task.Delay(2000).ContinueWith(_ =>
                {
                    //standartmässig delay um restart/reset..
                    Dispatcher.UIThread.Post(() =>
                    {
                        StartNewGame();
                    });
                });
            }
            
        }
    }
}
// begrenzte anzahl von wörter, arrray hier 200 circa
// possible : generator gegeben dictionary ?