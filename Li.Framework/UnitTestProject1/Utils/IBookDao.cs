using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Utils
{
    public interface IBookDao
    {
        string GetBookName(string id);
    }
}
