using System;

namespace data_structures_assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******************Deck Output***********************");
            Console.WriteLine("Deck Initialized");
            var deck = new Deck();
            Console.WriteLine(deck.ToString());
            ConsoleKeyInfo inputKey;
            do
            {
                deck.Shuffle();
                Console.WriteLine("Deck Shuffled");
                Console.WriteLine(deck.ToString());
                deck.Search();
                Console.WriteLine("Press Y to CONTINUE. Press any other key to EXIT");
                inputKey = Console.ReadKey();
            } while (inputKey.Key == ConsoleKey.Y);
               
            
            Console.WriteLine("*******************Tree Output***********************");
            var fileContent = FileReader.ReadFile();
            var tree = new Tree();
            foreach (var item in fileContent)
            {
                tree.Insert(item);
            }

            tree.CalculateLeaves(tree.GetRoot());
           
            Console.WriteLine("Total Number of Leaf Nodes {0}", tree.TotalLeafNodes);
            var height = tree.GetHeight(tree.GetRoot());
            Console.WriteLine("Height of Tree {0}", height);
        }
    }
}