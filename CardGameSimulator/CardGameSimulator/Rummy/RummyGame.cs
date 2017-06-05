using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameSimulator.Rummy
{
    class RummyGame
    {
        public void Run()
        {
            bool keepgoing = true;
            RummyDealer dlr = new RummyDealer();
            List<Card> deck = dlr.CreateDeck();
            string input = null;
            int numOfPlayers = 0;


            Console.WriteLine(" ");
            Console.WriteLine("Welcome to Rummy!");
            Console.WriteLine("-----------------");
            Console.WriteLine(" ");
            Console.WriteLine("Max Players: 6");
            Console.WriteLine("Min Players: 2");
            Console.WriteLine(" ");

            while (keepgoing)
            {
                Console.WriteLine(" ");
                Console.Write("How many Players are there: ");

                input = Console.ReadLine();
                int.TryParse(input, out numOfPlayers);

                if (numOfPlayers > 6 || numOfPlayers < 2)
                {
                    Console.WriteLine("Please enter a valid number of players");
                    keepgoing = true;
                }
                else
                {
                    keepgoing = false;
                }

            }

            List<RummyPlayer> players = GeneratePlayers(numOfPlayers);
            deck = dlr.ShuffleCards(deck);
            deck = dlr.DealCards(players, deck);



            // This cod will print what is in the random deck that was not dealt
            // Along with the hands of all the players

            //foreach (RummyPlayer player in players)
            //{
            //    Console.WriteLine("Hands");
            //    Console.WriteLine(" ");
            //    player.PrintPlayerHand();
            //    Console.WriteLine(" ");
            //}

            //foreach(Card card in deck)
            //{
            //    Console.WriteLine(card);
            //}


            Card previouslyDiscarded = null;


            foreach (RummyPlayer player in players)
            {
                previouslyDiscarded = Turn(player, deck, previouslyDiscarded);
            }



        }



        public void PrintDeck(List<Card> deck)
        {
            int counter = 0;
            foreach (Card card in deck)
            {
                counter++;
                Console.WriteLine(card + " Card #: " + counter);
            }
            Console.WriteLine("");
        }



        public List<RummyPlayer> GeneratePlayers(int numberOfPlayers)
        {

            List<RummyPlayer> players = new List<RummyPlayer>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                int counter = i + 1;
                Console.Write("Player " + counter + " name: ");
                string name = Console.ReadLine();
                RummyPlayer player = new RummyPlayer(name);
                players.Add(player);
            }

            return players;
        }




        public Card Turn(RummyPlayer player, List<Card> deck, Card previouslyDiscarded)
        {
            bool keepgoing = true;
            string input = null;
            bool cardIsThere = true;

            Console.WriteLine(" ");
            Console.WriteLine("Current Hand for " + player.GetName());
            Console.WriteLine("------------------------");
            player.PrintPlayerHand();
            Console.WriteLine(" ");



            if (previouslyDiscarded == null)
            {
                Console.WriteLine(" ");
                Console.WriteLine("No cards have been discarded yet.");
                Console.WriteLine(" ");
                cardIsThere = false;
            }
            else
            {
                Console.WriteLine("Card previously Discarded: " + previouslyDiscarded);
                cardIsThere = true;
            }

            while (keepgoing)
            {
                if (cardIsThere)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("What action would you like to take? ");
                    Console.WriteLine("1) Draw off the top of remainder pile.");
                    Console.WriteLine("2) Draw previously discarded card.");
                }
                else
                {
                    Console.WriteLine("What action would you like to take? ");
                    Console.WriteLine("1) Draw off the top of remainder pile.");
                }
                input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    Card cardDrawn = deck[1];
                    Console.WriteLine("");
                    Console.WriteLine("You drew a " + cardDrawn);
                    Console.WriteLine("");
                    player.AddCard(cardDrawn);
                    deck.Remove(cardDrawn);
                    keepgoing = false;
                }
                else if (input.Equals("2") && cardIsThere)
                {
                    Console.WriteLine("");
                    player.AddCard(previouslyDiscarded);
                    Console.WriteLine("You added a " + previouslyDiscarded + " to your hand");
                    keepgoing = false;

                }
                else
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Please enter valid input");
                    Console.WriteLine(" ");
                    keepgoing = true;
                }
            }

            //Discard process

            bool loop = true;

            while (loop)
            {

                Console.WriteLine(" ");
                Console.WriteLine("What card would you like to discard?");
                Console.WriteLine("------------------------------------");
                player.PrintPlayerHand();
                string num = Console.ReadLine();
                if (!int.TryParse(num, out int discard))
                {
                    Console.WriteLine("Please enter valid input");
                    loop = true;
                }
                if (discard > 6 || discard < 1)
                {
                    Console.WriteLine("Please enter valid input");
                    loop = true;
                }
                else
                {
                    discard -= 1;
                    previouslyDiscarded = player.Discard(discard);
                    Console.WriteLine(" ");
                    Console.WriteLine("You have chosen to discard your " + previouslyDiscarded);
                    loop = false;
                }
            }


            bool validMatchs = CheckForValueMatches(player.phand);
            bool validRun = CheckForRun(player.phand);






            return previouslyDiscarded;
        }







        public bool CheckForValueMatches(List<Card> hand)
        {
         Card[] playersHand = hand.ToArray<Card>();
            Card cardToCheck;
            int counter = 0;
            bool valid = true;

           
            for (int i = 0; i < playersHand.Length; i++)
            {
               cardToCheck = hand[i];
                counter = 0;

                for (int p = 0; p < playersHand.Length; p++)
                {
                    Card cardToCompare = hand[p];

                    if (cardToCheck.value == cardToCompare.value)
                    {
                        counter++;
                    }
                }
            }

            if(counter > 4)
            {
                valid = true;
            }

            return valid;
        }

        public bool CheckForRun(List<Card> hand)
        {
            Card[] playersHand = hand.ToArray<Card>();
            Card cardToCheck;
            int counter = 0;
            bool valid = true;

            

            for (int i = 0; i < playersHand.Length; i++)
            {
                cardToCheck = hand[i];
                counter = 0;

                for (int p = 0; p < playersHand.Length; p++)
                {
                    Card cardToCompare = hand[p];

                    if (cardToCheck.value > cardToCompare.value)
                    {
                        counter++;
                    }
                }
            }

            if (counter > 4)
            {
                valid = true;
            }
            else{
                valid = false;
            }

            return valid;
        }
    }
}
