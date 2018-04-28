using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Scales;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	public class ScaleFacade
	{
		private readonly ScaleRepositories _repositories;
		private readonly ToneToDtoConverter _toneToDtoConverter = new ToneToDtoConverter();
		private readonly ToneCalculator _toneCalculator = new ToneCalculator();

		public ScaleFacade(ScaleRepositories repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
		}

		// Create

		public Scale Create(Document document, ScaleTypeEnum scaleTypeEnum = default)
		{
			if (document == null) throw new NullException(() => document);

			var scale = new Scale { ID = _repositories.IDRepository.GetID() };
			scale.SetScaleTypeEnum(scaleTypeEnum, _repositories.ScaleTypeRepository);
			scale.LinkTo(document);
			_repositories.ScaleRepository.Insert(scale);

			new Scale_SideEffect_SetDefaults(scale, _repositories.ScaleTypeRepository).Execute();
			new Scale_SideEffect_GenerateName(scale).Execute();

			return scale;
		}

		// Save

		public VoidResult Save(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);
			if (scale.ID == 0) throw new ZeroException(() => scale.ID);

			VoidResult result = SaveWithoutTones(scale);

			IValidator validator = new ScaleValidator_Tones(scale);

			result.Combine(validator.ToResult());

			return result;
		}

		public VoidResult SaveWithoutTones(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);
			if (scale.ID == 0) throw new ZeroException(() => scale.ID);

			var validators = new List<IValidator>
			{
				new ScaleValidator_Versatile_WithoutTones(scale),
				new ScaleValidator_UniqueName(scale)
			};

			if (scale.Document != null)
			{
				validators.Add(new ScaleValidator_InDocument(scale));
			}

			return validators.ToResult();
		}

		// Delete

		public VoidResult DeleteWithRelatedEntities(int id)
		{
			Scale scale = _repositories.ScaleRepository.Get(id);
			return DeleteWithRelatedEntities(scale);
		}		

		public VoidResult DeleteWithRelatedEntities(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			scale.DeleteRelatedEntities(_repositories.ToneRepository);
			scale.UnlinkRelatedEntities();
			_repositories.ScaleRepository.Delete(scale);

			return ResultHelper.Successful;
		}

		// Tone Operations

		public Tone CreateTone(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			Tone previousTone = scale.Tones.Sort().LastOrDefault();

			var tone = new Tone { ID = _repositories.IDRepository.GetID() };
			_repositories.ToneRepository.Insert(tone);
			tone.LinkTo(scale);

			new Tone_SideEffect_SetDefaults_Versatile(tone, previousTone).Execute();

			return tone;
		}

		// ReSharper disable once UnusedMember.Global
		public VoidResult SaveTone(Tone tone)
		{
			if (tone == null) throw new NullException(() => tone);

			IValidator validator = new ToneValidator(tone);
			return validator.ToResult();
		}

		public void DeleteTone(int id)
		{
			Tone tone = _repositories.ToneRepository.Get(id);
			DeleteTone(tone);
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public void DeleteTone(Tone tone)
		{
			if (tone == null) throw new NullException(() => tone);

			tone.UnlinkScale();
			_repositories.ToneRepository.Delete(tone);
		}

		// Misc

		public IList<ToneDto> GetToneDtosWithCompleteSetOfOctaves(Scale scale)
		{
			IList<Tone> scaleTones = scale.Tones;
			return GetToneDtosWithCompleteSetOfOctaves(scaleTones);
		}

		private IList<ToneDto> GetToneDtosWithCompleteSetOfOctaves(IList<Tone> tones)
		{
			IEnumerable<ToneDto> toneDtos = tones.Select(x => _toneToDtoConverter.Convert(x));
			IList<ToneDto> toneDtos2 = _toneCalculator.MakeOctavesComplete(toneDtos);
			return toneDtos2;
		}
	}
}
