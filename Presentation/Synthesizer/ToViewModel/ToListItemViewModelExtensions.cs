﻿using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using System;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToListItemViewModelExtensions
    {
        // AudioFileOutput

        public static IList<AudioFileOutputListItemViewModel> ToListItemViewModels(this IList<AudioFileOutput> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            IList<AudioFileOutputListItemViewModel> viewModels = entities.OrderBy(x => x.Name)
                                                                         .Select(x => x.ToListItemViewModel())
                                                                         .ToList();
            return viewModels;
        }

        public static AudioFileOutputListItemViewModel ToListItemViewModel(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new AudioFileOutputListItemViewModel
            {
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                ID = entity.ID
            };

            // TODO: Do this with Enums.
            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = PropertyDisplayNames.ResourceManager.GetString(entity.AudioFileFormat.Name);
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = PropertyDisplayNames.ResourceManager.GetString(entity.SampleDataType.Name);
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = PropertyDisplayNames.ResourceManager.GetString(entity.SpeakerSetup.Name);
            }

            return viewModel;
        }

        // Curves

        public static IList<CurveListItemViewModel> ToListItemViewModels(this IList<UsedInDto<Curve>> dtos)
        {
            if (dtos == null) throw new NullException(() => dtos);

            IList<CurveListItemViewModel> viewModels = dtos.Select(x => x.ToListItemViewModel())
                                                           .OrderBy(x => x.Name)
                                                           .ToList();
            return viewModels;
        }

        public static CurveListItemViewModel ToListItemViewModel(this UsedInDto<Curve> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var viewModel = new CurveListItemViewModel
            {
                ID = dto.Entity.ID,
                Name = dto.Entity.Name,
                UsedIn = ViewModelHelper.FormatUsedInList(dto.UsedInIDAndNames)
            };

            return viewModel;
        }

        // Patches

        public static IList<PatchListItemViewModel> ToListItemViewModels(this IList<UsedInDto<Patch>> dtos)
        {
            if (dtos == null) throw new NullException(() => dtos);

            IList<PatchListItemViewModel> viewModels = dtos.Select(x => x.ToListItemViewModel())
                                                           .OrderBy(x => x.Name)
                                                           .ToList();
            return viewModels;
        }

        public static PatchListItemViewModel ToListItemViewModel(this UsedInDto<Patch> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            var viewModel = new PatchListItemViewModel
            {
                ID = dto.Entity.ID,
                Name = dto.Entity.Name,
                UsedIn = ViewModelHelper.FormatUsedInList(dto.UsedInIDAndNames)
            };

            return viewModel;
        }

        // Samples

        public static IList<SampleListItemViewModel> ToListItemViewModels(this IList<UsedInDto<Sample>> dtos)
        {
            if (dtos == null) throw new NullException(() => dtos);

            IList<SampleListItemViewModel> viewModels = dtos.Select(x => x.ToListItemViewModel())
                                                            .OrderBy(x => x.Name)
                                                            .ToList();
            return viewModels;
        }

        public static SampleListItemViewModel ToListItemViewModel(this UsedInDto<Sample> dto)
        {
            if (dto == null) throw new NullException(() => dto);

            Sample entity = dto.Entity;

            var viewModel = new SampleListItemViewModel
            {
                IsActive = entity.IsActive,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                ID = entity.ID,
                UsedIn = ViewModelHelper.FormatUsedInList(dto.UsedInIDAndNames)
            };

            // TODO: Do this with enums.
            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = PropertyDisplayNames.ResourceManager.GetString(entity.SampleDataType.Name);
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = PropertyDisplayNames.ResourceManager.GetString(entity.SpeakerSetup.Name);
            }

            return viewModel;
        }
    }
}