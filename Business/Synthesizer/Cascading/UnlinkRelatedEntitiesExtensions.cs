﻿using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Cascading
{
    /// <summary> Unlinks related entities that are not inherently part of the entity. </summary>
    public static class UnlinkRelatedEntitiesExtensions
    {
        public static void UnlinkRelatedEntities(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.UnlinkAudioFileFormat();
            audioFileOutput.UnlinkSampleDataType();
            audioFileOutput.UnlinkSpeakerSetup();
            audioFileOutput.UnlinkDocument();
            audioFileOutput.UnlinkOutlet();
        }

        public static void UnlinkRelatedEntities(this AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            audioOutput.UnlinkSpeakerSetup();
        }

        public static void UnlinkRelatedEntities(this DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            documentReference.UnlinkLowerDocument();

            // NOTE: Do not unlink DependentDocument: those should either be deleted first, or things should crash on reference constraint violations.
        }

        public static void UnlinkRelatedEntities(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.UnlinkDimension();
            inlet.UnlinkInputOutlet();
            inlet.UnlinkOperator();
        }

        public static void UnlinkRelatedEntities(this MidiMappingGroup midiMappingGroup)
        {
            if (midiMappingGroup == null) throw new NullException(() => midiMappingGroup);

            midiMappingGroup.UnlinkDocument();
        }

        public static void UnlinkRelatedEntities(this MidiMapping midiMapping)
        {
            if (midiMapping == null) throw new NullException(() => midiMapping);

            midiMapping.UnlinkMidiMappingGroup();
            midiMapping.UnlinkMidiMappingType();
            midiMapping.UnlinkDimension();
        }

        public static void UnlinkRelatedEntities(this Node node)
        {
            if (node == null) throw new NullException(() => node);

            node.UnlinkInterpolationType();
            node.UnlinkCurve();
        }

        public static void UnlinkRelatedEntities(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.UnlinkPatch();
            op.UnlinkUnderlyingPatch();
            op.UnlinkStandardDimension();
        }

        public static void UnlinkRelatedEntities(this Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            foreach (Inlet connectedInlet in outlet.ConnectedInlets.ToArray())
            {
                connectedInlet.UnlinkInputOutlet();
            }

            outlet.UnlinkDimension();
            outlet.UnlinkOperator();
        }

        public static void UnlinkRelatedEntities(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            patch.UnlinkDocument();
            patch.UnlinkStandardDimension();
        }

        public static void UnlinkRelatedEntities(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.UnlinkAudioFileFormat();
            sample.UnlinkInterpolationType();
            sample.UnlinkSampleDataType();
            sample.UnlinkSpeakerSetup();
        }

        public static void UnlinkRelatedEntities(this Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            scale.UnlinkDocument();
            scale.UnlinkScaleType();
        }
    }
}
