using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Services
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
