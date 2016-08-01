//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class InletValidator_ForCustomOperator : FluentValidator<Inlet>
//    {
//        public InletValidator_ForCustomOperator(Inlet obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            For(() => Object.GetDimensionEnum(), PropertyDisplayNames.Dimension).IsEnum<DimensionEnum>();
//        }
//    }
//}
