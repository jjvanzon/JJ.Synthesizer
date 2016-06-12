using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class OperatorTypeRepository : DefaultRepositories.OperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Add");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Adder");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Divide");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Multiply");
            RepositoryHelper.EnsureEnumEntity(this, 5, "PatchInlet");
            RepositoryHelper.EnsureEnumEntity(this, 6, "PatchOutlet");
            RepositoryHelper.EnsureEnumEntity(this, 7, "Power");
            RepositoryHelper.EnsureEnumEntity(this, 8, "Sine");
            RepositoryHelper.EnsureEnumEntity(this, 9, "Subtract");
            RepositoryHelper.EnsureEnumEntity(this, 10, "Delay");
            RepositoryHelper.EnsureEnumEntity(this, 11, "SpeedUp");
            RepositoryHelper.EnsureEnumEntity(this, 12, "SlowDown");
            RepositoryHelper.EnsureEnumEntity(this, 13, "TimePower");
            RepositoryHelper.EnsureEnumEntity(this, 14, "Earlier");
            RepositoryHelper.EnsureEnumEntity(this, 15, "Number");
            RepositoryHelper.EnsureEnumEntity(this, 16, "Curve");
            RepositoryHelper.EnsureEnumEntity(this, 17, "Sample");
            RepositoryHelper.EnsureEnumEntity(this, 18, "Noise");
            RepositoryHelper.EnsureEnumEntity(this, 19, "Resample");
            RepositoryHelper.EnsureEnumEntity(this, 20, "CustomOperator");
            RepositoryHelper.EnsureEnumEntity(this, 21, "SawUp");
            RepositoryHelper.EnsureEnumEntity(this, 22, "Square");
            RepositoryHelper.EnsureEnumEntity(this, 23, "Triangle");
            RepositoryHelper.EnsureEnumEntity(this, 24, "Exponent");
            RepositoryHelper.EnsureEnumEntity(this, 25, "Loop");
            RepositoryHelper.EnsureEnumEntity(this, 26, "Select");
            RepositoryHelper.EnsureEnumEntity(this, 27, "Bundle");
            RepositoryHelper.EnsureEnumEntity(this, 28, "Unbundle");
            RepositoryHelper.EnsureEnumEntity(this, 29, "Stretch");
            RepositoryHelper.EnsureEnumEntity(this, 30, "Narrower");
            RepositoryHelper.EnsureEnumEntity(this, 31, "Shift");
            RepositoryHelper.EnsureEnumEntity(this, 32, "Reset");
            RepositoryHelper.EnsureEnumEntity(this, 33, "LowPassFilter");
            RepositoryHelper.EnsureEnumEntity(this, 34, "HighPassFilter");
            RepositoryHelper.EnsureEnumEntity(this, 35, "Spectrum");
            RepositoryHelper.EnsureEnumEntity(this, 36, "Pulse");
            RepositoryHelper.EnsureEnumEntity(this, 37, "Random");
            RepositoryHelper.EnsureEnumEntity(this, 38, "Equal");
            RepositoryHelper.EnsureEnumEntity(this, 39, "NotEqual");
            RepositoryHelper.EnsureEnumEntity(this, 40, "LessThan");
            RepositoryHelper.EnsureEnumEntity(this, 41, "GreaterThan");
            RepositoryHelper.EnsureEnumEntity(this, 42, "LessThanOrEqual");
            RepositoryHelper.EnsureEnumEntity(this, 43, "GreaterThanOrEqual");
            RepositoryHelper.EnsureEnumEntity(this, 44, "And");
            RepositoryHelper.EnsureEnumEntity(this, 45, "Or");
            RepositoryHelper.EnsureEnumEntity(this, 46, "Not");
            RepositoryHelper.EnsureEnumEntity(this, 47, "If");
            RepositoryHelper.EnsureEnumEntity(this, 48, "Minimum");
            RepositoryHelper.EnsureEnumEntity(this, 49, "Maximum");
            RepositoryHelper.EnsureEnumEntity(this, 50, "Average");
            RepositoryHelper.EnsureEnumEntity(this, 51, "Scaler");
            RepositoryHelper.EnsureEnumEntity(this, 52, "SawDown");
            RepositoryHelper.EnsureEnumEntity(this, 53, "Absolute");
            RepositoryHelper.EnsureEnumEntity(this, 54, "Reverse");
            RepositoryHelper.EnsureEnumEntity(this, 55, "Round");
            RepositoryHelper.EnsureEnumEntity(this, 56, "Negative");
            RepositoryHelper.EnsureEnumEntity(this, 57, "OneOverX");
            RepositoryHelper.EnsureEnumEntity(this, 58, "Cache");
            RepositoryHelper.EnsureEnumEntity(this, 59, "Filter");
            RepositoryHelper.EnsureEnumEntity(this, 60, "PulseTrigger");
            RepositoryHelper.EnsureEnumEntity(this, 61, "ChangeTrigger");
            RepositoryHelper.EnsureEnumEntity(this, 62, "ToggleTrigger");
            RepositoryHelper.EnsureEnumEntity(this, 63, "GetDimension");
            RepositoryHelper.EnsureEnumEntity(this, 64, "SetDimension");
            RepositoryHelper.EnsureEnumEntity(this, 65, "Hold");
            RepositoryHelper.EnsureEnumEntity(this, 66, "Range");
            RepositoryHelper.EnsureEnumEntity(this, 67, "MakeDiscrete");
            RepositoryHelper.EnsureEnumEntity(this, 68, "MakeContinuous");
        }
    }
}