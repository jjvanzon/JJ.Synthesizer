using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class CaseBase<TMainProp>
        where TMainProp : struct
    {
        public CaseProp<TMainProp> MainProp { get; set; }
            
        /// <inheritdoc cref="docs._strict />
        public bool Strict { get; set; } = true;

        public CaseBase()
        {
            MainProp = new CaseProp<TMainProp>();
        }
        
        public CaseBase(CaseProp<TMainProp> mainProp)
        {
            MainProp = mainProp;
        }
    }
}
