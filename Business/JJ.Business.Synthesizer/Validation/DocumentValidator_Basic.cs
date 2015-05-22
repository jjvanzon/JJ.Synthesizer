using JJ.Business.Synthesizer.Constants;
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
    public class DocumentValidator_Basic : FluentValidator<Document>
    {
        public DocumentValidator_Basic(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            if (document == null) throw new NullException(() => document);

            For(() => document.Name, CommonTitles.Name)
                .NotNullOrWhiteSpace()
                .MaxLength(DefaultConstants.NAME_MAX_LENGTH)
                .NotInteger();
        }
    }
}
