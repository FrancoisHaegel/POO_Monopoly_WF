using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;

namespace Monopoly.controller
{
    class BoardManager
    {
        private static List<Tile> board;
        public static int boardSize = 40;

        public List<Tile> getBoard()
        {
            return board;
        }

        public Tile getStart()
        {
            return board[0];
        }

        public Tile getJail()
        {
            return board[10];
        }

        public void init(List<PrivateProperty> privates, List<Railroad> railroads, List<Utility> utilities)
        {
            board = new List<Tile>();
            int privatesCount = 0;
            int railRoadsCount = 0;
            int utilitiesCount = 0;
            for (int i = 0; i < boardSize; i++)
            {
                if (i == 0)
                {
                    board.Add(new Tile(i, null, 2));
                }
                else if (i == 10)
                {
                    board.Add(new Tile(i, null, 0));
                }
                else if (i == 20)
                {
                    board.Add(new Tile(i, null, 3));
                }
                else if (i == 30)
                {
                    board.Add(new Tile(i, null, 1));
                }
                else if (i % 10 == 5)
                {
                    board.Add(new Tile(i, railroads[railRoadsCount], 9));
                    railRoadsCount++;
                }
                else if (i == 12 || i == 28)
                {
                    board.Add(new Tile(i, utilities[utilitiesCount], 5));
                    utilitiesCount++;
                }
                else if (i == 2 || i == 17 || i == 31)
                {
                    board.Add(new Tile(i, null, 7));
                }
                else if (i == 7 || i == 22 || i == 36)
                {
                    board.Add(new Tile(i, null, 6));
                }
                else if (i == 4 || i == 38)
                {
                    board.Add(new Tile(i, null, 8));
                }
                else
                {
                    board.Add(new Tile(i, privates[privatesCount], 4));
                    privatesCount++;
                }
            }
        }

        public void describe()
        {
            Form_board.GetInstance.insert_console("Description du plateau : ");
            foreach (Tile t in board)
            {
                t.describe();
            }
        }

        public static Tile getTile(int index)
        {
            foreach (Tile t in board)
            {
                if (t.getIndex() == index) return t;
            }

            return null;
        }
    }
}