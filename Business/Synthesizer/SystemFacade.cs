using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer
{
	/// <summary>
	/// Helps retrieve 'system' entities: 
	/// a default set of synthesizer objects, 
	/// that are put in the system document,
	/// such as the configurations of standard operators.
	/// </summary>
	public class SystemFacade
	{
		private static Document _systemDocument;
		private static readonly object _systemDocumentLock = new object();

		private static Dictionary<string, Patch> _systemPatchDictionary;
		private static readonly object _systemPatchDictionaryLock = new object();

		private readonly IDocumentRepository _documentRepository;

		public SystemFacade(IDocumentRepository documentRepository)
		{
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
		}

		public Document GetSystemDocument()
		{
			lock (_systemDocumentLock)
			{
				if (_systemDocument == null)
				{
					_systemDocument = _documentRepository.GetByNameComplete(DocumentHelper.SYSTEM_DOCUMENT_NAME);
				}

				return _systemDocument;
			}
		}

		private Dictionary<string, Patch> GetSystemPatchDictionary()
		{
			lock (_systemPatchDictionaryLock)
			{
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
			return patch;
		}

		public IList<MidiMappingElement> GetDefaultMidiMappingElements()
		{
			return GetSystemDocument().MidiMappings.SelectMany(x => x.MidiMappingElements).ToArray();
		}
	}
}