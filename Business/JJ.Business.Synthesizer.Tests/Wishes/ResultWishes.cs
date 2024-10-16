using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class ResultWishes
    {
        public static CanonicalModel.Result<TData> ToResult<TData>(
            this List<Framework.Validation.ValidationMessage> frameworkValidationMessages, 
            TData data, bool successful = true)
            => new CanonicalModel.Result<TData>
            {
                Data               = data,
                ValidationMessages = frameworkValidationMessages.ToCanonical(),
                Successful         = successful
            };


    }
}