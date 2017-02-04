using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class InterpolationType_DataProperty_Validator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public InterpolationType_DataProperty_Validator(string obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            string data = Object;

            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string interpolationTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);

                For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                    .NotNullOrEmpty()
                    .IsEnum<InterpolationTypeEnum>()
                    .IsNot(InterpolationTypeEnum.Undefined);
            }
        }
    }
}
