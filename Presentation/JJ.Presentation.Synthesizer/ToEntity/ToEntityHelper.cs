using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityHelper
    {
        /// <summary>
        /// Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
        /// the inlets and outlets too, which are not defined in the OperatorPropertiesViewModel.
        /// Also, we need to associate the Operator with its patch (derived from the PatchDetailsViewModel),
        /// because other code uses the PatchManager.
        /// 
        /// TODO: If lookups in view models were fast, you might not need to return the OperatorViewModel for performance,
        /// but instead do a second lookups, so it becomes a regular ToEntity method,
        /// which return the entity and move this to the ToEntityExcentions.
        /// </summary>
        public static OperatorEntityAndViewModel ToOperatorWithInletsAndOutletsAndPatch(DocumentViewModel documentViewModel, int operatorID, PatchRepositories repositories)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);
            if (repositories == null) throw new NullException(() => repositories);

            PatchDetailsViewModel patchDetailsViewModel = DocumentViewModelHelper.GetPatchDetailsViewModel_ByOperatorID(documentViewModel, operatorID);
            OperatorViewModel operatorViewModel = patchDetailsViewModel.Entity.Operators.Where(x => x.ID == operatorID).First();
            Patch patch = patchDetailsViewModel.Entity.ToPatch(repositories.PatchRepository);
            Operator op = operatorViewModel.ToEntityWithInletsAndOutlets(repositories);
            PatchManager patchManager = new PatchManager(patch, repositories);
            patchManager.SaveOperator(op);

            return new OperatorEntityAndViewModel(op, operatorViewModel);
        }

        /// <summary> Hack back in a PatchOutlet's Outlet, that was excluded from the view model. </summary>
        public static Outlet HACK_CreatePatchOutletOutletIfNeeded(
            Operator op, IOutletRepository outletRepository, IIDRepository idRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
            {
                Outlet outlet = op.Outlets.FirstOrDefault();
                if (outlet == null)
                {
                    outlet = new Outlet();
                    outlet.ID = idRepository.GetID();
                    outlet.ListIndex = 0;
                    outletRepository.Insert(outlet);
                    outlet.LinkTo(op);
                }

                return outlet;
            }

            return null;
        }

        /// <summary> Hack back in a PatchInlet's Inlet, that was excluded from the view model. </summary>
        public static Inlet HACK_CreatePatchInletInletIfNeeded(Operator op, IInletRepository inletRepository, IIDRepository idRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
            {
                Inlet inlet = op.Inlets.FirstOrDefault();
                if (inlet == null)
                {
                    inlet = new Inlet();
                    inlet.ID = idRepository.GetID();
                    inlet.ListIndex = 0;
                    inletRepository.Insert(inlet);
                    inlet.LinkTo(op);
                }
                return inlet;
            }

            return null;
        }
    }
}
