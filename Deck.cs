using System;
using System.Collections.Generic;
using System.Linq;

namespace data_structures_assignment3
{
    public class Deck
    {
        private readonly List<Card> _cards = new List<Card>(); // list of cards

        private static readonly string[] Suits = Enum.GetValues(typeof(ESuit)).Cast<ESuit>().Select(x => x.ToString())
            .ToArray(); // list of string of suits
        private int Size => _cards.Count; // size of deck
        private bool IsDeckSorted { get; set; }

        // deck initialization
        public Deck()
        {
            foreach (var suit in Suits)
                for (var i = 1; i < 14; i++)
                    _cards.Add(new Card(suit, i));
            IsDeckSorted = true;
        }
        
        // string override to display if deck is sorted or not.
        public override string ToString()
        {
            return $"The deck of {Size} cards that is {DisplaySort()}";
        }

        private string DisplaySort()
        {
            return IsDeckSorted ? "sorted" : "not sorted";
        }
        
        // card shuffling by Fisher-Yates shuffle algorithm reference: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        public void Shuffle()
        {
            var random = new Random();
            for (var n = Size - 1; n > 0; n--)
            {
                var nextRandom = random.Next(n);
                var temp = _cards[n];
                _cards[n] = _cards[nextRandom];
                _cards[nextRandom] = temp;
            }
            IsDeckSorted = false;
        }

        // returns true if deck is sorted
        private bool IsSorted()
        {
            return IsDeckSorted;
        }

        
        // search function to search card through deck
        public void Search()
        {
            var card = DescribeCard();
            if (card is null) // invalid card
            {
                throw new ArgumentException("Invalid Card");
            }
            if (!IsSorted()) Sort(); // check if deck is already sorted
            var index = card.Suit * 13 + card.Value - 1; // get card index by formula suit * suit_size + card_value - 1
            if (card.Equals(_cards[index]))
            {
                Console.WriteLine("Card index is {0}", index);
                Console.WriteLine(_cards[index].ToString());
            }
            else
            {
                Console.WriteLine("Please check your logic");
            }
        }

        // gets card input from user
        private Card DescribeCard()
        {
            Console.WriteLine("What suit is the card?");
                var suit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a number from 1 to 13 (1 = Ace, 11 = Jack, 12 = Queen, 13 = King)");
                var value = Convert.ToInt32(Console.ReadLine());
                if (Enumerable.Range(1, 4).Contains(suit) && Enumerable.Range(1, 13).Contains(value)) // if suit and card are in range respectively, return card
                {
                    var card = new Card(Suits[suit - 1], value);
                    return card;
                }
                return null; // if card is invalid, return null;
        }

        private void Sort() // bubble sort
        {
            var iterationNumber = Size - 1;
            for (var i = iterationNumber; i > 0; i--)
            {
                for (var j = 0; j < i; j++)
                {
                    if (_cards[j].Suit == _cards[j + 1].Suit && _cards[j].Value > _cards[j + 1].Value) // if current's suit and next's suit are equal but current's value is greater than next
                                                                                                        // swap cards
                    {
                        var temp = _cards[j];
                        _cards[j] = _cards[j + 1];
                        _cards[j + 1] = temp;
                    }
                    else if (_cards[j].Suit > _cards[j + 1].Suit) // if current's suit is greater than next's suit, swap cards
                    {
                        var temp = _cards[j];
                        _cards[j] = _cards[j + 1];
                        _cards[j + 1] = temp;
                    }
                }
            }

            IsDeckSorted = true;
        }
        
        internal class Card
        {
            public int Suit { get; } // stores suit
            public int Value { get; } // stores card value
            private string ValueName // property to get value name
            {
                get
                {
                    return Value switch
                    {
                        1 => ECard.Ace.ToString(),
                        11 => ECard.Jack.ToString(),
                        12 => ECard.Queen.ToString(),
                        13 => ECard.King.ToString(),
                        _ => Value.ToString()
                    };
                }
            }
            
            // function to get numeric value of suit
            private int GetSuitValue(string suit)
            {
                if (ESuit.Diamond.ToString() == suit) return (int) ESuit.Diamond;

                if (ESuit.Clubs.ToString() == suit) return (int) ESuit.Clubs;

                if (ESuit.Hearts.ToString() == suit) return (int) ESuit.Hearts;

                if (ESuit.Spades.ToString() == suit) return (int) ESuit.Spades;
                throw new ArgumentException("Invalid Suit");
            }
            
            public Card(string suitIn, int valueIn)
            {
                Suit = GetSuitValue(suitIn);
                Value = valueIn;
            }
            
            
            // override equals
            public override bool Equals(object card)
            {
                return Equals(card as Card);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Suit, Value);
            }

            // displays card
            public override string ToString()
            {
                return $"{ValueName} of {Suits[Suit]}";
            }

            // if card's suits are different OR values are different, they are not equal
            public bool Equals(Card card)
            {
                if (Suit != card.Suit) return false;

                return Value == card.Value;
            }
        }

        
    }
}