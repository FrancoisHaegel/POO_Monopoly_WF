﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class CardChance : Card
    {
        private int id;

        override
        public int getId()
        {
            return id;
        }

        public CardChance(int pId, string pDescription) : base(pDescription)
        {
            id = pId;
        }

        public static CardChance FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            int id = int.Parse(values[0]);
            string description = values[1];

            CardChance card = new CardChance(id, description);
            return card;
        }

        override
        public void describe()
        {
            Console.WriteLine("Id : " + id + " - description : " + this.getDescription());
        }

    }
}