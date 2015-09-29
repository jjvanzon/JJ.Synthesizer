using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Managers
{
    public class ScaleManager
    {
        private ScaleRepositories _repositories;

        public ScaleManager(ScaleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Create

        public Scale Create(Document document, bool mustSetDefaults, bool mustGenerateName)
        {
            return Create(document, default(ScaleTypeEnum), mustSetDefaults, mustGenerateName);
        }

        public Scale Create(
            Document document,
            ScaleTypeEnum scaleTypeEnum = ScaleTypeEnum.Undefined, 
            bool mustSetDefaults = false, 
            bool mustGenerateName = false)
        {
            if (document == null) throw new NullException(() => document);

            Scale scale = Create(scaleTypeEnum, mustSetDefaults);
            scale.LinkTo(document);

            // NOTE: This side-effect can only be executed if the scale has a document.
            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Scale_SideEffect_GenerateName(scale);
                sideEffect.Execute();
            }

            return scale;
        }

        public Scale Create(ScaleTypeEnum scaleTypeEnum, bool mustSetDefaults = false)
        {
            var scale = new Scale();
            scale.ID = _repositories.IDRepository.GetID();
            scale.SetScaleTypeEnum(scaleTypeEnum, _repositories.ScaleTypeRepository);
            _repositories.ScaleRepository.Insert(scale);

            if (mustSetDefaults)
            {
                ISideEffect sideEffect = new Scale_SideEffect_SetDefaults(scale, _repositories.ScaleTypeRepository);
                sideEffect.Execute();
            }

            return scale;
        }

        // Save

        public VoidResult Save(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);
            if (scale.ID == 0) throw new ZeroException(() => scale.ID);

            IValidator validator1 = new ScaleValidator_Versatile(scale);
            IValidator validator2 = new ScaleValidator_InDocument(scale);

            if (!validator1.IsValid || !validator2.IsValid)
            {
                return new VoidResult
                {
                    Messages = validator1.ValidationMessages.Union(validator2.ValidationMessages).ToCanonical()
                };
            }

            return new VoidResult
            {
                Successful = true
            };
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

            var tone = new Tone();
            tone.ID = _repositories.IDRepository.GetID();
            tone.LinkTo(scale);
            _repositories.ToneRepository.Insert(tone);
            return tone;
        }

        public VoidResult SaveTone(Tone tone)
        {
            if (tone == null) throw new NullException(() => tone);
            if (tone.ID == 0) throw new ZeroException(() => tone.ID);

            IValidator validator = new ToneValidator(tone);
            return new VoidResult
            {
                Successful = validator.IsValid,
                Messages = validator.ValidationMessages.ToCanonical()
            };
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
