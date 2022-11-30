﻿namespace CleanCodeLaboration;

public class MooGame
{
    public void Run()
    {
        bool playOn = true;
        Console.WriteLine("Enter your user name:\n");
        string name = Console.ReadLine();

        while (playOn)
        {
            string goal = GenerateTargetDigits();

            Console.WriteLine("New game:\n");
            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + goal + "\n");
            string guess = Console.ReadLine();

            int nGuess = 1;
            string bbcc = checkBC(goal, guess);
            Console.WriteLine(bbcc + "\n");
            while (bbcc != "BBBB,")
            {
                nGuess++;
                guess = Console.ReadLine();
                Console.WriteLine(guess + "\n");
                bbcc = checkBC(goal, guess);
                Console.WriteLine(bbcc + "\n");
            }
            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + nGuess);
            output.Close();
            showTopList();
            Console.WriteLine("Correct, it took " + nGuess + " guesses\nContinue?");
            string answer = Console.ReadLine();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                playOn = false;
            }
        }
    }

    public static string GenerateTargetDigits()
    {
        var random = new Random();
        string output = "";
        while (output.Length < 4)
        {
            string newDigit = random.Next(10).ToString();
            if (!output.Contains(newDigit))
                output += newDigit;
        }
        return output;
    }

    public static string checkBC(string goal, string guess)
    {
        int cows = 0, bulls = 0;
        guess += "    ";     // if player entered less than 4 chars
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (goal[i] == guess[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }

    public static void showTopList()
    {
        StreamReader input = new StreamReader("result.txt");
        List<PlayerData> results = new List<PlayerData>();
        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            PlayerData pd = new PlayerData(name, guesses);
            int pos = results.IndexOf(pd);
            if (pos < 0)
            {
                results.Add(pd);
            }
            else
            {
                results[pos].Update(guesses);
            }
        }
        results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        Console.WriteLine("Player   games average");
        foreach (PlayerData p in results)
        {
            Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
        }
        input.Close();
    }
}
