using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DocumentReferences;
using JJ.Business.Synthesizer.Validation.Patches;
using JJ.Business.Synthesizer.Validation.Scales;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_Recursive : VersatileValidator
	{
		public DocumentValidator_Recursive(
			Document document,
			RepositoryWrapper repositories,
			HashSet<object> alreadyDone)
		{
			if (document == null) throw new NullException(() => document);
			if (repositories == null) throw new NullException(() => repositories);
			if (alreadyDone == null) throw new AlreadyDoneIsNullException();

			if (alreadyDone.Contains(document))
			{
				return;
			}
			alreadyDone.Add(document);

			ExecuteValidator(new DocumentValidator_Basic(document));
			ExecuteValidator(new DocumentValidator_Unicity(document, repositories.DocumentRepository));
			ExecuteValidator(new DocumentValidator_DoesNotReferenceItself(document));
			ExecuteValidator(new DocumentValidator_SystemDocumentReferenceMustExist(document, repositories));

			foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
			{
				ExecuteValidator(new AudioFileOutputValidator(audioFileOutput), ValidationHelper.GetMessagePrefix(audioFileOutput));
			}

			if (document.AudioOutput != null)
			{
				ExecuteValidator(new AudioOutputValidator(document.AudioOutput), ValidationHelper.GetMessagePrefix(document.AudioOutput));
			}

			foreach (Patch patch in document.Patches)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
				ExecuteValidator(
					new PatchValidator_WithRelatedEntities(
						patch,
						repositories.CurveRepository,
						repositories.SampleRepository,
						alreadyDone),
					messagePrefix);
			}

			foreach (Scale scale in document.Scales)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(scale);
				ExecuteValidator(new ScaleValidator_InDocument(scale), messagePrefix);
				ExecuteValidator(new Versatile_ScaleValidator_WithoutTones(scale), messagePrefix);
				ExecuteValidator(new ScaleValidator_Tones(scale), messagePrefix);
			}

			foreach (DocumentReference lowerDocumentReference in document.LowerDocumentReferences)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix_ForLowerDocumentReference(lowerDocumentReference);
				ExecuteValidator(new DocumentReferenceValidator_Basic(lowerDocumentReference), messagePrefix);
			}
		}
	}
}