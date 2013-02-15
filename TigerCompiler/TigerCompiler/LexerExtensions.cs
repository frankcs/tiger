using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public partial class TigerLexer
{
    public bool Error = false;

    public override void EmitErrorMessage(string msg)
    {
        Console.WriteLine(Regex.Replace(msg, @"line (\d+):(\d+)", m => "(" + m.Groups[1].Value + ", " + m.Groups[2].Value + ")"));
        Error = true;
        base.EmitErrorMessage(msg);
    }

}