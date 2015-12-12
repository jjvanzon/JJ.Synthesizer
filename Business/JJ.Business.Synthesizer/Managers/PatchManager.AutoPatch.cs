using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        private class AutoPatchTuple
        {
            /// <summary> nullable </summary>
            public Inlet Inlet { get; set; }
            /// <summary> nullable </summary>
            public Outlet Outlet { get; set; }
        }

        /// <summary> Will return null if no Frequency inlet or Signal outlet is found. </summary>
        public Outlet TryAutoPatch_WithTone(Tone tone, IList<Patch> underlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            // Create a new patch out of the other patches.
            CustomOperator_OperatorWrapper tempCustomOperator = AutoPatch_ToCustomOperator(underlyingPatches);

            // TODO: InletTypes and OutletTypes do not have to be unique and in that case this method crashes.
            Inlet inlet = OperatorHelper.TryGetInlet(tempCustomOperator, InletTypeEnum.Frequency);
            if (inlet != null)
            {
                double frequency = tone.GetFrequency();
                inlet.InputOutlet = Number(frequency);

                Outlet outlet = OperatorHelper.TryGetOutlet(tempCustomOperator, OutletTypeEnum.Signal);
                return outlet;
            }

            return null;
        }

        private CustomOperator_OperatorWrapper AutoPatch_ToCustomOperator(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            AutoPatch(underlyingPatches);
            Patch tempUnderlyingPatch = Patch;

            // Use new patch as custom operator.
            CreatePatch();
            var customOperator = CustomOperator(tempUnderlyingPatch);

            return customOperator;
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

                    if (AreMatch(outletTuple.Outlet, inletTuple.Inlet))
                    {
                        inletTuple.Inlet.InputOutlet = outletTuple.Outlet;

                        matchedInletTuples.Add(inletTuple);
                        matchedOutletTuples.Add(outletTuple);
                    }
                }
            }

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IEnumerable<AutoPatchTuple> unmatchedInletTuples = tuples.Where(x => x.Inlet != null)
                                                                     .Except(matchedInletTuples);

            foreach (AutoPatchTuple unmatchedInletTuple in unmatchedInletTuples)
            {
                var patchInlet = PatchInlet();
                patchInlet.Name = unmatchedInletTuple.Inlet.Name;
                patchInlet.ListIndex = unmatchedInletTuple.Inlet.ListIndex;
                patchInlet.InletTypeEnum = unmatchedInletTuple.Inlet.GetInletTypeEnum();
                patchInlet.DefaultValue = unmatchedInletTuple.Inlet.DefaultValue;

                unmatchedInletTuple.Inlet.InputOutlet = patchInlet;
            }

            // Unmatched outlets of the custom operators become outlets of the new patch.
            IEnumerable<AutoPatchTuple> unmatchedOutletTuples = tuples.Where(x => x.Outlet != null)
                                                                      .Except(matchedOutletTuples);
            foreach (AutoPatchTuple unmatchedOutletTuple in unmatchedOutletTuples)
            {
                var patchOutlet = PatchOutlet();
                patchOutlet.Name = unmatchedOutletTuple.Outlet.Name;
                patchOutlet.ListIndex = unmatchedOutletTuple.Outlet.ListIndex;
                patchOutlet.OutletTypeEnum = unmatchedOutletTuple.Outlet.GetOutletTypeEnum();

                patchOutlet.Input = unmatchedOutletTuple.Outlet;
            }

            // TODO: If there is overlap in type or name, they will merge to a single inlet or outlet.
        }

        private IList<AutoPatchTuple> GetAutoPatchTuples(IList<Patch> underlyingPatches)
        {
            var tuples = new List<AutoPatchTuple>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                var customOperatorWrapper = CustomOperator(underlyingPatch);

                foreach (Inlet inlet in customOperatorWrapper.Inlets)
                {
                    var tuple = new AutoPatchTuple
                    {
                        Inlet = inlet
                    };

                    tuples.Add(tuple);
                }

                foreach (Outlet outlet in customOperatorWrapper.Outlets)
                {
                    var tuple = new AutoPatchTuple
                    {
                        Outlet = outlet
                    };

                    tuples.Add(tuple);
                }
            }

            return tuples;
        }

        private bool AreMatch(Outlet outlet, Inlet inlet)
        {
            if (outlet == null)
            {
                return false;
            }

            if (inlet == null)
            {
                return false;
            }

            // First match by OutletType / InletType.
            OutletTypeEnum outletTypeEnum = outlet.GetOutletTypeEnum();
            if (outletTypeEnum != OutletTypeEnum.Undefined)
            {
                InletTypeEnum inletTypeEnum = inlet.GetInletTypeEnum();
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
            if (String.Equals(outlet.Name, inlet.Name))
            {
                return true;
            }

            // Do not match by list index, because that would result in something arbitrary.

            return false;
        }
    }
}
