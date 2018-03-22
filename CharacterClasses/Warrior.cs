using DungeonsAndCodeWizards.AbstractClasses;
using DungeonsAndCodeWizards.BagClasses;
using DungeonsAndCodeWizards.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.CharacterClasses
{
    class Warrior : Character, IAttackable
    {
        public Warrior(string name, Faction faction)
            : base(name, 100, 50, 40, new Satchel(), faction)
        {
            base.Name = name;
            base.Faction = faction;
        }

        public void Attack(Character character)
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");

            if (character == this)
                throw new InvalidOperationException("Cannot attack self!");

            if (this.Faction == character.Faction)
                throw new ArgumentException($"Friendly fire! Both characters are from {this.Faction} faction!");

            character.TakeDamage(this.AbilityPoints);
        }
    }
}
