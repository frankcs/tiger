using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code_Generation
{
    public class FunctionInfo : TigerInfo
    {
        public Code_Generation.VariableInfo[] Parameters { get; set; }


        public bool IsStandard { get; set; }

        ////For Code Generation
        //public VMFunction VMFunction { get; set; }
    }
}
