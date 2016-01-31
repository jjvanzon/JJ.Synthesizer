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
            RepositoryHelper.EnsureEnumEntity(this, 18, "WhiteNoise");
            RepositoryHelper.EnsureEnumEntity(this, 19, "Resample");
            RepositoryHelper.EnsureEnumEntity(this, 20, "CustomOperator");
            RepositoryHelper.EnsureEnumEntity(this, 21, "SawTooth");
            RepositoryHelper.EnsureEnumEntity(this, 22, "SquareWave");
            RepositoryHelper.EnsureEnumEntity(this, 23, "TriangleWave");
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
        }
    }
}