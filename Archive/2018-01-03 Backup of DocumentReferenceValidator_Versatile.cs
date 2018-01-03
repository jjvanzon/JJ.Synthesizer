//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.DocumentReferences
//{
//	internal class DocumentReferenceValidator_Versatile : VersatileValidator
//	{
//		public DocumentReferenceValidator_Versatile(DocumentReference documentReference) 
//		{
//			ExecuteValidator(new DocumentReferenceValidator_Basic(documentReference));
//			ExecuteValidator(new DocumentReferenceValidator_DoesNotReferenceItself(documentReference));
//			ExecuteValidator(new DocumentReferenceValidator_UniqueAlias(documentReference));
//			ExecuteValidator(new DocumentReferenceValidator_UniqueLowerDocument(documentReference));
//		}
//	}
//}
