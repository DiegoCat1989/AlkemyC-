using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.Abstractions
{
    public interface IDBContext<T>: ICrud<T>
    {
    }
}
