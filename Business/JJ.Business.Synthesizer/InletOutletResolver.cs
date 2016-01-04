using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer
{
    internal static class InletOutletResolver
    {
        // TODO: Put any matching between inlets and outlets of custom operators or underlying patches in this class.

        /// <summary> Currently used by PatchToOperatorConverter (2016-01-04). </summary>
        public static Inlet TryGetCustomOperatorInlet(IList<Inlet> destCustomOperatorInlets, Operator sourcePatchInlet)
        {
            if (destCustomOperatorInlets == null) throw new NullException(() => destCustomOperatorInlets);
            if (sourcePatchInlet == null) throw new NullException(() => sourcePatchInlet);

            // Try match by name
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                if (String.Equals(destCustomOperatorInlet.Name, sourcePatchInlet.Name))
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by type
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                // TODO: I should really only match if it is unique.
                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destCustomOperatorInlet.GetInletTypeEnum() == sourcePatchInletWrapper.Inlet.GetInletTypeEnum())
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by list index
            foreach (Inlet destInlet in destCustomOperatorInlets)
            {
                var wrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destInlet.ListIndex == wrapper.ListIndex)
                {
                    return destInlet;
                }
            }

            return null;
        }

        /// <summary> Currently used by PatchToOperatorConverter (2016-01-04). </summary>
        public static Outlet TryGetCustomOperatorOutlet(IList<Outlet> destCustomOperatorOutlets, Operator sourcePatchOutlet)
        {
            if (destCustomOperatorOutlets == null) throw new NullException(() => destCustomOperatorOutlets);
            if (sourcePatchOutlet == null) throw new NullException(() => sourcePatchOutlet);

            // Try match by name
            foreach (Outlet destCustomOperatorOutlet in destCustomOperatorOutlets)
            {
                if (String.Equals(destCustomOperatorOutlet.Name, sourcePatchOutlet.Name))
                {
                    return destCustomOperatorOutlet;
                }
            }

            // Try match by type
            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                // TODO: I should really only match if it is unique.
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.GetOutletTypeEnum() == wrapper.Result.GetOutletTypeEnum())
                {
                    return destOutlet;
                }
            }

            // Try match by list index
            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.ListIndex == wrapper.ListIndex)
                {
                    return destOutlet;
                }
            }

            return null;
        }

        /// <summary> Currently used by PatchManager.AutoPatch (2016-01-04). </summary>
        public static bool AreMatch(Outlet outlet, Inlet inlet)
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
