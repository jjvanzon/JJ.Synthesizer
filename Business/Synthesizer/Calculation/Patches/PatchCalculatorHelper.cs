using System.Runtime.CompilerServices;
using System.Threading;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public static class PatchCalculatorHelper
    {
        // Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InterlockedAdd(ref float location1, float value)
        {
            float newCurrentValue = 0;
            while (true)
            {
                float currentValue = newCurrentValue;
                float newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                    return newValue;
            }
        }
    }
}
