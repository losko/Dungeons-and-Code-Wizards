using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.AbstractClasses
{
    abstract class Character
    {
        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;
        private Bag bag;
        private bool isAlive;
        private Faction faction;

        public Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
        {
            this.Name = name;
            this.BaseHealth = health;
            this.BaseArmor = armor;
            this.Health = BaseHealth;
            this.Armor = BaseArmor;
            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
            this.Faction = faction;
            this.IsAlive = true;
        }

        protected virtual double RestHealMultiplier => (double)1 / 5;

        public Faction Faction
        {
            get { return faction; }
            protected set { faction = value; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Bag Bag
        {
            get { return bag; }
            protected set { bag = value; }
        }

        public double AbilityPoints
        {
            get { return abilityPoints; }
            protected set { abilityPoints = value; }
        }

        public double Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public double BaseArmor
        {
            get { return baseArmor; }
            protected set { baseArmor = value; }
        }

        public double Health
        {
            get { return health; }
            set { health = value; }
        }

        public double BaseHealth
        {
            get { return baseHealth; }
            protected set { baseHealth = value; }
        }

        public string Name
        {
            get { return name; }
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or whitespace!");
                name = value;
            }
        }

        public void TakeDamage(double hitPoints)
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");

            double currentAttack = hitPoints;
            if (this.Armor >= currentAttack)
            {
                this.Armor -= currentAttack;
            }
            else
            {
                currentAttack = currentAttack - this.Armor;
                this.Armor = 0;
                this.Health -= currentAttack;
                if (this.Health <= 0)
                {
                    this.IsAlive = false;
                    this.Health = 0;
                }
            }
        }

        public void Rest()
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            this.Health += (this.BaseHealth * this.RestHealMultiplier);
            if (this.Health > this.BaseHealth)
            {
                this.Health = this.BaseHealth;
            }
        }

        public void UseItem(Item item)
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            item.AffectCharacter(this);
        }

        public void UseItemOn(Item item, Character character)
        {
            if (!this.IsAlive && !character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            item.AffectCharacter(character);
        }

        public void GiveCharacterItem(Item item, Character character)
        {
            if (!this.IsAlive && !character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            character.ReceiveItem(item);
        }

        public void ReceiveItem(Item item)
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            this.Bag.AddItem(item);
        }

        public override string ToString()
        {
            string state;
            if (this.IsAlive)
            {
                state = "Alive";
            }
            else
            {
                state = "Dead";
            }
            return $"{this.name} - HP: {this.Health}/{this.BaseHealth}, AP: {this.Armor}/{this.BaseArmor}, Status: {state}";
        }
    }
}
