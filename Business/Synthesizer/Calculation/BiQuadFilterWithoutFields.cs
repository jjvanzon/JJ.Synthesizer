// Jan-Joost van Zon, 2016-06-25:
// This is an adaptation of the class from NAudio. Some of the changes:
// - Added AggressiveInlining attributes.
// - Made all variable doubles.
// - Removed some unnecessary XML comment.
// - Renamed Create methods.
// - Created a Set method for each Create method, so you can change the filter parameters, without creating a new filter.
// - Turned all fields into out or ref parameters

// Original comment from NAudio:

// based on Cookbook formulae for audio EQ biquad filter coefficients
// http://www.musicdsp.org/files/Audio-EQ-Cookbook.txt
// by Robert Bristow-Johnson  <rbj@audioimagination.com>

//    alpha = sin(w0)/(2*Q)                                       (case: Q)
//          = sin(w0)*sinh( ln(2)/2 * BW * w0/sin(w0) )           (case: BW)
//          = sin(w0)/2 * sqrt( (A + 1/A)*(1/S - 1) + 2 )         (case: S)
// Q: (the EE kind of definition, except for peakingEQ in which A*Q is
// the classic EE Q.  That adjustment in definition was made so that
// a boost of N dB followed by a cut of N dB for identical Q and
// f0/Fs results in a precisely flat unity gain filter or "wire".)
//
// BW: the bandwidth in octaves (between -3 dB frequencies for BPF
// and notch or between midpoint (dBgain/2) gain frequencies for
// peaking EQ)
//
// S: a "transition slope" parameter (for shelving EQ only).  When S = 1,
// the transition slope is as steep as it can be and remain monotonically
// increasing or decreasing gain with frequency.  The transition slope, in
// dB/octave, remains proportional to S for all other values for a
// fixed f0/Fs and dBgain.

using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation
{
    internal static class BiQuadFilterWithoutFields
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetSamples(out double x1, out double x2, out double y1, out double y2)
        {
            // zero initial samples
            x1 = 0;
            x2 = 0;
            y1 = 0;
            y2 = 0;
        }

        /// <summary> Passes a single sample through the filter. </summary>
        /// <param name="inSample">Input sample</param>
        /// <returns>Output sample</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Transform(
            double inSample, double a0, double a1, double a2, double a3, double a4,
            ref double x1, ref double x2, ref double y1, ref double y2)
        {
            // compute result
            double result = a0 * inSample + a1 * x1 + a2 * x2 - a3 * y1 - a4 * y2;

            // shift x1 to x2, sample to x1 
            x2 = x1;
            x1 = inSample;

            // shift y1 to y2, result to y1 
            y2 = y1;
            y1 = result;

            return y1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetCoefficients(
            double aa0, double aa1, double aa2, double b0, double b1, double b2,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            a0 = b0 / aa0;
            a1 = b1 / aa0;
            a2 = b2 / aa0;
            a3 = aa1 / aa0;
            a4 = aa2 / aa0;
        }

        // Set Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLowPassFilterVariables(
            double samplingRate, double cutoffFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = 1 / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * cutoffFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * bandWidth);

            double b0 = (1 - cosw0) / 2;
            double b1 = 1 - cosw0;
            double b2 = (1 - cosw0) / 2;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        /// <param name="bandWidth">Bandwidth (Q)</param>
        /// <param name="dbGain">Gain in decibels</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPeakingEQFilterVariables(
            double samplingRate, double centerFrequency, double bandWidth, double dbGain,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = (s^2 + s*(A/Q) + 1) / (s^2 + s/(A*Q) + 1)
            double w0 = MathHelper.TWO_PI * centerFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);
            double a = Math.Pow(10, dbGain / 40); // TODO: should we square root this value?

            double b0 = 1 + alpha * a;
            double b1 = -2 * cosw0;
            double b2 = 1 - alpha * a;
            double aa0 = 1 + alpha / a;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha / a;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHighPassFilterVariables(
            double samplingRate, double cutoffFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = s^2 / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * cutoffFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * bandWidth);

            double b0 = (1 + cosw0) / 2;
            double b1 = -(1 + cosw0);
            double b2 = (1 + cosw0) / 2;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;
            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBandPassFilterConstantTransitionGainVariables(
            double samplingRate, double centerFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = s / (s^2 + s/Q + 1)  (constant skirt gain, peak gain = Q)
            double w0 = MathHelper.TWO_PI * centerFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = sinw0 / 2; // =   Q*alpha
            const double b1 = 0;
            double b2 = -sinw0 / 2; // =  -Q*alpha
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBandPassFilterConstantPeakGainVariables(
            double samplingRate, double centerFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = (s/Q) / (s^2 + s/Q + 1)      (constant 0 dB peak gain)
            double w0 = MathHelper.TWO_PI * centerFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = alpha;
            const double b1 = 0;
            double b2 = -alpha;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetNotchFilterVariables(
            double samplingRate, double centerFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            // H(s) = (s^2 + 1) / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * centerFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            const double b0 = 1;
            double b1 = -2 * cosw0;
            const double b2 = 1;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAllPassFilterVariables(
            double samplingRate, double centerFrequency, double bandWidth,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            //H(s) = (s^2 - s/Q + 1) / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * centerFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = 1 - alpha;
            double b1 = -2 * cosw0;
            double b2 = 1 + alpha;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        /// <summary> H(s) = A * (s^2 + (sqrt(A)/Q)*s + A)/(A*s^2 + (sqrt(A)/Q)*s + 1) </summary>
        /// <param name="transitionSlope">a "transition slope" parameter (for shelving EQ only).  
        /// When S = 1, the transition slope is as steep as it can be and remain monotonically
        /// increasing or decreasing gain with frequency.  The transition slope, in dB/octave, 
        /// remains proportional to S for all other values for a fixed f0/Fs and dBgain.</param>
        /// <param name="dbGain">Gain in decibels</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLowShelfFilterVariables(
            double samplingRate, double transitionFrequency, double transitionSlope, double dbGain,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            double w0 = MathHelper.TWO_PI * transitionFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double a = Math.Pow(10, dbGain / 40);     // TODO: should we square root this value?
            double alpha = sinw0 / 2 * Math.Sqrt((a + 1 / a) * (1 / transitionSlope - 1) + 2);
            double temp = 2 * Math.Sqrt(a) * alpha;

            double b0 = a * ((a + 1) - (a - 1) * cosw0 + temp);
            double b1 = 2 * a * ((a - 1) - (a + 1) * cosw0);
            double b2 = a * ((a + 1) - (a - 1) * cosw0 - temp);
            double aa0 = (a + 1) + (a - 1) * cosw0 + temp;
            double aa1 = -2 * ((a - 1) + (a + 1) * cosw0);
            double aa2 = (a + 1) + (a - 1) * cosw0 - temp;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }

        /// <summary> H(s) = A * (A*s^2 + (sqrt(A)/Q)*s + 1)/(s^2 + (sqrt(A)/Q)*s + A) </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHighShelfFilterVariables(
            double samplingRate, double transitionFrequency, double transitionSlope, double dbGain,
            out double a0, out double a1, out double a2, out double a3, out double a4)
        {
            double w0 = MathHelper.TWO_PI * transitionFrequency / samplingRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double a = Math.Pow(10, dbGain / 40); // TODO: should we square root this value?
            double alpha = sinw0 / 2 * Math.Sqrt((a + 1 / a) * (1 / transitionSlope - 1) + 2);
            double temp = 2 * Math.Sqrt(a) * alpha;

            double b0 = a * ((a + 1) + (a - 1) * cosw0 + temp);
            double b1 = -2 * a * ((a - 1) + (a + 1) * cosw0);
            double b2 = a * ((a + 1) + (a - 1) * cosw0 - temp);
            double aa0 = (a + 1) - (a - 1) * cosw0 + temp;
            double aa1 = 2 * ((a - 1) - (a + 1) * cosw0);
            double aa2 = (a + 1) - (a - 1) * cosw0 - temp;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2, out a0, out a1, out a2, out a3, out a4);
        }
    }
}