using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace awa_assignment_2
{
    class Program
    {
        //Why error when entering ingredients with the same price or name? Max and min value should be able to be the same.
        // As well as you could choose the same fruit twice cause you might want 2 of them. And how to fix it? 

        //Create new files/classes from the project: Calculator maybe one class...? So it is possible to create classes without creating new
        //objects from them. Do the methods in the class static (and public I guess) and then call them
        //from Main (or from anywhere in another file?) using Classname.MethodName(argument);. Could also make the whole class static and then you 
        //can't create an object.
        public static string Ingredient { get; set; }
        public static double Price { get; set; }
        public static double MaxValue { get; set; }
        public static double MinValue { get; set; }
        public static string KeyOfMaxValue { get; set; }
        public static string KeyOfMinValue { get; set; }
        public static string SantaString { get; set; }
        public static double Remainder { get; set; }

        public static double TotalPrice { get; set; }

        public static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        public static List<string> surpriseSalad = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                WelcomeUser();
                AskForIngredient();
                CalculatePrices();
                Console.WriteLine($"The remainder for Santa to pay is: SEK {Remainder}:-. {SantaString}");
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
        private static void WelcomeUser()
        {
            Console.WriteLine("Hello and welcome to this fruit salad party!".ToUpper());
            Console.WriteLine("\nOh, but the fruit salad isn't ready yet... in fact we didn't even decide " +
                "which ingredients we want - let's fix that!\n\nAND TO PREPARE YOUR MINDSET, there will be some extras here and it wont make sense " +
                "if you don't read all the \"bla bla bla\", so my recommendation is that you do just that :) Oh, and it will take some time!" +
                "\n\nPlease press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void AskForIngredient()
        {
            Console.WriteLine("Let's begin!\n\nWe expect a total of 4 ingredients in your fruit salad.\n");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enter an ingredient for your fruit salad: ");
                //Kan man använda Console.ReadKey här i stället?
                Ingredient = Console.ReadLine();
                if (Ingredient.Length < 2)
                {
                    i--;
                    Console.WriteLine("Are you really making a proper fruit salad? At least two characters are needed " +
                        "to be a fruit around here...");
                    continue;
                }
                Console.Write("Enter price in SEK for \"one unit\" of the ingredient: ");

                try
                {
                    Price = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                    continue;
                }
                ingredients.Add(Ingredient, Price);
            }
            Console.WriteLine();
        }

        private static void CalculatePrices()
        {
            Console.Clear();

            MaxValue = ingredients.Values.Max();
            KeyOfMaxValue = ingredients.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            MinValue = ingredients.Values.Min();
            KeyOfMinValue = ingredients.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

            foreach (KeyValuePair<string, double> item in ingredients)
            {
                TotalPrice += item.Value;
            }

            CalculateRemainder();
        }

        private static (double, string) CalculateRemainder()
        {

            Console.WriteLine("It turns out that 2 of your friends will share the cost with you and Santa will pay the remainder - how random!\n");
            Remainder = TotalPrice % 3;

            if (Remainder < 1)
            {
                SantaString = "\"- That was a good deal for me! I could be a nice guy and still lose nothing :)\" /Santa";
            }
            else
            {
                SantaString = "\"- What?? I never gave my consent to this! I'm afraid you should consider this as your pre-given " +
                    "2021 Christmas gift...\" /Santa";
            }
            return (Remainder, SantaString);
        }

private static void ShowPrices()
        {
            ShowTotalPrice();
            ShowMostExpensive();
            ShowCheapest();
        }
       
        private static void ShowTotalPrice()
        {
            Console.WriteLine("\nValuable facts about your fruit salad:".ToUpper());
            Console.WriteLine();
            Console.WriteLine($"Total price: SEK {TotalPrice}:-");
            Console.WriteLine();
        }
        private static void ShowMostExpensive()
        {
            Console.WriteLine($"Most expensive ingredient: {KeyOfMaxValue}\nPrice: SEK {MaxValue}:-");
            Console.WriteLine();
        }
        private static void ShowCheapest()
        {
            Console.WriteLine($"Cheapest ingredient: {KeyOfMinValue}\nPrice: SEK {MinValue}:-");
            Console.WriteLine();
            Console.WriteLine("Bon appéttit!\n\nOh, but wait...".ToUpper());
            Console.WriteLine("...something peculiar just occured!\n\nPlease press Enter to find out more...");
            Console.ReadLine();
        }

        //Dela upp CreateSurpriseSalad i flera funktioner för bättre översikt???? OCH surpriseSpices i surpriseSallad kan vara en egen klass
        // (SurpriseSpice) med arguments of name, color and price. ELLER lägga till en till ingrediens eller något annat som får vara en egen
        // klass till att börja med kanske...? Kanske guest list med name, age och eyeColor: Mademoiselle Petit, Madame Mustard, Monsieur Hercules
        // Poirot och His Majesty Soosh.
        private static void CreateSurpriseSalad()
        {
            Console.Clear();
            Console.WriteLine("When you sent this fruit salad recipe to your fruit salad teacher he identified some areas of improvement. " +
                "His biggest concern was that it seemed a bit too..." +
                " conventional. Your guests' taste buds might get bored, and we can't take that risk!\n\nLet's create a small but OH SO " +
                "DELICIOUS surprise salad instead, with fresh ingredients from all over the world!\n\nPlease press Enter to continue...\n");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Oh oh oh!\n\nSomething of highest importance just crossed my mind! We don't know who your honored " +
                "guests are(?)\n\nLet's find " +
                "out by pressing Enter!");
            Console.ReadLine();
            Console.Clear();

            PresentGuestList();

            Console.WriteLine("Please press Enter to start creating your surprise salad!".ToUpper());
            Console.ReadLine();
            Console.Clear();

            string[] surpriseIngredients;
            char typedLetter;
            string randomizedSpice;
            string[] surpriseSpices;

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

            do
            {
                // Göra lista med ingredientNumbers (first, second, third) där man går upp en i listan efter varje loop?

                Console.WriteLine($"Please type any letter to get one of your ingredients. OBS! Some letters wont give you any fruit " +
                    $"(such is life I guess)... " +
                    $"In that case, don't lose hope - be persistent and keep typing!");

                try
                {
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
                        Console.WriteLine($"\"{typedLetter}\" gives ju {fruit.ToUpper()}");
                        surpriseSalad.Add(fruit);
                        Console.WriteLine();
                    }
                }
            } while (surpriseSalad.Count < 3);
            Console.WriteLine("You've got all your fruits! Please press Enter to continue...".ToUpper());
            Console.ReadLine();
            Console.Clear();

            surpriseSpices = new string[] { "vata churna", "dill",
                "thyme", "catnip", "fenugreek", "garlic", "kawakawa", "savory" };

            //Capitalize all ingredients and spices from array?
            Console.WriteLine("And now we'll add a generous amount of a random spice to your surprise salad!".ToUpper());
            Console.WriteLine("\nPlease press Enter to find out which one...");
            Console.ReadLine();
            Console.Clear();

            Random random = new Random();

            randomizedSpice = surpriseSpices[random.Next(0, surpriseSpices.Length)];
            Console.WriteLine($"Your spice of choice is: {randomizedSpice.ToUpper()}\n");
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

            Guest guest3 = new Guest("Madame L'étonnement", 33.5);
            Guest guest1 = new Guest("Hercule Poirot", 4);
            Guest guest2 = new Guest("Miss Lemon", 4);
            Guest guest4 = new Guest("Mademoiselle E. Petit", 1.5);

            Console.WriteLine("Wow, what a list! We can expect a great deal of fun I think :)\n");
        }

        private static void PresentGuestGrades()
        {
            Console.WriteLine("Okay... please sit down... the moment of truth has arrived. Each of your guests is now ready to present to " +
                "you the grade they have given your surprise salad.\n\nPlease press Enter to get your grades (scale 0-5)");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("YOUR GRADES: \n\n");

            Guest guest3 = new Guest("Madame L'étonnement", 1.0, "I love surprises! Unfortunately I don't like fruit.");
            Guest guest1 = new Guest("Hercule Poirot", 2.1572, "Too unpredictable and messy.");
            Guest guest2 = new Guest("Miss Lemon", 10, "Missed some balancing sour.");
            Guest guest4 = new Guest("Mademoiselle E. Petit", 5, "Mmmmmmmmmmm...");

            Console.WriteLine("But what's this now??? I think a surprise guest just arrived! Yes it's correct, we have another guest " +
                "on the list!\n\n");
            Console.WriteLine($"NAME: You your fine self\nGRADE: ?\nCOMMENT: ?\n\n");
            Console.WriteLine("You have created this extravagant surprise salad and as a guest at your own party it's now time for " +
                "you to evaluate it. Oh no no no no, not here!\n");
        }

        private static void SayGoodbye()
        {
            Console.WriteLine("AAAAND... PJU! IT'S FINALLY DONE! And your fruit salad teacher seems happy :)\n");
            Console.WriteLine("GOODBYE FOR NOW!");
        }
    }
}
