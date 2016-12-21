using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class OperatorTypeRepository : DefaultRepositories.OperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        {
            EnsureEntity(1, "Add");
            //EnsureEntity(2, "Adder");
            EnsureEntity(3, "Divide");
            EnsureEntity(4, "MultiplyWithOrigin");
            EnsureEntity(5, "PatchInlet");
            EnsureEntity(6, "PatchOutlet");
            EnsureEntity(7, "Power");
            EnsureEntity(8, "Sine", hasDimension: true);
            EnsureEntity(9, "Subtract");
            //EnsureEntity(10, "Delay", hasDimension: true);
            //EnsureEntity(11, "SpeedUp", hasDimension: true);
            //EnsureEntity(12, "SlowDown", hasDimension: true);
            EnsureEntity(13, "TimePower", hasDimension: true);
            //EnsureEntity(14, "Earlier", hasDimension: true);
            EnsureEntity(15, "Number");
            EnsureEntity(16, "Curve", hasDimension: true);
            EnsureEntity(17, "Sample", hasDimension: true);
            EnsureEntity(18, "Noise", hasDimension: true);
            EnsureEntity(19, "Interpolate", hasDimension: true);
            EnsureEntity(20, "CustomOperator");
            EnsureEntity(21, "SawUp", hasDimension: true);
            EnsureEntity(22, "Square", hasDimension: true);
            EnsureEntity(23, "Triangle", hasDimension: true);
            EnsureEntity(24, "Exponent");
            EnsureEntity(25, "Loop", hasDimension: true);
            EnsureEntity(26, "Select", hasDimension: true);
            //EnsureEntity(27, "Bundle", hasDimension: true);
            //EnsureEntity(28, "Unbundle", hasDimension: true);
            EnsureEntity(29, "Stretch", hasDimension: true);
            EnsureEntity(30, "Squash", hasDimension: true);
            EnsureEntity(31, "Shift", hasDimension: true);
            EnsureEntity(32, "Reset");
            EnsureEntity(33, "LowPassFilter");
            EnsureEntity(34, "HighPassFilter");
            EnsureEntity(35, "Spectrum", hasDimension: true);
            EnsureEntity(36, "Pulse", hasDimension: true);
            EnsureEntity(37, "Random", hasDimension: true);
            EnsureEntity(38, "Equal");
            EnsureEntity(39, "NotEqual");
            EnsureEntity(40, "LessThan");
            EnsureEntity(41, "GreaterThan");
            EnsureEntity(42, "LessThanOrEqual");
            EnsureEntity(43, "GreaterThanOrEqual");
            EnsureEntity(44, "And");
            EnsureEntity(45, "Or");
            EnsureEntity(46, "Not");
            EnsureEntity(47, "If");
            EnsureEntity(48, "MinFollower", hasDimension: true);
            EnsureEntity(49, "MaxFollower", hasDimension: true);
            EnsureEntity(50, "AverageFollower", hasDimension: true);
            EnsureEntity(51, "Scaler");
            EnsureEntity(52, "SawDown", hasDimension: true);
            EnsureEntity(53, "Absolute");
            EnsureEntity(54, "Reverse", hasDimension: true);
            EnsureEntity(55, "Round");
            EnsureEntity(56, "Negative");
            EnsureEntity(57, "OneOverX");
            EnsureEntity(58, "Cache", hasDimension: true);
            //EnsureEntity(59, "Filter");
            EnsureEntity(60, "PulseTrigger");
            EnsureEntity(61, "ChangeTrigger");
            EnsureEntity(62, "ToggleTrigger");
            EnsureEntity(63, "GetDimension", hasDimension: true);
            EnsureEntity(64, "SetDimension", hasDimension: true);
            EnsureEntity(65, "Hold");
            EnsureEntity(66, "RangeOverDimension", hasDimension: true);
            EnsureEntity(67, "DimensionToOutlets", hasDimension: true);
            EnsureEntity(68, "InletsToDimension", hasDimension: true);
            EnsureEntity(69, "MaxOverInlets");
            EnsureEntity(70, "MinOverInlets");
            EnsureEntity(71, "AverageOverInlets");
            EnsureEntity(72, "MaxOverDimension", hasDimension: true);
            EnsureEntity(73, "MinOverDimension", hasDimension: true);
            EnsureEntity(74, "AverageOverDimension", hasDimension: true);
            EnsureEntity(75, "SumOverDimension", hasDimension: true);
            EnsureEntity(76, "SumFollower", hasDimension: true);
            EnsureEntity(77, "Multiply");
            EnsureEntity(78, "ClosestOverInlets");
            EnsureEntity(79, "ClosestOverDimension", hasDimension: true);
            EnsureEntity(80, "ClosestOverInletsExp");
            EnsureEntity(81, "ClosestOverDimensionExp", hasDimension: true);
            EnsureEntity(82, "SortOverInlets");
            EnsureEntity(83, "SortOverDimension", hasDimension: true);
            EnsureEntity(84, "BandPassFilterConstantSkirtGain");
            EnsureEntity(85, "BandPassFilterConstantPeakGain");
            EnsureEntity(86, "NotchFilter");
            EnsureEntity(87, "AllPassFilter");
            EnsureEntity(88, "PeakingEQFilter");
            EnsureEntity(89, "LowShelfFilter");
            EnsureEntity(90, "HighShelfFilter");
            EnsureEntity(91, "RangeOverOutlets");
        }

        private void EnsureEntity(int id, string name, bool hasDimension = false)
        {
            OperatorType entity = RepositoryHelper.EnsureEnumEntity(this, id, name);
            entity.HasDimension = hasDimension;
        }
    }
}