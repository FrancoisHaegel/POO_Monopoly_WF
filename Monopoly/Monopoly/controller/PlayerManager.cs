using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using Monopoly.exception;
using System.Drawing;

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

        public void init(Dictionary<string, Color> playerParams, Tile tile)
        {
            foreach (KeyValuePair<string, Color> player in playerParams)
            {
                createPlayer(player.Key, player.Value, tile);
            }
        }

        public int createPlayer(string name, Color color, Tile tile)
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
            Form_board.GetInstance.insert_console("Descirption des Players :");
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
                Form_board.GetInstance.insert_console("Err - Playermanager.giveMoney : " + e);
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
                if (prop.getType() == model.Property.PropType.PRIVATE)
                {
                    Form_board.GetInstance.addPropriete(player.getName(), prop.getName(), Color.FromName(((PrivateProperty)prop).getColor()), prop.Tile.getIndex());
                }
                else
                {
                    if (prop.getType() == Property.PropType.RAILROAD)
                    {
                        foreach (Property p in player.getproperties())
                        {
                            if (p.getType() == prop.getType() && p != prop)
                            {
                                p.upgradeCurrentRent();
                                prop.upgradeCurrentRent();
                            }
                        }
                    }
                    if (prop.getType() == Property.PropType.UTILITY)
                    {
                        foreach (Property p in player.getproperties())
                        {
                            if (p.getType() == prop.getType() && p != prop)
                            {
                                p.upgradeCurrentRent();
                                prop.upgradeCurrentRent();
                            }
                        }
                        Form_board.GetInstance.addPropriete(player.getName(), prop.getName(), Color.WhiteSmoke, prop.Tile.getIndex());
                    }
                }
            }
            catch (Exception e)
            {
                Form_board.GetInstance.insert_console("Err - Playermanager.giveProperty : " + e);
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
                if (prop.getType() == Property.PropType.RAILROAD)
                {
                    foreach (Property p in player.getproperties())
                    {
                        if (p.getType() == Property.PropType.RAILROAD)
                        {
                            p.downgradeCurrentRent();
                            prop.downgradeCurrentRent();
                        }
                    }
                }
                if (prop.getType() == Property.PropType.UTILITY)
                {
                    foreach (Property p in player.getproperties())
                    {
                        if (p.getType() == Property.PropType.UTILITY)
                        {
                            p.downgradeCurrentRent();
                            prop.downgradeCurrentRent();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Form_board.GetInstance.insert_console("Err - Playermanager.takeProperty : " + e);
                return false;
            }
            return true;
        }

        public bool giveMortgagedProperty(Player player, Property prop)
        {
            try
            {
                player.addMortgagedProperty(prop);
            }
            catch (Exception e)
            {
                Form_board.GetInstance.insert_console("Err - Playermanager.giveMortgagedProperty : " + e);
                return false;
            }
            return true;
        }

        public bool takeMortgagedProperty(Player player, Property prop)
        {
            try
            {
                player.removeMortgagedProperty(prop);
            }
            catch (Exception e)
            {
                Form_board.GetInstance.insert_console("Err - Playermanager.takeMortgagedProperty : " + e);
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
                Form_board.GetInstance.insert_console("Err - Playermanager.giveCard : " + e);
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
                Form_board.GetInstance.insert_console("Err - Playermanager.takeCard : " + e);
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
                Form_board.GetInstance.insert_console("Err - Playermanager.sendToJail : " + e);
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
                Form_board.GetInstance.insert_console("Err - Playermanager.getOutOfJail : " + e);
                return false;
            }
            return true;
        }

        public Tile moovePlayer(Player player, int distance)
        {
            int previousTile = player.getLocation().getIndex();

            player.setLocation(BoardManager.getTile((player.getLocation().getIndex() + distance) % 40));

            int currentTile = player.getLocation().getIndex();

            if (currentTile < previousTile)
            {
                Form_board.GetInstance.insert_console("Vous êtes passé par la case Départ, vous récoltez 200$");
                giveMoney(player, 200);
            }
            
            return player.getLocation();
        }

        public Tile moovePlayer(Player player, Tile tile)
        {
            int previousTile = player.getLocation().getIndex();

            player.setLocation(tile);

            int currentTile = player.getLocation().getIndex();

            if (currentTile < previousTile)
            {
                Form_board.GetInstance.insert_console("Vous êtes passé par la case Départ, vous récoltez 200$");
                giveMoney(player, 200);
            }
            return player.getLocation();
        }

        public void mortgage(Player player, Property property)
        {
            if(property.getHousesCount() == 0)
            {
                giveMoney(player, property.getPrice() / 2);
                takeProperty(player, property);
                giveMortgagedProperty(player, property);
                property.setMortgaged(true);
            }
            else
            {
                throw new ImpossibleMortgageException();
            }
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
                takeMortgagedProperty(player, property);
                giveProperty(player, property);
                property.setMortgaged(false);
            }
        }

        public void killPlayer(Player killed, Player killer)
        {
            if(killer != null)
            {
                foreach(Property p in killed.getproperties())
                {
                    giveProperty(killer, p);
                }
                foreach (Property p in killed.getMortagedProperties())
                {
                    giveMortgagedProperty(killer, p);
                }
                foreach (Card c in killed.getCards())
                {
                    giveCard(killer, c);
                }
            }
            else{
                foreach (Property p in killed.getproperties())
                {
                    takeProperty(killed, p);
                }
                foreach (Property p in killed.getMortagedProperties())
                {
                    takeMortgagedProperty(killed, p);
                }
                foreach (Card c in killed.getCards())
                {
                    takeCard(killed, c);
                }
            }
            players.Remove(killed);
            foreach(Player p in players)
            {
                if (p.getId() > killed.getId())
                {
                    p.setId(p.getId() - 1);
                }
            }
        }
    }
}
