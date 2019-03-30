using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using Monopoly.exception;


namespace Monopoly.controller
{
    class GameManager
    {
        private static GameManager instance;

        private PlayerManager playerManager = new PlayerManager();
        private BoardManager boardManager = new BoardManager();
        private CardManager cardManager = new CardManager();
        private PropertyManager propertyManager = new PropertyManager();

        private static string pathToPrivateProperty = "../../locales/privateProperties.csv";
        private static string pathToRailroads = "../../locales/railroads.csv";
        private static string pathToUtilities = "../../locales/utilities.csv";
        private static string pathToChance = "../../locales/chanceCards.csv";
        private static string pathToCommunity = "../../locales/communityCards.csv";

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

        public void Init()
        {
            Dictionary<string, string> players = new Dictionary<string, string>();
            Form_board.GetInstance.insert_console("Création des joueurs");
            string choice = "";
            while (choice != "OK")
            {
                Form_board.GetInstance.insert_console("Entrer un nom de joueur (string)");
                string player = Console.ReadLine();
                Form_board.GetInstance.insert_console("Entrer une couleur (string)");
                string couleur = Console.ReadLine();
                players.Add(player, couleur);

                Form_board.GetInstance.insert_console("appuyer sur ENTER pour ajouter un autre joueur, ecrire OK pour finir");
                choice = Console.ReadLine();
            }

            propertyManager.init(pathToPrivateProperty, pathToRailroads, pathToUtilities);
            boardManager.init(propertyManager.getPrivateProperties(), propertyManager.getRailRoads(), propertyManager.getUtilityCompanies());
            playerManager.init(players, boardManager.getStart());
            cardManager.init(pathToCommunity, pathToChance);

            //Test du chargement des CSV    

            //propertyManager.describe();
            //playerManager.describe();
            //cardManager.describe();
            //boardManager.describe();

            startGame();

            Console.ReadLine();



        }

        public bool isOver()
        {
            return !(playerManager.getPlayers().Count() > 1);
        }

        public void NextTurn()
        {
            Form_board.GetInstance.insert_console("C'est au tour de " + playerManager.getCurrentPlayer().getName());
            Form_board.GetInstance.insert_console("Solde du porte-monnaie : " + playerManager.getCurrentPlayer().getMoney());
            Form_board.GetInstance.insert_console("Faire un choix d'action");

            dicesRolled = false;
            currentPlayerRerollsCount = 0;
            playerManager.setCurrentPlayer(playerManager.getPlayers()[(playerManager.getCurrentPlayer().getId() + 1) % playerManager.getPlayers().Count()]);
        }

        public void startGame()
        {
            Form_board.GetInstance.insert_console("Il y a " + playerManager.getPlayers().Count().ToString() + " joueurs dans la partie");

            playerManager.setCurrentPlayer(playerManager.getPlayers()[0]);

            playerManager.giveProperty(playerManager.getCurrentPlayer(), propertyManager.getPrivateProperties()[2]);
            playerManager.giveProperty(playerManager.getCurrentPlayer(), propertyManager.getPrivateProperties()[3]);
            playerManager.giveProperty(playerManager.getCurrentPlayer(), propertyManager.getPrivateProperties()[4]);

            playerManager.getCurrentPlayer().describe();

            while (!isOver())
            {
                NextTurn();
            }

            Form_board.GetInstance.insert_console("Fin de la partie");
            Form_board.GetInstance.insert_console("Le gagnant est " + playerManager.getPlayers()[0].getName() + " !");

            Console.ReadLine();
        }

        public int getInput()
        {
            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            return x;
        }

        public static int[] rollDices()
        {
            int dice1 = random.Next(1, 6);
            int dice2 = random.Next(1, 6);

            int[] res = { dice1, dice2 };
            return res;
        }

        public void clickRollDices()
        {

            if (!dicesRolled)
            {
                int[] dices = rollDices();
                //int[] dices = { 3, 4 };
                //int[] dices = {5, 5};
                Form_board.GetInstance.showDices(dices[0], dices[1]);

                Form_board.GetInstance.insert_console("Vous avez fait un " + dices[0].ToString() + " et un " + dices[1].ToString());

                if (dices[0] != dices[1] || (dices[0] == dices[1] && currentPlayerRerollsCount < 2))
                {
                    if (dices[0] != dices[1])
                    {
                        dicesRolled = true;
                    }
                    else
                    {
                        currentPlayerRerollsCount++;
                        Form_board.GetInstance.insert_console("Vous avez fait un double, vous pouvez relancer les dés");
                    }

                    int previousTile = playerManager.getCurrentPlayer().getLocation().getIndex();

                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), dices[0] + dices[1]);

                    int currentTile = playerManager.getCurrentPlayer().getLocation().getIndex();

                    if (currentTile < previousTile)
                    {
                        Form_board.GetInstance.insert_console("Vous êtes passé par la case Départ, vous récoltez 200$");
                        playerManager.giveMoney(playerManager.getCurrentPlayer(), 200);
                    }

                    Console.Write(playerManager.getCurrentPlayer().getName() + " est atterit sur la case : ");
                    playerManager.getCurrentPlayer().getLocation().describe();

                    if (playerManager.getCurrentPlayer().getLocation().getProperty() != null)
                    {
                        landOnProperty();
                    }
                    else
                    {
                        if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.START)
                        {
                            Form_board.GetInstance.insert_console("Vous etes atterit sur la case Départ, vou collectez 200$");
                            playerManager.giveMoney(playerManager.getCurrentPlayer(), 200);
                            Form_board.GetInstance.insert_console("Nouveau solde du porte-monnaie : " + playerManager.getCurrentPlayer().getMoney().ToString());
                        }
                        else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.JAIL)
                        {
                            Form_board.GetInstance.insert_console("Vous êtes en prison, vous ne pouvez plus avancer");
                            sendToJail();
                        }
                        else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.GO_TO_JAIL)
                        {
                            Form_board.GetInstance.insert_console("Direction la prison !");
                            sendToJail();
                        }
                        else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.FREE_PARKING)
                        {
                            Form_board.GetInstance.insert_console("Vous pouvez restez ici tranquillement");
                        }
                        else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.CHANCE)
                        {
                            Card card = cardManager.drawCard(Card.CardType.CHANCE);
                            card.describe();
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
                            card.describe();
                            if (card.getId() == 8)
                            {
                                Form_board.GetInstance.insert_console("La carte \"Get out of jail\" a été ajoutée à vos cartes");
                                playerManager.giveCard(playerManager.getCurrentPlayer(), cardManager.getChanceCard(8));
                            }
                            else
                            {
                                //cardManager.communityAction(card.getId());
                            }
                        }
                        else if (playerManager.getCurrentPlayer().getLocation().getType() == Tile.TileType.TAX)
                        {
                            Form_board.GetInstance.insert_console("Vous devez payez la tax de richesse qui est de 100$");
                            try
                            {
                                playerManager.takeMoney(playerManager.getCurrentPlayer(), 100);
                            }
                            //Si le joueur n'a pas assez d'argent pour payer son loyer
                            catch (NotEnoughMoneyException)
                            {
                                Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent, vous etes forcé de faire une ipothèque");
                                try
                                {
                                    askMortgage(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty().getCurrentRent() - playerManager.getCurrentPlayer().getMoney());
                                }
                                //Si le joueur n'a plus de propriété à ipothéquer ou pas assez
                                catch (ImpossibleMortgageException)
                                {
                                    Form_board.GetInstance.insert_console("Vous ne pouvez pas faire d'ipothèque, vous avez perdu la partie !");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Form_board.GetInstance.insert_console("Vous avez fait 3 double à la suite, vous allez en prison et votre tour est terminé !");
                    sendToJail();
                }
            }
            else
            {
                Form_board.GetInstance.insert_console("Vous avez déjà lancé les dés ce tour");
            }
        }

        public void clickEndTurn()
        {
            if (dicesRolled)
                NextTurn();
            else
                Form_board.GetInstance.insert_console("Vous êtes obligé de lancer les dés pour finir votre tour");
        }

        public void printChoices()
        {
            Form_board.GetInstance.insert_console("Faites un choix !");

            switch (getInput())
            {
                // Roll the dices
                case 1:
                    
                    break;
                case 2:
                    // demander une hypothèque
                    try
                    {
                        askMortgage(playerManager.getCurrentPlayer(), 0);
                    }
                    catch (ImpossibleMortgageException)
                    {
                        Form_board.GetInstance.insert_console("Vous n'avez pas de propriété à hypothéquer");
                    }
                    break;
                case 3:
                    // demander de racheter une maison hypothéquée
                    try
                    {
                        askUnMortgage(playerManager.getCurrentPlayer());
                    }
                    catch (ImpossibleMortgageException)
                    {
                        Form_board.GetInstance.insert_console("Vous n'avez pas de propriété à racheter");
                    }

                    break;
                case 4:
                    // buy a house
                    if (playerManager.getCurrentPlayer().getproperties().Count > 0)
                    {
                        Form_board.GetInstance.insert_console("Voici la liste de vos propriétés privées : ");
                        foreach (PrivateProperty p in playerManager.getCurrentPlayer().getproperties())
                        {
                            p.describe();
                        }
                        Form_board.GetInstance.insert_console("Sur laquelle souhaitez vous construire une maison ?");
                        buyHouse(propertyManager.getPrivateProperties()[getInput()]);
                    }
                    else
                    {
                        Form_board.GetInstance.insert_console("Vous n'avez pas de propriétés");
                    }
                    break;
                case 5:
                    // sell a house
                    if (playerManager.getCurrentPlayer().getproperties().Count > 0)
                    {
                        Form_board.GetInstance.insert_console("Voici la liste de vos propriétés privées sur lesquelles vous avez des maisons : ");
                        foreach (PrivateProperty p in playerManager.getCurrentPlayer().getproperties())
                        {
                            if (p.getHousesCount() > 0)
                            {
                                p.describe();
                            }
                        }
                        Form_board.GetInstance.insert_console("Sur laquelle souhaitez vous vendre une maison ?");
                        sellHouse(propertyManager.getPrivateProperties()[getInput()]);
                    }
                    else
                    {
                        Form_board.GetInstance.insert_console("Vous n'avez pas de propriétés");
                    }
                    break;
                default:
                    Form_board.GetInstance.insert_console("Invalid Input");
                    getInput();
                    break;
            }
        }

        //Send a player to jail
        public void sendToJail()
        {
            playerManager.getCurrentPlayer().setLocation(boardManager.getJail());
            playerManager.sendToJail(playerManager.getCurrentPlayer());
        }

        //When the player lands on a tile which has a property
        public void landOnProperty()
        {
            if (playerManager.getCurrentPlayer().getLocation().getProperty().getOwner() != null)
            {
                if (playerManager.getCurrentPlayer().getLocation().getProperty().getMortgaged() == true)
                {
                    Form_board.GetInstance.insert_console("Cette propriété est hypothéquée, vous ne payez pas de loyer, mais elle ne peut pas etre achetée");
                }
                else
                {
                    payRent();
                }
            }
            else
            {
                askForPurchase();
            }
        }

        //Ask the player if he wants to buy an ownerless property
        public void askForPurchase()
        {
            Form_board.GetInstance.insert_console("La propriété de cette case est libre, voulez vous l'acheter pour " + playerManager.getCurrentPlayer().getLocation().getProperty().getPrice().ToString() + " ?");
            string answer = Console.ReadLine();
            if (answer == "oui")
            {
                try
                {
                    playerManager.takeMoney(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty().getPrice());
                    playerManager.giveProperty(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty());
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
                    startBid(playerManager.getCurrentPlayer().getLocation().getProperty());
                }
            }
        }

        //Buy a house when the player owns a whole street
        public void buyHouse(PrivateProperty property)
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
                            Form_board.GetInstance.insert_console("Le prix d'une maison pour cette propriété est de " + property.getHouseCost().ToString() + "$");
                            try
                            {
                                playerManager.takeMoney(playerManager.getCurrentPlayer(), property.getHouseCost());
                                property.upgradeCurrentRent();
                                housesCount++;
                            }
                            catch (NotEnoughMoneyException)
                            {
                                Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent pour acheter une maison");
                            }
                        }
                        else
                        {
                            if (hotelsCount < MAX_HOTELS)
                            {
                                Form_board.GetInstance.insert_console("Vous avez déjà 4 amisons, un hotel est donc construit à la place pour " + property.getHouseCost().ToString() + "$");
                                try
                                {
                                    playerManager.takeMoney(playerManager.getCurrentPlayer(), property.getHouseCost());
                                    property.upgradeCurrentRent();
                                    hotelsCount++;
                                }
                                catch (NotEnoughMoneyException)
                                {
                                    Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent pour acheter l'hotel");
                                }
                            }
                            else
                            {
                                Form_board.GetInstance.insert_console("Le nombre d'hotels maximum est déjà atteint, si des hotels sont vendus, d'autres pourront êtres construient");
                            }
                        }
                    }
                    else
                    {
                        Form_board.GetInstance.insert_console("Vous avez déjà un hotel sur cette propriété, c'est le maximum");
                    }

                }
                else
                {
                    Form_board.GetInstance.insert_console("Vous n'avez pas toutes propriétés de cette rue, vous ne pouvez pas construire de maisons");
                }
            }
            else
            {
                Form_board.GetInstance.insert_console("Le nombre de maisons maximum est déjà atteint, si des maisons sont vendus, d'autres pourront êtres construitent");
            }

        }

        public void sellHouse(PrivateProperty property)
        {
            if (property.getHousesCount() > 0)
            {
                if (property.getHousesCount() == 5)
                {
                    Form_board.GetInstance.insert_console("Vous vendez l'hotel sur cette propriété pour " + (property.getHouseCost() * 4 / 2).ToString() + "$, il ne reste plus rien !");
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), property.getHouseCost() * 4 / 2);
                    property.resetCurrentRent();
                }
                else
                {
                    Form_board.GetInstance.insert_console("Vous vendez une maison pour " + (property.getHouseCost() / 2).ToString() + "$ sur cette propriété");
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), property.getHouseCost() / 2);
                    property.downgradeCurrentRent();
                }
            }
            else
            {
                Form_board.GetInstance.insert_console("Vous n'avez pas de maisons sur cette propriété");
            }
        }

        //Make the player pay his rent when he lands on a tile which has property already owned
        public void payRent()
        {
            Form_board.GetInstance.insert_console("La propriété sur cette case est détenue par " + playerManager.getCurrentPlayer().getLocation().getProperty().getOwner().getName() + ", vous devez lui donner " + playerManager.getCurrentPlayer().getLocation().getProperty().getCurrentRent().ToString() + "$");
            try
            {
                playerManager.takeMoney(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty().getCurrentRent());
                playerManager.giveMoney(playerManager.getCurrentPlayer().getLocation().getProperty().getOwner(), playerManager.getCurrentPlayer().getLocation().getProperty().getCurrentRent());
            }
            //If the player hasn't enough money to pay the rent
            catch (NotEnoughMoneyException)
            {
                Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent, vous etes forcé de faire une ipothèque");
                try
                {
                    askMortgage(playerManager.getCurrentPlayer(), playerManager.getCurrentPlayer().getLocation().getProperty().getCurrentRent() - playerManager.getCurrentPlayer().getMoney());
                }
                //If the player can't mortgage enough to pay his dept
                catch (ImpossibleMortgageException)
                {
                    Form_board.GetInstance.insert_console("Vous ne pouvez pas faire d'ipothèque, vous avez perdu la partie !");
                }
            }
        }

        public void askMortgage(Player player, int toPay)
        {
            if (player.getproperties().Count() > 0)
            {
                if (toPay > 0)
                {
                    Console.Write("Votre dette est de " + toPay.ToString());
                }
                Form_board.GetInstance.insert_console("Votre solde courrant est de " + player.getMoney().ToString());
                Form_board.GetInstance.insert_console("Voici la liste de vos propriétés :");
                foreach (Property p in player.getproperties())
                {
                    p.describe();
                }
                Form_board.GetInstance.insert_console("Laquelle souhaitez vous hypothéquer 50% de son prix ?");
                Property property = propertyManager.getPrivateProperties()[getInput()];
                playerManager.mortgage(player, property);
                Form_board.GetInstance.insert_console("Votre nouveau solde est de : " + player.getMoney().ToString() + "$");
            }
            else
            {
                throw new ImpossibleMortgageException();
            }
        }

        public void askUnMortgage(Player player)
        {
            Form_board.GetInstance.insert_console("Voici la liste de vos propriétés que vous pouvez racheter :");
            foreach (Property p in player.getMortagedProperties())
            {
                p.describe();
            }

            Form_board.GetInstance.insert_console("Laquelle souhaitez vous récupérer 110% de son prix d'hypothèque ?");
            Property property = propertyManager.getPrivateProperties()[getInput()];

            try
            {
                playerManager.unmortgage(player, property);
            }
            catch (NotEnoughMoneyException)
            {
                Form_board.GetInstance.insert_console("Vous n'avez pas assez d'argent pour racheter cette propriété");
            }
        }

        public void startBid(Property property)
        {
            List<Player> bidders = new List<Player>(playerManager.getPlayers());
            bidders.Remove(playerManager.getCurrentPlayer());

            Console.Write("Début des enchères entre ");
            for (int i = 0; i < bidders.Count() - 1; i++)
            {
                Console.Write(bidders[i].getName() + ", ");
            }
            Form_board.GetInstance.insert_console("et " + bidders[bidders.Count() - 1].getName());
            Dictionary<Player, int> bids = new Dictionary<Player, int>();
            foreach (Player p in bidders)
            {
                Console.Write(p.getName() + " entre une proposition pour ");
                property.describe();

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
                            Form_board.GetInstance.insert_console("Vous ne pouvez pas proposer plus que ce que contient votre porte-monnaie");
                        }
                    }
                    else
                    {
                        Form_board.GetInstance.insert_console("Entrez une valeur valide pour l'enchère");
                    }
            }
            Player player = bids.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            int amount = bids.Values.Max();
            Form_board.GetInstance.insert_console("Le gagnant de l'enchère est " + player.getName() + " qui a fait une proposition à " + amount.ToString());
            playerManager.takeMoney(player, amount);
            playerManager.giveProperty(player, property);
        }

        public void chanceAction(int id)
        {
            switch (id)
            {
                case 0:
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), boardManager.getStart());
                    playerManager.giveMoney(playerManager.getCurrentPlayer(), 200);
                    Form_board.GetInstance.insert_console("Vous êtes maintenant à la case départ, vous avez collecté 200$");
                    playerManager.getCurrentPlayer().describe();
                    break;
                case 1:
                    playerManager.moovePlayer(playerManager.getCurrentPlayer(), BoardManager.getTile(24));
                    Console.Write("Vous êtes atterit sur la case : ");
                    playerManager.getCurrentPlayer().getLocation().describe();
                    askForPurchase();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
            }
        }
    }
}
