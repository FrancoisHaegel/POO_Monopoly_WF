using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class Player
    {
        private int id;
        private string name;
        private string color;
        private int money;
        private bool inJail;
        public int turnInJail;
        private List<Property> properties;
        private List<Property> mortgagedProperties;
        private List<Card> cards;
        private Tile location;

        public int getId()
        {
            return id;
        }

        public int getMoney()
        {
            return money;
        }

        public string getName()
        {
            return name;
        }

        public void setMoney(int n)
        {
            money = n;
        }

        public List<Property> getproperties()
        {
            return properties;
        }

        public List<Property> getMortagedProperties()
        {
            return mortgagedProperties;
        }

        public List<Card> getCards()
        {
            return cards;
        }

        public void addProperty(Property p)
        {
            properties.Add(p);
        }

        public void removeProperty(Property p)
        {
            properties.Remove(p);
        }

        public void addMortgagedProperty(Property p)
        {
            mortgagedProperties.Add(p);
        }

        public void removeMortgagedProperty(Property p)
        {
            mortgagedProperties.Remove(p);
        }

        public void addCard(Card c)
        {
            cards.Add(c);
        }

        public void removeCard(Card c)
        {
            cards.Remove(c);
        }

        public void setInJail(bool state)
        {
            inJail = state;
        }

        public bool getJailState()
        {
            return inJail;
        }

        public Tile getLocation()
        {
            return location;
        }

        public void setLocation(Tile tile)
        {
            location = tile;
        }

        public Player(int pId, string pName, string pColor, Tile pTile)
        {
            id = pId;
            name = pName;
            color = pColor;
            money = 1500;
            inJail = false;
            turnInJail = 0;
            properties = new List<Property>();
            mortgagedProperties = new List<Property>();
            cards = new List<Card>();
            location = pTile;
        }

        public void describe()
        {
            Console.WriteLine("Id : " + id.ToString() + " - name : " + name + " - color : " + color + " - money : " + money.ToString() + " - inJail : " + inJail.ToString());
            Console.Write("Location : ");
            location.describe();
            Console.WriteLine("Properties : ");
            foreach (Property p in properties)
            {
                p.describe();
            }
            Console.WriteLine("Cards : ");
            foreach (Card c in cards)
            {
                c.describe();
            }
        }
    }
}