using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal class DataMigrationExecutor
    {
        private class InletOrOutletTuple
        {
            public InletOrOutletTuple(
                OperatorTypeEnum operatorTypeEnum,
                int listIndex,
                DimensionEnum dimensionEnum)
            {
                OperatorTypeEnum = operatorTypeEnum;
                ListIndex = listIndex;
                DimensionEnum = dimensionEnum;
            }

            public OperatorTypeEnum OperatorTypeEnum { get; }
            public int ListIndex { get; }
            public DimensionEnum DimensionEnum { get; }
        }

        public static void AssertAllDocuments(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                AssertDocuments(repositories, progressCallback);
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void DeleteOrphanedEntityPositions(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);
                int rowsAffected = entityPositionManager.DeleteOrphans();

                AssertDocuments(repositories, progressCallback);

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_SetDimension_OfInletsAndOutlets_OfStandardOperators_SecondTimeAround(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                var inletTuples = new List<InletOrOutletTuple>();
                var outletTuples = new List<InletOrOutletTuple>();

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Absolute, 0, DimensionEnum.Number));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Absolute, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Add, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 2, DimensionEnum.Width));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 1, DimensionEnum.SliceLength));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 2, DimensionEnum.SampleCount));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 1, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 2, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 3, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverInlets, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 2, DimensionEnum.Width));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 2, DimensionEnum.Width));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 1, DimensionEnum.Start));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 2, DimensionEnum.End));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 3, DimensionEnum.SamplingRate));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 0, DimensionEnum.PassThrough));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 1, DimensionEnum.Reset));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 0, DimensionEnum.PassThrough));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 0, DimensionEnum.Input));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 1, DimensionEnum.Collection));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 2, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 3, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 4, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 0, DimensionEnum.Input));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 1, DimensionEnum.Collection));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 2, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 3, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 4, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInlets, 0, DimensionEnum.Input));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInlets, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInletsExp, 0, DimensionEnum.Input));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInletsExp, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Curve, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.DimensionToOutlets, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 1, DimensionEnum.B));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 2, DimensionEnum.Origin));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 0, DimensionEnum.Low));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 1, DimensionEnum.High));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 2, DimensionEnum.Ratio));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 2, DimensionEnum.BlobVolume));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 2, DimensionEnum.Slope));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 3, DimensionEnum.Decibel));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Hold, 0, DimensionEnum.Signal));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Hold, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 0, DimensionEnum.Condition));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 1, DimensionEnum.Then));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 2, DimensionEnum.Else));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.InletsToDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 1, DimensionEnum.SamplingRate));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Multiply, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 1, DimensionEnum.B));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 2, DimensionEnum.Origin));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Negative, 0, DimensionEnum.Number));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Negative, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Number, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.OneOverX, 0, DimensionEnum.Number));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.OneOverX, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 0, DimensionEnum.Base));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 1, DimensionEnum.Exponent));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Not, 0, DimensionEnum.Number));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Not, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 0, DimensionEnum.Number));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 0, DimensionEnum.A));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 1, DimensionEnum.B));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Noise, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 1, DimensionEnum.Width));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Frequency));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 2, DimensionEnum.BlobVolume));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 2, DimensionEnum.Slope));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 3, DimensionEnum.Decibel));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 2, DimensionEnum.Width));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 1, DimensionEnum.Frequency));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 2, DimensionEnum.Width));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 3, DimensionEnum.Decibel));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Sound));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 0, DimensionEnum.PassThrough));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 1, DimensionEnum.Reset));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 0, DimensionEnum.PassThrough));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reset, 0, DimensionEnum.PassThrough));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reset, 0, DimensionEnum.PassThrough));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 0, DimensionEnum.PassThrough));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 1, DimensionEnum.Reset));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 0, DimensionEnum.PassThrough));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 1, DimensionEnum.Factor));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 0, DimensionEnum.PassThrough));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 1, DimensionEnum.Number));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 0, DimensionEnum.PassThrough));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 1, DimensionEnum.Difference));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 1, DimensionEnum.Factor));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 2, DimensionEnum.Origin));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 1, DimensionEnum.Factor));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 2, DimensionEnum.Origin));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 1, DimensionEnum.Exponent));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 2, DimensionEnum.Origin));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GetDimension, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverInlets, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverInlets, 0, DimensionEnum.Number));

                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverInlets, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 1, DimensionEnum.SliceLength));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 2, DimensionEnum.SampleCount));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 1, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 2, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 3, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 1, DimensionEnum.SliceLength));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 2, DimensionEnum.SampleCount));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 1, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 2, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 3, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 0, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 1, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 2, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverOutlets, 0, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverOutlets, 1, DimensionEnum.Step));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 1, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 2, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 3, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 1, DimensionEnum.SliceLength));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 2, DimensionEnum.SampleCount));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 1, DimensionEnum.From));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 2, DimensionEnum.Till));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 3, DimensionEnum.Step));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Random, 0, DimensionEnum.Rate));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Random, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 1, DimensionEnum.Step));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 2, DimensionEnum.Offset));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Sound));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 1, DimensionEnum.Start));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 2, DimensionEnum.End));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 3, DimensionEnum.FrequencyCount));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Volume));

                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 1, DimensionEnum.Skip));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 2, DimensionEnum.LoopStartMarker));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 3, DimensionEnum.LoopEndMarker));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 4, DimensionEnum.ReleaseEndMarker));
                inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 5, DimensionEnum.NoteDuration));
                outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));

                Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> inletTupleDictionary = inletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);
                Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> outletTupleDictionary = outletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                IList<Operator> operators = repositories.OperatorRepository.GetAll();

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];

                    OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                    if (inletTupleDictionary.TryGetValue(operatorTypeEnum, out IList<InletOrOutletTuple> inletTuples2))
                    {
                        foreach (InletOrOutletTuple inletTuple in inletTuples2)
                        {
                            Inlet inlet = op.Inlets.Where(x => x.ListIndex == inletTuple.ListIndex).Single();
                            inlet.SetDimensionEnum(inletTuple.DimensionEnum, repositories.DimensionRepository);
                        }
                    }

                    if (outletTupleDictionary.TryGetValue(operatorTypeEnum, out IList<InletOrOutletTuple> outletTuples2))
                    {
                        foreach (InletOrOutletTuple outletTuple in outletTuples2)
                        {
                            Outlet outlet = op.Outlets.Where(x => x.ListIndex == outletTuple.ListIndex).Single();
                            outlet.SetDimensionEnum(outletTuple.DimensionEnum, repositories.DimensionRepository);
                        }
                    }

                    switch (operatorTypeEnum)
                    {
                        case OperatorTypeEnum.Add:
                        case OperatorTypeEnum.AverageOverInlets:
                        case OperatorTypeEnum.InletsToDimension:
                        case OperatorTypeEnum.Multiply:
                        case OperatorTypeEnum.MaxOverInlets:
                        case OperatorTypeEnum.MinOverInlets:
                        case OperatorTypeEnum.SortOverInlets:
                            foreach (Inlet inlet in op.Inlets)
                            {
                                inlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
                            }
                            break;

                        case OperatorTypeEnum.ClosestOverInlets:
                        case OperatorTypeEnum.ClosestOverInletsExp:
                            // Skip 1
                            foreach (Inlet inlet in op.Inlets.Skip(1))
                            {
                                inlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
                            }
                            break;

                        case OperatorTypeEnum.DimensionToOutlets:
                        case OperatorTypeEnum.RangeOverOutlets:
                            foreach (Outlet outlet in op.Outlets)
                            {
                                outlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
                            }
                            break;

                    }

                    // Cannot validate the operator, because it will do a recursive validation,
                    // validating not yet migrated operators.

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_SetUnderlyingPatch_ForAbsoluteOperators(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                var documentManager = new DocumentManager(repositories);

                Patch underlyingPatch = documentManager.GetSystemPatch(OperatorTypeEnum.Absolute);
                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];

                    DataPropertyParser.SetValue(op, "UnderlyingPatchID", underlyingPatch.ID);

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_Convert_SignalToSound_ForCustomOperators_PatchInlets_And_PatchOutlets(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                IList<Operator> customOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);
                IList<Operator> patchInlets = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.PatchInlet);
                IList<Operator> patchOutlets = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.PatchOutlet);
                IList<Operator> operators = customOperators.Union(patchInlets).Union(patchOutlets).ToArray();

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];

                    foreach (Inlet inlet in op.Inlets)
                    {
                        if (inlet.GetDimensionEnum() == DimensionEnum.Signal)
                        {
                            inlet.SetDimensionEnum(DimensionEnum.Sound, repositories.DimensionRepository);
                        }
                    }

                    foreach (Outlet outlet in op.Outlets)
                    {
                        if (outlet.GetDimensionEnum() == DimensionEnum.Signal)
                        {
                            outlet.SetDimensionEnum(DimensionEnum.Sound, repositories.DimensionRepository);
                        }
                    }

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_AddSystemDocument_AsLibrary_ToAllDocuments(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                DocumentManager documentManager = new DocumentManager(repositories);

                Document systemDocument = documentManager.GetSystemDocument();
                IList<Document> documents = repositories.DocumentRepository.GetAll();

                for (int i = 0; i < documents.Count; i++)
                {
                    Document document = documents[i];

                    if (document.ID == systemDocument.ID)
                    {
                        continue;
                    }

                    bool alreadyExists = document.LowerDocumentReferences
                                                 .Where(x => x.LowerDocument.ID == systemDocument.ID)
                                                 .Any();
                    if (alreadyExists)
                    {
                        continue;
                    }

                    documentManager.CreateDocumentReference(document, systemDocument);

                    string progressMessage = $"Migrated {nameof(Document)} {i + 1}/{documents.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");
                
                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_UnderlyingPatch_DataKey_ToForeignKey(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                IList<Operator> customOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);
                IList<Operator> absoluteOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);
                IList<Operator> operators = customOperators.Union(absoluteOperators).ToArray();

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];

                    int? underlyingPatchID = DataPropertyParser.TryGetInt32(op, "UnderlyingPatchID");
                    DataPropertyParser.TryRemoveKey(op, "UnderlyingPatchID");

                    if (!underlyingPatchID.HasValue)
                    {
                        op.UnlinkUnderlyingPatch();
                    }
                    else
                    {
                        Patch underlyingPatch = repositories.PatchRepository.Get(underlyingPatchID.Value);
                        op.LinkToUnderlyingPatch(underlyingPatch);
                    }

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_Clear_AbsoluteOperator_OperatorType(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];
                    op.UnlinkOperatorType();

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_PatchInletOutlet_ListIndexes_FromDataProperty_ToInletAndOutlet(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                {
                    const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchInlet;
                    IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
                    for (int i = 0; i < operators.Count; i++)
                    {
                        Operator op = operators[i];

                        const string key = nameof(Inlet.ListIndex);
                        int? listIndex = DataPropertyParser.TryGetInt32(op, key);
                        if (!listIndex.HasValue)
                        {
                            throw new NullException(() => listIndex);
                        }
                        DataPropertyParser.TryRemoveKey(op, key);

                        Inlet inlet = op.Inlets.Single();
                        inlet.ListIndex = listIndex.Value;

                        string progressMessage = $"Step 1: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
                        progressCallback(progressMessage);
                    }
                }

                {
                    const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchOutlet;
                    IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
                    for (int i = 0; i < operators.Count; i++)
                    {
                        Operator op = operators[i];

                        const string key = nameof(Outlet.ListIndex);
                        int? listIndex = DataPropertyParser.TryGetInt32(op, key);
                        if (!listIndex.HasValue)
                        {
                            throw new NullException(() => listIndex);
                        }
                        DataPropertyParser.TryRemoveKey(op, key);

                        Outlet outlet = op.Outlets.Single();
                        outlet.ListIndex = listIndex.Value;

                        string progressMessage = $"Step 2: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
                        progressCallback(progressMessage);
                    }
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_PatchInletOutlet_Names_FromOperator_ToInletAndOutlet(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                {
                    const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchInlet;
                    IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
                    for (int i = 0; i < operators.Count; i++)
                    {
                        Operator op = operators[i];
                        Inlet inlet = op.Inlets.Single();

                        // Check what's expected, so you do not accidentally overwrite.
                        if (!string.IsNullOrEmpty(inlet.Name))
                        {
                            throw new NotNullOrEmptyException(() => inlet.Name);
                        }

                        inlet.Name = op.Name;
                        op.Name = null;

                        string progressMessage = $"Step 1: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
                        progressCallback(progressMessage);
                    }
                }

                {
                    const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchOutlet;
                    IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
                    for (int i = 0; i < operators.Count; i++)
                    {
                        Operator op = operators[i];
                        Outlet outlet = op.Outlets.Single();

                        // Check what's expected, so you do not accidentally overwrite.
                        if (!string.IsNullOrEmpty(outlet.Name))
                        {
                            throw new NotNullOrEmptyException(() => outlet.Name);
                        }

                        outlet.Name = op.Name;
                        op.Name = null;

                        string progressMessage = $"Step 2: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
                        progressCallback(progressMessage);
                    }
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        // Helpers

        private static void AssertDocuments(RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

            AssertDocuments(rootDocuments, repositories, progressCallback);
        }

        private static void AssertDocuments(IList<Document> rootDocuments, RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IResultDto totalResult = new VoidResultDto { Successful = true };
            for (int i = 0; i < rootDocuments.Count; i++)
            {
                Document rootDocument = rootDocuments[i];

                string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{rootDocument.Name}'.";
                progressCallback(progressMessage);

                // Validate
                var documentManager = new DocumentManager(repositories);
                VoidResultDto result = documentManager.Save(rootDocument);
                totalResult.Combine(result);
            }

            try
            {
                ResultHelper.Assert(totalResult);
            }
            catch
            {
                progressCallback("Exception while validating documents.");
                throw;
            }
        }
    }
}