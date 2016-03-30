using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_Sample : OperatorWarningValidator_Base_AllInletsFilled
    {
        public OperatorWarningValidator_Sample(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            if (DataPropertyParser.DataIsWellFormed(Object.Data))
            {
                string sampleIDString = DataPropertyParser.TryGetString(Object, PropertyNames.SampleID);

                For(() => sampleIDString, PropertyDisplayNames.Sample)
                    .NotNullOrEmpty();
            }
        }
    }
}