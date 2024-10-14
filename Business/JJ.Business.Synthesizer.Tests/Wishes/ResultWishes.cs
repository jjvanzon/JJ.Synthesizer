using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class ResultWishes
    {
        public static CanonicalModel.ValidationMessage ToCanonical(
            this Framework.Validation.ValidationMessage frameworkValidationMessage)
        {
            if (frameworkValidationMessage == null) throw new NullException(() => frameworkValidationMessage);

            return new CanonicalModel.ValidationMessage
            {
                PropertyKey = frameworkValidationMessage.PropertyKey,
                Text        = frameworkValidationMessage.Text
            };
        }
        
        public static List<CanonicalModel.ValidationMessage> ToCanonical(
            this IList<Framework.Validation.ValidationMessage> frameworkValidationMessages)
        {
            if (frameworkValidationMessages == null) throw new NullException(() => frameworkValidationMessages);
            return frameworkValidationMessages.Select(x => x.ToCanonical()).ToList();
        }
        
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