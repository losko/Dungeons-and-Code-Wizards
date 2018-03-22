using DungeonsAndCodeWizards.ItemClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace DungeonsAndCodeWizards.AbstractClasses
{
    abstract class Bag
    {
        private int capacity;
        private readonly List<Item> items;

        protected Bag (int capacity)
        {
            this.Capacity = 100;
            this.Capacity = capacity;
            this.items = new List<Item>();
        }

        public int Capacity
        {
            get { return capacity; }
            private set { capacity = value; }
        }
        
        private int Load => this.items.Sum(i => i.Weight);

        public IReadOnlyCollection<Item> Items
        {
            get
            {
                return this.items.AsReadOnly();
            }
        }


        public void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.Capacity)
                throw new InvalidOperationException("Bag is full!");
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            this.CheckItem(name);

            var item = this.items.First(i => i.GetType().Name == name);
            this.items.Remove(item);

            return item;
        }

        private void CheckItem(string name)
        {

            if (!this.items.Any())
            {
                throw new InvalidOperationException("Bag is empty!");
            }

            var itemExists = this.Items.Any(i => i.GetType().Name == name);
            if (!itemExists)
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }
        }
    }
}
