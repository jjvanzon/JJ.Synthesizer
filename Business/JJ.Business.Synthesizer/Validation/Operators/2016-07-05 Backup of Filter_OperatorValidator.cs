//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Validation.DataProperty;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal class Filter_OperatorValidator : OperatorValidator_Base
//    {
//        public Filter_OperatorValidator(Operator obj)
//            : base(
//                  obj, 
//                  OperatorTypeEnum.Filter, 
//                  expectedInletCount: 5, 
//                  expectedOutletCount: 1,
//                  expectedDataKeys: new string[] { PropertyNames.FilterType })
//        { }

//        protected override void Execute()
//        {
//            base.Execute();

//            Execute(new FilterType_DataProperty_Validator(Object.Data));
//        }
//    }
//}
