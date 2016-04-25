using System;
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
        
        double GetValue(DimensionEnum dimensionEnum);
        void SetValue(DimensionEnum dimensionEnum, double value);

        double GetValue(DimensionEnum dimensionEnum, int listIndex);
        void SetValue(DimensionEnum dimensionEnum, int listIndex, double value);
        
        double Calculate(DimensionStack dimensionStack);

        double[] Calculate(double t0, double frameDuration, int frameCount, DimensionStack dimensionStack);

        void Reset(DimensionStack dimensionStack);
        void Reset(DimensionStack dimensionStack, string name);
        void Reset(DimensionStack dimensionStack, int listIndex);

        void CloneValues(IPatchCalculator sourcePatchCalculator);
    }
}
