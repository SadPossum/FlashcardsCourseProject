using System;
using System.Collections.Generic;
using System.Text;

namespace KursahProject.Models
{
    public class CardSet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

    }
}
