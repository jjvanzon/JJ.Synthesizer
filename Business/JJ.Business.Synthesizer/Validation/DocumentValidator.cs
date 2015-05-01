using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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
    public class DocumentValidator : FluentValidator<Document>
    {
        public DocumentValidator(Document document)
            : base(document)
        {}

        protected override void Execute()
        {
            Document document = Object;

            if (document == null) throw new NullException(() => document);

            For(() => document.Name, CommonTitles.Name)
                .NotNullOrWhiteSpace()
                .NotInteger();
        }
    }
}
