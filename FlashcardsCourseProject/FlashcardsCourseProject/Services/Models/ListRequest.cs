using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Services.Models
{
    public class ListRequest
    {
        public string Search { get; set; }
        public Pagination Pagination { get; set; }
    }
}
