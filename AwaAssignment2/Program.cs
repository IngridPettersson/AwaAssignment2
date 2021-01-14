using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace awa_assignment_2
{
    class Program
    {

        // Why do I need to declare the fields as static? I use them in static methods and that's what VS complains about but I can't remove static from the 
        // methods either it seems. Due to the documentation they shouldn't need to be static...?
        static string ingredient;
        static double price;
        static double maxValue;
        static double minValue;
        static string keyOfMaxValue;
        static string keyOfMinValue;
        static string santaString;
        static double remainder;
        static double totalPrice;
        static int counter;
        static string surpriseInstructions;
        static string[] surpriseIngredients;
        static char typedLetter;
        static string[] surpriseSpices;
        static string randomizedSpice;
        static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        static List<string> surpriseSalad = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                WelcomeUser();
                AskForIngredient();
                CalculatePrices();
                Console.WriteLine($"The remainder for Santa to pay is: SEK {remainder}:-. {santaString}");
                ShowPrices();
                CreateSurpriseSalad();
                PresentGuestGrades();
                SayGoodbye();
            }
            catch
            {
                Console.WriteLine("Oops, something went wrong... Seems like you're left on your own to handle your fruit salad... :/");
            }
        }

        static void WelcomeUser()
        {
            Console.WriteLine("Hello and welcome to this fruit salad party!".ToUpper());
            Console.WriteLine("\nOh, but the fruit salad isn't ready yet... in fact we didn't even decide " +
                "which ingredients we want - let's fix that!\n\nAND TO PREPARE YOUR MINDSET, there will be some extras here and it wont make sense " +
                "if you don't read all the \"bla bla bla\", so my recommendation is that you do just that :) Oh, and it will take some time (aka if you're out " +
                "of Caffeine - please refill)!" +
                "\n\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void AskForIngredient()
        {
            Console.WriteLine("Let's begin!\n\nWe expect a total of 4 ingredients in your fruit salad.\n");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enter an ingredient for your fruit salad: ");
                //Kan man använda Console.ReadKey här i stället?
                ingredient = Console.ReadLine();
                if (ingredient.Length < 2)
                {
                    i--;
                    Console.WriteLine("Are you really making a proper fruit salad? At least two characters are needed to be a fruit around here...");
                    continue;
                }
                Console.Write("Enter price for \"one unit\" of the ingredient (SEK): ");

                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                    continue;
                }
                try
                {

                ingredients.Add(ingredient, price);
                }
                catch
                {
                    i--;
                    Console.WriteLine("The same ingredient twice - really? Some more imagination please!");
                    continue;
                }
            }
            Console.WriteLine();
        }

        static void CalculatePrices()
        {
            Console.Clear();

                maxValue = ingredients.Values.Max();
                keyOfMaxValue = ingredients.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                minValue = ingredients.Values.Min();
                keyOfMinValue = ingredients.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

            foreach (KeyValuePair<string, double> item in ingredients)
            {
                totalPrice += item.Value;
            }

            CalculateRemainder();
        }

        static (double, string) CalculateRemainder()
        {

            Console.WriteLine("It turns out that 2 of your friends will share the cost with you and Santa will pay the remainder - how random!\n");
            remainder = totalPrice % 3;

            if (remainder < 1)
                santaString = "\"- That was a good deal for me! I could be a nice guy and still lose nothing :)\" /Santa";
            else
                santaString = "\"- What?? I never gave my consent to this! I'm afraid you should consider this as your pre-given " +
                    "2021 Christmas gift...\" /Santa";

            // Alternativt syntax med samma resultat
            //santaString = (remainder < 1) ? "\"- That was a good deal for me! I could be a nice guy and still lose nothing :)\" /Santa" : "\"- What?? I never gave my consent to this! I'm afraid you should consider this as your pre-given " +
            //        "2021 Christmas gift...\" /Santa";

            return (remainder, santaString);
        }

        static void ShowPrices()
        {
            ShowTotalPrice();
            ShowMostExpensive();
            ShowCheapest();
        }
       
        static void ShowTotalPrice()
        {
            Console.WriteLine("\nValuable facts about your fruit salad:".ToUpper());
            Console.WriteLine();
            Console.WriteLine($"Total price: SEK {totalPrice}:-");
            Console.WriteLine();
        }
        static void ShowMostExpensive()
        {
            Console.WriteLine($"Most expensive ingredient: {keyOfMaxValue}\nPrice: SEK {maxValue}:-");
            Console.WriteLine();
        }
        static void ShowCheapest()
        {
            Console.WriteLine($"Cheapest ingredient: {keyOfMinValue}\nPrice: SEK {minValue}:-");
            Console.WriteLine();
            Console.WriteLine("Bon appéttit!\n\nOh, but wait...".ToUpper());
            Console.WriteLine("...something peculiar just occured!\n\nPress Enter to find out more...");
            Console.ReadLine();
        }

        // Add His Majesty Soosh to Guest list??
        static void CreateSurpriseSalad()
        {
            MakeGameFlow();
            FetchData();
            GetUserInput();
            MakeGameFlow2();
            GetRandomSpice();
            PresentSurpriseSalad();
        }

        static void MakeGameFlow()
        {
            Console.Clear();
            Console.WriteLine("When you sent this fruit salad recipe to your fruit salad teacher he identified some areas of improvement. " +
                "His biggest concern was that it seemed a bit too..." +
                " conventional. Your guests' taste buds might get bored, and we can't take that risk!\n\nLet's create a small but OH SO " +
                "DELICIOUS surprise salad instead, with fresh ingredients from all over the world!\n\nPress Enter to continue...\n");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Oh oh oh!\n\nSomething of highest importance just crossed my mind! We don't know who your honored " +
                "guests are(?)\n\nLet's find out by pressing Enter!");
            Console.ReadLine();
            Console.Clear();

            PresentGuestList();

            Console.WriteLine("Press Enter to start creating your surprise salad!".ToUpper());
            Console.ReadLine();
            Console.Clear();
        }

        static void FetchData()
        {
            try
            {
                surpriseIngredients = File.ReadAllLines(@"C:\Users\ingri\OneDrive\Desktop\surpriseIngredients.txt");
            }
            catch (FileNotFoundException)
            {
                surpriseIngredients = new string[] { "Chayote", "Kiwano", "Dragon fruit", "Feijoa",
                    "Tamarillo", "Loquat", "Jabuticaba", "Monstera deliciosa", "Rambutan", "Pacay", "Starfruit", "Atemoya",
                "Buddha's hand", "Guanabana", "Noni fruit" };
            }

            surpriseSpices = new string[] { "vata churna", "dill",
                "thyme", "catnip", "fenugreek", "garlic", "kawakawa", "savory" };
        }

        static void GetUserInput()
        {
            Console.WriteLine($"Letters are fruit! OBS! Some letters wont give you any " +
                            $"fruit (such is life I guess)... " +
                            $"In that case, don't lose hope - be persistent and keep typing!\n");
            do
            {
                switch (surpriseSalad.Count())
                {
                    case 0:
                        counter = 1;
                        surpriseInstructions = $"Type any letter to get ingredient number {counter}: ";
                        break;
                    case 1:
                        counter = 2;
                        surpriseInstructions = $"Type another letter to get ingredient number {counter}: ";
                        break;
                    case 2:
                        counter = 3;
                        surpriseInstructions = $"Type another letter to get ingredient number {counter}: ";
                        break;
                    default:
                        break;
                }
                try
                {
                    Console.Write($"{surpriseInstructions}");
                    typedLetter = char.Parse(Console.ReadLine().ToUpper());
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                foreach (string fruit in surpriseIngredients)
                {
                    if (fruit.StartsWith(typedLetter))
                    {
                        Console.WriteLine($"\n\"{typedLetter}\" gives ju {fruit.ToUpper()}");
                        surpriseSalad.Add(fruit);
                        Console.WriteLine();
                    }
                }

            } while (surpriseSalad.Count < 3); ;
        }

        static void MakeGameFlow2()
        {
            Console.WriteLine("You've got all your fruits! Press Enter to continue...".ToUpper());
            Console.ReadLine();
            Console.Clear();
        }

        static void GetRandomSpice()
        {
            Console.WriteLine("And now we'll add a generous amount of a random spice to your surprise salad!".ToUpper());
            Console.WriteLine("\nPress Enter to find out which one...");
            Console.ReadLine();
            Console.Clear();

            Random random = new Random();

            randomizedSpice = surpriseSpices[random.Next(0, surpriseSpices.Length)];
            Console.WriteLine($"Your spice of choice is: {randomizedSpice.ToUpper()}\n");
        }

        static void PresentSurpriseSalad()
        {
            Console.WriteLine("Voilà! Let me introduce you to your surprise salad! And you know what - as the ingredients need to be exclusively" +
            " shipped from all over the world especially for you, the price will be the biggest surprise of all! :)\n");
            Console.WriteLine("Your surprise salad is a blend made to perfection out of the following ingredients:\n".ToUpper());

            foreach (string fruit in surpriseSalad)
            {
                Console.WriteLine(fruit.ToUpper());
            }

            Console.WriteLine(randomizedSpice.ToUpper());
            Console.WriteLine();
            Console.WriteLine("SURPRISE PRICE: SEK 3 275 483:-\n");
        }

        private static void PresentGuestList()
        {
            Console.WriteLine($"Guest list:\n".ToUpper());

            Guest guest3 = new Guest("Her Majesty Doctora", 10);
            Guest guest1 = new Guest("Hercule Poirot", 3.5);
            Guest guest2 = new Guest("Miss Lemon", 3);
            Guest guest4 = new Guest("Mademoiselle E. Petit", 1.5);

            Console.WriteLine("Wow, what a list! We can expect a great deal of fun I think :)\n");
        }

        private static void PresentGuestGrades()
        {
            Console.WriteLine("Okay... please sit down... the moment of truth has arrived. Each of your guests is now ready to present to " +
                "you the grade they have given your surprise salad.\n\nPress Enter to get your grades (scale 0-5)");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("YOUR GRADES: \n");

            Guest guest3 = new Guest("Her Majesty Doctora", 1.0, "I love surprises! Unfortunately I don't like to give grades.");
            Guest guest1 = new Guest("Hercule Poirot", 2.1572, "Too unpredictable and messy.");
            Guest guest2 = new Guest("Miss Lemon", 10, "Missed some balancing sour.");
            Guest guest4 = new Guest("Mademoiselle E. Petit", 5, "Mmmmmmmmmmm...");

            Console.WriteLine("I hope you are satisfied with your grades. Press Enter to continue...");
            Console.ReadLine();
        }

        private static void SayGoodbye()
        {
            Console.Clear();
            Console.WriteLine("But what's this now??? I think a surprise guest just arrived! Yes it's correct, we have another guest " +
                "on the list!\n");
            Console.WriteLine($"NAME: You your fine self\nGRADE: ?\nCOMMENT: ?\n");
            Console.WriteLine("You have created this extravagant surprise salad and as a guest at your own party it's now time for " +
                "you to evaluate it. Oh no no no no, not here!\n");
            Console.WriteLine("AAAAND... PJU! IT'S FINALLY DONE! And your fruit salad teacher seems happy :)\n");
            Console.WriteLine("GOODBYE FOR NOW!");
        }
    }
}
