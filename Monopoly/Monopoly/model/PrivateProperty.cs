using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class PrivateProperty : Property
    {
        private string color;
        private int houseCost;

        public string getColor()
        {
            return color;
        }

        public int getHouseCost()
        {
            return houseCost;
        }

        public PrivateProperty(int pId, int pPrice, string pName, int[] pRent, string pColor, int pHouseCost) : base(pId, pPrice, pName, pRent, 0)
        {
            color = pColor;
            houseCost = pHouseCost;
        }

        public bool sameStreetAs(PrivateProperty p)
        {
            return color == p.getColor();
        }

        public static PrivateProperty FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            int id = int.Parse(values[0]);
            string name = values[1];
            string color = values[2];
            int price = int.Parse(values[3]);
            int houseCost = int.Parse(values[4]);
            int[] rent = { int.Parse(values[5]), int.Parse(values[6]), int.Parse(values[7]), int.Parse(values[8]), int.Parse(values[9]), int.Parse(values[10]) };


            PrivateProperty property = new PrivateProperty(id, price, name, rent, color, houseCost);
            return property;
        }

        override
        public void describe()
        {
            string rentToString = string.Join(",", this.getRent());
            Form_board.GetInstance.insert_console(this.getType() + " - " + this.getId().ToString() + " - " + this.getName() + " - " + this.getColor() + " - " + this.getPrice().ToString() + " - " + this.getHouseCost().ToString() + " - " + rentToString + " - housesCount : " + getHousesCount().ToString() + " - currentRent : " + getCurrentRent().ToString());
        }
    }
}