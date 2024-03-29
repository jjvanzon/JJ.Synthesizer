﻿//using System;

//namespace JJ.Business.SynthesizerPrototype.Calculation
//{
//    internal static class Loop_OperatorCalculator_Helper
//    {
//        /// <summary>
//        /// Only programmed to then port it to C# code generator.
//        /// The reason the generated code does not just call this helper,
//        /// is because then you would have to retrieve all the inputs first,
//        /// while if you inline it, you could get inputs only when you need them,
//        /// which is more efficient.
//        /// </summary>
//        public static double? GetTransformedPosition(
//            double position,
//            double origin,
//            double skip,
//            double loopStartMarker,
//            double loopEndMarker,
//            double releaseEndMarker,
//            double noteDuration)
//        {
//            double? nullableInputPosition;

//            double outputPosition = position;
//            double inputPosition = outputPosition;

//            inputPosition -= origin;

//            // BeforeAttack
//            inputPosition += skip;
//            bool isBeforeAttack = inputPosition < skip;
//            if (isBeforeAttack)
//            {
//                nullableInputPosition = null;
//            }
//            else
//            {
//                // InAttack
//                bool isInAttack = inputPosition < loopStartMarker;
//                if (isInAttack)
//                {
//                    nullableInputPosition = inputPosition;
//                }
//                else
//                {
//                    // InLoop
//                    double cycleLength = loopEndMarker - loopStartMarker;

//                    // Round up end of loop to whole cycles.
//                    double outputLoopStart = loopStartMarker - skip;
//                    double noteEndPhase = (noteDuration - outputLoopStart) / cycleLength;
//                    double outputLoopEnd = outputLoopStart + Math.Ceiling(noteEndPhase) * cycleLength;

//                    bool isInLoop = outputPosition < outputLoopEnd;
//                    if (isInLoop)
//                    {
//                        double phase = (inputPosition - loopStartMarker) % cycleLength;
//                        inputPosition = loopStartMarker + phase;
//                        nullableInputPosition = inputPosition;
//                    }
//                    else
//                    {
//                        // InRelease
//                        double releaseLength = releaseEndMarker - loopEndMarker;
//                        double outputReleaseEndPosition = outputLoopEnd + releaseLength;
//                        bool isInRelease = outputPosition < outputReleaseEndPosition;
//                        if (isInRelease)
//                        {
//                            double positionInRelease = outputPosition - outputLoopEnd;
//                            inputPosition = loopEndMarker + positionInRelease;
//                            nullableInputPosition = inputPosition;
//                        }
//                        else
//                        {
//                            // AfterRelease
//                            nullableInputPosition = null;
//                        }
//                    }
//                }
//            }

//            return nullableInputPosition;
//        }
//    }
//}
