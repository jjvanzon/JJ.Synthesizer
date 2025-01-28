using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
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
        internal virtual IList<ICaseProp> Props 
        {
            get
            {
                // ReSharper disable once UnusedVariable
                PropertyInfo[] properties = GetType().GetProperties(BINDING_FLAGS_ALL)
                                                     .Where(x => x.PropertyType.GetBaseClasses().Contains(typeof(ICaseProp)))
                                                     .ToArray();
                return default;
            }
        }
        
        // Descriptions
        
        public string Name { get; set; }
        public override string ToString() => Descriptor;
        public object[] DynamicData => new object[] { Descriptor };
        string DebuggerDisplay => DebuggerDisplay(this);
        internal virtual IList<object> DescriptorElements { get; }
        public string Descriptor => new CaseDescriptorBuilder<TMainProp>(this).BuildDescriptor();

        // Templating

        public CaseBase<TMainProp>[] FromTemplate(params CaseBase<TMainProp>[] destCases) 
            => FromTemplate(this, destCases);

        public static CaseBase<TMainProp>[] FromTemplate(CaseBase<TMainProp> template, params CaseBase<TMainProp>[] destCases) 
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
