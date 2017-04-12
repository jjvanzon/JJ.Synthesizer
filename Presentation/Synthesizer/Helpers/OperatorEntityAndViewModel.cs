using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class OperatorEntityAndViewModel
    {
        public OperatorEntityAndViewModel(Operator op, OperatorViewModel operatorViewModel)
        {
            Operator = op ?? throw new NullException(() => op);
            OperatorViewModel = operatorViewModel ?? throw new NullException(() => operatorViewModel);
        }

        public Operator Operator { get; }
        public OperatorViewModel OperatorViewModel { get; }
    }
}
