using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Models
{
    public class UserAddedFlashCardSet
    {
        public int FlashCardSetId { get; set; } // Внешний ключ
        public FlashCardSet FlashCardSet { get; set; } // Навигационное ствойство

        public int UserId { get; set; } // Внешний ключ
        public User User { get; set; } // Навигационное ствойство
    }
}
