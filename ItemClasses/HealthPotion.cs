using DungeonsAndCodeWizards.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.ItemClasses
{
    class HealthPotion : Item
    {
        private const int HitPointsRestored = 20;

        public HealthPotion() : base(5)
        {

        }

        public override void AffectCharacter(Character character)
        {
            if (!character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            character.Health += HitPointsRestored;
        }
    }
}
