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
        /// <summary>
        /// Use the Patch property after calling this method.
        /// Do a rollback after calling this method to prevent saving the new patch.
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
            Patch.Name = "Auto-generated Patch";
            
            var customOperators = new List<Operator>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                CustomOperator_OperatorWrapper customOperatorWrapper = CustomOperator(underlyingPatch);
                customOperatorWrapper.Name = String.Format("Auto-generated CustomOperator '{0}'", underlyingPatch.Name);

                customOperators.Add(customOperatorWrapper);
            }

            var matchedOutlets = new List<Outlet>();
            var matchedInlets = new List<Inlet>();

            for (int i = 0; i < customOperators.Count; i++)
            {
                for (int j = i + 1; j < customOperators.Count; j++)
                {
                    Operator customOperator1 = customOperators[i];
                    Operator customOperator2 = customOperators[j];

                    foreach (Outlet outlet in customOperator1.Outlets)
                    {
                        foreach (Inlet inlet in customOperator2.Inlets)
                        {
                            if (AreMatch(outlet, inlet))
                            {
                                inlet.LinkTo(outlet);

                                matchedOutlets.Add(outlet);
                                matchedInlets.Add(inlet);
                            }
                        }
                    }
                }
            }

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IEnumerable<Inlet> unmatchedInlets = customOperators.SelectMany(x => x.Inlets)
                                                                .Except(matchedInlets);
            foreach (Inlet unmatchedInlet in unmatchedInlets)
            {
                PatchInlet_OperatorWrapper patchInletWrapper = PatchInlet();
                patchInletWrapper.ListIndex = unmatchedInlet.ListIndex;
                patchInletWrapper.InletTypeEnum = unmatchedInlet.GetInletTypeEnum();
                patchInletWrapper.DefaultValue = unmatchedInlet.DefaultValue;
                patchInletWrapper.Name = String.Format("Auto-generated PatchInlet '{0}'.", unmatchedInlet.Name);

                unmatchedInlet.LinkTo((Outlet)patchInletWrapper);
            }

            // Unmatched outlets of the custom operators become outlets of the new patch.
            IEnumerable<Outlet> unmatchedOutlets = customOperators.SelectMany(x => x.Outlets)
                                                                  .Except(matchedOutlets);
            foreach (Outlet unmatchedOutlet in unmatchedOutlets)
            {
                PatchOutlet_OperatorWrapper patchOutletWrapper = PatchOutlet();
                patchOutletWrapper.Name = unmatchedOutlet.Name;
                patchOutletWrapper.ListIndex = unmatchedOutlet.ListIndex;
                patchOutletWrapper.OutletTypeEnum = unmatchedOutlet.GetOutletTypeEnum();
                patchOutletWrapper.Name = String.Format("Auto-generated PatchOutlet '{0}'.", unmatchedOutlet.Name);

                patchOutletWrapper.Input = unmatchedOutlet;
            }

            // TODO: If there is overlap in type or name, they will merge to a single inlet or outlet.
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

        /// <summary> Will return null if no Frequency inlet or Signal outlet is found. </summary>
        public Outlet TryAutoPatch_WithTone(Tone tone, IList<Patch> underlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            // Create a new patch out of the other patches.
            AutoPatch(underlyingPatches);

            double frequency = tone.GetFrequency();
            Number_OperatorWrapper frequencyNumberOperatorWrapper = Number(frequency);

            IEnumerable<Inlet> frequencyInlets = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                      .Where(x => x.InletTypeEnum == InletTypeEnum.Frequency)
                                                      .Select(x => x.Inlet);

            foreach (Inlet frequencyInlet in frequencyInlets)
            {
                frequencyInlet.InputOutlet = frequencyNumberOperatorWrapper;
            }

            IEnumerable<Outlet> signalOutlets = Patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                                     .Where(x => x.OutletTypeEnum == OutletTypeEnum.Signal)
                                                     .Select(x => x.Result);

            // TODO: Add up the signals instead of taking the first one.
            Outlet outlet = signalOutlets.First();
            return outlet;
        }
    }
}
