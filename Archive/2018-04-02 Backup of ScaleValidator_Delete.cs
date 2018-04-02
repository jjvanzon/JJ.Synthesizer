//using System;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Scales
//{
//	internal class ScaleValidator_Delete : VersatileValidator
//	{
//		public ScaleValidator_Delete(Scale scale)
//		{
//			if (scale == null) throw new ArgumentNullException(nameof(scale));

//			string scaleIdentifier = ResourceFormatter.Scale + " " + ValidationHelper.GetUserFriendlyIdentifier(scale);

//			foreach (MidiMapping midiMapping in scale.MidiMapping)
//			{
//				string midiMappingElemenIdentifier = $"{ResourceFormatter.MidiMapping} ({ValidationHelper.GetUserFriendlyIdentifierLong(midiMapping)})";
//				string message = CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(scaleIdentifier, midiMappingElemenIdentifier);
//				Messages.Add(message);
//			}
//		}
//	}
//}
