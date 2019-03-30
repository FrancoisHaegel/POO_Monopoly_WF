using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    class Tile
    {
        private int index;
        private Property property;
        private TileType type;
        public enum TileType
        {
            JAIL,
            GO_TO_JAIL,
            START,
            FREE_PARKING,
            PRIVATE_PROPERTY,
            PUBLIC_COMPANY,
            CHANCE,
            COMMUNITY_CHEST,
            TAX,
            RAILROADS
        };

        public int getIndex()
        {
            return index;
        }

        public Property getProperty()
        {
            return property;
        }

        public TileType getType()
        {
            return type;
        }

        public Tile(int pId, Property pProperty, int pType)
        {
            index = pId;
            property = pProperty;
            switch (pType)
            {
                case 0:
                    type = TileType.JAIL;
                    break;
                case 1:
                    type = TileType.GO_TO_JAIL;
                    break;
                case 2:
                    type = TileType.START;
                    break;
                case 3:
                    type = TileType.FREE_PARKING;
                    break;
                case 4:
                    type = TileType.PRIVATE_PROPERTY;
                    break;
                case 5:
                    type = TileType.PUBLIC_COMPANY;
                    break;
                case 6:
                    type = TileType.CHANCE;
                    break;
                case 7:
                    type = TileType.COMMUNITY_CHEST;
                    break;
                case 8:
                    type = TileType.TAX;
                    break;
                case 9:
                    type = TileType.RAILROADS;
                    break;
            }
        }

        public void describe()
        {
            if (type == TileType.PRIVATE_PROPERTY || type == TileType.PUBLIC_COMPANY || type == TileType.RAILROADS)
                Console.WriteLine("Id : " + index + " - type : " + type + " - property : " + property.getName());
            else
                Console.WriteLine("Id : " + index + " - type : " + type);
        }
    }
}