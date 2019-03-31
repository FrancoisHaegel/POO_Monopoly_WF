using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.model;
using System.IO;

namespace Monopoly.controller
{
    class PropertyManager
    {
        private List<PrivateProperty> privateProperties;
        private List<Railroad> railRoads;
        private List<Utility> utilityCompanies;

        public List<PrivateProperty> getPrivateProperties()
        {
            return privateProperties;
        }
        public List<Railroad> getRailRoads()
        {
            return railRoads;
        }
        public List<Utility> getUtilityCompanies()
        {
            return utilityCompanies;
        }

        public void init(string pathToPrivateProperty, string pathToRailroads, string pathToUtilities)
        {
            try
            {
                privateProperties = File.ReadAllLines(pathToPrivateProperty).Skip(1).Select(v => PrivateProperty.FromCsv(v)).ToList();
                railRoads = File.ReadAllLines(pathToRailroads).Skip(1).Select(v => Railroad.FromCsv(v)).ToList();
                utilityCompanies = File.ReadAllLines(pathToUtilities).Skip(1).Select(v => Utility.FromCsv(v)).ToList();
            }
            catch (DirectoryNotFoundException e)
            {
                Form_board.GetInstance.insert_console("Error : Cannot find path : " + e);
            }
        }

        public void describe()
        {
            Form_board.GetInstance.insert_console("Descirption des Property :");
            Form_board.GetInstance.insert_console("Descirption des Railroads :");
            foreach (Railroad r in railRoads)
            {
                r.describe();
            }
            Form_board.GetInstance.insert_console("Descirption des Utilities :");
            foreach (Utility u in utilityCompanies)
            {
                u.describe();
            }
            Form_board.GetInstance.insert_console("Descirption des PrivateProperties :");
            foreach (PrivateProperty pp in privateProperties)
            {
                pp.describe();
            }
        }
    }
}
