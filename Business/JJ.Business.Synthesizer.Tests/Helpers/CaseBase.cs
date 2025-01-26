using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class CaseBase<TMainProp> : CaseProp<TMainProp>
        where TMainProp : struct
    {
        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;

        // Descriptions
        
        public string Name { get; set; }
        public override string ToString() => Descriptor;
        public object[] DynamicData => new object[] { Descriptor };
        
        // Properties
        
        private readonly List<ICaseProp> _caseProps = new List<ICaseProp>();
        
        protected CaseProp<T> GetProp<T>(int index) where T: struct
        {
            // Auto-size collection
            while (_caseProps.Count <= index) _caseProps.Add(null);
            
            // Get
            ICaseProp obj = _caseProps[index]; 
            
            // TODO: Throw better exception message in case of wrong cast.

            // Cast
            var prop = (CaseProp<T>)obj; 
            
            // Or overwrite
            prop = prop ?? new CaseProp<T>(); 
            
            // Store
            _caseProps[index] = prop;
            
            return prop;
        }
        
        protected void SetProp<T>(int index, CaseProp<T> prop) where T: struct
        {
            while (_caseProps.Count <= index) _caseProps.Add(null);
            _caseProps[index] = prop ?? throw new NullException(() => prop);;
        }
        
        // Constructors
        
        public CaseBase() : base() { }
        public CaseBase( TMainProp  value) : base(value) { }
        public CaseBase( TMainProp? value) : base(value) { }
        public CaseBase( TMainProp  from ,  TMainProp  to) : base(from, to) { }
        public CaseBase( TMainProp  from ,  TMainProp? to) : base(from, to) { }
        public CaseBase( TMainProp? from ,  TMainProp  to) : base(from, to) { }
        public CaseBase( TMainProp? from ,  TMainProp? to) : base(from, to) { }
        public CaseBase((TMainProp  from ,  TMainProp  to) values) : base(values) { }
        public CaseBase((TMainProp? from ,  TMainProp  to) values) : base(values) { }
        public CaseBase((TMainProp  from ,  TMainProp? to) values) : base(values) { }
        public CaseBase((TMainProp? from ,  TMainProp? to) values) : base(values) { }
        public CaseBase( TMainProp  from , (TMainProp? nully         ,  TMainProp coalesced) to) : base(from, to) { }
        public CaseBase((TMainProp? nully,  TMainProp coalesced) from,  TMainProp to) : base(from, to) { }
        public CaseBase((TMainProp? nully,  TMainProp coalesced) from, (TMainProp? nully, TMainProp coalesced) to) : base(from, to) { }
    }
}
