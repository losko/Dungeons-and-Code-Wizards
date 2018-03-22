using DungeonsAndCodeWizards.AbstractClasses;
using DungeonsAndCodeWizards.CharacterClasses;
using DungeonsAndCodeWizards.Factories;
using DungeonsAndCodeWizards.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards
{
    class DungeonMaster
    {
        private readonly List<Character> characterParty;
        private readonly List<Item> itemPool;
        private int lastSurvivorRounds;
        
        public DungeonMaster()
        {
            this.characterParty = new List<Character>();
            this.itemPool = new List<Item>();
            this.lastSurvivorRounds = 0;
        }

        public string JoinParty(string[] args)
        {
            string faction = args[0];
            string characterType = args[1];
            string name = args[2];

            CharacterFactory characterFactory = new CharacterFactory();
            
            Character character = characterFactory.CreateCharacter(faction, characterType, name);
            this.characterParty.Add(character);
            return $"{name} joined the party!";
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];

            ItemFactory itemFactory = new ItemFactory();
            
            Item item = itemFactory.CreateItem(itemName);

            this.itemPool.Add(item);
            return $"{itemName} added to pool.";
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            Character character = characterParty.Find(c => c.Name == characterName);
            if (character == null)
                throw new ArgumentException($"Character {characterName} not found!");
            if (this.itemPool.Count <= 0)
                throw new InvalidOperationException("No items left in pool!");
            Item item = this.itemPool[this.itemPool.Count - 1];
            this.itemPool.RemoveAt(this.itemPool.Count - 1);
            string itemName = item.GetType().Name;
            character.ReceiveItem(item);
            return $"{characterName} picked up {itemName}!";
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            Character character = characterParty.Find(c => c.Name == characterName);

            if (character == null)
                throw new ArgumentException($"Character {characterName} not found!");

            Item item = character.Bag.GetItem(itemName);

            character.UseItem(item);
            return $"{characterName} used {itemName}.";
        }

        public string UseItemOn(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            Character giver = characterParty.Find(c => c.Name == giverName);
            Character reciever = characterParty.Find(c => c.Name == receiverName);

            if (giver == null)
                throw new ArgumentException($"Character {giverName} not found!");
            if (reciever == null)
                throw new ArgumentException($"Character {receiverName} not found!");

            Item item = giver.Bag.GetItem(itemName);

            giver.UseItemOn(item, reciever);
            return $"{giver.Name} used {itemName} on {reciever.Name}.";
        }

        public string GiveCharacterItem(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            Character giver = characterParty.Find(c => c.Name == giverName);
            Character reciever = characterParty.Find(c => c.Name == receiverName);

            if (giver == null)
                throw new ArgumentException($"Character {giverName} not found!");
            if (reciever == null)
                throw new ArgumentException($"Character {receiverName} not found!");

            Item item = giver.Bag.GetItem(itemName);
            giver.GiveCharacterItem(item, reciever);

            return $"{giverName} gave {receiverName} {itemName}.";
        }

        public string GetStats()
        {
            StringBuilder result = new StringBuilder();
            List<Character> sorted = characterParty.OrderByDescending(c => c.IsAlive)
                .ThenByDescending(c => c.Health)
                .ToList();
            sorted.ForEach(p => result.AppendLine(p.ToString()));
            return result.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            Character attacker = characterParty.Find(c => c.Name == attackerName);
            Character reciever = characterParty.Find(c => c.Name == receiverName);

            if (attacker == null)
                throw new ArgumentException($"Character {attackerName} not found!");
            if (reciever == null)
                throw new ArgumentException($"Character {receiverName} not found!");

            if (attacker.GetType().Name != "Warrior")
                throw new ArgumentException($"{attacker.Name} cannot attack!");
            Warrior war = (Warrior)attacker;
            war.Attack(reciever);
            string result = $"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! {receiverName} has {reciever.Health}/{reciever.BaseHealth} HP and {reciever.Armor}/{reciever.BaseArmor} AP left!";
            if (!reciever.IsAlive)
            {
                result += $"\n{reciever.Name} is dead!";
            }
            return result.TrimEnd();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            Character healer = characterParty.Find(c => c.Name == healerName);
            Character receiver = characterParty.Find(c => c.Name == healingReceiverName);

            if (healer == null)
                throw new ArgumentException($"Character {healerName} not found!");
            if (receiver == null)
                throw new ArgumentException($"Character {healingReceiverName} not found!");

            string type = healer.GetType().Name;

            if (healer.GetType().Name != "Cleric")
                throw new ArgumentException($"{healerName} cannot heal!");

            Cleric cleric = (Cleric)healer;
            cleric.Heal(receiver);
            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
        }

        public string EndTurn(string[] args)
        {
            StringBuilder result = new StringBuilder();
            int survivors = 0;
            foreach (Character character in characterParty)
            {
                if (character.IsAlive)
                {
                    double prevHealth = character.Health;
                    character.Rest();
                    double afterRestHealth = character.Health;
                    result.AppendLine($"{character.Name} rests ({prevHealth} => {afterRestHealth})");
                    survivors++;
                }
            }
            if (survivors <= 1)
            {
                this.lastSurvivorRounds++;
            }
            return result.ToString().TrimEnd();
        }

        public bool IsGameOver()
        {
            if (this.lastSurvivorRounds >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
