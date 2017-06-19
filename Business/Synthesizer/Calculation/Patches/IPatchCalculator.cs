using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public interface IPatchCalculator
    {
        /// <param name="buffer">
        /// The buffer values may be added to, not overwritten, to facilitate for instance writing multiple notes concurrently to the same buffer.
        /// The consequence is that you have to clear the buffer if you intend to start with all zeroes.
        /// </param>
        /// <param name="frameCount">
        /// You cannot use buffer.Length as a basis for frameCount, 
        /// because if you write to the buffer beyond frameCount, then the audio driver might fail.
        /// A frameCount based on the entity model can differ from the frameCount you get from the driver,
        /// and you only know the frameCount at the time the driver calls us.
        /// </param>
        void Calculate(float[] buffer, int frameCount, double t0);

        double GetValue(int position);
        void SetValue(int position, double value);

        double GetValue(string name);
        void SetValue(string name, double value);

        double GetValue(string name, int position);
        void SetValue(string name, int position, double value);

        double GetValue(DimensionEnum dimensionEnum);
        void SetValue(DimensionEnum dimensionEnum, double value);

        double GetValue(DimensionEnum dimensionEnum, int position);
        void SetValue(DimensionEnum dimensionEnum, int position, double value);

        void CloneValues(IPatchCalculator sourcePatchCalculator);

        void Reset(double time);
        void Reset(double time, string name);
        void Reset(double time, int position);
    }
}
