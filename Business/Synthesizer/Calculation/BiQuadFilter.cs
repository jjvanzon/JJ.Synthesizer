// Jan-Joost van Zon, 2016-06-25:
// This is an adaptation of the class from NAudio. Some of the changes:
// - Added AggressiveInlining attributes.
// - Made all variable doubles.
// - Removed some unnecessary XML comment.
// - Renamed Create methods.
// - Created a Set method for each Create method, so you can change the filter parameters, without creating a new filter.

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
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class BiQuadFilter
    {
        // coefficients
        private double _a0;
        private double _a1;
        private double _a2;
        private double _a3;
        private double _a4;

        // state
        private double _x1;
        private double _x2;
        private double _y1;
        private double _y2;

        public BiQuadFilter()
        {
            ResetSamples();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetCoefficients(double aa0, double aa1, double aa2, double b0, double b1, double b2)
        {
            // precompute the coefficients
            _a0 = b0 / aa0;
            _a1 = b1 / aa0;
            _a2 = b2 / aa0;
            _a3 = aa1 / aa0;
            _a4 = aa2 / aa0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetSamples()
        {
            // zero initial samples
            _x1 = 0;
            _x2 = 0;
            _y1 = 0;
            _y2 = 0;
        }

        /// <summary> Passes a single sample through the filter. </summary>
        /// <param name="inSample">Input sample</param>
        /// <returns>Output sample</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Transform(double inSample)
        {
            // compute result
            double result = _a0 * inSample + _a1 * _x1 + _a2 * _x2 - _a3 * _y1 - _a4 * _y2;

            // shift x1 to x2, sample to x1 
            _x2 = _x1;
            _x1 = inSample;

            // shift y1 to y2, result to y1 
            _y2 = _y1;
            _y1 = result;

            return _y1;
        }

        // Set Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLowPassFilterVariables(double sampleRate, double cutoffFrequency, double blobVolume)
        {
            // H(s) = 1 / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * cutoffFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * blobVolume);

            double b0 = (1 - cosw0) / 2;
            double b1 = 1 - cosw0;
            double b2 = (1 - cosw0) / 2;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2);
        }

        /// <param name="width">Bandwidth (Q)</param>
        /// <param name="dbGain">Gain in decibels</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPeakingEQVariables(double sampleRate, double centreFrequency, double width, double dbGain)
        {
            // H(s) = (s^2 + s*(A/Q) + 1) / (s^2 + s/(A*Q) + 1)
            double w0 = MathHelper.TWO_PI * centreFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * width);
            double a = Math.Pow(10, dbGain / 40);     // TODO: should we square root this value?

            double b0 = 1 + alpha * a;
            double b1 = -2 * cosw0;
            double b2 = 1 - alpha * a;
            double aa0 = 1 + alpha / a;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha / a;

            SetCoefficients(aa0, aa1, aa2, b0, b1, b2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetHighPassFilterVariables(double sampleRate, double cutoffFrequency, double blobVolume)
        {
            // H(s) = s^2 / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * cutoffFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double alpha = Math.Sin(w0) / (2 * blobVolume);

            double b0 = (1 + cosw0) / 2;
            double b1 = -(1 + cosw0);
            double b2 = (1 + cosw0) / 2;
            double aa0 = 1 + alpha;
            double aa1 = -2 * cosw0;
            double aa2 = 1 - alpha;
            SetCoefficients(aa0, aa1, aa2, b0, b1, b2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBandPassFilterConstantSkirtGainVariables(double sampleRate, double centreFrequency, double bandWidth)
        {
            // H(s) = s / (s^2 + s/Q + 1)  (constant skirt gain, peak gain = Q)
            double w0 = MathHelper.TWO_PI * centreFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = sinw0 / 2; // =   Q*alpha
            const double b1 = 0;
            double b2 = -sinw0 / 2; // =  -Q*alpha
            double a0 = 1 + alpha;
            double a1 = -2 * cosw0;
            double a2 = 1 - alpha;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBandPassFilterConstantPeakGainVariables(double sampleRate, double centreFrequency, double bandWidth)
        {
            // H(s) = (s/Q) / (s^2 + s/Q + 1)      (constant 0 dB peak gain)
            double w0 = MathHelper.TWO_PI * centreFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = alpha;
            const double b1 = 0;
            double b2 = -alpha;
            double a0 = 1 + alpha;
            double a1 = -2 * cosw0;
            double a2 = 1 - alpha;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetNotchFilterVariables(double sampleRate, double centreFrequency, double q)
        {
            // H(s) = (s^2 + 1) / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * centreFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * q);

            const double b0 = 1;
            double b1 = -2 * cosw0;
            const double b2 = 1;
            double a0 = 1 + alpha;
            double a1 = -2 * cosw0;
            double a2 = 1 - alpha;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAllPassFilterVariables(double sampleRate, double centreFrequency, double bandWidth)
        {
            //H(s) = (s^2 - s/Q + 1) / (s^2 + s/Q + 1)
            double w0 = MathHelper.TWO_PI * centreFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double alpha = sinw0 / (2 * bandWidth);

            double b0 = 1 - alpha;
            double b1 = -2 * cosw0;
            double b2 = 1 + alpha;
            double a0 = 1 + alpha;
            double a1 = -2 * cosw0;
            double a2 = 1 - alpha;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        /// <summary> H(s) = A * (s^2 + (sqrt(A)/Q)*s + A)/(A*s^2 + (sqrt(A)/Q)*s + 1) </summary>
        /// <param name="transitionSlope">a "transition slope" parameter (for shelving EQ only).  
        /// When S = 1, the transition slope is as steep as it can be and remain monotonically
        /// increasing or decreasing gain with frequency.  The transition slope, in dB/octave, 
        /// remains proportional to S for all other values for a fixed f0/Fs and dBgain.</param>
        /// <param name="dbGain">Gain in decibels</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLowShelfVariables(double sampleRate, double cutoffFrequency, double transitionSlope, double dbGain)
        {
            double w0 = MathHelper.TWO_PI * cutoffFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double a = Math.Pow(10, dbGain / 40);     // TODO: should we square root this value?
            double alpha = sinw0 / 2 * Math.Sqrt((a + 1 / a) * (1 / transitionSlope - 1) + 2);
            double temp = 2 * Math.Sqrt(a) * alpha;

            double b0 = a * ((a + 1) - (a - 1) * cosw0 + temp);
            double b1 = 2 * a * ((a - 1) - (a + 1) * cosw0);
            double b2 = a * ((a + 1) - (a - 1) * cosw0 - temp);
            double a0 = (a + 1) + (a - 1) * cosw0 + temp;
            double a1 = -2 * ((a - 1) + (a + 1) * cosw0);
            double a2 = (a + 1) + (a - 1) * cosw0 - temp;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        /// <summary> H(s) = A * (A*s^2 + (sqrt(A)/Q)*s + 1)/(s^2 + (sqrt(A)/Q)*s + A) </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetHighShelfVariables(double sampleRate, double cutoffFrequency, double transitionSlope, double dbGain)
        {
            double w0 = MathHelper.TWO_PI * cutoffFrequency / sampleRate;
            double cosw0 = Math.Cos(w0);
            double sinw0 = Math.Sin(w0);
            double a = Math.Pow(10, dbGain / 40); // TODO: should we square root this value?
            double alpha = sinw0 / 2 * Math.Sqrt((a + 1 / a) * (1 / transitionSlope - 1) + 2);
            double temp = 2 * Math.Sqrt(a) * alpha;

            double b0 = a * ((a + 1) + (a - 1) * cosw0 + temp);
            double b1 = -2 * a * ((a - 1) + (a + 1) * cosw0);
            double b2 = a * ((a + 1) + (a - 1) * cosw0 - temp);
            double a0 = (a + 1) - (a - 1) * cosw0 + temp;
            double a1 = 2 * ((a - 1) - (a + 1) * cosw0);
            double a2 = (a + 1) - (a - 1) * cosw0 - temp;

            SetCoefficients(a0, a1, a2, b0, b1, b2);
        }

        // Create Methods

        public static BiQuadFilter CreateLowPassFilter(double sampleRate, double cutoffFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetLowPassFilterVariables(sampleRate, cutoffFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreateHighPassFilter(double sampleRate, double cutoffFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetHighPassFilterVariables(sampleRate, cutoffFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreateBandPassFilterConstantSkirtGain(double sampleRate, double centreFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetBandPassFilterConstantSkirtGainVariables(sampleRate, centreFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreateBandPassFilterConstantPeakGain(double sampleRate, double centreFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetBandPassFilterConstantPeakGainVariables(sampleRate, centreFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreateNotchFilter(double sampleRate, double centreFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetNotchFilterVariables(sampleRate, centreFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreateAllPassFilter(double sampleRate, double centreFrequency, double q)
        {
            var filter = new BiQuadFilter();
            filter.SetAllPassFilterVariables(sampleRate, centreFrequency, q);
            return filter;
        }

        public static BiQuadFilter CreatePeakingEQ(double sampleRate, double centreFrequency, double q, double dbGain)
        {
            var filter = new BiQuadFilter();
            filter.SetPeakingEQVariables(sampleRate, centreFrequency, q, dbGain);
            return filter;
        }

        /// <summary> H(s) = A * (s^2 + (sqrt(A)/Q)*s + A)/(A*s^2 + (sqrt(A)/Q)*s + 1) </summary>
        /// <param name="transitionSlope">a "transition slope" parameter (for shelving EQ only).  
        /// When S = 1, the transition slope is as steep as it can be and remain monotonically
        /// increasing or decreasing gain with frequency.  The transition slope, in dB/octave, 
        /// remains proportional to S for all other values for a fixed f0/Fs and dBgain.</param>
        /// <param name="dbGain">Gain in decibels</param>
        public static BiQuadFilter CreateLowShelf(double sampleRate, double cutoffFrequency, double transitionSlope, double dbGain)
        {
            var filter = new BiQuadFilter();
            filter.SetLowShelfVariables(sampleRate, cutoffFrequency, transitionSlope, dbGain);
            return filter;
        }

        /// <summary> H(s) = A * (A*s^2 + (sqrt(A)/Q)*s + 1)/(s^2 + (sqrt(A)/Q)*s + A) </summary>
        public static BiQuadFilter CreateHighShelf(double sampleRate, double cutoffFrequency, double transitionSlope, double dbGain)
        {
            var filter = new BiQuadFilter();
            filter.SetHighShelfVariables(sampleRate, cutoffFrequency, transitionSlope, dbGain);
            return filter;
        }
    }
}