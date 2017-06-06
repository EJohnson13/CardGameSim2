using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameSimulator.Durak
{
    public class DurakPlayer : IPlayerable
    {
        public List<Card> playerHand = new List<Card>();

        List<Card> IPlayerable.playerHand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddCard(Card newCard)
        {
            if (newCard == null) playerHand.TrimExcess();
            else playerHand.Add(newCard);
        }

        public Card PlayCard() { return null; }

        public List<string> DisplayHand()
        {
            List<string> handList = new List<string>();
            foreach(Card card in playerHand)
            {
                if(card != null) handList.Add(card.ToString());
            }
            return handList;
        }
    }
}
