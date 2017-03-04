using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Sample_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Sample_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(Obj.Data))
            {
                string sampleIDString = DataPropertyParser.TryGetString(Obj, PropertyNames.SampleID);

                For(() => sampleIDString, PropertyDisplayNames.Sample)
                    .NotNullOrEmpty();
            }
        }
    }
}