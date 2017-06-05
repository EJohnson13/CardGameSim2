using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC150_ConsoleMenu;

namespace CardGameSimulator.War
{
    public class WarGame
    {
        private Card player1card;
        private Card player2card;
        private WarDealer dealer;
        private WarPlayer player1;
        private WarPlayer player2;
        List<Card> player1Dealt = new List<Card>();
        List<Card> player2Dealt = new List<Card>();

        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            Boolean menuSwitch = false;
            string[] option = { "Play" };
            int warGameMenu = CIO.PromptForMenuSelection(option, true);
            Console.WriteLine(warGameMenu);
            do
            {
                switch (warGameMenu)
                {
                    case 1:
                        PlayGame();
                        break;
                    case 0:
                        Console.WriteLine("Thanks for Playing!\n");
                        menuSwitch = true;
                        break;
                }
            } while (!menuSwitch);
        }

        public void PlayGame()
        {
            dealer = new WarDealer();
            string Name1 = CIO.PromptForInput("Who is Player 1: ", false);
            string Name2 = CIO.PromptForInput("Who is Player 2: ", false);
            dealer.CreateDeck();
            dealer.Shuffle();
            List<Card> player1Cards = dealer.Deck.GetRange(0, 26);
            List<Card> player2Cards = dealer.Deck.GetRange(27, 26);
            player1 = new WarPlayer(Name1, player1Cards);
            player2 = new WarPlayer(Name2, player2Cards);
            Console.WriteLine("The Game Begins!");
            Rungame();
        }

        public void Player1turn()
        {
            player1card = player1.PlayCard();
            player1.DisplayCard(player1card);
        }

        public void Player2turns()
        {
            player2card = player2.PlayCard();
            player2.DisplayCard(player2card);
        }

        public void Rungame()
        {
            Boolean gaming = true;
            do
            {
                player1.PopulatePlayerhand();
                player2.PopulatePlayerhand();
                Console.WriteLine(player1.ToString());
                Player1turn();
                Console.WriteLine();
                Console.WriteLine(player2.ToString());
                Player2turns();
                Console.WriteLine("\nPlayer 1 played: " + player1card);
                Console.WriteLine("\nPlayer 2 played: " + player2card + "\n");
                DetermineWin();
                if (player1.Win())
                {
                    Console.WriteLine("Player 1 won!");
                    gaming = false;
                } else if (player2.Win())
                {
                    Console.WriteLine("Player 2 won!");
                    gaming = false;
                }

            } while (gaming);
        }

        public void DetermineWin()
        {
            player1Dealt.Add(player1card);
            player2Dealt.Add(player2card);
            if (player2card.value == player1card.value)
            {
                Console.WriteLine("TIME FOR WAR!");
                War();
            }
            else if (player1card.value == CardEnums.Face.Ace && player2card.value != CardEnums.Face.Ace)
            {
                Console.WriteLine("Player 1 wins this round!");
                player1.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if (player1card.value != CardEnums.Face.Ace && player2card.value == CardEnums.Face.Ace)
            {
                Console.WriteLine("Player 2 wins this round!");
                player2.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if ((int)player1card.value > (int)player2card.value)
            {
                Console.WriteLine("Player 1 wins this round!");
                player1.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if ((int)player2card.value > (int)player1card.value)
            {
                Console.WriteLine("Player 2 wins this round!");
                player2.TakeWinCards(player1Dealt, player2Dealt);
            }
            Console.WriteLine("\nPress enter");
            Console.ReadLine();
            player1Dealt.Clear();
            player2Dealt.Clear();
        }

        public void War()
        {
            player1.PopulatePlayerhand();
            player2.PopulatePlayerhand();
            Player1turn();
            Player2turns();
            player1Dealt.Add(player1card);
            player2Dealt.Add(player2card);
            Console.WriteLine("\nPlayer 1 played: " + player1card);
            Console.WriteLine("\nPlayer 2 played: " + player2card + "\n");

            if (player2card.value == player1card.value)
            {
                Console.WriteLine("TIME FOR WAR!... AGAIN!");
                War();
            }
            else if (player1card.value == CardEnums.Face.Ace && player2card.value != CardEnums.Face.Ace)
            {
                Console.WriteLine("Player 1 wins this round!");
                player1.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if (player1card.value != CardEnums.Face.Ace && player2card.value == CardEnums.Face.Ace)
            {
                Console.WriteLine("Player 2 wins this round!");
                player2.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if ((int)player1card.value > (int)player2card.value)
            {
                Console.WriteLine("Player 1 wins this round!");
                player1.TakeWinCards(player1Dealt, player2Dealt);
            }
            else if ((int)player2card.value > (int)player1card.value)
            {
                Console.WriteLine("Player 2 wins this round!");
                player2.TakeWinCards(player1Dealt, player2Dealt);
            }
            player1Dealt.Clear();
            player2Dealt.Clear();
        }
    }
}