using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Scales;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	public class ScaleFacade
	{
		private readonly ScaleRepositories _repositories;

		public ScaleFacade(ScaleRepositories repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
		}

		// Create

		public Scale Create(Document document, ScaleTypeEnum scaleTypeEnum = default)
		{
			if (document == null) throw new NullException(() => document);

			Scale scale = Create(scaleTypeEnum);
			scale.LinkTo(document);

			new Scale_SideEffect_GenerateName(scale).Execute();

			return scale;
		}

		public Scale Create(ScaleTypeEnum scaleTypeEnum)
		{
			var scale = new Scale { ID = _repositories.IDRepository.GetID() };
			scale.SetScaleTypeEnum(scaleTypeEnum, _repositories.ScaleTypeRepository);
			_repositories.ScaleRepository.Insert(scale);

			new Scale_SideEffect_SetDefaults(scale, _repositories.ScaleTypeRepository).Execute();

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

		public void DeleteWithRelatedEntities(int id)
		{
			Scale scale = _repositories.ScaleRepository.Get(id);
			DeleteWithRelatedEntities(scale);
		}		

		public void DeleteWithRelatedEntities(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			// No delete constraints yet, but they might come in the future.

			scale.DeleteRelatedEntities(_repositories.ToneRepository);
			scale.UnlinkRelatedEntities();
			_repositories.ScaleRepository.Delete(scale);
		}

		// Tone Actions

		public Tone CreateTone(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			var tone = new Tone { ID = _repositories.IDRepository.GetID() };
			tone.LinkTo(scale);
			_repositories.ToneRepository.Insert(tone);
			return tone;
		}

		public VoidResult SaveTone(Tone tone)
		{
			if (tone == null) throw new NullException(() => tone);
			if (tone.ID == 0) throw new ZeroException(() => tone.ID);

			IValidator validator = new ToneValidator(tone);
			return validator.ToResult();
		}

		public void DeleteTone(int id)
		{
			Tone tone = _repositories.ToneRepository.Get(id);
			DeleteTone(tone);
		}

		public void DeleteTone(Tone tone)
		{
			if (tone == null) throw new NullException(() => tone);

			tone.UnlinkScale();
			_repositories.ToneRepository.Delete(tone);
		}
	}
}
