using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ValidationMessageExtensions
    {
        public static JJ.Business.CanonicalModel.Message ToCanonical(
            this JJ.Framework.Validation.ValidationMessage sourceEntity)
        {
            return new JJ.Business.CanonicalModel.Message
            {
                PropertyKey = sourceEntity.PropertyKey,
                Text = sourceEntity.Text
            };
        }

        public static IList<JJ.Business.CanonicalModel.Message> ToCanonical(
            this IEnumerable<JJ.Framework.Validation.ValidationMessage> sourceList)
        {
            var destList = new List<JJ.Business.CanonicalModel.Message>();

            foreach (JJ.Framework.Validation.ValidationMessage sourceItem in sourceList)
            {
                JJ.Business.CanonicalModel.Message destItem = sourceItem.ToCanonical();
                destList.Add(destItem);
            }

            return destList;
        }
    }
}
