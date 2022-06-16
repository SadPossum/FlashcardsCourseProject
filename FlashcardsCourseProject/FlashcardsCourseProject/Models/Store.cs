using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlashcardsCourseProject.Models
{
    
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CardSetId")]
        public int CardSetId { get; set; }

        public virtual CardSet CardSet { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        //public List<CardSet> CardsSet { get; set; } = new List<CardSet>();
    }
}
