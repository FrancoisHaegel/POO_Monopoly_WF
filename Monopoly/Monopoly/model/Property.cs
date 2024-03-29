﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.model
{
    public abstract class Property
    {
        private int id;
        private int price;
        private Tile tile;
        private Player owner;
        private bool mortgaged;
        private string name;
        private int[] rent;
        private int housesCount;
        public enum PropType { PRIVATE, RAILROAD, UTILITY };
        private PropType type;

        public Tile Tile
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
            }
        }

        public int getId()
        {
            return id;
        }

        public int getPrice()
        {
            return price;
        }

        public Player getOwner()
        {
            return owner;
        }

        public bool getMortgaged()
        {
            return mortgaged;
        }

        public string getName()
        {
            return name;
        }

        public int[] getRent()
        {
            return rent;
        }

        public PropType getType()
        {
            return type;
        }

        public int getHousesCount()
        {
            return housesCount;
        }

        public int getCurrentRent()
        {
            return rent[housesCount];
        }

        public void setMortgaged(bool m)
        {
            mortgaged = m;
        }

        public void setOwner(Player p)
        {
            owner = p;
        }

        public void resetCurrentRent()
        {
            housesCount = 0;
        }

        public void upgradeCurrentRent()
        {
            housesCount++;
            Form_board.GetInstance.setRent(this, this.getCurrentRent());
        }

        public void downgradeCurrentRent()
        {
            housesCount--;
            Form_board.GetInstance.setRent(this, this.getCurrentRent());
        }

        public abstract void describe();

        public Property(int pId, int pPrice, string pName, int[] pRent, int pType)
        {
            id = pId;
            price = pPrice;
            owner = null;
            mortgaged = false;
            name = pName;
            rent = pRent;
            housesCount = 0;
            switch (pType)
            {
                case 0:
                    type = PropType.PRIVATE;
                    break;
                case 1:
                    type = PropType.RAILROAD;
                    break;
                case 2:
                    type = PropType.UTILITY;
                    break;
            }

        }
    }
}