using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.AbstractClasses
{
    abstract class Item
    {
        private int weight;

        public int Weight
        {
            get { return weight; }
            private set { weight = value; }
        }

        public Item(int weight)
        {
            this.Weight = weight;
        }

        public virtual void AffectCharacter(Character character)
        {
            if (!character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
        }

    }
}
