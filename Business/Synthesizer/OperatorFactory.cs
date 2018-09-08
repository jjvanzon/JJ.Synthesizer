using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer
{
    /// <summary> TODO: Refactor away more and more specific methods and use generic ones instead. </summary>
	public class OperatorFactory
	{
		private readonly Patch _patch;
		private readonly RepositoryWrapper _repositories;
		private readonly CurveFacade _curveFacade;
		private readonly PatchFacade _patchFacade;
		private readonly SampleFacade _sampleFacade;
		private readonly SystemFacade _systemFacade;

		public OperatorFactory(Patch patch, RepositoryWrapper repositories)
		{
			_patch = patch ?? throw new NullException(() => patch);
			_repositories = repositories ?? throw new NullException(() => repositories);

			_curveFacade = new CurveFacade(new CurveRepositories(repositories));
			_patchFacade = new PatchFacade(_repositories);
			_sampleFacade = new SampleFacade(new SampleRepositories(repositories));
			_systemFacade = new SystemFacade(repositories.DocumentRepository);
		}

	    private OperatorWrapper AverageFollower(
			Outlet signal = null,
			Outlet sliceLength = null,
			Outlet sampleCount = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		    => NewForAggregateFollower(
		        signal,
		        sliceLength,
		        sampleCount,
		        standardDimension,
		        customDimension);

	    private OperatorWrapper_WithCollectionRecalculation AverageOverDimension(
			Outlet signal = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForAggregateOverDimension(
	            signal,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    private Cache_OperatorWrapper Cache(
			Outlet signal = null,
			Outlet start = null,
			Outlet end = null,
			Outlet samplingRate = null,
			InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line,
			SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Mono,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		{
			Operator op = NewWithDimension(standardDimension, customDimension);

			var wrapper = new Cache_OperatorWrapper(op);
			wrapper.Inputs[DimensionEnum.Signal] = signal;
			wrapper.Inputs[DimensionEnum.Start] = start;
			wrapper.Inputs[DimensionEnum.End] = end;
			wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
			wrapper.InterpolationType = interpolationTypeEnum;
			wrapper.SpeakerSetup = speakerSetupEnum;

			return wrapper;
		}

	    private OperatorWrapper_WithCollectionRecalculation ClosestOverDimension(
			Outlet input = null,
			Outlet collection = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
		    => NewForClosestOverDimension_OrClosestOverDimensionExp(
		        input,
		        collection,
		        from,
		        till,
		        step,
		        standardDimension,
		        customDimension,
		        collectionRecalculation);

	    private OperatorWrapper_WithCollectionRecalculation ClosestOverDimensionExp(
			Outlet input = null,
			Outlet collection = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForClosestOverDimension_OrClosestOverDimensionExp(
	            input,
	            collection,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    private OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items) => NewWithInputAndItemsInlets(input, items);

	    private OperatorWrapper ClosestOverInletsExp(Outlet input, IList<Outlet> items) => NewWithInputAndItemsInlets(input, items);

	    private OperatorWrapper Curve(
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			Operator op = wrapper.WrappedOperator;

			Curve curve = _curveFacade.Create(_patch.Document);
			op.LinkTo(curve);

			return wrapper;
		}

		public OperatorWrapper Curve(
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			params (double x, double y, InterpolationTypeEnum interpolationTypeEnum)[] nodeTuples)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			Operator op = wrapper.WrappedOperator;

			Curve curve = _curveFacade.Create(nodeTuples);
			op.LinkTo(curve);

			return wrapper;
		}

	    private OperatorWrapper Curve(
			double xSpan,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			params (double y, InterpolationTypeEnum interpolationTypeEnum)?[] nodeTuples)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			Operator op = wrapper.WrappedOperator;

			Curve curve = _curveFacade.Create(xSpan, nodeTuples);
			op.LinkTo(curve);

			return wrapper;
		}

		public OperatorWrapper Curve(
			double xSpan,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			params double?[] yValues)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			Operator op = wrapper.WrappedOperator;

			Curve curve = _curveFacade.Create(xSpan, yValues);
			op.LinkTo(curve);

			return wrapper;
		}

	    private OperatorWrapper DimensionToOutlets(Outlet signal, int outletCount) => DimensionToOutletsPrivate(signal, null, null, outletCount);

	    private OperatorWrapper DimensionToOutletsPrivate(
			Outlet signal,
			DimensionEnum? standardDimension,
			string customDimension,
			int? outletCount)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension, nameof(SystemPatchNames.DimensionToOutlets));
			Operator op = wrapper.WrappedOperator;

			// OutletCount
			VoidResult setOutletCountResult = _patchFacade.SetOperatorOutletCount(op, outletCount ?? 1);
			setOutletCountResult.Assert();

			wrapper.Inputs[DimensionEnum.Signal] = signal;

			return wrapper;
		}

	    private OperatorWrapper_WithInterpolation InletsToDimension(params Outlet[] items) => InletsToDimensionPrivate(items, null, null);

	    private OperatorWrapper_WithInterpolation InletsToDimensionPrivate(
			IList<Outlet> items,
			InterpolationTypeEnum? interpolation,
			DimensionEnum? standardDimension,
			string customDimension = null)
		{
			if (items == null) throw new NullException(() => items);

			Operator op = NewWithDimension(standardDimension, customDimension, nameof(InletsToDimension));

			var wrapper = new OperatorWrapper_WithInterpolation(op)
			{
				InterpolationType = interpolation ?? InterpolationTypeEnum.Stripe
			};

			// Items
		    VoidResult setOperatorInletCountResult = _patchFacade.SetOperatorInletCount(op, items.Count);
		    setOperatorInletCountResult.Assert();
		    wrapper.Inputs.SetMany(DimensionEnum.Item, items);

			return wrapper;
		}

		public OperatorWrapper_WithInterpolation_AndFollowingMode Interpolate(
			Outlet signal = null,
			Outlet samplingRate = null,
			InterpolationTypeEnum interpolationType = InterpolationTypeEnum.Cubic,
			DimensionEnum? standardDimension = DimensionEnum.Time,
			string customDimension = null,
			FollowingModeEnum followingMode = FollowingModeEnum.LagBehind)
		{
			Operator op = NewWithDimension(standardDimension, customDimension);

			var wrapper = new OperatorWrapper_WithInterpolation_AndFollowingMode(op);
			wrapper.Inputs[DimensionEnum.Signal] = signal;
			wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
			wrapper.InterpolationType = interpolationType;
			wrapper.FollowingMode = followingMode;

			return wrapper;
		}

	    private OperatorWrapper MaxFollower(
			Outlet signal = null,
			Outlet sliceLength = null,
			Outlet sampleCount = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
	        => NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

	    private OperatorWrapper_WithCollectionRecalculation MaxOverDimension(
			Outlet signal = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForAggregateOverDimension(
	            signal,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    private OperatorWrapper MinFollower(
			Outlet signal = null,
			Outlet sliceLength = null,
			Outlet sampleCount = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
	        => NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

	    private OperatorWrapper_WithCollectionRecalculation MinOverDimension(
			Outlet signal = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForAggregateOverDimension(
	            signal,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    public OperatorWrapper MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
	    {
	        OperatorWrapper wrapper = NewBase();

	        wrapper.Inputs[DimensionEnum.A] = a;
	        wrapper.Inputs[DimensionEnum.B] = b;
	        wrapper.Inputs[DimensionEnum.Origin] = origin;

	        return wrapper;
	    }

	    public OperatorWrapper Noise(
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			return wrapper;
		}

		public Number_OperatorWrapper Number(double number)
		{
			Operator op = NewBase();

			var wrapper = new Number_OperatorWrapper(op) { Number = number };

			return wrapper;
		}

		public PatchInletOrOutlet_OperatorWrapper PatchInlet()
		{
			Operator op = NewBase();

			new Operator_SideEffect_GeneratePatchInletPosition(op).Execute();

			// Call save to execute side-effects and robust validation.
			_patchFacade.SaveOperator(op).Assert();

			return new PatchInletOrOutlet_OperatorWrapper(op);
		}

	    public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
	    {
	        PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();

	        wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

	        return wrapper;
	    }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
		{
			PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();

			wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);
			wrapper.Inlet.DefaultValue = defaultValue;

			return wrapper;
		}

		public PatchInletOrOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
		{
			Operator op = NewBase();

			var wrapper = new PatchInletOrOutlet_OperatorWrapper(op) { Input = input };

			new Operator_SideEffect_GeneratePatchOutletPosition(op).Execute();

			// Call save to execute side-effects and robust validation.
			_patchFacade.SaveOperator(op).Assert();

			return wrapper;
		}

		public PatchInletOrOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
		{
			PatchInletOrOutlet_OperatorWrapper wrapper = PatchOutlet(input);

			wrapper.Outlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

			return wrapper;
		}

	    private OperatorWrapper_WithInterpolation Random(
			Outlet rate = null,
			InterpolationTypeEnum interpolationType = InterpolationTypeEnum.Stripe,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		{
			Operator op = NewWithDimension(standardDimension, customDimension);

			var wrapper = new OperatorWrapper_WithInterpolation(op);
			wrapper.Inputs[DimensionEnum.Rate] = rate;
			wrapper.InterpolationType = interpolationType;

			return wrapper;
		}

	    private OperatorWrapper RangeOverOutlets(
			Outlet from = null,
			Outlet step = null,
			int? outletCount = null)
		{
			OperatorWrapper wrapper = NewBase();
			Operator op = wrapper.WrappedOperator;

			if (outletCount.HasValue)
			{
				VoidResult setOutletCountResult = _patchFacade.SetOperatorOutletCount(op, outletCount.Value);
				setOutletCountResult.Assert();
			}

			wrapper.Inputs[DimensionEnum.From] = from;
			wrapper.Inputs[DimensionEnum.Step] = step;

			return wrapper;
		}

	    private Reset_OperatorWrapper Reset(Outlet passThrough = null, int? position = null)
		{
			Operator op = NewBase();

			var wrapper = new Reset_OperatorWrapper(op);
			wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
			wrapper.Position = position;

			return wrapper;
		}

	    private OperatorWrapper Sample(
			Stream stream,
			Outlet frequency = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
		{
			byte[] bytes = StreamHelper.StreamToBytes(stream);

			OperatorWrapper wrapper = Sample(bytes, frequency, standardDimension, customDimension, audioFileFormatEnum);

			return wrapper;
		}

		public OperatorWrapper Sample(
			byte[] bytes,
			Outlet frequency = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
			Operator op = wrapper.WrappedOperator;

			SampleInfo sampleInfo = _sampleFacade.CreateSample(bytes, audioFileFormatEnum);
			Sample sample = sampleInfo.Sample;
			op.LinkTo(sample);

			wrapper.Inputs[DimensionEnum.Frequency] = frequency;

			return wrapper;
		}

	    public OperatorWrapper SawUp(
			Outlet frequency = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
	        => NewWithFrequency(frequency, standardDimension, customDimension);

		public OperatorWrapper Sine(
			Outlet frequency = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		    => NewWithFrequency(frequency, standardDimension, customDimension);

	    private OperatorWrapper_WithCollectionRecalculation SortOverDimension(
			Outlet signal = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForAggregateOverDimension(
	            signal,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    private OperatorWrapper SumFollower(
			Outlet signal = null,
			Outlet sliceLength = null,
			Outlet sampleCount = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null)
		    => NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

	    private OperatorWrapper_WithCollectionRecalculation SumOverDimension(
			Outlet signal = null,
			Outlet from = null,
			Outlet till = null,
			Outlet step = null,
			DimensionEnum? standardDimension = null,
			string customDimension = null,
			CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
	        => NewForAggregateOverDimension(
	            signal,
	            from,
	            till,
	            step,
	            standardDimension,
	            customDimension,
	            collectionRecalculation);

	    public OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null) => NewWithFrequency(frequency, standardDimension, customDimension);

	    // Helpers

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper NewWithFrequency(
			Outlet frequency,
			DimensionEnum? standardDimension,
			string customDimension,
			[CallerMemberName] string systemPatchName = null)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension, systemPatchName);

			wrapper.Inputs[DimensionEnum.Frequency] = frequency;

			return wrapper;
		}

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper NewWithDimension(
			DimensionEnum? standardDimension,
			string customDimension,
			[CallerMemberName] string systemPatchName = null)
		{
			OperatorWrapper wrapper = NewBase(systemPatchName);
			Operator op = wrapper.WrappedOperator;

			if (standardDimension.HasValue)
			{
				op.SetStandardDimensionEnum(standardDimension.Value, _repositories.DimensionRepository);
			}

			op.CustomDimensionName = customDimension;

			return wrapper;
		}

	    public OperatorWrapper NewWithItemInlets(Patch patch, params Outlet[] items) => NewWithItemInlets(patch, (IList<Outlet>)items);

	    public OperatorWrapper NewWithItemInlets(Patch patch, IList<Outlet> items) => NewWithItemInlets(patch, null, items);

	    public OperatorWrapper NewWithItemInlets(string systemPatchName, params Outlet[] items)
	        => NewWithItemInlets(systemPatchName, (IList<Outlet>)items);

	    public OperatorWrapper NewWithItemInlets(string systemPatchName, IList<Outlet> items) => NewWithItemInlets(null, systemPatchName, items);

	    private OperatorWrapper NewWithItemInlets(Patch underlyingPatch, string systemPatchName, IList<Outlet> items)
	    {
	        if (items == null) throw new NullException(() => items);

	        Operator op;

	        if (underlyingPatch != null)
	        {
	            op = NewBase(underlyingPatch);
	        }
	        else if (!string.IsNullOrEmpty(systemPatchName))
	        {
	            op = NewBase(systemPatchName);
	        }
	        else
	        {
	            throw new Exception($"Neither {nameof(underlyingPatch)} nor {nameof(systemPatchName)} is filled in.");
	        }

	        VoidResult setInletCountResult = _patchFacade.SetOperatorInletCount(op, items.Count);
	        setInletCountResult.Assert();

	        var wrapper = new OperatorWrapper(op);
	        wrapper.Inputs.SetMany(DimensionEnum.Item, items);

	        return wrapper;
	    }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewWithInputAndItemsInlets(
			Outlet input,
			IList<Outlet> items,
			[CallerMemberName] string systemPatchName = null)
		{
			if (items == null) throw new NullException(() => items);

			Operator op = NewBase(systemPatchName);

		    VoidResult setInletCountResult = _patchFacade.SetOperatorInletCount(op, items.Count + 1);
		    setInletCountResult.Assert();

		    var wrapper = new OperatorWrapper(op);
			wrapper.Inputs[DimensionEnum.Input] = input;
			wrapper.Inputs.SetMany(DimensionEnum.Item, items);

			return wrapper;
		}

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper NewForAggregateFollower(
			Outlet signal,
			Outlet sliceLength,
			Outlet sampleCount,
			DimensionEnum? standardDimension,
			string customDimension,
			[CallerMemberName] string systemPatchName = null)
		{
			OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension, systemPatchName);

			wrapper.Inputs[DimensionEnum.Signal] = signal;
			wrapper.Inputs[DimensionEnum.SliceLength] = sliceLength;
			wrapper.Inputs[DimensionEnum.SampleCount] = sampleCount;

			return wrapper;
		}

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper_WithCollectionRecalculation NewForAggregateOverDimension(
			Outlet signal,
			Outlet from,
			Outlet till,
			Outlet step,
			DimensionEnum? standardDimension,
			string customDimension,
			CollectionRecalculationEnum collectionRecalculation,
			[CallerMemberName] string systemPatchName = null)
		{
			Operator op = NewWithDimension(standardDimension, customDimension, systemPatchName);

			var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
			wrapper.Inputs[DimensionEnum.Signal] = signal;
			wrapper.Inputs[DimensionEnum.From] = from;
			wrapper.Inputs[DimensionEnum.Till] = till;
			wrapper.Inputs[DimensionEnum.Step] = step;
			wrapper.CollectionRecalculation = collectionRecalculation;

			return wrapper;
		}

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper_WithCollectionRecalculation NewForClosestOverDimension_OrClosestOverDimensionExp(
			Outlet input,
			Outlet collection,
			Outlet from,
			Outlet till,
			Outlet step,
			DimensionEnum? standardDimension,
			string customDimension,
			CollectionRecalculationEnum collectionRecalculation,
			[CallerMemberName] string systemPatchName = null)
		{
			Operator op = NewWithDimension(standardDimension, customDimension, systemPatchName);

			var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
			wrapper.Inputs[DimensionEnum.Input] = input;
			wrapper.Inputs[DimensionEnum.Collection] = collection;
			wrapper.Inputs[DimensionEnum.From] = from;
			wrapper.Inputs[DimensionEnum.Till] = till;
			wrapper.Inputs[DimensionEnum.Step] = step;
			wrapper.CollectionRecalculation = collectionRecalculation;

			return wrapper;
		}

		// Generic methods for operator creation

		public OperatorWrapper New(string systemPatchName, int variableInletOrOutletCount = 16)
		{
			Patch patch = _systemFacade.GetSystemPatch(systemPatchName);
			return New(patch, variableInletOrOutletCount);
		}

	    public OperatorWrapper New(string systemPatchName, params Outlet[] operands)
	    {
	        Patch patch = _systemFacade.GetSystemPatch(systemPatchName);
	        return New(patch, operands);
	    }

        private OperatorWrapper New(Patch underlyingPatch, params Outlet[] operands) => New(underlyingPatch, (IList<Outlet>)operands);

	    private OperatorWrapper New(Patch underlyingPatch, IList<Outlet> operands)
		{
			if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
			if (operands == null) throw new NullException(() => operands);

			OperatorWrapper wrapper = New(underlyingPatch);
			Operator op = wrapper.WrappedOperator;

			if (op.Inlets.Count != operands.Count)
			{
				throw new NotEqualException(() => op.Inlets.Count, () => operands.Count);
			}

			IList<Inlet> sortedInlets = op.Inlets.Sort().ToArray();

			for (int i = 0; i < operands.Count; i++)
			{
				Inlet inlet = sortedInlets[i];
				Outlet operand = operands[i];

				inlet.LinkTo(operand);
			}

			return wrapper;
		}

		/// <summary>
		/// Called from the DocumentTree view's New action.
		/// Does not support creating a Sample operator.
		/// </summary>
		public OperatorWrapper New(Patch underlyingPatch, int variableInletOrOutletCount = 16)
		{
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.AverageFollower))) return AverageFollower();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.AverageOverDimension))) return AverageOverDimension();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Cache))) return Cache();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.ClosestOverDimension))) return ClosestOverDimension();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.ClosestOverDimensionExp))) return ClosestOverDimensionExp();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.ClosestOverInlets))) return ClosestOverInlets(null, new Outlet[variableInletOrOutletCount]);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.ClosestOverInletsExp))) return ClosestOverInletsExp(null, new Outlet[variableInletOrOutletCount]);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Curve))) return Curve();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.DimensionToOutlets))) return DimensionToOutlets(null, variableInletOrOutletCount);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.InletsToDimension))) return InletsToDimension(new Outlet[variableInletOrOutletCount]);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Interpolate))) return Interpolate();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MaxFollower))) return MaxFollower();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MaxOverDimension))) return MaxOverDimension();
		    if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MinFollower))) return MinFollower();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MinOverDimension))) return MinOverDimension();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Number))) return Number(1);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.PatchInlet))) return PatchInlet();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.PatchOutlet))) return PatchOutlet();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Random))) return Random();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.RangeOverOutlets))) return RangeOverOutlets(outletCount: variableInletOrOutletCount);
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Reset))) return Reset();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.SortOverDimension))) return SortOverDimension();
		    if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.SumFollower))) return SumFollower();
			if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.SumOverDimension))) return SumOverDimension();

		    if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Add)) ||
		        NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.AverageOverInlets)) ||
		        NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MaxOverInlets)) ||
		        NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.MinOverInlets)) ||
		        NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.Multiply)) ||
		        NameHelper.AreEqual(underlyingPatch.Name, nameof(SystemPatchNames.SortOverInlets)))
		    {
		        return NewWithItemInlets(underlyingPatch.Name, new Outlet[variableInletOrOutletCount]);
		    }

		    if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Sample)))
			{
				throw new NotSupportedException(
					$"{nameof(Sample)} operator cannot be created with the generic {nameof(New)} method, " +
					$"because it needs a byte array or Stream. Call the {nameof(Sample)} method instead.");
			}

			OperatorWrapper op = NewBase(underlyingPatch);
			return op;
		}

		/// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
		private OperatorWrapper NewBase([CallerMemberName] string systemPatchName = null)
		{
			Patch patch = _systemFacade.GetSystemPatch(systemPatchName);
			return NewBase(patch);
		}

		private OperatorWrapper NewBase(Patch underlyingPatch)
		{
			var op = new Operator { ID = _repositories.IDRepository.GetID() };
			_repositories.OperatorRepository.Insert(op);
			op.LinkTo(_patch);

			op.LinkToUnderlyingPatch(underlyingPatch);

			new Operator_SideEffect_AutoCreateEntityPosition(op, _repositories.EntityPositionRepository, _repositories.IDRepository).Execute();
			new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

			new OperatorValidator_Basic(op).Assert();

			var wrapper = new OperatorWrapper(op);

			// Must Get UnderlyingPatch by ID again,
			// because a cached system patch, might not be from the same context.
			// It will make the ORM crash if you link two entities from different contexts together.
			// The advantage of using the cached system patch graph does hold up.
			op.LinkToUnderlyingPatch(_repositories.PatchRepository.Get(underlyingPatch.ID));

			return wrapper;
		}
	}
}