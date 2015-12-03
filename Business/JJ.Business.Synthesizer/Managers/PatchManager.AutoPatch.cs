using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        private class AutoPatchTuple
        {
            public Patch UnderlyingPatch { get; set; }

        }

        public void AutoPatch_New(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            var tuples = new List<AutoPatchTuple>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                var tuple = new AutoPatchTuple();

                var customOperator = CustomOperator(

                tuples.Add(tuple);
            }
        }

        #region Old
        /// <summary>
        /// NOT FINISHED.
        /// Do a rollback after calling this method to prevent saving the new patch.
        /// Use the Patch property after calling this method.
        /// Tries to produce a new patch by tying together existing patches,
        /// trying to match PatchInlet and PatchOutlet operators by:
        /// 1) InletType.Name and OutletType.Name
        /// 2) PatchInlet Operator.Name and PatchOutlet Operator.Name.
        /// The non-matched inlets and outlets will become inlets and outlets of the new patch.
        /// If there is overlap in type or name, they will merge to a single inlet or outlet.
        /// </summary>
        public void AutoPatch(IList<Document> underlyingDocuments)
        {
            throw new NotImplementedException();

            if (underlyingDocuments == null) throw new NullException(() => underlyingDocuments);

            CreatePatch();

            Document previousUnderlyingDocument = null;
            OperatorWrapper_CustomOperator previousCustomOperatorWrapper = null;

            foreach (Document nextUnderlyingDocument in underlyingDocuments)
            {
                OperatorWrapper_CustomOperator nextCustomOperatorWrapper = CustomOperator(nextUnderlyingDocument);

                if (previousUnderlyingDocument != null)
                {
                    // TODO: Should you allow this?
                    if (previousUnderlyingDocument.Patches.Count == 1 &&
                        nextUnderlyingDocument.Patches.Count == 1)
                    {
                        IList<OperatorWrapper_PatchOutlet> patchOutletWrappers = previousUnderlyingDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                                                                                      .Select(x => new OperatorWrapper_PatchOutlet(x))
                                                                                                                      .OrderBy(x => x.ListIndex)
                                                                                                                      .ToArray();

                        IList<OperatorWrapper_PatchInlet> patchInletWrappers = nextUnderlyingDocument.Patches[0].GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                                                                                                .Select(x => new OperatorWrapper_PatchInlet(x))
                                                                                                                .OrderBy(x => x.ListIndex)
                                                                                                                .ToArray();
                        for (int outletIndex = 0; outletIndex < patchOutletWrappers.Count; outletIndex++)
                        {
                            OperatorWrapper_PatchOutlet patchOutletWrapper = patchOutletWrappers[outletIndex];
                            OutletTypeEnum outletTypeEnum = patchOutletWrapper.OutletTypeEnum ?? OutletTypeEnum.Undefined;
                            string outletTypeEnumString = outletTypeEnum.ToString();

                            if (outletTypeEnum != OutletTypeEnum.Undefined)
                            {
                                for (int inletIndex = 0; inletIndex < patchInletWrappers.Count; inletIndex++)
                                {
                                    OperatorWrapper_PatchInlet patchInletWrapper = patchInletWrappers[inletIndex];
                                    InletTypeEnum inletTypeEnum = patchInletWrapper.InletTypeEnum ?? InletTypeEnum.Undefined;
                                    string inletTypeEnumString = inletTypeEnum.ToString();

                                    bool isMatch = String.Equals(outletTypeEnumString, inletTypeEnumString) ||
                                                   String.Equals(patchOutletWrapper.Name, patchInletWrapper.Name);

                                    if (isMatch)
                                    {
                                        nextCustomOperatorWrapper.Inlets[inletIndex].InputOutlet = previousCustomOperatorWrapper.Outlets[outletIndex];
                                    }
                                }
                            }
                        }
                    }
                }

                previousUnderlyingDocument = nextUnderlyingDocument;
                previousCustomOperatorWrapper = nextCustomOperatorWrapper;
            }

            throw new NotImplementedException();
        }
        #endregion
    }
}
