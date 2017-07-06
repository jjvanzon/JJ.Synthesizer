//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Pulse_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
//    {
//        public Pulse_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Frequency
//        {
//            get => FrequencyInlet.InputOutlet;
//            set => FrequencyInlet.LinkTo(value);
//        }

//        public Inlet FrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);

//        public Outlet Width
//        {
//            get => WidthInlet.InputOutlet;
//            set => WidthInlet.LinkTo(value);
//        }

//        public Inlet WidthInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Width);
//    }
//}