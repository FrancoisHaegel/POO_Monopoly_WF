using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    public class Player
    {
        private int id;
        private string name;
        private Color color;
        private int money;
        private bool inJail;
        public int turnInJail;
        private List<Property> properties;
        private List<Property> mortgagedProperties;
        private List<Card> cards;
        private Tile location;
        
        public int getTurnInJail()
        {
            return turnInJail;
        }

        public Color Color
        {
            get
            {
                return color;
            }
        }

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

        public void setId(int i)
        {
            id = i;
        }

        public void resetTurnInJail()
        {
            turnInJail = 0;
        }

        public void increaseTurnInJail()
        {
            turnInJail++;
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

        public Player(int pId, string pName, Color pColor, Tile pTile)
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
            Form_board.GetInstance.insert_console("Id : " + id.ToString() + " - name : " + name + " - color : " + color + " - money : " + money.ToString() + " - inJail : " + inJail.ToString());
            Form_board.GetInstance.insert_console("Location : ");
            location.describe();
            Form_board.GetInstance.insert_console("Properties : ");
            foreach (Property p in properties)
            {
                p.describe();
            }
            Form_board.GetInstance.insert_console("Cards : ");
            foreach (Card c in cards)
            {
                c.describe();
            }
        }
    }
}