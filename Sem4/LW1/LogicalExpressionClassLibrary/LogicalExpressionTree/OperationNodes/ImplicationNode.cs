namespace LogicalExpressionClassLibrary.LogicalExpressionTree.OperationNodes
{
    public sealed class ImplicationNode : TreeNode
    {
        protected override bool Evaluate()
        {
            if(Left!.Evaluation && !Right!.Evaluation)
            {
                return false;
            }
            return true;
        }
        public override string ToString()
        {
            return $"{(char)LogicalSymbols.LeftBracket}{Left}{(char)LogicalSymbols.Implication}{Right}{(char)LogicalSymbols.RightBracket}";
        }
    }
}
