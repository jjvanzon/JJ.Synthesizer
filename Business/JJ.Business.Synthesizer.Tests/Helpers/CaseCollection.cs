using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class CaseCollection<TCase> : IEnumerable<object[]>
        where TCase : ICase
    {
        private readonly IList<TCase> _cases = new List<TCase>();
        private readonly IList<CaseCollection<TCase>> _collections = new List<CaseCollection<TCase>>();
        private readonly Dictionary<string, TCase> _dictionary = new Dictionary<string, TCase>();
        
        public CaseCollection() { }
        public CaseCollection(params TCase[] cases) : this((IList<TCase>)cases) { }
        public CaseCollection(IList<TCase> cases)
        {
            _cases.AddRange(AssertCases(cases));

            // TODO: Check duplicate keys and throw exception with descriptor in it.
            cases.ForEach(x => _dictionary.Add(x.Descriptor, x));
        }
        
        public CaseCollection<TCase> Add(params TCase[] cases) => Add((IList<TCase>)cases);
        public CaseCollection<TCase> Add(IList<TCase> cases)
        {
            AssertCases(cases);
            
            _cases.AddRange(cases);
            
            var collection = new CaseCollection<TCase>(cases);
            _collections.Add(collection);
            
            // TODO: Check duplicate keys and throw exception with descriptor in it.
            cases.ForEach(x => _dictionary.Add(x.Descriptor, x));
            
            return collection;
        }

        private IList<TCase> AssertCases(IList<TCase> cases)
        {
            if (cases == null) throw new NullException(() => cases);
            if (cases.Contains(default)) throw new Exception($"{nameof(cases)} collection contains empty elements.");
            return cases;
        }
                
        public IEnumerable<object[]> DynamicData => _cases.Select(x => x.DynamicData).ToArray();

        public TCase this[string descriptor] => Get(descriptor);
        
        public TCase Get(string descriptor)
        {
            if (_dictionary.TryGetValue(descriptor, out TCase testCase)) return testCase;
            throw new Exception($"Case not found: {descriptor}");
        }
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
        public static implicit operator object[][](CaseCollection<TCase> caseCollection)
        {
            if (caseCollection == null) throw new NullException(() => caseCollection);
            return caseCollection.DynamicData.ToArray();
        }
        
        public IEnumerator<object[]> GetEnumerator() => DynamicData.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
