using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Aggregates;
using JJ.Framework.Exceptions.Basic;

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
        /// <summary> Hashset for value comparisons. </summary>
        private static readonly HashSet<string> _defaultCanonicalMidiMappingGroupNames = GetDefaultMidiMappingGroupNames();
        private static readonly string _defaultScaleName = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultScaleName;
        private static readonly object _lock = new object();

        private static Document _systemDocument;
        private static Dictionary<string, Patch> _systemPatchDictionary;
        private static IList<MidiMapping> _systemMidiMappings;
        private static IList<MidiMappingGroup> _systemMidiMappingGroups;
        private static Scale _systemScale;

        private readonly IDocumentRepository _documentRepository;

        public SystemFacade(IDocumentRepository documentRepository) =>
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));

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
                _systemMidiMappingGroups = _systemDocument.MidiMappingGroups
                                                          .Where(x => _defaultCanonicalMidiMappingGroupNames.Contains(NameHelper.ToCanonical(x.Name)))
                                                          .ToArray();
                _systemMidiMappings = _systemMidiMappingGroups.SelectMany(x => x.MidiMappings).ToArray();
                _systemScale = _systemDocument.Scales.FirstOrDefault(x => NameHelper.AreEqual(x.Name, _defaultScaleName));
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
                _systemMidiMappingGroups = null;
                _systemMidiMappings = null;
                _systemScale = null;
            }
        }

        public Document GetSystemDocument()
        {
            EnsureCache();
            return _systemDocument;
        }

        public IList<MidiMappingGroup> GetDefaultMidiMappingGroups()
        {
            EnsureCache();
            return _systemMidiMappingGroups;
        }

        public IList<MidiMapping> GetDefaultMidiMappings()
        {
            EnsureCache();
            return _systemMidiMappings;
        }

        // ReSharper disable once UnusedMember.Global
        public Patch GetSystemPatch(OperatorTypeEnum operatorTypeEnum) => GetSystemPatch($"{operatorTypeEnum}");

        public Patch GetSystemPatch(string name)
        {
            EnsureCache();

            if (!_systemPatchDictionary.TryGetValue(name, out Patch patch))
            {
                throw new NotFoundException<Patch>(new { name, hidden = false });
            }

            return patch;
        }

        public Scale GetDefaultScale()
        {
            EnsureCache();
            return _systemScale;
        }

        // Helpers

        private static HashSet<string> GetDefaultMidiMappingGroupNames()
        {
            IList<string> names = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultMidiMappingGroupNames;

            if (names == null)
            {
                throw new NullException($"{nameof(ConfigurationSection)}.{nameof(ConfigurationSection.DefaultMidiMappingGroupNames)}");
            }

            HashSet<string> formattedNames = names.Select(NameHelper.ToCanonical).ToHashSet();

            return formattedNames;
        }
    }
}