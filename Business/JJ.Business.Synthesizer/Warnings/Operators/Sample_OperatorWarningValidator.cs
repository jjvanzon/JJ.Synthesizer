using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Sample_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledIn
    {
        public Sample_OperatorWarningValidator(Operator obj)
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