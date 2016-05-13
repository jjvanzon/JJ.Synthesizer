using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public interface IPatchCalculator
    {
        double Calculate(double time);
        double Calculate(double time, int channelIndex);
        double[] Calculate(double t0, double frameDuration, int frameCount);
        double[] Calculate(double t0, double frameDuration, int frameCount, int channelIndex);

        double GetValue(int listIndex);
        void SetValue(int listIndex, double value);

        double GetValue(string name);
        void SetValue(string name, double value);

        double GetValue(string name, int listIndex);
        void SetValue(string name, int listIndex, double value);
        
        double GetValue(DimensionEnum dimensionEnum);
        void SetValue(DimensionEnum dimensionEnum, double value);

        double GetValue(DimensionEnum dimensionEnum, int listIndex);
        void SetValue(DimensionEnum dimensionEnum, int listIndex, double value);

        void CloneValues(IPatchCalculator sourcePatchCalculator);

        void Reset(double time);
        void Reset(double time, string name);
        void Reset(double time, int listIndex);
    }
}
