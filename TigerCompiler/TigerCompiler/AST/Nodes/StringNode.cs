﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class StringNode : TigerCompiler.ConstantNode
    {
        public StringNode(IToken payload) : base(payload)
        {
        }
    }
}
