using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameSimulator.Durak
{
    public class GameRunner
    {
        public DurakLogic durak = new DurakLogic();
        public DurakDealer dealer = new DurakDealer();
        public DurakPlayer player1 = new DurakPlayer();
        public DurakPlayer player2 = new DurakPlayer();
        public bool p1Att = true;
        public bool defPass;
        public bool noHand = false;

        public void Run()
        {
            // Creating the deck
            dealer.deck = dealer.CreateDeck();
            // Shuffling the deck
            dealer.Shuffle();
            // Getting Players' hands
            GetStartHand();
            // Finding Trump Suit
            GetTrump();
            // Bout 1
            defPass = durak.Bout(player1, player2);
            RestockHand(player1, player2);
            // All subsequent Bouts
            do
            {
                // Checking for who was the attacker
                if (p1Att == true)
                {
                    if (defPass)
                    {
                        defPass = durak.Bout(player2, player1);
                        p1Att = false;
                        RestockHand(player2, player1);
                    }
                    else
                    {
                        defPass = durak.Bout(player1, player2);
                        RestockHand(player1, player2);
                    }
                }
                else if ( p1Att == false)
                {
                    if (defPass)
                    {
                        defPass = durak.Bout(player1, player2);
                        p1Att = true;
                        RestockHand(player1, player2);
                    }
                    else
                    {
                        defPass = durak.Bout(player2, player1);
                        RestockHand(player2, player1);
                    }
                }
                CheckLoss(player1, player2);
            } while (!noHand);
        }

        public void GetTrump()
        {
            Card trumpCard = dealer.Deal();
            durak.FindTrump(trumpCard);
            durak.DisplayTrump();
            dealer.deck.Insert(dealer.deck.Count, trumpCard);
        }

        public void GetStartHand()
        {
            for (int i = 0; i < 6; i++)
            {
                player1.playerHand.Add(dealer.Deal());
                player2.playerHand.Add(dealer.Deal());
            }
        }

        public void CheckLoss(DurakPlayer p1, DurakPlayer p2)
        {
            if (p1.playerHand.Count == 0)
            {
                Console.WriteLine("Player 2 has lost.");
                noHand = true;
            }
            else if (p2.playerHand.Count == 0)
            {
                Console.WriteLine("Player 1 has lost.");
                noHand = true;
            }
            else noHand = false;
        }

        public void RestockHand(DurakPlayer attack, DurakPlayer defense)
        {
            for (int i = attack.playerHand.Count;i < 6;i++)
            {
                Card restock = dealer.Deal();
                attack.AddCard(restock);
            }
            if (defPass == true)
            {
                for (int i = defense.playerHand.Count; i < 6; i++)
                {
                    Card restock = dealer.Deal();
                    defense.AddCard(restock);
                }
            }
        }
    }
}
