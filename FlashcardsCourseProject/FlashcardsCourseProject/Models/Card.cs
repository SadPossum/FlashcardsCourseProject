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

        [ForeignKey("FrontImageId")]
        public int FrontImageId { get; set; }
        public FileImage FileImageFront { get; set; }

        [ForeignKey("BackImageId")]
        public int BackImageId { get; set; }
        public FileImage FileImageBack { get; set; }

        public string BackText { get; set; }

        [ForeignKey("CardSetId")]
        public int CardSetId { get; set; } // Внешний ключ
        public CardSet CardSet { get; set; } // Навигационное ствойство

    }
}
