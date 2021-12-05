using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardsCourseProject.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FrontImage { get; set; }
        public string BackImage { get; set; }
        public string BackText { get; set; }
        public int CardSetId { get; set; }
        public CardSet CardSet { get; set; }

    }
}
