using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class Mage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int HealPoints { get; set; }

        public int ManaPoints { get; set; }

        public int? PrimarySchoolId { get; set; }

        public int? PrimarySchoolLevel { get; set; }

        public int? SecondarySchoolId { get; set; }

        public int? SecondarySchoolLevel { get; set; }

        public string DescriptionFileName { get; set; }

        public string DescriptionFileType { get; set; }

        public string HeroFileName { get; set; }

        public string HeroFileType { get; set; }

        public Mage()
        {
            Id = 0;
            Name = "";
            HealPoints = 0;
            ManaPoints = 0;
            PrimarySchoolId = null;
            PrimarySchoolLevel = null;
            SecondarySchoolId = null;
            SecondarySchoolLevel = null;

            DescriptionFileName = "";
            DescriptionFileType = "png";
            HeroFileName = "";
            HeroFileType = "png";
        }
    }

    public static class MageExtentions
    {
        public static void Update(this Mage mage, Mage up)
        {
            mage.Name = up.Name;
            mage.HealPoints = up.HealPoints;
            mage.ManaPoints = up.ManaPoints;
            mage.PrimarySchoolId = up.PrimarySchoolId;
            mage.PrimarySchoolLevel = up.PrimarySchoolLevel;
            mage.SecondarySchoolId = up.SecondarySchoolId;
            mage.SecondarySchoolLevel = up.SecondarySchoolLevel;

            mage.DescriptionFileType = up.DescriptionFileType;
            mage.DescriptionFileName = up.DescriptionFileName;
            mage.HeroFileName = up.HeroFileName;
            mage.HeroFileType = up.HeroFileType;
        }
    }
}
