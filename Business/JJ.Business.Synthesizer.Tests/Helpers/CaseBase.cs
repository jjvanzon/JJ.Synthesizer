using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
using static System.Activator;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;
using static JJ.Framework.Reflection.ReflectionHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal abstract class CaseBase<TMainProp> : CaseProp<TMainProp>, ICase
        where TMainProp : struct
    {
        // Properties

        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;
        public CaseProp<TMainProp> MainProp => this;
        
        public virtual IList<ICaseProp> Props
            => GetCasePropInfos().Select(x => x.GetValue(this))
                             .Cast<ICaseProp>()
                             .Distinct()
                             .ToArray();
        
        private IList<PropertyInfo> GetCasePropInfos()
            => GetType().GetProperties(BINDING_FLAGS_ALL)
                        .Where(x => x.PropertyType.HasInterfaceRecursive<ICaseProp>())
                        .ToArray();
        
        private IList<FieldInfo> GetCasePropFields()
            => GetType().GetFields(BINDING_FLAGS_ALL)
                        .Where(x => x.FieldType.HasInterfaceRecursive<ICaseProp>())
                        .ToArray();
        
        private void AutoCreateProps() => GetCasePropFields().Where(x => x.GetValue(this) == null)
                                                     .ForEach(x => x.SetValue(this, CreateInstance(x.FieldType)));

        // Descriptions
        
        public string Name { get; set; }
        public override string ToString() => Key;
        public object[] DynamicData => new object[] { Key };
        string DebuggerDisplay => DebuggerDisplay(this);
        internal virtual IList<object> KeyElements { get; }
        public string Key => new CaseKeyBuilder<TMainProp>(this).BuildKey();

        // Templating

        // Instance
        
        /// <inheritdoc cref="docs._casetemplate" />
        public ICase[] FromTemplate(params ICase[] destCases) 
            => FromTemplate(this, destCases);
        
        /// <inheritdoc cref="docs._casetemplate" />
        public ICollection<ICase> FromTemplate(ICollection<ICase> destCases) 
            => FromTemplate(this, destCases);

        // Static
        
        /// <inheritdoc cref="docs._casetemplate" />
        public static ICase[] FromTemplate(ICase template, params ICase[] destCases) 
            => FromTemplate(template, (ICollection<ICase>)destCases).ToArray();

        /// <inheritdoc cref="docs._casetemplate" />
        public static ICollection<ICase> FromTemplate(ICase template, ICollection<ICase> destCases) 
        {
            if (template == null) throw new NullException(() => template);
            if (destCases == null) throw new NullException(() => destCases);
            if (destCases.Contains(null)) throw new Exception($"{nameof(destCases)} contains nulls.");
            
            var sourceCase = template;
            
            foreach (var destCase in destCases)
            {
                // Basic
                destCase.Name = Coalesce(destCase.Name, template.Name);
                if (template.Strict == false)
                {
                    destCase.Strict = false; // Yield over alleviation from strictness.
                }
                
                // Main Prop
                destCase.CloneFrom(sourceCase);
                
                // Props
                for (int j = 0; j < template.Props.Count; j++)
                {
                    var sourceProp = template.Props[j];
                    if (sourceProp != null)
                    {
                        ICaseProp destProp = destCase.Props[j];
                        destProp.CloneFrom(sourceProp);
                    }
                }
            }
            
            return destCases;
        }

        // Constructors

        public CaseBase() : base() => Initialize();
        public CaseBase(TMainProp value) : base(value) => Initialize();
        public CaseBase(TMainProp? value) : base(value) => Initialize();
        public CaseBase(TMainProp from, TMainProp to) : base(from, to) => Initialize();
        public CaseBase(TMainProp from, TMainProp? to) : base(from, to) => Initialize();
        public CaseBase(TMainProp? from, TMainProp to) : base(from, to) => Initialize();
        public CaseBase(TMainProp? from, TMainProp? to) : base(from, to) => Initialize();
        public CaseBase((TMainProp from, TMainProp to) values) : base(values) => Initialize();
        public CaseBase((TMainProp? from, TMainProp to) values) : base(values) => Initialize();
        public CaseBase((TMainProp from, TMainProp? to) values) : base(values) => Initialize();
        public CaseBase((TMainProp? from, TMainProp? to) values) : base(values) => Initialize();
        public CaseBase(TMainProp from, (TMainProp? nully, TMainProp coalesced) to) : base(from, to) => Initialize();
        public CaseBase((TMainProp? nully, TMainProp coalesced) from, TMainProp to) : base(from, to) => Initialize();
        public CaseBase((TMainProp? nully, TMainProp coalesced) from, (TMainProp? nully, TMainProp coalesced) to) : base(from, to) => Initialize();
        
        private void Initialize() => AutoCreateProps();
    }
}
