using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class Railroad : Property
    {
        public Railroad(int pId, int pPrice, string pName, int[] pRent) : base(pId, pPrice, pName, pRent, 1)
        {

        }

        public static Railroad FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            int id = int.Parse(values[0]);
            string name = values[1];
            int price = int.Parse(values[2]);
            int[] rent = { int.Parse(values[3]), int.Parse(values[4]), int.Parse(values[5]), int.Parse(values[6]) };


            Railroad railroads = new Railroad(id, price, name, rent);
            return railroads;
        }

        override
        public void describe()
        {
            string rentToString = string.Join(",", this.getRent());
            Console.WriteLine(this.getType() + " - " + this.getId().ToString() + " - " + this.getName() + " - " + this.getPrice().ToString() + " - " + rentToString);
        }
    }
}