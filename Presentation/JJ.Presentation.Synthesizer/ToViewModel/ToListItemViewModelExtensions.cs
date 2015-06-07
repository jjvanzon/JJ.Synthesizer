using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    public static class ToListItemViewModelExtensions
    {
        // AudioFileOutput

        public static IList<AudioFileOutputListItemViewModel> ToListItemViewModels(this IList<AudioFileOutput> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            var viewModels = new List<AudioFileOutputListItemViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                AudioFileOutput entity = entities[i];
                AudioFileOutputListItemViewModel viewModel = entity.ToListItemViewModel(i);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static AudioFileOutputListItemViewModel ToListItemViewModel(this AudioFileOutput entity, int listIndex)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            var viewModel = new AudioFileOutputListItemViewModel
            {
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                Keys = new AudioFileOutputKeysViewModel
                {
                    ID = entity.ID,
                    DocumentID = entity.Document.ID,
                    ListIndex = listIndex
                }
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }

        // Curve

        public static IList<CurveListItemViewModel> ToListItemViewModels(
            this IList<Curve> entities,
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            var viewModels = new List<CurveListItemViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Curve entity = entities[i];
                CurveListItemViewModel viewModel = entity.ToListItemViewModel(i, childDocumentListIndex);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static CurveListItemViewModel ToListItemViewModel(
            this Curve entity,
            int listIndex,
            int? childDocumentListIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            Document rootDocument = ChildDocumentHelper.GetRootDocument(entity.Document);
            ChildDocumentTypeEnum? childDocumentTypeEnum = ChildDocumentHelper.TryGetChildDocumentTypeEnum(entity.Document);

            return new CurveListItemViewModel
            {
                Name = entity.Name,
                Keys = new CurveKeysViewModel
                {
                    ID = entity.ID,
                    RootDocumentID = rootDocument.ID,
                    ListIndex = listIndex,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };
        }

        // ChildDocument

        public static IList<ChildDocumentListItemViewModel> ToChildDocumentListItemsViewModel(this IList<Document> sourceEntities)
        {
            if (sourceEntities == null) throw new NullException(() => sourceEntities);

            sourceEntities = sourceEntities.OrderBy(x => x.Name).ToArray();

            var destList = new List<ChildDocumentListItemViewModel>(sourceEntities.Count);

            for (int i = 0; i < sourceEntities.Count; i++)
            {
                Document sourceEntity = sourceEntities[i];
                ChildDocumentListItemViewModel destListItem = sourceEntity.ToChildDocumentListItemViewModel(i);
                destList.Add(destListItem);
            }

            return destList;
        }

        public static ChildDocumentListItemViewModel ToChildDocumentListItemViewModel(this Document childDocument, int listIndex)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            return new ChildDocumentListItemViewModel
            {
                Name = childDocument.Name,
                Keys = new ChildDocumentKeysViewModel
                {
                    ID = childDocument.ID,
                    ParentDocumentID = ChildDocumentHelper.GetParentDocumentID(childDocument),
                    ChildDocumentTypeEnum = ChildDocumentHelper.GetChildDocumentTypeEnum(childDocument),
                    ListIndex = listIndex,
                }
            };
        }

        // Patch

        public static IList<PatchListItemViewModel> ToListItemViewModels(
            this IList<Patch> entities,
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            var viewModels = new List<PatchListItemViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Patch entity = entities[i];
                PatchListItemViewModel viewModel = entity.ToListItemViewModel(i, childDocumentListIndex);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static PatchListItemViewModel ToListItemViewModel(
            this Patch entity,
            int listIndex,
            int? childDocumentListIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            Document rootDocument = ChildDocumentHelper.GetRootDocument(entity.Document);
            ChildDocumentTypeEnum? childDocumentTypeEnum = ChildDocumentHelper.TryGetChildDocumentTypeEnum(entity.Document);

            var viewModel = new PatchListItemViewModel
            {
                Name = entity.Name,
                Keys = new PatchKeysViewModel
                {
                    ID = entity.ID,
                    RootDocumentID = rootDocument.ID,
                    ListIndex = listIndex,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            return viewModel;
        }

        // Samples

        public static IList<SampleListItemViewModel> ToListItemViewModels(
            this IList<Sample> entities,
            int documentID,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex)
        {
            if (entities == null) throw new NullException(() => entities);

            entities = entities.OrderBy(x => x.Name).ToArray();

            var viewModels = new List<SampleListItemViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Sample entity = entities[i];
                SampleListItemViewModel viewModel = entity.ToListItemViewModel(i, childDocumentListIndex);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static SampleListItemViewModel ToListItemViewModel(
            this Sample entity, 
            int listIndex,
            int? childDocumentListIndex)
        {
            if (entity == null) throw new NullException(() => entity);

            Document rootDocument = ChildDocumentHelper.GetRootDocument(entity.Document);
            ChildDocumentTypeEnum? childDocumentTypeEnum = ChildDocumentHelper.TryGetChildDocumentTypeEnum(entity.Document);

            var viewModel = new SampleListItemViewModel
            {
                IsActive = entity.IsActive,
                Name = entity.Name,
                SamplingRate = entity.SamplingRate,
                Keys = new SampleKeysViewModel
                {
                    ID = entity.ID,
                    DocumentID = rootDocument.ID,
                    ListIndex = listIndex,
                    ChildDocumentTypeEnum = childDocumentTypeEnum,
                    ChildDocumentListIndex = childDocumentListIndex
                }
            };

            if (entity.AudioFileFormat != null)
            {
                viewModel.AudioFileFormat = entity.AudioFileFormat.Name;
            }

            if (entity.SampleDataType != null)
            {
                viewModel.SampleDataType = entity.SampleDataType.Name;
            }

            if (entity.SpeakerSetup != null)
            {
                viewModel.SpeakerSetup = entity.SpeakerSetup.Name;
            }

            return viewModel;
        }
    }
}
