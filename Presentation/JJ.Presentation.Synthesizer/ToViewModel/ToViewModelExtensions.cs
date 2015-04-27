using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                ID = entity.ID,
                PatchName = entity.Name
            };

            return viewModel;
        }

        public static OperatorViewModel ToViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string name;
            if (String.Equals(entity.OperatorTypeName, PropertyNames.ValueOperator))
            {
                var wrapper = new ValueOperatorWrapper(entity);
                name = wrapper.Value.ToString("0.####");
            }
            else
            {
                name = entity.Name;
            }

            var viewModel = new OperatorViewModel
            {
                ID = entity.ID,
                TemporaryID = Guid.NewGuid(),
                Name = name,
                OperatorTypeName = entity.OperatorTypeName
            };

            return viewModel;
        }

        public static InletViewModel ToViewModel(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };

            return viewModel;
        }

        public static OutletViewModel ToViewModel(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                TemporaryID = Guid.NewGuid()
            };

            return viewModel;
        }

        public static PatchListItemViewModel ToListItemViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchListItemViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
            };

            if (entity.Document != null)
            {
                viewModel.DocumentName = entity.Document.Name;
            }

            return viewModel;
        }
    }
}
