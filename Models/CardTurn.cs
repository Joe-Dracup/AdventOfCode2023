namespace Models
{
    public class CardTurn
    {
        public string Hand { get; set; }
        public int Bid { get; set; }

        public CardTurn(string hand, int bid)
        {
            Hand = hand;
            Bid = bid;
        }

        Dictionary<char, char> mappingValues = new Dictionary<char, char>
            {
                {'A', 'A'}, {'K', 'B'}, {'Q', 'C'},
                {'J', 'D'}, {'T', 'E'}, {'9', 'F'},
                {'8', 'G'}, {'7', 'H'}, {'6', 'I'},
                {'5', 'J'}, {'4', 'K'}, {'3', 'L'},
                {'2', 'M'}
            };

        public string HandAsAlphabetical
        {
            get
            {
                return string.Concat(Hand.Select(x => mappingValues[x].ToString()));
            }
        }

        public int Score
        {
            get
            {
                if (IsFiveOfAKind())
                {
                    return 7;
                }
                else if (IsFourOfAKind())
                {
                    return 6;
                }
                else if (IsFullHouse())
                {
                    return 5;
                }
                else if (IsThreeOfAKind())
                {
                    return 4;
                }
                else if (IsTwoPair())
                {
                    return 3;
                }
                else if (IsOnePair())
                {
                    return 2;
                }
                else if (IsHighCard())
                {
                    return 1;
                }

                throw new Exception($"{Hand}: hand does not fit anything");
            }
        }

        private bool IsFiveOfAKind()
        {
            return Hand.Distinct().Count() == 1;
        }

        private bool IsFourOfAKind()
        {
            var distinctChars = Hand.Distinct().ToList();
            return distinctChars.Count == 2
                   && (Hand.Count(x => distinctChars[0] == x) == 1
                   || Hand.Count(x => distinctChars[1] == x) == 1);
        }

        private bool IsFullHouse()
        {
            var distinctChars = Hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return Hand.Count(y => x == y);
            });

            return charCounts.All(x => x == 2 || x == 3);
        }
        
        private bool IsThreeOfAKind()
        {
            var distinctChars = Hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return Hand.Count(y => x == y);
            });

            return distinctChars.Count == 3 && charCounts.All(x => x == 1 || x == 3);
        }

        private bool IsTwoPair()
        {
            var distinctChars = Hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return Hand.Count(y => x == y);
            });

            return distinctChars.Count == 3 && charCounts.All(x => x == 2 || x == 1);
        }

        private bool IsOnePair()
        {
            var distinctChars = Hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return Hand.Count(y => x == y);
            });

            return distinctChars.Count == 4 && charCounts.All(x => x == 2 || x == 1);
        }

        private bool IsHighCard()
        {
            return Hand.Distinct().Count() == 5;
        }
    }
}