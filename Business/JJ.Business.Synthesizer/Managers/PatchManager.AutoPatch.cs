using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        private class AutoPatchTuple
        {
            public CustomOperator_OperatorWrapper CustomOperatorWrapper { get; set; }
            public Patch UnderlyingPatch { get; set; }
            /// <summary> nullable </summary>
            public PatchInlet_OperatorWrapper UnderlyingPatchInletWrapper { get; set; }
            /// <summary> nullable </summary>
            public Inlet CustomOperatorInlet { get; set; }
            /// <summary> nullable </summary>
            public PatchOutlet_OperatorWrapper UnderlyingPatchOutletWrapper { get; set; }
            /// <summary> nullable </summary>
            public Outlet CustomOperatorOutlet { get; set; }
        }

        /// <summary>
        /// Do a rollback after calling this method to prevent saving the new patch.
        /// Use the Patch property after calling this method.
        /// Tries to produce a new patch by tying together existing patches,
        /// trying to match PatchInlet and PatchOutlet operators by:
        /// 1) InletType.Name and OutletType.Name
        /// 2) PatchInlet Operator.Name and PatchOutlet Operator.Name.
        /// The non-matched inlets and outlets will become inlets and outlets of the new patch.
        /// If there is overlap in type or name, they will merge to a single inlet or outlet.
        /// </summary>
        public void AutoPatch(Document document, IList<Patch> underlyingPatches)
        {
            if (document == null) throw new NullException(() => document);
            AutoPatch(underlyingPatches);
            Patch.LinkTo(document);
        }

        /// <summary>
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
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            CreatePatch();

            IList<AutoPatchTuple> tuples = GetAutoPatchTuples(underlyingPatches);

            var matchedInletTuples = new List<AutoPatchTuple>(tuples.Count);
            var matchedOutletTuples = new List<AutoPatchTuple>(tuples.Count);

            for (int i = 0; i < tuples.Count; i++)
            {
                for (int j = i + 1; j < tuples.Count; j++)
                {
                    AutoPatchTuple outletTuple = tuples[i];
                    AutoPatchTuple inletTuple = tuples[j];

                    if (AutoPatchTuplesAreMatch(outletTuple, inletTuple))
                    {
                        inletTuple.CustomOperatorInlet.InputOutlet = outletTuple.CustomOperatorOutlet;

                        matchedInletTuples.Add(inletTuple);
                        matchedOutletTuples.Add(outletTuple);
                    }
                }
            }

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IEnumerable<AutoPatchTuple> unmatchedInletTuples = tuples.Where(x => x.CustomOperatorInlet != null)
                                                                     .Except(matchedInletTuples);

            foreach (AutoPatchTuple unmatchedInletTuple in unmatchedInletTuples)
            {
                var patchInlet = PatchInlet();
                patchInlet.DefaultValue = unmatchedInletTuple.UnderlyingPatchInletWrapper.DefaultValue;
                patchInlet.InletTypeEnum = unmatchedInletTuple.UnderlyingPatchInletWrapper.InletTypeEnum;
                patchInlet.ListIndex = unmatchedInletTuple.UnderlyingPatchInletWrapper.ListIndex;
                patchInlet.Name = unmatchedInletTuple.UnderlyingPatchInletWrapper.Name;

                unmatchedInletTuple.CustomOperatorInlet.InputOutlet = patchInlet;
            }

            // Unmatched outlets of the custom operators become outlets of the new patch.
            IEnumerable<AutoPatchTuple> unmatchedOutletTuples = tuples.Where(x => x.CustomOperatorOutlet != null)
                                                                      .Except(matchedOutletTuples);
            foreach (AutoPatchTuple unmatchedOutletTuple in unmatchedOutletTuples)
            {
                var patchOutlet = PatchOutlet();
                patchOutlet.Name = unmatchedOutletTuple.UnderlyingPatchOutletWrapper.Name;
                patchOutlet.OutletTypeEnum = unmatchedOutletTuple.UnderlyingPatchOutletWrapper.OutletTypeEnum;
                patchOutlet.ListIndex = unmatchedOutletTuple.UnderlyingPatchOutletWrapper.ListIndex;

                patchOutlet.Input = unmatchedOutletTuple.CustomOperatorOutlet;
            }

            // TODO: If there is overlap in type or name, they will merge to a single inlet or outlet.
        }

        private IList<AutoPatchTuple> GetAutoPatchTuples(IList<Patch> underlyingPatches)
        {
            var tuples = new List<AutoPatchTuple>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                var customOperatorWrapper = CustomOperator(underlyingPatch);

                // Inlets
                {
                    var joined = from customOperatorInlet in customOperatorWrapper.Inlets
                                 join underlyingPatchInlet in underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                 on customOperatorInlet.Name equals underlyingPatchInlet.Name
                                 select new { CustomOperatorInlet = customOperatorInlet, UnderlyingPatchInlet = underlyingPatchInlet };

                    foreach (var joinItem in joined)
                    {
                        var underlyingPatchInletWrapper = new PatchInlet_OperatorWrapper(joinItem.UnderlyingPatchInlet);

                        var tuple = new AutoPatchTuple
                        {
                            UnderlyingPatch = underlyingPatch,
                            CustomOperatorWrapper = customOperatorWrapper,
                            UnderlyingPatchInletWrapper = underlyingPatchInletWrapper,
                            CustomOperatorInlet = joinItem.CustomOperatorInlet
                        };

                        tuples.Add(tuple);
                    }
                }


                // Outlets
                {
                    var joined = from customOperatorOutlet in customOperatorWrapper.Outlets
                                 join underlyingPatchOutlet in underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                 on customOperatorOutlet.Name equals underlyingPatchOutlet.Name
                                 select new { CustomOperatorOutlet = customOperatorOutlet, UnderlyingPatchOutlet = underlyingPatchOutlet };

                    foreach (var joinItem in joined)
                    {
                        var underlyingPatchOutletWrapper = new PatchOutlet_OperatorWrapper(joinItem.UnderlyingPatchOutlet);

                        var tuple = new AutoPatchTuple
                        {
                            UnderlyingPatch = underlyingPatch,
                            CustomOperatorWrapper = customOperatorWrapper,
                            UnderlyingPatchOutletWrapper = underlyingPatchOutletWrapper,
                            CustomOperatorOutlet = joinItem.CustomOperatorOutlet
                        };

                        tuples.Add(tuple);
                    }
                }
            }

            return tuples;
        }

        private bool AutoPatchTuplesAreMatch(AutoPatchTuple outletTuple, AutoPatchTuple inletTuple)
        {
            if (outletTuple.CustomOperatorOutlet == null)
            {
                return false;
            }

            if (inletTuple.CustomOperatorInlet == null)
            {
                return false;
            }

            // First match by OutletType / InletType.
            OutletTypeEnum outletTypeEnum = outletTuple.UnderlyingPatchOutletWrapper.OutletTypeEnum ?? OutletTypeEnum.Undefined;
            if (outletTypeEnum != OutletTypeEnum.Undefined)
            {
                InletTypeEnum inletTypeEnum = inletTuple.UnderlyingPatchInletWrapper.InletTypeEnum ?? InletTypeEnum.Undefined;
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
            if (String.Equals(outletTuple.CustomOperatorOutlet.Name, inletTuple.CustomOperatorInlet.Name))
            {
                return true;
            }

            // I doubt this will lead to the desired result:
            // Then match by list index
            //if (outletTuple.CustomOperatorOutlet.ListIndex == inletTuple.CustomOperatorInlet.ListIndex)
            //{
            //    return true;
            //}

            return false;
        }
    }
}
