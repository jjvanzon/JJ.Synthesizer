//using JJ.Business.Synthesizer.Resources;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Warnings
//{
//    internal class InletWarningValidator_BootStrapped : VersatileValidator<Inlet>
//    {
//        public InletWarningValidator_BootStrapped(Inlet inlet)
//            : base(inlet)
//        {
//            if (inlet.WarnIfEmpty && inlet.InputOutlet == null)
//            {
//                string identifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);
//                ValidationMessages.AddNotFilledInMessage(nameof(Inlet), identifier);
//            }
//        }
//    }
//}