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
        var indexOf = msg.IndexOf("line ");
        msg = msg.Substring(indexOf);
        msg = Regex.Replace(msg, @"line (\d+):(\d+)", m => "(" + m.Groups[1].Value + ", " + m.Groups[2].Value + "):")+".";
        Console.WriteLine(msg);
        Error = true;
        base.EmitErrorMessage(msg + ".");
    }

}