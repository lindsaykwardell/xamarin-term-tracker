using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeXamarin.Interfaces
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
