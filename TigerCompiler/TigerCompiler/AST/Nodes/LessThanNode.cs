﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class LessThanNode : TigerCompiler.RelationalOperationNode
    {
        public LessThanNode(IToken payload) : base(payload)
        {
        }
    }
}
