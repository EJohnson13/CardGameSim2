using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameSimulator.War
{
    public class WarPlayer : IPlayerable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Your name cannot be null or empty");
                }
                else
                {
                    name = value;
                }
            }
        }

        public List<Card> DiscardDeck { get; set; }
        public List<Card> playerHand { get; set; }

        public void AddCard(Card newCard)
        {
            throw new NotImplementedException();
        }

        public Card PlayCard()
        {
            Card playedcard = playerHand[0];
            DisplayCard(playedcard);
            playerHand.Remove(playerHand[0]);
            return playedcard;
        }

        public WarPlayer(string name, List<Card> DealtCards)
        {
            Name = name;
            playerHand = DealtCards;
            DiscardDeck = new List<Card>();
        }
        public void TakeWinCards(List<Card> player1DealtCards, List<Card> player2DealtCards)
        {
            
            List<Card> holder = player1DealtCards.Concat(player2DealtCards).ToList();
            DiscardDeck = DiscardDeck.Concat(holder).ToList();
        }
        public void PopulatePlayerhand()
        {
            if (playerHand.Count == 0)
            {
                playerHand = playerHand.Concat(DiscardDeck).ToList();
                DiscardDeck.Clear();
            }
        }
        public Boolean Win()
        {
            int amountOfCards = playerHand.Count + DiscardDeck.Count;
            if (amountOfCards == 52)
            {
                return true;
            }
            return false;
        }

        public string DisplayCard(Card card)
        {
            return $"{card.color} {card.value} of {card.suit}";
        }
        public override string ToString()
        {
            return $"\nName: {name}\nAmount of cards in hand: {playerHand.Count}, \nAmount of cards in Win Pile: {DiscardDeck.Count}";
        }
    }
}
