using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using Monopoly.exception;
using System.IO;

namespace Monopoly.controller
{
    class CardManager
    {
        private List<CardChance> chanceCards = new List<CardChance>();
        private List<CardCommunity> communityCards = new List<CardCommunity>();

        public void init(string pathToCommunity, string pathToChance)
        {
            try
            {
                communityCards = File.ReadAllLines(pathToCommunity).Skip(1).Select(v => CardCommunity.FromCsv(v)).ToList();
                chanceCards = File.ReadAllLines(pathToChance).Skip(1).Select(v => CardChance.FromCsv(v)).ToList();
            }
            catch (DirectoryNotFoundException e)
            {
                Form_board.GetInstance.insert_console("Error : Cannot find path : " + e);
            }
        }

        public void describe()
        {
            Form_board.GetInstance.insert_console("Description des Community-Cards :");
            foreach (Card c in communityCards)
            {
                c.describe();
            }
            Form_board.GetInstance.insert_console("Description des Chance-Cards :");
            foreach (Card c in chanceCards)
            {
                c.describe();
            }

        }

        public Card getCommunityCard(int id)
        {
            foreach (Card c in communityCards)
            {
                if (c.getId() == id) return c;
            }
            return null;
        }

        public Card getChanceCard(int id)
        {
            foreach (Card c in chanceCards)
            {
                if (c.getId() == id) return c;
            }
            return null;
        }

        public CardCommunity drawCommunity()
        {
            CardCommunity res = communityCards[0];
            communityCards.RemoveAt(0);
            //If it's the "Get out of jail" card
            if (res.getId() != 8)
            {
                communityCards.Insert(communityCards.Count(), res);
            }
            return res;
        }

        public CardChance drawChance()
        {
            CardChance res = chanceCards[0];
            chanceCards.RemoveAt(0);
            //If it's the "Get out of jail" card
            if (res.getId() != 7)
            {
                chanceCards.Insert(chanceCards.Count(), res);
            }
            return res;
        }

        public Card drawCard(Card.CardType type)
        {
            if (type == Card.CardType.CHANCE)
            {
                return drawChance();

            }
            else if (type == Card.CardType.COMMUNITY)
            {
                return drawCommunity();
            }
            return null;
        }
    }
}
