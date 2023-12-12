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

        protected virtual Dictionary<char, char> GetMappingValues()
        {
            return new()
            {
                {'A', 'A'}, {'K', 'B'}, {'Q', 'C'},
                {'J', 'D'}, {'T', 'E'}, {'9', 'F'},
                {'8', 'G'}, {'7', 'H'}, {'6', 'I'},
                {'5', 'J'}, {'4', 'K'}, {'3', 'L'},
                {'2', 'M'}
            };
        }

        public string HandAsAlphabetical
        {
            get
            {
                var mappingValues = GetMappingValues();

                return string.Concat(Hand.Select(x => mappingValues[x].ToString()));
            }
        }

        public int Score
        {
            get
            {
                return GetScore(Hand);
            }
        }

        protected int GetScore(string hand)
        {
            if (IsFiveOfAKind(hand))
            {
                return 7;
            }
            else if (IsFourOfAKind(hand))
            {
                return 6;
            }
            else if (IsFullHouse(hand))
            {
                return 5;
            }
            else if (IsThreeOfAKind(hand))
            {
                return 4;
            }
            else if (IsTwoPair(hand))
            {
                return 3;
            }
            else if (IsOnePair(hand))
            {
                return 2;
            }
            else if (IsHighCard(hand))
            {
                return 1;
            }

            throw new Exception($"{Hand}: hand does not fit anything");
        }

        private bool IsFiveOfAKind(string hand)
        {
            return hand.Distinct().Count() == 1;
        }

        private bool IsFourOfAKind(string hand)
        {
            var distinctChars = hand.Distinct().ToList();
            return distinctChars.Count == 2
                   && (hand.Count(x => distinctChars[0] == x) == 1
                   || hand.Count(x => distinctChars[1] == x) == 1);
        }

        private bool IsFullHouse(string hand)
        {
            var distinctChars = hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return hand.Count(y => x == y);
            });

            return charCounts.All(x => x == 2 || x == 3);
        }
        
        private bool IsThreeOfAKind(string hand)
        {
            var distinctChars = hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return hand.Count(y => x == y);
            });

            return distinctChars.Count == 3 && charCounts.All(x => x == 1 || x == 3);
        }

        private bool IsTwoPair(string hand)
        {
            var distinctChars = hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return hand.Count(y => x == y);
            });

            return distinctChars.Count == 3 && charCounts.All(x => x == 2 || x == 1);
        }

        private bool IsOnePair(string hand)
        {
            var distinctChars = hand.Distinct().ToList();

            var charCounts = distinctChars.Select(x =>
            {
                return hand.Count(y => x == y);
            });

            return distinctChars.Count == 4 && charCounts.All(x => x == 2 || x == 1);
        }

        private bool IsHighCard(string hand)
        {
            return hand.Distinct().Count() == 5;
        }
    }
}