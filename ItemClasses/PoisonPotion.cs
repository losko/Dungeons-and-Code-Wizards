using DungeonsAndCodeWizards.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.ItemClasses
{
    class PoisonPotion : Item
    {
        public PoisonPotion () : base(5)
        {

        }

        public override void AffectCharacter(Character character)
        {
            if (!character.IsAlive)
                throw new InvalidOperationException("Must be alive to perform this action!");
            character.Health -= 20;
            if (character.Health <= 0)
            {
                character.IsAlive = false;
                character.Health = 0;
            }
        }
    }
}
