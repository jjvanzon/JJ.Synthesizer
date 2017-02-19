using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class Loop_OperatorCalculator_Helper
    {
        public static double? GetTransformedPosition(
            double position,
            double origin,
            double skip,
            double loopStartMarker,
            double loopEndMarker,
            double releaseEndMarker,
            double noteDuration)
        {
            double? nullableInputPosition;

            double outputPosition = position;

            outputPosition -= origin;

            // BeforeAttack
            double inputPosition = outputPosition + skip;
            bool isBeforeAttack = inputPosition < skip;
            if (isBeforeAttack)
            {
                nullableInputPosition = null;
            }
            else
            {
                // InAttack
                bool isInAttack = inputPosition < loopStartMarker;
                if (isInAttack)
                {
                    nullableInputPosition = inputPosition;
                }
                else
                {
                    // InLoop
                    double cycleLength = loopEndMarker - loopStartMarker;

                    // Round up end of loop to whole cycles.
                    double outputLoopStart = loopStartMarker - skip;
                    double noteEndPhase = (noteDuration - outputLoopStart) / cycleLength;
                    double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleLength;

                    bool isInLoop = outputPosition < outputLoopEnd;
                    if (isInLoop)
                    {
                        double phase = (inputPosition - loopStartMarker) % cycleLength;
                        inputPosition = loopStartMarker + phase;
                        nullableInputPosition = inputPosition;
                    }
                    else
                    {
                        // InRelease
                        double releaseLength = releaseEndMarker - loopEndMarker;
                        double outputReleaseEndPosition = outputLoopEnd + releaseLength;
                        bool isInRelease = outputPosition < outputReleaseEndPosition;
                        if (isInRelease)
                        {
                            double positionInRelease = outputPosition - outputLoopEnd;
                            inputPosition = loopEndMarker + positionInRelease;
                            nullableInputPosition = inputPosition;
                        }
                        else
                        {
                            // AfterRelease
                            nullableInputPosition = null;
                        }
                    }
                }
            }

            return nullableInputPosition;
        }
    }
}
