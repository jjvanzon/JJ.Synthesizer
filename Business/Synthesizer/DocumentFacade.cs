using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.DocumentReferences;
using JJ.Business.Synthesizer.Validation.Documents;
using JJ.Business.Synthesizer.Warnings;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	public class DocumentFacade
	{
		private readonly RepositoryWrapper _repositories;

		public DocumentFacade(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
		}

		// Get

		public Document Get(int id)
		{
			Document document = _repositories.DocumentRepository.GetComplete(id);
			Refresh(document);
			return document;
		}

		public Document Get(string name)
		{
			Document document = _repositories.DocumentRepository.GetByNameComplete(name);
			Refresh(document);
			return document;
		}

		/// <summary> Will reapply patches (from external documents). </summary>
		public void Refresh(int id)
		{
			Document document = _repositories.DocumentRepository.Get(id);
			Refresh(document);
		}

		/// <summary> Will reapply patches (from external documents). </summary>
		public void Refresh(Document document)
		{
			new Document_SideEffect_ApplyUnderlyingPatches(document, _repositories).Execute();

			// This is sensitive, error prone code, so verify its result.
			VoidResult result = Save(document);
			result.Assert();
		}

		// Create
		
		public Document Create()
		{
			var document = new Document { ID = _repositories.IDRepository.GetID() };
			_repositories.DocumentRepository.Insert(document);

			new Document_SideEffect_GenerateName(document, _repositories.DocumentRepository).Execute();
			new Document_SideEffect_AutoCreate_SystemDocumentReference(document, _repositories).Execute();
			new Document_SideEffect_AutoCreate_AudioOutput(
				document,
				_repositories.AudioOutputRepository,
				_repositories.SpeakerSetupRepository,
				_repositories.IDRepository)
				.Execute();

			IResult result = Save(document);
			result.Assert();

			return document;
		}
		
		public Document CreateWithPatch()
		{
			Document document = Create();

			new Document_SideEffect_CreatePatch(document, _repositories).Execute();

			return document;
		}

		public Result<DocumentReference> CreateDocumentReference(Document higherDocument, Document lowerDocument)
		{
			if (higherDocument == null) throw new NullException(() => higherDocument);
			if (lowerDocument == null) throw new ArgumentNullException(nameof(lowerDocument));

			DocumentReference documentReference = _repositories.DocumentReferenceRepository.Create();
			documentReference.ID = _repositories.IDRepository.GetID();
			documentReference.LinkToHigherDocument(higherDocument);
			documentReference.LinkToLowerDocument(lowerDocument);

			var result = new Result<DocumentReference>
			{
				Successful = true,
				Data = documentReference
			};

			var validators = new IValidator[]
			{
				new DocumentReferenceValidator_Basic(documentReference),
				new DocumentReferenceValidator_DoesNotReferenceItself(documentReference),
				new DocumentReferenceValidator_UniqueLowerDocument(documentReference),
				new DocumentReferenceValidator_UniqueAlias(documentReference)
			};

			validators.ToResult(result);

			return result;
		}

		// Save

		public VoidResult Save(Document document)
		{
			if (document == null) throw new NullException(() => document);

			IValidator validator = new DocumentValidator_WithRelatedEntities(document, _repositories, new HashSet<object>());

			return validator.ToResult();
		}

		public VoidResult SaveDocumentReference(DocumentReference documentReference)
		{
			IValidator validator = new DocumentReferenceValidator_Basic(documentReference);

			return validator.ToResult();
		}

		// Delete
		
		public VoidResult DeleteWithRelatedEntities(int documentID)
		{
			Document document = _repositories.DocumentRepository.Get(documentID);
			return DeleteWithRelatedEntities(document);
		}
		
		public VoidResult DeleteWithRelatedEntities(Document document)
		{
			if (document == null) throw new NullException(() => document);

			VoidResult result = CanDelete(document);
			if (!result.Successful)
			{
				return result;
			}

			document.DeleteRelatedEntities(_repositories);
			_repositories.DocumentRepository.Delete(document);

			// Order-Dependence:
			// You need to postpone deleting this 1-to-1 related entity till after deleting the document, 
			// or ORM will try to update Document.AudioOutputID to null and crash.
			if (document.AudioOutput != null)
			{
				document.AudioOutput.UnlinkRelatedEntities();
				_repositories.AudioOutputRepository.Delete(document.AudioOutput);
			}

			return new VoidResult { Successful = true };
		}

		public VoidResult CanDelete(Document document)
		{
			IValidator validator = new DocumentValidator_Delete(document);
			return validator.ToResult();
		}

		public VoidResult DeleteDocumentReference(int documentReferenceID)
		{
			DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(documentReferenceID);
			VoidResult result = DeleteDocumentReference(documentReference);
			return result;
		}

		public VoidResult DeleteDocumentReference(DocumentReference documentReference)
		{
			if (documentReference == null) throw new NullException(() => documentReference);

			IValidator validator = new DocumentReferenceValidator_Delete(documentReference, _repositories.CurveRepository);

			// ReSharper disable once InvertIf
			if (validator.IsValid)
			{
				documentReference.UnlinkHigherDocument();
				documentReference.UnlinkLowerDocument();
				_repositories.DocumentReferenceRepository.Delete(documentReference);
			}

			return validator.ToResult();
		}

		// Other

		public IList<Document> GetLowerDocumentCandidates(Document higherDocument)
		{
			if (higherDocument == null) throw new NullException(() => higherDocument);

			HashSet<int> idsToExcludeHashSet = higherDocument.LowerDocumentReferences.Select(x => x.LowerDocument.ID).ToHashSet();
			idsToExcludeHashSet.Add(higherDocument.ID);

			IList<Document> allDocuments = _repositories.DocumentRepository.GetAll();
			IList<Document> lowerDocumentCandidates = allDocuments.Except(x => idsToExcludeHashSet.Contains(x.ID)).ToArray();

			return lowerDocumentCandidates;
		}

		public VoidResult GetWarningsRecursive(Document entity)
		{
			if (entity == null) throw new NullException(() => entity);

			IValidator warningsValidator = new DocumentWarningValidator_Recursive(
				entity,
				_repositories.CurveRepository,
				_repositories.SampleRepository,
				new HashSet<object>());

			return warningsValidator.ToResult();
		}

		public IList<IDAndName> GetUsedIn(Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);
			if (patch.Document == null) throw new NullException(() => patch.Document);

			IList<Operator> internalOperators =
				patch.Document
					 .Patches
					 .SelectMany(x => x.Operators)
					 .Where(x => x.UnderlyingPatch?.ID == patch.ID)
					 .ToArray();

			var idAndNames = new List<IDAndName>();

			IList<Patch> internalHigherPatches = internalOperators.Select(x => x.Patch)
																  .Distinct(x => x.ID)
																  .OrderBy(x => x.Name)
																  .ToArray();

			foreach (Patch internalHigherPatch in internalHigherPatches)
			{
				idAndNames.Add(new IDAndName { ID = internalHigherPatch.ID, Name = internalHigherPatch.Name });
			}

			IList<Operator> flushedOperators = _repositories.OperatorRepository.GetManyByUnderlyingPatchID(patch.ID);

			IList<Operator> externalOperators = flushedOperators.Where(x => x.Patch != null && // Handles orphaned operators up for deletion.
																			x.Patch.Document.ID != patch.Document.ID)
																.ToArray();

			IList<Patch> externalHigherPatches = externalOperators.Select(x => x.Patch)
																  .Distinct(x => x.ID)
																  .OrderBy(x => x.Document.Name)
																  .ThenBy(x => x.Name)
																  .ToArray();

			foreach (Patch externalHigherPatch in externalHigherPatches)
			{
				string name = externalHigherPatch.Document.Name + ": " + externalHigherPatch.Name;
				idAndNames.Add(new IDAndName { ID = externalHigherPatch.ID, Name = name });
			}

			return idAndNames;
		}

		// System Document

		private static Document _systemDocument;
		private static readonly object _systemDocumentLock = new object();

		private static Dictionary<string, Patch> _systemPatchDictionary;
		private static readonly object _systemPatchDictionaryLock = new object();

		public Document GetSystemDocument()
		{
			lock (_systemDocumentLock)
			{
				// ReSharper disable once ConvertIfStatementToNullCoalescingExpression
				if (_systemDocument == null)
				{
					_systemDocument = _repositories.DocumentRepository.GetByNameComplete(DocumentHelper.SYSTEM_DOCUMENT_NAME);
				}
				return _systemDocument;
			}
		}

		private Dictionary<string, Patch> GetSystemPatchDictionary()
		{
			lock (_systemPatchDictionaryLock)
			{
				// ReSharper disable once ConvertIfStatementToNullCoalescingExpression
				if (_systemPatchDictionary == null)
				{
					_systemDocument = GetSystemDocument();
					_systemPatchDictionary = _systemDocument.Patches.Where(x => !x.Hidden).ToDictionary(x => x.Name);
				}
				return _systemPatchDictionary;
			}
		}

		public void RefreshSystemDocumentIfNeeded(Document document)
		{
			// ReSharper disable once InvertIf
			if (document.IsSystemDocument())
			{
				lock (_systemDocumentLock)
				{
					_systemDocument = null;
					_systemPatchDictionary = null;
				}
			}
		}

		public Patch GetSystemPatch(string name)
		{
			Patch patch = TryGetSystemPatch(name);

			if (patch == null)
			{
				throw new NotFoundException<Patch>(new { name, hidden = false });
			}

			return patch;
		}

		private Patch TryGetSystemPatch(string name)
		{
			GetSystemPatchDictionary().TryGetValue(name, out Patch patch);

			if (patch == null)
			{
				return null;
			}

			// Get from current context, instead of cached context.
			Patch patch2 = _repositories.PatchRepository.Get(patch.ID);

			return patch2;
		}
	}
}
