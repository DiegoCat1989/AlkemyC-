using DisneyAlk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.Services
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters pars);
    }
}
