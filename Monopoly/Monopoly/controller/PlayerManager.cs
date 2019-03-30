using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using Monopoly.exception;

namespace Monopoly.controller
{
    class PlayerManager
    {
        private static int nextId = 0;
        private static List<Player> players = new List<Player>();
        private Player currentPlayer;

        public Player getCurrentPlayer()
        {
            return currentPlayer;
        }

        public void setCurrentPlayer(Player p)
        {
            currentPlayer = p;
        }

        public void init(Dictionary<string, string> playerParams, Tile tile)
        {
            foreach (KeyValuePair<string, string> player in playerParams)
            {
                createPlayer(player.Key, player.Value, tile);
            }
        }

        public int createPlayer(string name, string color, Tile tile)
        {
            players.Add(new Player(nextId, name, color, tile));

            nextId++;
            return nextId - 1;
        }

        public Player getPlayer(int id)
        {
            foreach (Player p in players)
            {
                if (p.getId() == id) return p;
            }

            return null;
        }

        public List<Player> getPlayers()
        {
            return players;
        }

        public void describe()
        {
            Console.WriteLine("Descirption des Players :");
            foreach (Player p in players)
            {
                p.describe();
            }
        }

        public bool giveMoney(Player p, int amount)
        {
            try
            {
                p.setMoney(p.getMoney() + amount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.giveMoney : " + e);
                return false;
            }
            return true;
        }

        public bool takeMoney(Player player, int amount)
        {
            if (player.getMoney() >= amount)
            {
                player.setMoney(player.getMoney() - amount);
            }
            else
            {
                throw (new NotEnoughMoneyException());
            }
            return true;
        }

        public bool giveProperty(Player player, Property prop)
        {
            try
            {
                player.addProperty(prop);
                prop.setOwner(player);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.giveProperty : " + e);
                return false;
            }
            return true;
        }

        public bool takeProperty(Player player, Property prop)
        {
            try
            {
                player.removeProperty(prop);
                prop.setOwner(null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.takeProperty : " + e);
                return false;
            }
            return true;
        }

        public bool giveCard(Player player, Card card)
        {
            try
            {
                player.addCard(card);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.giveCard : " + e);
                return false;
            }
            return true;
        }

        public bool takeCard(Player player, Card card)
        {
            try
            {
                player.removeCard(card);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.takeCard : " + e);
                return false;
            }
            return true;
        }

        public bool sendToJail(Player player)
        {
            try
            {
                player.setInJail(true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.sendToJail : " + e);
                return false;
            }
            return true;
        }

        public bool getOutOfJail(Player player)
        {
            try
            {
                player.setInJail(false);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err - Playermanager.getOutOfJail : " + e);
                return false;
            }
            return true;
        }

        public Tile moovePlayer(Player player, int distance)
        {
            player.setLocation(BoardManager.getTile((player.getLocation().getIndex() + distance) % 40));
            return player.getLocation();
        }

        public Tile moovePlayer(Player player, Tile tile)
        {
            player.setLocation(tile);
            return player.getLocation();
        }

        public void mortgage(Player player, Property property)
        {
            giveMoney(player, property.getPrice() / 2);
            player.removeProperty(property);
            player.addMortgagedProperty(property);
            property.setMortgaged(true);
        }

        public void unmortgage(Player player, Property property)
        {
            double price = property.getPrice() / 2 * 1.1;
            if (player.getMoney() < price)
            {
                throw new NotEnoughMoneyException();
            }
            else
            {
                takeMoney(player, int.Parse(price.ToString()));
                player.removeMortgagedProperty(property);
                player.addProperty(property);
                property.setMortgaged(false);
            }
        }
    }
}
