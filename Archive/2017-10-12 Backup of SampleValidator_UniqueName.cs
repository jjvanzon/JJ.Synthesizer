//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Samples
//{
//    internal class SampleValidator_UniqueName : VersatileValidator
//    {
//        /// <summary>
//        /// NOTE:
//        /// Do not always execute this validator everywhere,
//        /// because then validating a document becomes inefficient.
//        /// Extensive document validation will include validating that the Sample names are unique already
//        /// and it will do so in a more efficient way.
//        /// </summary>
//        public SampleValidator_UniqueName(Sample obj)
//        {
//            if (obj == null) throw new NullException(() => obj);

//            if (obj.Document == null)
//            {
//                return;
//            }

//            bool isUnique = ValidationHelper.SampleNameIsUnique(obj);
//            // ReSharper disable once InvertIf
//            if (!isUnique)
//            {
//                Messages.AddNotUniqueMessageSingular(CommonResourceFormatter.Name, obj.Name);
//            }
//        }
//    }
//}
