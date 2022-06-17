using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardsCourseProject.Models
{
    public class CardSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsStoreCardSet { get; set; } = false;
        public bool PublishStore { get; set; } = true;
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
