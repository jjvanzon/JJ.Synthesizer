using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public interface IPatchCalculator
    {
        double GetValue(int listIndex);
        void SetValue(int listIndex, double value);

        double GetValue(string name);
        void SetValue(string name, double value);

        double GetValue(string name, int listIndex);
        void SetValue(string name, int listIndex, double value);

        double GetValue(InletTypeEnum inletTypeEnum);
        void SetValue(InletTypeEnum inletTypeEnum, double value);

        double GetValue(InletTypeEnum inletTypeEnum, int listIndex);
        void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value);

        double Calculate(double time, int channelIndex);
        void ResetState();
        void CloneValues(IPatchCalculator sourcePatchCalculator);
    }
}
