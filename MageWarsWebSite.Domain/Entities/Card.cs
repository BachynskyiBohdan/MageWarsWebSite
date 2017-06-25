using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public int Amount { get; set; }

        public bool IsAnd { get; set; }

        public bool IsBasic { get; set; }

        public int? Cost { get; set; }

        public int? Reveal { get; set; }

        public ICollection<CardType> CardTypes { get; set; }

        public ICollection<SubType> SubTypes { get; set; }

        public ICollection<School> Schools { get; set; }

        public ICollection<Desk> Desks { get; set; }

        public Card()
        {
            Id = 0;
            Name = "";
            FileName = "";
            FileType = "";
            Amount = 0;
            IsAnd = false;
            IsBasic = false;
            Cost = null;
            Reveal = null;
            CardTypes = new HashSet< CardType>();
            SubTypes = new HashSet<SubType>();
            Schools = new HashSet<School>();
            Desks = new HashSet<Desk>();
        }
    }

    public static class CardExtentions
    {
        public static void Update(this Card card, Card up)
        {
            card.Name = up.Name;
            card.FileName = up.FileName;
            card.FileType = up.FileType;
            card.Amount = up.Amount;
            card.IsAnd = up.IsAnd;
            card.IsBasic = up.IsBasic;
            card.Cost = up.Cost;
            card.Reveal = up.Reveal;
            card.SubTypes = up.SubTypes;
            card.CardTypes = up.CardTypes;
            card.Schools = up.Schools;
            card.Desks = up.Desks;
        }
    }


    public enum CardTypes
    {
        Attack = 0,
        Conjuration = 1,
        Creature = 2,
        Enchantment = 3,
        Equipment = 4,
        Incantation = 5
    }

    public enum CardSubType
    {
        Acid = 0,
        Aegis,
        Altar,
        Angel,
        Animal,
        Antarian = 5,
        Ape,
        Artifact,
        Bat,
        Barrier,
        Bear = 10,
        Bird,
        Canine,
        Cat,
        Cervine,
        Charm = 15,
        Cleric,
        Cloud,
        Command,
        Control,
        Curse = 20,
        DarkElf,
        Defense,
        Demon,
        Dragon,
        Dwarf = 25,
        Elemental,
        Faerie,
        Flame,
        Flower,
        Force = 30,
        Gargoyle,
        Goblin,
        Golem,
        Gremlin,
        Harpy = 35,
        Healing,
        HighElf,
        Horse,
        Hydro,
        Illusion = 40,
        Insect,
        Knight,
        Light,
        Lightning,
        Lizard = 45,
        Lycanthrope,
        Mana,
        Metal,
        Metamagic,
        Minotaur = 50,
        Necro,
        Obelisk,
        Ooze,
        Orc,
        Outpost = 55,
        Plant,
        Poison,
        Portal,
        Protection,
        Psychic = 60,
        Psyoculus,
        Reptile,
        Rune,
        Seismic,
        Serpent = 65,
        Sequoian,
        Shadow,
        Skeleton,
        Soldier,
        Spider = 70,
        Spirit,
        Statue,
        Stone,
        Structure,
        Teleport = 75,
        Temple,
        Tome,
        Totem,
        Tower,
        Transform = 80,
        Trap,
        Tree,
        Troll,
        Undead,
        Vampire = 85,
        Vampiric,
        Vine,
        VTar,
        WarMachine,
        Weather = 90,
        Well,
        Wind,
        Worm,
        Zombie = 94,
        Griffin = 95,
        Aquatic = 96,
        Pirate = 97,
        Banner = 98,
        Deptonne = 99,
        Octopus = 100,
        Marren = 101,
        Fish = 102,
        Cloak = 103,
        Ring = 104,
        Spear = 105,
        Weapon = 106,
        Sword = 107,
        Blessing = 108,
        Merren = 109,
        Valor = 110,
        Instrument = 111,
        Lyre = 112,
    }

    public enum Schools
    {
        Arcane = 0,
        Dark = 1,
        Holy = 2,
        Mind,
        Nature,
        War,
        Air,
        Water = 7,
        Fire,
        Earth
    }
}