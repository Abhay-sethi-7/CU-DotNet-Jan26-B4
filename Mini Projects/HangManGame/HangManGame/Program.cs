

namespace HangManGame
{  
    class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>() { "Phone", "Laptop", "Class", "Notebook", "Cactus" };
            Random rnd = new Random();
            string word = words[rnd.Next(words.Count)].ToUpper();
            char[] progress = new char[word.Length];
            for (int i = 0; i < progress.Length; i++)
            {
                progress[i] = '_';
            }

            List<char> guessed = new List<char>();
            int life = 6;
            Console.WriteLine("Welcome to C# HangMan");
            while (life > 0 && new string(progress) != word) 
            {
                Console.WriteLine($"word: {string.Join(" ", progress)}");
                Console.WriteLine($"Lives Left:{life}");
                Console.WriteLine($"Guessed:{string.Join(", ", guessed)}");
                Console.Write("Guess a letter: ");
                string input = Console.ReadLine().ToUpper();
                if (input.Length <= 0 || !char.IsLetter(input[0]) || !string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a valid letter");
                    continue;
                }
                char guess = input[0];

                if (guessed.Contains(guess))
                {
                    Console.WriteLine($"You already guessed {guess}. Try Again");
                    continue;
                }
                guessed.Add(guess);
                if (word.Contains(guess))
                {
                    Console.WriteLine("good Catch");
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == guess)
                            progress[i] = guess;
                    }
                }
                else
                {
                    Console.WriteLine("Nope! That's not in the word.");
                    life--;
                }
            } 
            if (new string(progress) == word)
                Console.WriteLine("You won! The word was: " + word);
            else
                Console.WriteLine("Game Over! The word was: " + word);
        
        } 
    }
}
