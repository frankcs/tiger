using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerCompiler
{
    public enum TypeResolutionStatus
    {
        NotResolved,
        Resolving,
        Error,
        OK,
    }
}
