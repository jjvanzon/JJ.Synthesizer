
            //double constantOld = factors.Product(x => x.AsConst ?? 1);
            //if (constant != constantOld)
            //{
            //    throw new Exception($"(constantOld = {constantOld} = factors.Product(x => x.AsConst ?? 1)) != " +
            //                        $"(constant = {constant} = consts.Product(x => x.AsConst ?? 1)) " +
            //                        $"consts.Count = {consts.Length}");
            //}
        
        //public static void LogTermsEliminated(IList<FluentOutlet> termsBefore, IList<FluentOutlet> termAfter)
        //    => LogEliminationIfNeeded("term(s)", termsBefore, termAfter, "+");

        //public static void LogFactorsEliminated(IList<FluentOutlet> factorsBefore, IList<FluentOutlet> factorsAfter)
        //    => LogEliminationIfNeeded("factor(s)", factorsBefore, factorsAfter, "*");

        //public static void LogEliminationIfNeeded(string operandName, IList<FluentOutlet> operandsBefore, IList<FluentOutlet> operandsAfter, string mathSymbol)
        //{
        //    if (operandsBefore == null) throw new ArgumentNullException(nameof(operandsBefore));
        //    if (operandsAfter == null) throw new ArgumentNullException(nameof(operandsAfter));

        //    if (operandsBefore.Count != operandsAfter.Count)
        //    {
        //        int count = operandsBefore.Count - operandsAfter.Count;
        //        string sep = " " + mathSymbol + " ";
        //        Console.WriteLine($"{PrettyTime()} Eliminate {count} {operandName} : {Stringify(sep, operandsBefore)} = {Stringify(sep, operandsAfter)}");
        //    }
        //}

        //public static string Stringify(string separator, params FluentOutlet[] operands)
        //    => Stringify(separator, (IList<FluentOutlet>)operands);
