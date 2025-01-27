using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class CaseBase<TMainProp> : CaseProp<TMainProp>
        where TMainProp : struct
    {
        // Properties

        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;
        
        public CaseProp<TMainProp> MainProp => this;

        internal virtual IList<ICaseProp> Props { get; }
        
        private readonly IList<ICaseProp> _props = new List<ICaseProp>();
        
        protected ICaseProp GetProp(Type type, int index) 
        {
            if (type == null) throw new NullException(() => type);
            while (_props.Count <= index) _props.Add(null); // Auto-size collection
            return _props[index] = _props[index] ?? (ICaseProp)Activator.CreateInstance(type); // Get or Create
        }

        protected CaseProp<TProp> GetProp<TProp>(int index) where TProp: struct
        {
            return (CaseProp<TProp>)GetProp(typeof(CaseProp<TProp>), index); // Cast
        }

        protected void SetProp<T>(int index, CaseProp<T> prop) where T: struct
        {
            if (prop == null) throw new NullException(() => prop);
            while (_props.Count <= index) _props.Add(null); // Auto-size collection
            _props[index] = prop;
        }
        
        // Descriptions
        
        public string Name { get; set; }
        public override string ToString() => Descriptor;
        public object[] DynamicData => new object[] { Descriptor };
        string DebuggerDisplay => DebuggerDisplay(this);
        internal virtual IList<object> DescriptorElements { get; }
        public virtual string Descriptor => new CaseDescriptorBuilder<TMainProp>(this).BuildDescriptor();

        // Templating

        public static CaseBase<TMainProp>[] FromTemplate(CaseBase<TMainProp> template, params CaseBase<TMainProp>[] cases) 
        {
            if (template == null) throw new NullException(() => template);
            return template.CloneTo(cases).ToArray();
        }

        public CaseBase<TMainProp>[] CloneTo(params CaseBase<TMainProp>[] destCases)
        {
            if (destCases == null) throw new NullException(() => destCases);
            if (destCases.Contains(null)) throw new Exception($"{nameof(destCases)} contains nulls.");
            
            var sourceCase = this;
            
            foreach (var destCase in destCases)
            {
                destCase.Name = Coalesce(destCase.Name, Name);
                if (Strict == false)
                {
                    destCase.Strict = false; // Yield over alleviation from strictness.
                }
                
                destCase.CloneFrom(sourceCase);
                
                for (int j = 0; j < sourceCase._props.Count; j++)
                {
                    var sourceProp = _props[j];
                    if (sourceProp != null)
                    {
                        ICaseProp destProp = destCase.GetProp(sourceProp.GetType(), j);
                        destProp.CloneFrom(sourceProp);
                    }
                }
            }
            
            return destCases;
        }

        // Constructors

        public CaseBase() : base() { }
        public CaseBase(TMainProp value) : base(value) { }
        public CaseBase(TMainProp? value) : base(value) { }
        public CaseBase(TMainProp from, TMainProp to) : base(from, to) { }
        public CaseBase(TMainProp from, TMainProp? to) : base(from, to) { }
        public CaseBase(TMainProp? from, TMainProp to) : base(from, to) { }
        public CaseBase(TMainProp? from, TMainProp? to) : base(from, to) { }
        public CaseBase((TMainProp from, TMainProp to) values) : base(values) { }
        public CaseBase((TMainProp? from, TMainProp to) values) : base(values) { }
        public CaseBase((TMainProp from, TMainProp? to) values) : base(values) { }
        public CaseBase((TMainProp? from, TMainProp? to) values) : base(values) { }
        public CaseBase(TMainProp from, (TMainProp? nully, TMainProp coalesced) to) : base(from, to) { }
        public CaseBase((TMainProp? nully, TMainProp coalesced) from, TMainProp to) : base(from, to) { }
        public CaseBase((TMainProp? nully, TMainProp coalesced) from, (TMainProp? nully, TMainProp coalesced) to) : base(from, to) { }
    }
}
