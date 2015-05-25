using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class DocumentValidator_Basic : FluentValidator<Document>
    {
        public DocumentValidator_Basic(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            if (document == null) throw new NullException(() => document);

            string messagePrefix = PropertyDisplayNames.Document + ": ";

            Execute(new NameValidator(document.Name), messagePrefix);
        }
    }
}
