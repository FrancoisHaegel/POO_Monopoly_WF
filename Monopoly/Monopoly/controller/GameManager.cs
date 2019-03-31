using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using Monopoly.exception;
using System.Drawing;
using System.Windows.Forms;

namespace Monopoly.controller
{
    class GameManager
    {
        private static GameManager instance;

        public PlayerManager playerManager = new PlayerManager();
        public BoardManager boardManager = new BoardManager();
        public CardManager cardManager = new CardManager();
        public PropertyManager propertyManager = new PropertyManager();

        private static string pathToPrivateProperty = "../../locale/privateProperties.csv";
        private static string pathToRailroads = "../../locale/railroads.csv";
        private static string pathToUtilities = "../../locale/utilities.csv";
        private static string pathToChance = "../../locale/chanceCards.csv";
        private static string pathToCommunity = "../../locale/communityCards.csv";

        private const int MAX_HOUSES = 32;
        private const int MAX_HOTELS = 12;

        private int housesCount = 0;
        private int hotelsCount = 0;

        private static Random random = new Random();
        private bool dicesRolled = false;
        private int currentPlayerRerollsCount = 0;

        public static GameManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void Init(Dictionary<string, Color> players)
        {
            propertyManager.init(pathToPrivateProperty, pathToRailroads, pathToUtilities);
            boardManager.init(propertyManager.getPrivateProperties(), propertyManager.getRailRoads(), propertyManager.getUtilityCompanies());
            playerManager.init(players, boardManager.getStart());
            cardManager.init(pathToCommunity, pathToChance);
        }

        public bool isOver()
        {
            return !(playerManager.getPlayers().Count() > 1);
        }

        public void NextTurn()
        {
            if(playerManager.getPlayers().Count() != 1)
            {
                playerManager.setCurrentPlayer(playerManager.getPlayers()[(playerManager.getCurrentPlayer().getId() + 1) % playerManager.getPlayers().Count()]);
                Form_board.GetInstance.insert_console("C'est au tour de " + playerManager.getCurrentPlayer().getName());
                Form_board.GetInstance.insert_console("Faire un choix d'action");

                dicesRolled = false;
                currentPlayerRerollsCount = 0;
            }
            else
            {
                endGame();
            }
            
        }

        public void startGame()
        {
            Form_board.GetInstance.insert_console("Il y a " + playerManager.getPlayers().Count().ToString() + " joueurs dans la partie");

            playerManager.setCurrentPlayer(playerManager.getPlayers()[0]);

            NextTurn();
        }

        public void endGame()
        {
            MessageBox.Show("Fin de la partie, le gagnant est " + playerManager.getPlayers()[0].getName() + " !");
        }

        public void clickRollDices()
        {

            if (!dicesRolled)
            {
                int[] dices = rollDices();
                Form_board.GetInstance.showDices(dices[0], dices[1]);
                Form_board.GetInstance.insert_console(playerManager.getCurrentPlayer().getName() + " fait un " + dices[0].ToString() + " et un " + dices[1].ToString());
                if (playerManager.getCurrentPlayer().getJailState())
                {
                    jailedPlayerRollDice(dices);
                }
                else
                {
                    freePlayerRollDice(dices, false);
                }
            }
            else
            {
                MessageBox.Show("Vous avez déjà lancé les dés ce tour");
            }
        }

        public void clickEndTurn()
        {
            if (dicesRolled)
                NextTurn();
            else
                MessageBox.Show("Vous êtes obligé de lancer les dés pour finir votre tour");
        }

        public void clickMortgage(int index)
        {
            Property p = boardManager.getBoard()[index].getProperty();
            askMortgage(p.getOwner(), p);
        }

        public void clickUnMortgage(int index)
        {
            Property p = boardManager.getBoard()[index].getProperty();
            askUnMortgage(p.getOwner(), p);
        }

        public void clickBuyHouse(int index)
        {
            Property p = boardManager.getBoard()[index].getProperty();
            buyHouse(p.getOwner(), (PrivateProperty)p);
        }

        public void clickSellHouse(int index)
        {
            Property p = boardManager.getBoard()[index].getProperty();
            sellHouse(p.getOwner(), (PrivateProperty)p);
        }

        public void clickGetOutOfJail()
        {
            if(playerManager.getCurrentPlayer().getCards().Count() == 0)
            {
                Form_board.GetInstance.insert_console(playerManager.getCurrentPlayer().getName() + " paye 50$ pour sortir de prison");
                payTax(playerManager.getCurrentPlayer(), 50);
            }
            else
            {
                Form_board.GetInstance.insert_console(playerManager.getCurrentPlayer().getName() + " utilise sa carte magique pour sortir de prison");
                playerManager.getCurrentPlayer().getCards().RemoveAt(0);
            }
            
        }

        public static int[] rollDices()
        {
            int dice1 = random.Next(1, 6);
            int dice2 = random.Next(1, 6);

            int[] res = { dice1, dice2 };
            return res;
        }

        public void freePlayerRollDice(int[] dices, bool jailedOut)
        {
            if (dices[0] != dices[1] || (dices[0] == dices[1] && currentPlayerRerollsCount < 2))
            {
                if (dices[0] != dices[1] || jailedOut)
                {
                    dicesRolled = true;
                }
                else
                {
                    currentPlayerRerollsCount++;
                    Form_board.GetInstance.insert_console("Vous avez fait un double, vous pouvez relancer les dés");
                }

                playerManager.moovePlayer(playerManager.getCurrentPlayer(), dices[0] + dices[1]);
                
                if (playerManager.getCurrentPlayer().getLocation().getProperty() != null)
                {
                    landOnProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
                }
                else
                {
                    if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.START)
                    {
                        Form_board.GetInstance.insert_console("Vous etes atterit sur la case Départ, vou collectez 200$");
                        playerManager.giveMoney(playerManager.getCurrentPlayer(), 200);
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.JAIL)
                    {
                        Form_board.GetInstance.insert_console("Vous êtes en prison, vous ne pouvez plus avancer");
                        sendToJail(playerManager.getCurrentPlayer());
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.GO_TO_JAIL)
                    {
                        Form_board.GetInstance.insert_console("Direction la prison !");
                        sendToJail(playerManager.getCurrentPlayer());
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.FREE_PARKING)
                    {
                        Form_board.GetInstance.insert_console("Vous pouvez restez ici tranquillement");
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.CHANCE)
                    {
                        Card card = cardManager.drawCard(Card.CardType.CHANCE);
                        if (card.getId() == 7)
                        {
                            Form_board.GetInstance.insert_console("La carte \"Get out of jail\" a été ajoutée à vos cartes");
                            playerManager.giveCard(playerManager.getCurrentPlayer(), cardManager.getChanceCard(7));
                        }
                        else
                        {
                            chanceAction(card.getId());
                        }
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.COMMUNITY_CHEST)
                    {
                        Card card = cardManager.drawCard(Card.CardType.COMMUNITY);
                        if (card.getId() == 8)
                        {
                            Form_board.GetInstance.insert_console("La carte \"Get out of jail\" a été ajoutée à vos cartes");
                            playerManager.giveCard(playerManager.getCurrentPlayer(), cardManager.getChanceCard(8));
                        }
                        else
                        {
                            communityAction(card.getId());
                        }
                    }
                    else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.TAX)
                    {
                        Form_board.GetInstance.insert_console("Vous devez payez la tax de richesse qui est de 100$");
                        payTax(playerManager.getCurrentPlayer(), 100);
                    }
                }
            }
            else
            {
                Form_board.GetInstance.insert_console("Vous avez fait 3 double à la suite, vous allez en prison et votre tour est terminé !");
                sendToJail(playerManager.getCurrentPlayer());
                NextTurn();
            }
        }

        public void jailedPlayerRollDice(int[] dices)
        {
            dicesRolled = true;

            if(dices[0] == dices[1])
            {
                Form_board.GetInstance.insert_console("Vous avez fait un double, vous sortez de prison !");
                playerManager.getOutOfJail(playerManager.getCurrentPlayer());
                playerManager.getCurrentPlayer().resetTurnInJail();
                freePlayerRollDice(dices, true);
            }
            else
            {
                playerManager.getCurrentPlayer().increaseTurnInJail();
                if(playerManager.getCurrentPlayer().getTurnInJail() == 3)
                {
                    Form_board.GetInstance.insert_console("C'est votre 3e tour en prison, vous êtes obligé de payer 50$");
                    payTax(playerManager.getCurrentPlayer(), 50);
                    playerManager.getOutOfJail(playerManager.getCurrentPlayer());
                    playerManager.getCurrentPlayer().resetTurnInJail();
                    freePlayerRollDice(dices, true);
                }
                else
                {
                    Form_board.GetInstance.insert_console("C'est votre " + playerManager.getCurrentPlayer().getTurnInJail().ToString() + "e tour en prison");
                }
            }

        }

        //Send a player to jail
        public void sendToJail(Player player)
        {
            player.setLocation(boardManager.getJail());
            playerManager.sendToJail(player);
        }

        //When the player lands on a tile which has a property
        public void landOnProperty(Player player, Property property)
        {
            if (property.getOwner() != null)
            {
                if (property.getMortgaged() == true)
                {
                    Form_board.GetInstance.insert_console("Cette propriété est hypothéquée, vous ne payez pas de loyer");
                }
                else if(property.getType() == Property.PropType.UTILITY)
                {
                    payUtilityRent(player, property);
                }
                else
                {
                    payPrivateRent(player, property);
                }
            }else if(property.getOwner() == player)
            {
                Form_board.GetInstance.insert_console("Cette propriété vous appartient");
            }
            else
            {
                askForPurchase(player, property);
            }
        }

        public void purchase(Player player, Property property, bool purchase)
        {
            if (purchase)
            {
                try
                {
                    playerManager.takeMoney(player, property.getPrice());
                    playerManager.giveProperty(player, property);
                }
                catch (NotEnoughMoneyException)
                {
                    Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent pour acheter cette propriété");
                }
            }
            else
            {
                if (playerManager.getPlayers().Count() < 3)
                {
                    Form_board.GetInstance.insert_console("Pas assez de joueur pour commencer un enchère");
                }
                else
                {
                    Form_board.GetInstance.popupEnchere(property.Tile.getIndex());
                }
            }
        }

        //Ask the player if he wants to buy an ownerless property
        public void askForPurchase(Player player, Property property)
        {
            Form_board.GetInstance.popupCarte(property.Tile.getIndex(), true);
        }

        //Buy a house when the player owns a whole street
        public void buyHouse(Player player, PrivateProperty property)
        {
            bool getThewholeStreet = true;
            foreach (PrivateProperty p in propertyManager.getPrivateProperties())
            {
                if (property.sameStreetAs(p))
                {
                    if (p.getOwner() == null || p.getOwner().getId() != property.getOwner().getId())
                    {
                        getThewholeStreet = false;
                    }
                }
            }
            if (housesCount < MAX_HOUSES)
            {
                if (getThewholeStreet)
                {
                    if (property.getHousesCount() < 5)
                    {
                        if (property.getHousesCount() < 4)
                        {
                            try
                            {
                                playerManager.takeMoney(player, property.getHouseCost());
                                property.upgradeCurrentRent();
                                housesCount++;
                            }
                            catch (NotEnoughMoneyException)
                            {
                                MessageBox.Show("Vous n'avez pas assez d'argent pour acheter une maison");
                            }
                        }
                        else
                        {
                            if (hotelsCount < MAX_HOTELS)
                            {
                                try
                                {
                                    playerManager.takeMoney(player, property.getHouseCost());
                                    property.upgradeCurrentRent();
                                    hotelsCount++;
                                }
                                catch (NotEnoughMoneyException)
                                {
                                    MessageBox.Show("Vous n'avez pas assez d'argent pour acheter l'hotel");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Le nombre d'hotels maximum est déjà atteint");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous avez déjà un hotel sur cette propriété, c'est le maximum");
                    }

                }
                else
                {
                    MessageBox.Show("Vous n'avez pas toutes propriétés de cette rue");
                }
            }
            else
            {
                MessageBox.Show("Le nombre de maisons maximum est déjà atteint");
            }

        }

        public void sellHouse(Player player, PrivateProperty property)
        {
            if (property.getHousesCount() > 0)
            {
                if (property.getHousesCount() == 5)
                {
                    Form_board.GetInstance.insert_console("Vous vendez l'hotel sur cette propriété pour " + (property.getHouseCost() * 4 / 2).ToString() + "$");
                    playerManager.giveMoney(player, property.getHouseCost() * 6 / 2);
                    property.resetCurrentRent();
                }
                else
                {
                    Form_board.GetInstance.insert_console("Vous vendez une maison pour " + (property.getHouseCost() / 2).ToString() + "$ sur cette propriété");
                    playerManager.giveMoney(player, property.getHouseCost() / 2);
                    property.downgradeCurrentRent();
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez pas de maisons sur cette propriété");
            }
        }

        //Make the player pay his rent when he lands on a tile which has property already owned
        public void payPrivateRent(Player player, Property property)
        {
            Form_board.GetInstance.insert_console(property.getOwner().getName() + " detient cette propriété, vous devez lui donner " + property.getCurrentRent().ToString() + "$");
            try
            {
                playerManager.takeMoney(player, property.getCurrentRent());
                playerManager.giveMoney(property.getOwner(), property.getCurrentRent());
            }
            //If the player hasn't enough money to pay the rent
            catch (NotEnoughMoneyException)
            {
                MessageBox.Show("Vous n'avez pas assez d'argent, vous etes forcé de faire une ipothèque");
                try
                {
                    forceMortgage(player, property.getCurrentRent() - player.getMoney());
                }
                //If the player can't mortgage enough to pay his dept
                catch (ImpossibleMortgageException)
                {
                    MessageBox.Show("Vous ne pouvez pas faire d'ipothèque, vous avez perdu la partie !");
                    NextTurn();
                    playerManager.killPlayer(player, property.getOwner());
                }
            }
        }

        public void payUtilityRent(Player player, Property property)
        {
            Form_board.GetInstance.insert_console("Cette compagnie est publique, lancez un dé, puis payer " + property.getCurrentRent().ToString() + " fois le résultat");
            int dice = rollDices()[0];
            Form_board.GetInstance.showDices(dice);
            Form_board.GetInstance.insert_console("Vous avez fait un " + dice.ToString());
            try
            {
                playerManager.takeMoney(player, property.getCurrentRent() * dice);
                playerManager.giveMoney(property.getOwner(), property.getCurrentRent() * dice);
            }
            catch (NotEnoughMoneyException)
            {
                MessageBox.Show("Vous n'avez pas assez d'argent, vous etes forcé de faire une ipothèque");
                try
                {
                    forceMortgage(player, property.getCurrentRent() - player.getMoney());
                }
                //If the player can't mortgage enough to pay his dept
                catch (ImpossibleMortgageException)
                {
                    MessageBox.Show("Vous ne pouvez pas faire d'ipothèque, vous avez perdu la partie !");
                    NextTurn();
                    playerManager.killPlayer(player, property.getOwner());
                }
            }
        }

        public int playerPropertiesValue(Player player)
        {
            int res = 0;
            foreach (Property p in player.getproperties())
            {
                if (p.getType() == Property.PropType.PRIVATE)
                {
                    if (p.getHousesCount() == 5)
                    {
                        res += (((PrivateProperty)p).getHouseCost() * 6 / 2);
                    }
                    else
                    {
                        res += (((PrivateProperty)p).getHouseCost() * p.getHousesCount());
                    }
                }
                else
                {
                    res += (p.getPrice() / 2);
                }
            }
            return res;
        }

        public void askMortgage(Player player, Property property)
        {
            if(property.getHousesCount() == 0 || property.getType() == Property.PropType.RAILROAD || property.getType() == Property.PropType.UTILITY)
            {
                playerManager.mortgage(player, property);
                Form_board.GetInstance.insert_console("Votre nouveau solde est de : " + player.getMoney().ToString() + "$");
            }
            else
            {
                MessageBox.Show("Vous ne pouvez pas hypothéquer une propriété qui a des maisons");
            }                 
        }

        public void forceMortgage(Player player, int toPay)
        {
            if (playerPropertiesValue(player) >= toPay)
            {
                MessageBox.Show(player.getName() + " est endetté, il est obligé d'hypotéquer avant que la partie ne puisse continuée");
                //Form_board.FAIRE_UNE_PAUSE();
            }
            else
            {
                throw new ImpossibleMortgageException();
            }
        }

        public void askUnMortgage(Player player, Property property)
        {
            try
            {
                playerManager.unmortgage(player, property);
            }
            catch (NotEnoughMoneyException)
            {
                MessageBox.Show("Vous n'avez pas assez d'argent pour racheter cette propriété");
            }
        }

        public void payTax(Player player, int amount)
        {
            try
            {
                playerManager.takeMoney(player, amount);
            }
            //Si le joueur n'a pas assez d'argent pour payer son loyer
            catch (NotEnoughMoneyException)
            {
                MessageBox.Show("Vous n'avez pas assez d'argent, vous etes forcé de faire une ipothèque");
                try
                {
                    forceMortgage(player, amount - player.getMoney());
                }
                //Si le joueur n'a plus de propriété à ipothéquer ou pas assez
                catch (ImpossibleMortgageException)
                {
                    MessageBox.Show("Vous ne pouvez pas faire d'ipothèque, vous avez perdu la partie !");
                    NextTurn();
                    playerManager.killPlayer(player, null);
                }
            }
        }

        public void startBid(Property property)
        {
            Form_board.GetInstance.insert_console("Début des enchères entre ");
            for (int i = 0; i < playerManager.getPlayers().Count() - 1; i++)
            {
                Form_board.GetInstance.insert_console(playerManager.getPlayers()[i].getName() + ", ");
            }
            Form_board.GetInstance.insert_console("et " + playerManager.getPlayers()[playerManager.getPlayers().Count() - 1].getName());
            Dictionary<Player, int> bids = new Dictionary<Player, int>();
            foreach (Player p in playerManager.getPlayers())
            {
                int x = 0;
                bool invalidInput = true;
                while (invalidInput)
                    if (int.TryParse(Console.ReadLine(), out x))
                    {
                        if (p.getMoney() > x)
                        {
                            bids.Add(p, x);
                            invalidInput = false;
                        }
                        else
                        {
                            MessageBox.Show("Vous ne pouvez pas proposer plus que ce que contient votre porte-monnaie");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Entrez une valeur valide pour l'enchère");
                    }
            }
            Player player = bids.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            int amount = bids.Values.Max();
            Form_board.GetInstance.insert_console("Le gagnant de l'enchère est " + player.getName() + " qui a fait une proposition à " + amount.ToString());
            playerManager.takeMoney(player, amount);
            playerManager.giveProperty(player, property);
        }

        public void generalRepairs(Player player, int houseAmount, int hotelAmout)
        {
            int amount = 0;
            foreach(Property p in player.getproperties())
            {
                if(p.getType() == Property.PropType.PRIVATE)
                {
                    if(p.getHousesCount() == 5)
                    {
                        amount += hotelAmout;
                    }
                    else
                    {
                        amount += p.getHousesCount() * houseAmount;
                    }
                }
            }
            payTax(player, amount);
        }

        public void mooveToNext(Player player, Property.PropType propType)
        {
            foreach (Tile t in boardManager.getBoard())
            {
                if (t.getIndex() > player.getLocation().getIndex())
                {
                    if (t.getProperty() != null)
                    {
                        if (t.getProperty().getType() == propType)
                        {
                            playerManager.moovePlayer(player, t);
                            landOnProperty(player, t.getProperty());
                            break;
                        }
                    }
                }
            }
        }

        public void payEachPlayer(Player player, int amount)
        {
            foreach(Player p in playerManager.getPlayers())
            {
                if(p != player)
                {
                    playerManager.giveMoney(p, amount);
                }
            }
            payTax(player, amount * playerManager.getPlayers().Count());
        }

        public void collectFromEachPlayer(Player player, int amount)
        {
            playerManager.giveMoney(player, playerManager.getPlayers().Count() * amount);
            foreach (Player p in playerManager.getPlayers())
            {
                if (p != player)
                {
                    payTax(p, amount);
                }
            }
        }

        public void chanceAction(int id)
        {
            switch (id)
            {
                case 0:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(0).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), boardManager.getStart());
                    break;
                case 1:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(1).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), BoardManager.getTile(11));
                    landOnProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
                    break;
                case 2:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(2).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), BoardManager.getTile(24));
                    landOnProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
                    break;
                case 3:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(3).getDescription());
                    mooveToNext(playerManager.getCurrentPlayer(), Property.PropType.UTILITY);
                    break;
                case 4:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(4).getDescription());
                    mooveToNext(playerManager.getCurrentPlayer(), Property.PropType.RAILROAD);
                    break;
                case 5:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(5).getDescription());
                    mooveToNext(playerManager.getCurrentPlayer(), Property.PropType.RAILROAD);
                    break;
                case 6:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(6).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 50);
                    break;
                case 8:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(8).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), -3);

                    //on reprend 200 car moove player donne 200 si la nouvelle position est inférieur à l'ancienne
                    playerManager.takeMoney(playerManager.getCurrentPlayer(), 200);
                    break;
                case 9:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(9).getDescription());
                    sendToJail(playerManager.getCurrentPlayer());
                    NextTurn();
                    break;
                case 10:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(10).getDescription());
                    generalRepairs(playerManager.getCurrentPlayer(), 25, 100);
                    break;
                case 11:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(11).getDescription());
                    payTax(playerManager.getCurrentPlayer(), 15);
                    break;
                case 12:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(12).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), BoardManager.getTile(5));
                    landOnProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
                    break;
                case 13:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(13).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), BoardManager.getTile(39));
                    landOnProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
                    break;
                case 14:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(13).getDescription());
                    payEachPlayer(playerManager.getCurrentPlayer(), 50);
                    break;
                case 15:
                    Form_board.GetInstance.insert_console(cardManager.getChanceCard(15).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 150);
                    break;
            }
        }

        public void communityAction(int id)
        {
            switch (id)
            {
                case 0:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(0).getDescription());
                    sendToJail(playerManager.getCurrentPlayer());
                    NextTurn();
                    break;
                case 1:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(1).getDescription());
                    generalRepairs(playerManager.getCurrentPlayer(), 40, 115);
                    break;
                case 2:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(2).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 100);
                    break;
                case 3:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(3).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 200);
                    break;
                case 4:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(4).getDescription());
                    payTax(playerManager.getCurrentPlayer(), 150);
                    break;
                case 5:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(5).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 20);
                    break;
                case 6:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(6).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 100);
                    break;
                case 7:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(7).getDescription());
                    payTax(playerManager.getCurrentPlayer(), 50);
                    break;
                case 9:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(9).getDescription());
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), boardManager.getStart());
                    break;
                case 10:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(10).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 45);
                    break;
                case 11:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(11).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 100);
                    break;
                case 12:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(12).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 25);
                    break;
                case 13:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(13).getDescription());
                    collectFromEachPlayer(playerManager.getCurrentPlayer(), 50);
                    break;
                case 14:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(14).getDescription());
                    payTax(playerManager.getCurrentPlayer(), 100);
                    break;
                case 15:
                    Form_board.GetInstance.insert_console(cardManager.getCommunityCard(15).getDescription());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 100);
                    break;
            }
        }
    }
}
