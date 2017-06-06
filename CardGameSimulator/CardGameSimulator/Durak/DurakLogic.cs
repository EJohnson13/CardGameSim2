using System;
using CardGameSimulator.CardEnums;
using CSC150_ConsoleMenu;
using System.Collections.Generic;

namespace CardGameSimulator.Durak
{
    public class DurakLogic
    {
        public Suit trump;
        public List<Card> playfield = new List<Card>(12);
        public int wave = 1;

        public Card Attack(DurakPlayer att, int wave)
        {
            Card attack;
            Console.Clear();
            DisplayTrump();
            PrintField();
            if (wave == 1)
            {
                List<string> options = att.DisplayHand();
                int choice = CIO.PromptForMenuSelection(options, false);
                attack = att.playerHand[choice - 1];
            }
            else
            {
                List<string> options = att.DisplayHand();
                bool valid = false;
                do
                {
                    int choice = CIO.PromptForMenuSelection(options, true);
                    if (choice == 0)
                    {
                        valid = true;
                        attack = null;
                        playfield.Clear();
                    }
                    else
                    {
                        attack = att.playerHand[choice - 1];
                        valid = CheckAttack(attack);
                    }
                } while (!valid);
            }
            if(attack != null) playfield.Add(attack);
            att.playerHand.Remove(attack);
            return attack;
        }
        public bool Defend(DurakPlayer def, Card beat)
        {
            Console.Clear();
            DisplayTrump();
            Console.WriteLine("Attacker playerd:\t" + beat.ToString());
            Card defense;
            Console.WriteLine();
            List<string> options = def.DisplayHand();
            bool valid = false;
            do
            {
                
                int choice = CIO.PromptForMenuSelection(options, true);
                if(choice == 0)
                {
                    def.playerHand.AddRange(playfield);
                    playfield.Clear();
                    return false;
                }
                else
                {
                    defense = def.playerHand[choice - 1];
                    valid = CheckDefense(beat, defense);
                }
            } while (!valid);
            playfield.Add(defense);
            def.playerHand.Remove(defense);
            return true;
        }
        public bool Bout(DurakPlayer att, DurakPlayer def)
        {
            bool quit = false;
            bool victory = true;
            int check = def.playerHand.Count;
            wave = 1;
            do
            {
                Console.Clear();
                Card beat = Attack(att, wave);
                if (beat == null) quit = true;
                else
                {
                    victory = Defend(def, beat);
                }
                if (victory == false) quit = true;
                wave++;
            } while (!quit && wave <= 6 && wave <= check);

            return victory;
        }

        public bool CheckAttack(Card attack)
        {
            foreach (Card played in playfield)
            {
                if (attack.value == played.value) return true;
                else Console.WriteLine("Card must be of equal value to previously played cards.");
            }
            return false;
        }

        public bool CheckDefense(Card attack, Card defense)
        {
            if (defense.suit == trump)
            {
                if (attack.suit == trump)
                {
                    if (defense.value >= attack.value || defense.value == 0) return true;
                    else
                    {
                        Console.WriteLine("This card cannot beat the attacker's card.\nRetry or yeild and take up all played cards this bout.");
                        return false;
                    }
                }
                else return true;
            }
            else if (attack.suit != trump && defense.value >= attack.value || defense.value == 0) return true;
            else
            {
                Console.WriteLine("This card cannot beat the attacker's card.\nRetry or yeild and take up all played cards this bout.");
                return false;
            }
        }
        public void PrintField()
        {
            Console.WriteLine("Playfield:\n");
            for (int i = 0; i < playfield.Count; i++)
            {
                Console.WriteLine("\t" + playfield[i].ToString());
            }
            Console.WriteLine();
        }
        public void FindTrump(Card trumpCard)
        {
            trump = trumpCard.suit;
        }
        public void DisplayTrump()
        {
            Console.WriteLine("Trump Suit: " + trump.ToString());
        }
    }
}
