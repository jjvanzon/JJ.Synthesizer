using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToIDAndNameExtensions
    {
        public static IDNameAndListIndexViewModel ToIDNameAndListIndex(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDNameAndListIndexViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDNameAndListIndexViewModel ToIDNameAndListIndex(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new IDNameAndListIndexViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };

            return viewModel;
        }

        public static IList<IDNameAndListIndexViewModel> ToIDNameAndListIndexes(this IList<Patch> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModels = new List<IDNameAndListIndexViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Patch entity = entities[i];
                IDNameAndListIndexViewModel viewModel = entity.ToIDNameAndListIndex();
                viewModel.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static IDAndName ToIDAndName(this AudioFileFormat entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this SampleDataType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDNameAndListIndexViewModel ToIDNameAndListIndex(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDNameAndListIndexViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IList<IDNameAndListIndexViewModel> ToIDNameAndListIndexes(this IList<Curve> entities)
        {
            if (entities == null) throw new NullException(() => entities);

            var viewModels = new List<IDNameAndListIndexViewModel>(entities.Count);

            for (int i = 0; i < entities.Count; i++)
            {
                Curve entity = entities[i];
                IDNameAndListIndexViewModel viewModel = entity.ToIDNameAndListIndex();
                viewModel.ListIndex = i;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public static IDAndName ToIDAndName(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this NodeType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }
    }
}
