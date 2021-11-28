using System;
using System.Collections.Generic;
using System.Text;

namespace KursahProject.Services
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
