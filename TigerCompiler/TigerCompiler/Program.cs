using Antlr.Runtime.Tree;
using TigerCompiler.AST;

    public partial class TigerParser
    {
        partial void CreateTreeAdaptor(ref ITreeAdaptor adaptor)
        {
            adaptor = new OurTreeAdaptor();
        }
    }
