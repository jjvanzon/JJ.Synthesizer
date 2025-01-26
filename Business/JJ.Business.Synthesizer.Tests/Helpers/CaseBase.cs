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
    internal class CaseBase<TMainProp> : CaseProp<TMainProp>
        where TMainProp : struct
    {
        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;

        // Descriptions
        
        public string Name { get; set; }
        public override string ToString() => Descriptor;
        public object[] DynamicData => new object[] { Descriptor };
        string DebuggerDisplay => DebuggerDisplay(this);
        
        // Properties
        
        private readonly List<ICaseProp> _props = new List<ICaseProp>();
        
        protected CaseProp<TProp> GetProp<TProp>(int index) where TProp: struct
        {
            //while (_props.Count <= index) _props.Add(null); // Auto-size collection
            //_props[index] = _props[index] ?? new CaseProp<T>(); // Get or Create
            //return (CaseProp<T>)_props[index]; // Cast
            return (CaseProp<TProp>)GetProp(typeof(CaseProp<TProp>), index); // Cast
        }
        
        protected ICaseProp GetProp(Type type, int index) 
        {
            while (_props.Count <= index) _props.Add(null); // Auto-size collection
            return _props[index] = _props[index] ?? (ICaseProp)Activator.CreateInstance(type); // Get or Create
        }

        protected void SetProp<T>(int index, CaseProp<T> prop) where T: struct
        {
            while (_props.Count <= index) _props.Add(null);
            _props[index] = prop ?? throw new NullException(() => prop);;
        }
        
        // Constructors
        
        // Not strictly required
        //public CaseBase() : base() { }
        //public CaseBase( T  value) : base(value) { }
        //public CaseBase( T? value) : base(value) { }
        //public CaseBase( T  from ,  T  to) : base(from, to) { }
        //public CaseBase( T  from ,  T? to) : base(from, to) { }
        //public CaseBase( T? from ,  T  to) : base(from, to) { }
        //public CaseBase( T? from ,  T? to) : base(from, to) { }
        //public CaseBase((T  from ,  T  to) values) : base(values) { }
        //public CaseBase((T? from ,  T  to) values) : base(values) { }
        //public CaseBase((T  from ,  T? to) values) : base(values) { }
        //public CaseBase((T? from ,  T? to) values) : base(values) { }
        //public CaseBase( T  from , (T? nully         ,  T coalesced) to) : base(from, to) { }
        //public CaseBase((T? nully,  T coalesced) from,  T to) : base(from, to) { }
        //public CaseBase((T? nully,  T coalesced) from, (T? nully, T coalesced) to) : base(from, to) { }
 
        // Templating
        
        public static CaseBase<TMainProp>[] FromTemplate(CaseBase<TMainProp> template, params CaseBase<TMainProp>[] cases) 
            //where U : struct
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
                // Clone Basics
                destCase.Name = Coalesce(destCase.Name, Name);
                if (Strict == false)
                {
                    destCase.Strict = false; // Yield over alleviation from strictness.
                }

                // Auto-size dest collection
                //while (destCase._props.Count < _props.Count) destCase._props.Add(null);
                
                destCase.CloneFrom(sourceCase);
                
                // Clone props
                for (int j = 0; j < _props.Count; j++)
                {
                    //ICaseProp sourceProp = GetProp(j);
                    ICaseProp sourceProp = _props[j];
                    
                    // Skip empty entries from cloning
                    if (sourceProp == null) continue;

                    ICaseProp destProp = destCase.GetProp(sourceProp.GetType(), j); 
                    
                    // Get
                    //ICaseProp destProp = _props[j]; 
                    
                    // Or overwrite
                    //destProp = destProp ?? (ICaseProp)Activator.CreateInstance(sourceProp.GetType());

                    // Clone prop
                    destProp.CloneFrom(sourceProp);
                }
            }
            return destCases;
        }
    }
}
