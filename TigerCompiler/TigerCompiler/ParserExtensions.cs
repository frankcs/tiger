using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Antlr.Runtime.Tree;
using TigerCompiler.AST;

public partial class TigerParser
{
    public override void EmitErrorMessage(string msg)
    {
        var indexOf = msg.IndexOf("line ");
        msg = msg.Substring(indexOf);
        msg = Regex.Replace(msg, @"line (\d+):(\d+)", m => "(" + m.Groups[1].Value + ", " + m.Groups[2].Value + "):") + ".";
        Console.WriteLine(msg);
        base.EmitErrorMessage(msg + ".");
    }

    partial void CreateTreeAdaptor(ref ITreeAdaptor adaptor)
    {
        adaptor = new OurTreeAdaptor();
    }
}
