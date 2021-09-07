using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Utilities.Results
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
    }
}
