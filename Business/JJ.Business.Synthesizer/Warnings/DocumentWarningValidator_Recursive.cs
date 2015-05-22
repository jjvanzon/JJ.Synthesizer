using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class DocumentWarningValidator_Recursive : FluentValidator<Document>
    {
        public DocumentWarningValidator_Recursive(Document obj)
            : base (obj)
        { }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
