using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class SetDimension_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledIn
    {
        public SetDimension_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            if (DataPropertyParser.DataIsWellFormed(Object))
            {
                string dimensionString = DataPropertyParser.TryGetString(Object, PropertyNames.Dimension);

                For(() => dimensionString, PropertyDisplayNames.Dimension).IsNot(DimensionEnum.Undefined);
            }
        }
    }
}