//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Documents
//{
//    internal class RootDocument_DocumentValidator : FluentValidator<Document>
//    {
//        public RootDocument_DocumentValidator(Document obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            For(() => Object.AudioOutput, PropertyDisplayNames.AudioOutput).NotNull();
//        }
//    }
//}
