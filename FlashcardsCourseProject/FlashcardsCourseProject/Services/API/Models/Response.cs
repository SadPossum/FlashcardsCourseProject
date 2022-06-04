using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Services.API.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
