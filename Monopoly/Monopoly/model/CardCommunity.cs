using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class CardCommunity : Card
    {
        private int id;

        override
        public int getId()
        {
            return id;
        }

        public CardCommunity(int pId, string pDescription) : base(pDescription)
        {
            id = pId;
        }

        public static CardCommunity FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            int id = int.Parse(values[0]);
            string description = values[1];

            CardCommunity card = new CardCommunity(id, description);
            return card;
        }

        override
        public void describe()
        {
            Form_board.GetInstance.insert_console("Id : " + id + " - description : " + this.getDescription());
        }
    }
}
