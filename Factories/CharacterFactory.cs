using DungeonsAndCodeWizards.AbstractClasses;
using DungeonsAndCodeWizards.CharacterClasses;
using System;

namespace DungeonsAndCodeWizards.Factories
{
    class CharacterFactory
    {
        public Character CreateCharacter(string faction, string characterType, string name)
        {
            if (faction != Faction.Java.ToString() && faction != Faction.CSharp.ToString())
                throw new ArgumentException($"Invalid faction \"{faction}\"!");
            switch (characterType)
            {
                case "Warrior":
                    return new Warrior(name, (Faction)Enum.Parse(typeof(Faction), faction));
                case "Cleric":
                    return new Cleric(name, (Faction)Enum.Parse(typeof(Faction), faction));
                default:
                    throw new ArgumentException($"Invalid character type \"{characterType}\"!");
            }
        }
    }
}
