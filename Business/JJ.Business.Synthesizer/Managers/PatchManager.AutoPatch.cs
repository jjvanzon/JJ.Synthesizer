using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        private class AutoPatchTuple
        {
            public OperatorWrapper_CustomOperator CustomOperatorWrapper { get; set; }
            public Patch UnderlyingPatch { get; set; }
            ///// <summary> nullable </summary>
            //public Operator PatchInlet { get; set; }
            /// <summary> nullable </summary>
            public OperatorWrapper_PatchInlet PatchInletWrapper { get; set; }
            /// <summary> nullable </summary>
            public Inlet Inlet { get; set; }
            ///// <summary> nullable </summary>
            //public Operator PatchOutlet { get; set; }
            /// <summary> nullable </summary>
            public OperatorWrapper_PatchOutlet PatchOutletWrapper { get; set; }
            /// <summary> nullable </summary>
            public Outlet Outlet { get; set; }
        }

        public void AutoPatch_New(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            IList<AutoPatchTuple> tuples = GetAutoPatchTuples(underlyingPatches);

            for (int i = 0; i < tuples.Count; i++)
            {
                for (int j = i + 1; j < tuples.Count; j++)
                {
                    AutoPatchTuple outletTuple = tuples[i];
                    AutoPatchTuple inletTuple = tuples[j];

                    if (TuplesAreMatch(outletTuple, inletTuple))
                    {
                        // TODO: Tie together.
                        throw new NotImplementedException();
                    }
                }
            }

            throw new NotImplementedException();
        }

        private bool TuplesAreMatch(AutoPatchTuple outletTuple, AutoPatchTuple inletTuple)
        {
            if (outletTuple.Outlet == null)
            {
                return false;
            }

            if (inletTuple.Inlet == null)
            {
                return false;
            }

            // First match by OutletType / InletType.
            OutletTypeEnum outletTypeEnum = outletTuple.PatchOutletWrapper.OutletTypeEnum ?? OutletTypeEnum.Undefined;
            if (outletTypeEnum != OutletTypeEnum.Undefined)
            {
                InletTypeEnum inletTypeEnum = inletTuple.PatchInletWrapper.InletTypeEnum ?? InletTypeEnum.Undefined;
                if (inletTypeEnum != InletTypeEnum.Undefined)
                {
                    string outletTypeString = outletTypeEnum.ToString();
                    string inletTypeString = inletTypeEnum.ToString();

                    if (String.Equals(outletTypeString, inletTypeString))
                    {
                        return true;
                    }
                }
            }

            // Then match by name
            if (String.Equals(outletTuple.Outlet.Name, inletTuple.Inlet.Name))
            {
                return true;
            }

            // Then match by list index
            // TODO: Do later;
            throw new NotImplementedException();

            return false;
        }

        private IList<AutoPatchTuple> GetAutoPatchTuples(IList<Patch> underlyingPatches)
        {
            var tuples = new List<AutoPatchTuple>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                var customOperatorWrapper = CustomOperator(underlyingPatch);

                {
                    var joined = from inlet in customOperatorWrapper.Inlets
                                 join patchInlet in underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                 on inlet.Name equals patchInlet.Name
                                 select new { Inlet = inlet, PatchInlet = patchInlet };

                    foreach (var joinItem in joined)
                    {
                        var patchInletWrapper = new OperatorWrapper_PatchInlet(joinItem.PatchInlet);

                        var tuple = new AutoPatchTuple
                        {
                            UnderlyingPatch = underlyingPatch,
                            CustomOperatorWrapper = customOperatorWrapper,
                            PatchInletWrapper = patchInletWrapper,
                            Inlet = joinItem.Inlet
                        };

                        tuples.Add(tuple);
                    }
                }

                {
                    var joined = from outlet in customOperatorWrapper.Outlets
                                 join patchOutlet in underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                 on outlet.Name equals patchOutlet.Name
                                 select new { Outlet = outlet, PatchOutlet = patchOutlet };

                    foreach (var joinItem in joined)
                    {
                        var patchOutletWrapper = new OperatorWrapper_PatchOutlet(joinItem.PatchOutlet);

                        var tuple = new AutoPatchTuple
                        {
                            UnderlyingPatch = underlyingPatch,
                            CustomOperatorWrapper = customOperatorWrapper,
                            PatchOutletWrapper = patchOutletWrapper,
                            Outlet = joinItem.Outlet
                        };

                        tuples.Add(tuple);
                    }
                }
            }

            return tuples;
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
        public void AutoPatch(IList<Patch> underlyingPatches)
        {
            throw new NotImplementedException();

            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            CreatePatch();

            Patch previousUnderlyingPatch = null;
            OperatorWrapper_CustomOperator previousCustomOperatorWrapper = null;

            foreach (Patch nextUnderlyingPatch in underlyingPatches)
            {
                OperatorWrapper_CustomOperator nextCustomOperatorWrapper = CustomOperator(nextUnderlyingPatch);

                if (previousUnderlyingPatch != null)
                {
                    IList<OperatorWrapper_PatchOutlet> patchOutletWrappers = previousUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                                                                    .Select(x => new OperatorWrapper_PatchOutlet(x))
                                                                                                    .OrderBy(x => x.ListIndex)
                                                                                                    .ToArray();

                    IList<OperatorWrapper_PatchInlet> patchInletWrappers = nextUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
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

                previousUnderlyingPatch = nextUnderlyingPatch;
                previousCustomOperatorWrapper = nextCustomOperatorWrapper;
            }

            throw new NotImplementedException();
        }
        #endregion
    }
}
