﻿using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable UnusedMember.Global

// ReSharper disable RedundantCast

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class UnlinkExtensions
    {
        public static void UnlinkDocument(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.LinkTo((Document)null);
        }

        public static void UnlinkOutlet(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.LinkTo((Outlet)null);
        }

        public static void UnlinkAudioOutput(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            document.LinkTo(null);
        }

        public static void UnlinkHigherDocument(this DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            documentReference.LinkToHigherDocument(null);
        }

        public static void UnlinkLowerDocument(this DocumentReference lowerDocumentReference)
        {
            if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

            lowerDocumentReference.LinkToLowerDocument(null);
        }

        public static void UnlinkOperator(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.LinkTo((Operator)null);
        }

        public static void UnlinkInputOutlet(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.LinkTo((Outlet)null);
        }

        public static void UnlinkDocument(this MidiMappingGroup midiMappingGroup)
        {
            if (midiMappingGroup == null) throw new NullException(() => midiMappingGroup);

            midiMappingGroup.LinkTo(null);
        }

        public static void UnlinkEntityPosition(this MidiMapping midiMapping)
        {
            if (midiMapping == null) throw new NullException(() => midiMapping);

            midiMapping.LinkTo((EntityPosition)null);
        }

        public static void UnlinkMidiMappingGroup(this MidiMapping midiMapping)
        {
            if (midiMapping == null) throw new NullException(() => midiMapping);

            midiMapping.LinkTo((MidiMappingGroup)null);
        }

        public static void UnlinkMidiMappingType(this MidiMapping midiMapping)
        {
            if (midiMapping == null) throw new NullException(() => midiMapping);

            midiMapping.LinkTo((MidiMappingType)null);
        }

        public static void UnlinkCurve(this Node node)
        {
            if (node == null) throw new NullException(() => node);

            node.LinkTo((Curve)null);
        }

        public static void UnlinkPatch(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((Patch)null);
        }

        public static void UnlinkUnderlyingPatch(this Operator derivedOperator)
        {
            if (derivedOperator == null) throw new NullException(() => derivedOperator);

            derivedOperator.LinkToUnderlyingPatch(null);
        }

        public static void UnlinkCurve(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((Curve)null);
        }

        public static void UnlinkEntityPosition(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((EntityPosition)null);
        }

        public static void UnlinkSample(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((Sample)null);
        }

        public static void UnlinkOperator(this Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            outlet.LinkTo(null);
        }

        public static void UnlinkDocument(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            patch.LinkTo((Document)null);
        }

        public static void UnlinkDocument(this Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            scale.LinkTo((Document)null);
        }

        public static void UnlinkScale(this Tone tone)
        {
            if (tone == null) throw new NullException(() => tone);

            tone.LinkTo(null);
        }

        // Enum-Like Entities

        public static void UnlinkSpeakerSetup(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.LinkTo((SpeakerSetup)null);
        }

        public static void UnlinkAudioFileFormat(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.LinkTo((AudioFileFormat)null);
        }

        public static void UnlinkSampleDataType(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.LinkTo((SampleDataType)null);
        }

        public static void UnlinkSpeakerSetup(this AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            audioOutput.LinkTo(null);
        }

        public static void UnlinkDimension(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

            inletOrOutlet.LinkTo(null);
        }

        public static void UnlinkDimension(this MidiMapping midiMapping)
        {
            if (midiMapping == null) throw new NullException(() => midiMapping);

            midiMapping.LinkTo((Dimension)null);
        }

        public static void UnlinkInterpolationType(this Node node)
        {
            if (node == null) throw new NullException(() => node);

            node.LinkTo((InterpolationType)null);
        }

        public static void UnlinkStandardDimension(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            op.LinkTo((Dimension)null);
        }

        public static void UnlinkStandardDimension(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            patch.LinkTo((Dimension)null);
        }

        public static void UnlinkAudioFileFormat(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.LinkTo((AudioFileFormat)null);
        }

        public static void UnlinkInterpolationType(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.LinkTo((InterpolationType)null);
        }

        public static void UnlinkSampleDataType(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.LinkTo((SampleDataType)null);
        }

        public static void UnlinkSpeakerSetup(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.LinkTo((SpeakerSetup)null);
        }

        public static void UnlinkScaleType(this Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            scale.LinkTo((ScaleType)null);
        }
    }
}
