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
            Console.WriteLine("To Win: ");
            Console.WriteLine(" - Get 4 of one kind");
            Console.WriteLine(" - Get a Run of 4");
            Console.WriteLine(" ");
            Console.WriteLine(" - Max Players: 6");
            Console.WriteLine(" - Min Players: 2");
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

            Card previouslyDiscarded = null;
            RummyPlayer rp = null;
            bool hasWon = false;

            do
            {

                foreach (RummyPlayer player in players)
                {
                    rp = player;
                    previouslyDiscarded = Turn(player, deck, previouslyDiscarded);
                    hasWon = CheckForWin(player.phand);

                    if (hasWon == true) break;

                   Intermission(hasWon);

                    
                }

            } while (hasWon == false);

            Console.WriteLine(" ");
            Console.WriteLine(rp.GetName() + " has Won!");
            Console.WriteLine(" ");
            Console.WriteLine("Returning to main menu");

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
                if (discard > 5 || discard < 1)
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

            return previouslyDiscarded;
        }





        List<Card> testList = new List<Card>();
        Card test1 = new Card(CardEnums.Face.Two, CardEnums.Color.Black, CardEnums.Suit.Clubs);
        Card test2 = new Card(CardEnums.Face.Three, CardEnums.Color.Red, CardEnums.Suit.Spades);
        Card test3 = new Card(CardEnums.Face.Five, CardEnums.Color.Black, CardEnums.Suit.Diamonds);
        Card test4 = new Card(CardEnums.Face.Five, CardEnums.Color.Red, CardEnums.Suit.Hearts);

       public void Test()
        {
            testList.Add(test1);
            testList.Add(test2);
            testList.Add(test3);
            testList.Add(test4);


           bool worked = CheckForWin(testList);

            if (worked)
            {
                Console.WriteLine("");
                Console.WriteLine("It worked");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("Didnt work");
                Console.WriteLine(" ");
            }
        }

        public bool CheckForWin(List<Card> playersHand)
        {
            CardEnums.Face[] faces = new CardEnums.Face[13];    // Might be 12
            Card[] hand = playersHand.ToArray();
            int counterForMatches = 0;
            bool runValid = true;
            bool won = true;

            foreach (Card card in playersHand)
            {
                // Checking for matches
                for (int i = 0; i < hand.Length; i++)
                {
                    if (card.value == hand[i].value)
                    {
                        counterForMatches++;
                    }
                }

               
            }
            if (counterForMatches == 16)
            {
                won = true;
            }
            else
            {
                won = false;
            }

            return won;
        }



        //public bool DoubleCheckForWin(List<Card> playersHand)
        //{

        //    Card[] hand = playersHand.ToArray();



        //    for (int i = 0; i < hand.Length; i++)
        //    {
        //        CardEnums.Face cardCheckFace = card.value;
        //        CardEnums.Face c1 = hand[i + 1].value;
        //        CardEnums.Face c2 = hand[i + 2].value;
        //        CardEnums.Face c3 = hand[i + 3].value;


        //        if (cardCheckFace + 1 == c1 && cardCheckFace + 2 == c2 && cardCheckFace + 3 == c3)
        //        {
        //            runValid = true;
        //            break;
        //        }
        //        else
        //        {
        //            runValid = false;
        //        }
        //    }
        //}




        public void Intermission(bool hasWon)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("You hand is not valid for a win");
            Console.WriteLine("-------------------------------- ");
            Console.WriteLine("Your turn is over, starting next turn..");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Next turn will start in 10 seconds");
            Console.WriteLine(" ");

            System.Threading.Thread.Sleep(10000); // Sleep For 10 seconds

        }




    }
}
