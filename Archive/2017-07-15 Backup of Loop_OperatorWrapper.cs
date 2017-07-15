//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Loop_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
//    {
//        public Loop_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get => SignalInlet.InputOutlet;
//            set => SignalInlet.LinkTo(value);
//        }

//        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

//        public Outlet Skip
//        {
//            get => SkipInlet.InputOutlet;
//            set => SkipInlet.LinkTo(value);
//        }

//        public Inlet SkipInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Skip);

//        public Outlet LoopStartMarker
//        {
//            get => LoopStartMarkerInlet.InputOutlet;
//            set => LoopStartMarkerInlet.LinkTo(value);
//        }

//        public Inlet LoopStartMarkerInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.LoopStartMarker);

//        public Outlet LoopEndMarker
//        {
//            get => LoopEndMarkerInlet.InputOutlet;
//            set => LoopEndMarkerInlet.LinkTo(value);
//        }

//        public Inlet LoopEndMarkerInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.LoopEndMarker);

//        public Outlet ReleaseEndMarker
//        {
//            get => ReleaseEndMarkerInlet.InputOutlet;
//            set => ReleaseEndMarkerInlet.LinkTo(value);
//        }

//        public Inlet ReleaseEndMarkerInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.ReleaseEndMarker);

//        public Outlet NoteDuration
//        {
//            get => NoteDurationInlet.InputOutlet;
//            set => NoteDurationInlet.LinkTo(value);
//        }

//        public Inlet NoteDurationInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.NoteDuration);
//    }
//}