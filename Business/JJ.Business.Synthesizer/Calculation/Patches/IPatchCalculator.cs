using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public interface IPatchCalculator
    {
        void SetValue(int listIndex, double value);
        void SetValue(string name, double value);
        void SetValue(string name, int listIndex, double value);
        void SetValue(InletTypeEnum inletTypeEnum, double value);
        void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value);

        double Calculate(double time, int channelIndex);
    }
}
