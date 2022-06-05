using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public List<FlashCardSet> FlashCardSet { get; set; } = new List<FlashCardSet>();
    }
}
