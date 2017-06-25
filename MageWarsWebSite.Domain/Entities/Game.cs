using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MageWarsWebSite.Domain.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual string Type { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int Description { get; set; }

        public virtual int Statistics { get; set; }
    }
}
