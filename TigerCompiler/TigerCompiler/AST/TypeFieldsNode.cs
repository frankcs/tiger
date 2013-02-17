using System.Collections.Generic;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST
{
    /// <summary>
    /// type_fields:	type_field (','! type_field)*;
    /// type_field:	ID ':' type_id -> ^(ID type_id);
    /// </summary>
    internal class TypeFieldsNode : ASTNode
    {
        public TypeFieldsNode(IToken payload) : base(payload)
        {

        }

        public Dictionary<string, TypeInfo> Parameters { get; private set; } 

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            Parameters = new Dictionary<string, TypeInfo>();

            if (Children!=null)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    var paramTypeName = ((TypeIDNode) ((ASTNode) Children[i]).Children[0]).TypeName;

                    if (report.Assert(Children[i] as ASTNode, !Parameters.ContainsKey(Children[i].Text),
                                   "There is already a parameter named {0} on the function.",Children[i].Text))
                    {
                        Parameters.Add(Children[i].Text, scope.ResolveType(paramTypeName));
                    }
                }
            }
        }
    }
}