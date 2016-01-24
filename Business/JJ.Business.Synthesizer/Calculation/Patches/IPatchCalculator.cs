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

        double[] CalculateArray(int sampleCount, double t0, double sampleDuration, int channelIndex);

        void ResetState();
        void ResetState(string name);
        void ResetState(int listIndex);

        void CloneValues(IPatchCalculator sourcePatchCalculator);
    }
}
