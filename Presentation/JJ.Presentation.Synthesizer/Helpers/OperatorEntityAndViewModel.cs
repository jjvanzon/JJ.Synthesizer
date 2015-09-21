using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class OperatorEntityAndViewModel
    {
        public OperatorEntityAndViewModel(Operator op, OperatorViewModel operatorViewModel)
        {
            if (op == null) throw new NullException(() => op);
            if (operatorViewModel == null) throw new NullException(() => operatorViewModel);

            Operator = op;
            OperatorViewModel = operatorViewModel;
        }

        public Operator Operator { get; private set; }
        public OperatorViewModel OperatorViewModel { get; private set; }
    }
}
