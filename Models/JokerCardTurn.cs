
namespace Models
{
    public class JokerCardTurn(string hand, int bid) : CardTurn(hand, bid)
    {
        public new int Score
        {
            get
            {
                var hand = ReplaceJokerWithMostCommonCard(Hand);

                return GetScore(hand);
            }
        }

        protected override Dictionary<char, char> GetMappingValues()
        {
            return new()
            {
                {'A', 'A'}, {'K', 'B'}, {'Q', 'C'},
                {'T', 'E'}, {'9', 'F'}, {'8', 'G'},
                {'7', 'H'}, {'6', 'I'}, {'5', 'J'},
                {'4', 'K'}, {'3', 'L'}, {'2', 'M'},
                {'J', 'N'}
            };
        }

        public static string ReplaceJokerWithMostCommonCard(string hand)
        {
            char mostCommonChar = hand.Where(x => x != 'J')
                                      .GroupBy(x => x)
                                      .OrderByDescending(x => x.Count())
                                      .FirstOrDefault()?.Key ?? 'J';

            return hand.Replace('J', mostCommonChar);
        }
    }
}