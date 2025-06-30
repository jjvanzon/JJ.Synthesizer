using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class ValidationMessageExtensions
    {
        public static JJ.Business.CanonicalModel.ValidationMessage ToCanonical(this JJ.Framework.Validation.Legacy.ValidationMessage sourceEntity)
        {
            return new JJ.Business.CanonicalModel.ValidationMessage
            {
                PropertyKey = sourceEntity.PropertyKey,
                Text = sourceEntity.Text
            };
        }

        public static List<JJ.Business.CanonicalModel.ValidationMessage> ToCanonical(this IEnumerable<JJ.Framework.Validation.Legacy.ValidationMessage> sourceList)
        {
            var destList = new List<JJ.Business.CanonicalModel.ValidationMessage>();

            foreach (JJ.Framework.Validation.Legacy.ValidationMessage sourceItem in sourceList)
            {
                JJ.Business.CanonicalModel.ValidationMessage destItem = sourceItem.ToCanonical();
                destList.Add(destItem);
            }

            return destList;
        }
    }
}
