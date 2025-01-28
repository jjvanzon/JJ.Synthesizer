using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    /// <inheritdoc cref="docs._casecollection" />
    internal class CaseCollection<TCase> : IEnumerable<object[]>
        where TCase : ICase
    {
        // Storage Variables
        
        private readonly IList<TCase> _cases = new List<TCase>();
        private readonly IList<CaseCollection<TCase>> _collections = new List<CaseCollection<TCase>>();
        private readonly Dictionary<string, TCase> _dictionary = new Dictionary<string, TCase>();
        
        // Constructor (empty)
        
        public CaseCollection() { }

        // Constructors (single collection)

        public CaseCollection(params TCase[] cases) : this((ICollection<TCase>)cases) { }
        public CaseCollection(ICollection<TCase> cases) => Initialize(cases);
        
        // Adding (multi-collections)
        
        public CaseCollection<TCase> Add(params TCase[] cases) => Add((IList<TCase>)cases);
        public CaseCollection<TCase> Add(ICollection<TCase> cases) => Add(new CaseCollection<TCase>(cases));
        public CaseCollection<TCase> Add(CaseCollection<TCase> collection)
        {
            if (collection == null) throw new NullException(() => collection);
            _collections.Add(collection);
            
            ICollection<TCase> cases = collection.GetAll();
            
            Initialize(cases);
            
            return collection;
        }
        
        private void Initialize(ICollection<TCase> cases)
        {
            if (cases == null) throw new NullException(() => cases);
            if (cases.Contains(default)) throw new Exception($"{nameof(cases)} collection has empty elements.");
            
            _cases.AddRange(cases);
            
            foreach (TCase testCase in cases)
            {
                string key = testCase.Descriptor;
                
                if (_dictionary.ContainsKey(key))
                {
                    throw new Exception($"Duplicate key '{key}' found while adding to {nameof(cases)} collection.");
                }
                
                _dictionary.Add(key, testCase);
            }
        }
        
        // Get
        
        public TCase this[string descriptor] => Get(descriptor);
        
        public TCase Get(string descriptor)
        {
            if (_dictionary.TryGetValue(descriptor, out TCase testCase)) return testCase;
            throw new Exception($"Case not found: {descriptor}");
        }
                
        public ICollection<TCase> GetAll() => _cases;
        
        // Templating
        
        /// <inheritdoc cref="docs._casetemplate" />
        public CaseCollection<TCase> FromTemplate(TCase template, params TCase[] cases)
            => FromTemplate(template, (ICollection<TCase>)cases);
        
        /// <inheritdoc cref="docs._casetemplate" />
        public CaseCollection<TCase> FromTemplate(TCase template, ICollection<TCase> cases)
        {
            if (template == null) throw new NullException(() => template);
            if (cases == null) throw new NullException(() => cases);
            cases = template.FromTemplate(cases.Cast<ICase>().ToArray()).Cast<TCase>().ToArray();
            return Add(cases);
        }
        
        // DynamicData
                        
        public IEnumerable<object[]> DynamicData => _cases.Select(x => x.DynamicData).ToArray();
        
        public static implicit operator object[][](CaseCollection<TCase> caseCollection)
        {
            if (caseCollection == null) throw new NullException(() => caseCollection);
            return caseCollection.DynamicData.ToArray();
        }

        public IEnumerator<object[]> GetEnumerator() => DynamicData.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
