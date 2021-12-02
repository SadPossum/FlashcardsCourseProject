﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KursahProject.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FrontImage { get; set; }
        public string BackImage { get; set; }
        public string BackText { get; set; }
        public int CardSetId { get; set; }
        public CardSet CardSet { get; set; }

    }
}