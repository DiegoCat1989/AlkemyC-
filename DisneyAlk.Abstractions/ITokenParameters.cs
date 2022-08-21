using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.Abstractions
{
    public interface  ITokenParameters
    {
         string Username { get; set; }
         string Passwordhash { get; set; }
        string Id { get; set; }
    }
}
