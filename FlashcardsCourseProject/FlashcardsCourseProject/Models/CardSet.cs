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

        [ForeignKey("PictureId")]
        public int PictureId { get; set; }
        public FileImage FilePicture { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

    }
}
