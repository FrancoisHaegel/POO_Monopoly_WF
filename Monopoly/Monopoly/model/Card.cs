using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    abstract class Card
    {
        private string description;

        public abstract int getId();

        public enum CardType
        {
            CHANCE,
            COMMUNITY
        };

        public string getDescription()
        {
            return description;
        }

        public Card(string pDescription)
        {
            description = pDescription;
        }

        public abstract void describe();
    }
}