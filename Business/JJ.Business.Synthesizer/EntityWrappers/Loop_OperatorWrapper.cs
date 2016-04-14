using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Loop_OperatorWrapper : OperatorWrapperBase
    {
        public Loop_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Skip
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_SKIP_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SKIP_INDEX).LinkTo(value); }
        }

        public Outlet LoopStartMarker
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet LoopEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet ReleaseEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet NoteDuration
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, OperatorConstants.LOOP_NOTE_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_NOTE_DURATION_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.LOOP_RESULT_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.LOOP_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.LOOP_SKIP_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Skip);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_START_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => LoopStartMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_END_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => LoopEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => ReleaseEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_NOTE_DURATION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => NoteDuration);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Loop_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}