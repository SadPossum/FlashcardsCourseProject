using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardsCourseProject.Models
{
    public class FlashCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FrontSideText { get; set; }
        public string FrontSideImagePath { get; set; }
        public string BackSideImagePath { get; set; }
        public string BackSideText { get; set; }

        [ForeignKey("CardSetId")]
        public int FlashCardSetId { get; set; } // Внешний ключ
        public FlashCardSet FlashCardSet { get; set; } // Навигационное ствойство

    }
}
