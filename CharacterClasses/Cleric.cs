using DungeonsAndCodeWizards.AbstractClasses;
using DungeonsAndCodeWizards.BagClasses;
using DungeonsAndCodeWizards.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.CharacterClasses
{
    class Cleric : Character, IHealable
    {
        public Cleric(string name, Faction faction)
            : base(name, 50, 25, 40, new Backpack(), faction)
        {
            base.Name = name;
            base.Faction = faction;
        }

        protected override double RestHealMultiplier => 0.5;

        public void Heal(Character character)
        {
            if (!this.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");

            if (!character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");

            if (this.Faction != character.Faction)
                throw new InvalidOperationException("Cannot heal enemy character!");

            character.Health += this.AbilityPoints;

            if (character.Health > character.BaseHealth)
            {
                character.Health = character.BaseHealth;
            }
        }
    }
}
