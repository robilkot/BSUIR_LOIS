namespace LogicalExpressionClassLibrary.Minimization.Strategy
{
    public class EvaluationStrategy : IMinimizationStrategy
    {
        public LogicalExpression Minimize(LogicalExpression input, NormalForms form)
        {
            LogicalExpression source = form switch
            {
                NormalForms.FCNF => input.FCNF,
                NormalForms.FDNF => input.FDNF,
                _ => throw new NotImplementedException()
            };

            source = source.MergeConstituents(form);

            var implicants = MinimizationHelper.GetConstituents(source, form);

            bool targetValue = form switch
            {
                NormalForms.FCNF => true,
                NormalForms.FDNF => false,
                _ => throw new NotImplementedException()
            };

            List<string> allImplicants = implicants.Select(i => i.ToString()!).ToList();

            foreach (var implicant in implicants)
            {
                ConsoleLogger.Log($"Trying to remove implicant {implicant}", ConsoleLogger.DebugLevels.Info);

                var variables = MinimizationHelper.GetVariables(implicant, form);
                List<string> currentImplicantsCombination = [];

                foreach (var modifiedImplicant in allImplicants)
                {
                    string modifiedString = modifiedImplicant;
                    
                    foreach (var variable in variables)
                    {
                        modifiedString =
                            modifiedString.Replace(variable.variable.ToString()!,
                            (targetValue ^ variable.inverted) ?
                            ((char)LogicalSymbols.True).ToString()! :
                            ((char)LogicalSymbols.False).ToString()!);
                    }

                    currentImplicantsCombination.Add(modifiedString);

                    ConsoleLogger.Log($"Modified implicant {modifiedImplicant} to {modifiedString}", ConsoleLogger.DebugLevels.Debug);
                }

                // doesn't work because we removed variables;

                LogicalExpression remainder = MinimizationHelper.BuildNFFromStringSet(currentImplicantsCombination, form);

                ConsoleLogger.Log($"Expression without implicant {remainder}");
                ConsoleLogger.Log("\n" + remainder.ToTruthTableString());

                if (remainder.IsTautology())
                {
                    allImplicants.Remove(implicant.ToString()!);
                    ConsoleLogger.Log($"Found odd implicant {implicant}", ConsoleLogger.DebugLevels.Info);
                }
            }

            return MinimizationHelper.BuildNFFromStringSet(allImplicants, form);
        }
    }
}
