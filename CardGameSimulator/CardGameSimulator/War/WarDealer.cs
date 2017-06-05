using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameSimulator.War
{
    public class WarDealer : Dealer
    {
        public List<Card> Deck;

        Random randysavage = new Random();

        public override List<Card> CreateDeck()
        {
            List<Card> cards = new List<Card>();

            foreach (CardEnums.Color color in Enum.GetValues(typeof(CardEnums.Color)))
            {
                foreach (CardEnums.Suit suit in Enum.GetValues(typeof(CardEnums.Suit)))
                {
                    foreach (CardEnums.Face face in Enum.GetValues(typeof(CardEnums.Face)))
                    {
                        cards.Add(new Card(face, color, suit));
                    }
                }
            }
            Deck = cards;
            return cards;
        }

        public override Card Deal()
        {
            throw new NotImplementedException();
        }

        public override void Shuffle()
        {
            int k = 0;
            Card temp;
            Random randysavage = new Random();

            for (int i = Deck.Count - 1; i > 0; i--)
            {
                k = randysavage.Next(1, i + 1);
                temp = Deck[k];
                Deck[k] = Deck[i];
                Deck[i] = temp;
            }
        }


    }
}
