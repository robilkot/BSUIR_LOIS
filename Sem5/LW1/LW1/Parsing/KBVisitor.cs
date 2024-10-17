using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LW1.Model;

namespace LW1.Parsing
{
    public class KBVisitor : IFuzzyLogicVisitor<KB>
    {
        public KB VisitKb([NotNull] FuzzyLogicParser.KbContext context)
            => new()
            {
                Facts = [.. context.fact().Select(VisitFactInternal)],
                Rules = [.. context.rule().Select(VisitRuleInternal)]
            };

        private static Fact VisitFactInternal(FuzzyLogicParser.FactContext context)
            => new ($"{context.ID()}", context.pair().Select(p => ($"{p.ID()}", double.Parse($"{p.FLOAT()}"))));

        private static Rule VisitRuleInternal(FuzzyLogicParser.RuleContext context)
            => new($"{context.ID()[0]}", $"{context.ID()[1]}");

        #region Not implemented interface methods
        public KB Visit([NotNull] IParseTree tree)
            => throw new NotImplementedException();

        public KB VisitChildren([NotNull] IRuleNode node)
            => throw new NotImplementedException();

        public KB VisitErrorNode([NotNull] IErrorNode node)
            => throw new NotImplementedException();

        public KB VisitFact([NotNull] FuzzyLogicParser.FactContext context)
            => throw new NotImplementedException();

        public KB VisitPair([NotNull] FuzzyLogicParser.PairContext context)
            => throw new NotImplementedException();

        public KB VisitRule([NotNull] FuzzyLogicParser.RuleContext context)
            => throw new NotImplementedException();

        public KB VisitTerminal([NotNull] ITerminalNode node)
            => throw new NotImplementedException();
        #endregion
    }
}
