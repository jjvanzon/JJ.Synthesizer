//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class InletsValidator_ListIndexConsecutive : FluentValidator<IList<Inlet>>
//    {
//        public InletsValidator_ListIndexConsecutive(IList<Inlet> obj) 
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            IList<Inlet> inlets = Object;

//            IList<Inlet> sortedInlets = inlets.OrderBy(x => x.ListIndex).ToArray();
//            for (int i = 0; i < inlets.Count; i++)
//            {
//                For(() => inlets[i], GetPropertyDisplayName_ForInletListIndex(i)).Is(i);
//            }
//        }

//        private string GetPropertyDisplayName_ForInletListIndex(int index)
//        {
//            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, PropertyDisplayNames.ListIndex);
//        }
//    }
//}
