using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardsCourseProject.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FrontText { get; set; }
        public string FrontImagePath { get; set; }
        public string BackImagePath { get; set; }
        public string BackText { get; set; }

        [ForeignKey("CardSetId")]
        public int CardSetId { get; set; } // Внешний ключ
        public CardSet CardSet { get; set; } // Навигационное ствойство

        

    }
}
