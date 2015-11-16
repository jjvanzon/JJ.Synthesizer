//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class DocumentValidator_UniqueScaleNames : FluentValidator<Document>
//    {
//        public DocumentValidator_UniqueScaleNames(Document obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            IList<string> duplicateNames = ValidationHelper.GetDuplicateScaleNames(Object);

//            if (duplicateNames.Count > 0)
//            {
//                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.Scale, duplicateNames);
//                ValidationMessages.Add(PropertyNames.Scales, message);
//            }
//        }
//    }
//}
