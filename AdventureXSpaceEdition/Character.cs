using System;
using System.Collections.Generic;

namespace AdventureXSpaceEdition
{
    public abstract class Character
    {
        private string Name { get; set; }
        private int Health { get; set; }
        private int Level { get; set; }
        private int Strength { get; set; }
        private int Dexterity { get; set; }
        private int Persuasion { get; set; }
        private int Will { get; set; }
        private int GoodBoyPoints { get; set; }
        private bool Alive { get; set; }
        public List<ICharacterItem> InventoryItems = new List<ICharacterItem>();

        protected Character(string name, int health, int level, int strength, int dexterity, int persuasion, int will)
        {
            Name = name;
            Health = health;
            Level = level;
            Strength = strength;
            Dexterity = dexterity;
            Persuasion = persuasion;
            Will = will;
            Alive = true;
            GoodBoyPoints = 0;
        }

        public bool Attack(Character otherCharacter)
        {
            int dodgeChance = (otherCharacter.Dexterity - Dexterity/2)*2;
            dodgeChance = dodgeChance < 0 ? 0 : dodgeChance;
            if (Roll.IsSuccessful(dodgeChance))
                return false;
            otherCharacter.TakeDamage(Strength);
            return true;
        }

        public bool Persuade(Character otherCharacter)
        {
            int persuadeChance = Convert.ToInt32((Persuasion - otherCharacter.Will)*1.5);
            persuadeChance = persuadeChance < 0 ? 0 : persuadeChance;
            return Roll.IsSuccessful(persuadeChance);
        }

        public int Heal(int healAmount, int maxHp)
        {
            Health += healAmount;
            Health = Health > maxHp ? maxHp : Health;
            return Health;
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Alive = false;
            }
            return Alive;
        }

        public void UseItem(ICharacterItem item)
        {
            item.Use(this);
        }
        public void UseItem(ICharacterItem item, Character otherCharacter)
        {
            item.Use(otherCharacter);
        }

        public string Stats()
        {
            return "Name: " + Name + "\nHP: " + Health + "\nLevel: " + Level + "\nStrength: " + Strength + "\nDexterity: " + Dexterity
                + "\nPersuasion: " + Persuasion + "\nWill: " + Will;
        }

        public string ReportInventory()
        {
            string report = InventoryItems.Count > 0 ? "Inventory Items:" : "Inventory Empty";
            foreach (var inventoryItem in InventoryItems)
            {
                report += "\n" + inventoryItem.ItemName;
            }
            return report;
        }
    }
}