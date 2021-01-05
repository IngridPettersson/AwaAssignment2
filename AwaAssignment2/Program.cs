using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AwaAssignment2
{
    class Program
    {
        //Instead of all these public fields I should change some of them to properties within own classes in their own files, and others to return values.
        //Or make a new function where I return a value.
        static Dictionary<string, double> ingredients = new Dictionary<string, double>();
        static string ingredient;
        static double price;
        static double maxValue;
        static double minValue;
        static string keyOfMaxValue;
        static string keyOfMinValue;
        static double totalPrice = 0.0;
        // A string[] might preferrably be used for the variable "surpriseSalad" but I want to try out the List<> and see how it's behaving.
        static List<string> surpriseSalad = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                WelcomeUser();
                AskForIngredient();
                //CalculatePrices(); totalPrice blir det dubbla om man kör både denna och cw nedan
                Console.WriteLine($"The remainder for Santa to pay is: SEK {CalculatePrices()}:-");
                ShowTotalPrice();
                ShowMostExpensive();
                ShowCheapest();
                CreateSurpriseSalad();
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
                "if you don't read all the \"bla bla bla\", so my recommendation is that you do just that :)\n\nPlease press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void AskForIngredient()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter an ingredient for your fruit salad: ");
                // Add error handling for if the user didnt type a string (at least two characters). Check hangman (if else clause).
                ingredient = Console.ReadLine();
                Console.Write("Enter price in SEK for \"one unit\" of the ingredient: ");

                //Add error handling if the user didnt type an int.
                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                }
                //Is this exception from API correct? Another option could be "NotFiniteNumberException".
                catch (FormatException)
                {
                    //How to repeat this catch code until int is entered? Now it works if you make the mistake max once per ingredient.
                    Console.Write("Please type the price as a valid number: ");
                    price = Convert.ToDouble(Console.ReadLine());
                }
                ingredients.Add(ingredient, price);
            }
            Console.WriteLine();
        }

        //        Console.Write("Enter a Number\n");
        //          string input = Console.ReadLine(); //get the input
        //          int num = -1;
        //          if (!int.TryParse(input, out num))
        //          {
        //           Console.WriteLine("Not an integer");
        //          }
        //          else
        //          {
        //            ...
        //          }

        private static double CalculatePrices()
        // Kan dela upp den här i flera metoder, t.ex. CalculateMaxValue, ...MinValue och totalPrice och använda return.
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

            Console.WriteLine("It turns out that 2 of your friends will share the cost with you and Santa will pay the remainder - how random!\n");
            double remainder = totalPrice % 3;
            return remainder;

            // Beräkna totalPrice % 2 (modulus?) för att dela kostnaden på 2 och Jultomten betalar the remainder!??? Om remainder blir 0 så någonting, typ tomten blir glad :)
        }
        private static void ShowTotalPrice()
        {
            Console.WriteLine("\nValuable facts about your fruit salad:".ToUpper());
            Console.WriteLine();
            Console.WriteLine($"Total price: SEK {totalPrice}:-");
            Console.WriteLine();
        }
        private static void ShowMostExpensive()
        {
            Console.WriteLine($"Most expensive ingredient: {keyOfMaxValue}\nPrice: SEK {maxValue}:-");
            Console.WriteLine();
        }
        private static void ShowCheapest()
        {
            Console.WriteLine($"Cheapest ingredient: {keyOfMinValue}\nPrice: SEK {minValue}:-");
            Console.WriteLine();
            Console.WriteLine("Bon appéttit!\n\nOh, but wait...".ToUpper());
            Console.WriteLine("...something peculiar just occured! Please press Enter to find out more...");
            Console.ReadLine();
        }

        //Dela upp CreateSurpriseSalad i flera funktioner för bättre översikt????
        private static void CreateSurpriseSalad()
        {
            Console.Clear();
            Console.WriteLine("When you sent in the recipe to your fruit salad teacher it failed unfortunately, for seeming a bit too..." +
                " conventional. Your guests' taste buds might get bored. Let's create a small but oh so " +
                "delicious surprise salad instead, with fresh ingredients from all over the world!\n");
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
                surpriseIngredients = new string[] { "cherimoya", "kiwano", "dragon fruit", "feijoa",
                    "tamarillo", "loquat", "jujube", "mangosteen", "rambutan", "pacay", "starfruit", "atemoya" };
            }

            do
            {
                // Göra lista med ingredientNumbers (first, second, third) där man går upp en i listan efter varje loop?
                string ingredientNumber;

                Console.WriteLine($"Please type any letter to get your ingredientNumber ingredient. OBS! Some letters wont give you any fruit " +
                    $"(such is life I guess). " +
                    $"In that case, don't lose hope - be persistent and keep typing!");

                //Add error handling for typing something else than just one letter!
                //Do I always need to parse Console.ReadLine() if the input is else than a string?????? JA, enligt Mike.
                typedLetter = char.Parse(Console.ReadLine());

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

            surpriseSpices = new string[] { "black pepper", "vata churna", "chili", "turmeric", "cinnamon", "cardamom", "ginger", "dill",
                "fennel" };

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
                Console.WriteLine(fruit);
            }
            Console.WriteLine(randomizedSpice);
            Console.WriteLine();
            Console.WriteLine("Enjoy!".ToUpper());
        }
    }
}

