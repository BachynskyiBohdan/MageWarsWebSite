using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class SubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Card> Cards { get; set; }

        public SubType()
        {
            Id = 0;
            Name = "";
            Cards = new HashSet<Card>();
        }

        public SubType(SubType obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Cards = new HashSet<Card>(obj.Cards);
        }
    }
}
