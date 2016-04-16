using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System.Linq;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Business.Synthesizer.Extensions
{
    /// <summary> Unlinks related entities that are not inherently part of the entity. </summary>
    internal static class UnlinkRelatedEntitiesExtensions
    {
        public static void UnlinkRelatedEntities(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.UnlinkAudioFileFormat();
            audioFileOutput.UnlinkSampleDataType();
            audioFileOutput.UnlinkSpeakerSetup();
            audioFileOutput.UnlinkDocument();
        }

        public static void UnlinkRelatedEntities(this AudioFileOutputChannel audioFileOutputChannel)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            audioFileOutputChannel.UnlinkAudioFileOutput();
            audioFileOutputChannel.UnlinkOutlet();
        }

        public static void UnlinkRelatedEntities(this AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            audioOutput.UnlinkSpeakerSetup();
        }

        public static void UnlinkRelatedEntities(this Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            curve.UnlinkDocument();
        }

        public static void UnlinkRelatedEntities(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.UnlinkDimension();
            inlet.UnlinkOutlet();
            inlet.UnlinkOperator();
        }

        public static void UnlinkRelatedEntities(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            document.UnlinkParentDocument();
        }

        public static void UnlinkRelatedEntities(this DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            documentReference.UnlinkDependentOnDocument();

            // NOTE: Do not unlink DependentDocument: those should either be deleted first, or things should crash on reference constraint violations.
        }

        public static void UnlinkRelatedEntities(this Node node)
        {
            if (node == null) throw new NullException(() => node);

            node.UnlinkNodeType();
            node.UnlinkCurve();
        }

        public static void UnlinkRelatedEntities(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.UnlinkPatch();
            op.UnlinkOperatorType();
        }

        public static void UnlinkRelatedEntities(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            patch.UnlinkDocument();
        }

        public static void UnlinkRelatedEntities(this Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            foreach (Inlet connectedInlet in outlet.ConnectedInlets.ToArray())
            {
                connectedInlet.UnlinkOutlet();
            }

            outlet.UnlinkDimension();
            outlet.UnlinkOperator();
        }

        public static void UnlinkRelatedEntities(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.UnlinkAudioFileFormat();
            sample.UnlinkInterpolationType();
            sample.UnlinkSampleDataType();
            sample.UnlinkSpeakerSetup();
            sample.UnlinkDocument();
        }

        public static void UnlinkRelatedEntities(this Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            scale.UnlinkDocument();
            scale.UnlinkScaleType();
        }
    }
}
