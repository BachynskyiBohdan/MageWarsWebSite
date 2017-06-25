using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class Desk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MageId { get; set; }

        public int GameId { get; set; }

        public ICollection<Card> Cards { get; set; }

        public Desk()
        {
            Id = 0;
            UserId = 0;
            MageId = 0;
            GameId = 0;
            Cards = new HashSet<Card>();
        }
    }
}

