//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using JJ.Framework.Reflection;

//namespace JJ.Business.Synthesizer.Tests.Helpers
//{
//    public class CaseCollection<TCase>
//        where TCase : ICase
//    {
//        private readonly IList<TCase> _cases;

//        public CaseCollection(IList<TCase> cases)
//        {
//            if (cases == null) throw new NullException(() => cases);
//            if (cases.Contains(default)) throw new Exception($"{nameof(cases)} contains empty elements.");
//            _cases = cases;
//        }

//        public IEnumerable<object[]> DynamicData => _cases.Select(x => x.DynamicData).ToArray();
//    }
//}
