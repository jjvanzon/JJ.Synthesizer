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
	/// Also caches them.
	/// </summary>
	public class SystemFacade
	{
		private static readonly object _lock = new object();

		private static Document _systemDocument;
		private static Dictionary<string, Patch> _systemPatchDictionary;
		private static IList<MidiMappingElement> _systemMidiMappingElements;

		private readonly IDocumentRepository _documentRepository;

		public SystemFacade(IDocumentRepository documentRepository)
		{
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
		}

		private void EnsureCache()
		{
			lock (_lock)
			{
				if (_systemDocument != null)
				{
					return;
				}

				_systemDocument = _documentRepository.GetByNameComplete(DocumentHelper.SYSTEM_DOCUMENT_NAME);
				_systemPatchDictionary = _systemDocument.Patches.Where(x => !x.Hidden).ToDictionary(x => x.Name);
				_systemMidiMappingElements = _systemDocument.MidiMappings.SelectMany(x => x.MidiMappingElements).ToArray();
			}
		}

		public void RefreshSystemDocumentIfNeeded(Document document)
		{
			if (!document.IsSystemDocument())
			{
				return;
			}

			lock (_lock)
			{
				_systemDocument = null;
				_systemPatchDictionary = null;
				_systemMidiMappingElements = null;
			}
		}

		public Document GetSystemDocument()
		{
			EnsureCache();
			return _systemDocument;
		}

		public IList<MidiMappingElement> GetDefaultMidiMappingElements()
		{
			EnsureCache();
			return _systemMidiMappingElements;

		}

		public Patch GetSystemPatch(string name)
		{
			EnsureCache();

			if (!_systemPatchDictionary.TryGetValue(name, out Patch patch))
			{
				throw new NotFoundException<Patch>(new { name, hidden = false });
			}

			return patch;
		}
	}
}