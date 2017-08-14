//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer.Dto
//{
//    internal abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
//    {
//        public IList<InputDto> Vars { get; set; }
//        public InputDto Const { get; set; }

//        public override IReadOnlyList<InputDto> Inputs
//        {
//            get => Vars.Union(Const).ToArray();
//            set
//            {
//                Vars = value.Where(x => x.IsVar).ToArray();
//                Const = value.Where(x => x.IsConst).FirstOrDefault();
//            }
//        }
//    }
//}
