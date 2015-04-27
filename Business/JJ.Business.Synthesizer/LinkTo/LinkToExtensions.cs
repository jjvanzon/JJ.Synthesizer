using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class LinkToExtensions
    {
        public static void LinkTo(this Inlet inlet, Operator op)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.Operator != null)
            {
                if (inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Remove(inlet);
                }
            }

            inlet.Operator = op;

            if (inlet.Operator != null)
            {
                if (!inlet.Operator.Inlets.Contains(inlet))
                {
                    inlet.Operator.Inlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Outlet outlet, Operator op)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (outlet.Operator != null)
            {
                if (outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Remove(outlet);
                }
            }

            outlet.Operator = op;

            if (outlet.Operator != null)
            {
                if (!outlet.Operator.Outlets.Contains(outlet))
                {
                    outlet.Operator.Outlets.Add(outlet);
                }
            }
        }

        public static void LinkTo(this Inlet inlet, Outlet outlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                if (inlet.InputOutlet.ConnectedInlets.Contains(inlet))
                {
                    inlet.InputOutlet.ConnectedInlets.Remove(inlet);
                }
            }

            inlet.InputOutlet = outlet;

            if (inlet.InputOutlet != null)
            {
                if (!inlet.InputOutlet.ConnectedInlets.Contains(inlet))
                {
                    inlet.InputOutlet.ConnectedInlets.Add(inlet);
                }
            }
        }

        public static void LinkTo(this Node node, Curve curve)
        {
            if (node == null) throw new NullException(() => node);

            if (node.Curve != null)
            {
                if (node.Curve.Nodes.Contains(node))
                {
                    node.Curve.Nodes.Remove(node);
                }
            }

            node.Curve = curve;

            if (node.Curve != null)
            {
                if (!node.Curve.Nodes.Contains(node))
                {
                    node.Curve.Nodes.Add(node);
                }
            }
        }

        public static void LinkTo(this Sample sample, SpeakerSetup speakerSetup)
        {
            sample.SpeakerSetup = speakerSetup;

            // No inverse property
        }

        public static void LinkTo(this AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup)
        {
            audioFileOutput.SpeakerSetup = speakerSetup;

            // No inverse property
        }

        public static void LinkTo(this AudioFileOutputChannel audioFileOutputChannel, AudioFileOutput audioFileOutput)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            if (audioFileOutputChannel.AudioFileOutput != null)
            {
                if (audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Remove(audioFileOutputChannel);
                }
            }

            audioFileOutputChannel.AudioFileOutput = audioFileOutput;

            if (audioFileOutputChannel.AudioFileOutput != null)
            {
                if (!audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.AudioFileOutput.AudioFileOutputChannels.Add(audioFileOutputChannel);
                }
            }
        }
        
        public static void LinkTo(this AudioFileOutputChannel audioFileOutputChannel, Outlet outlet)
        {
            if (audioFileOutputChannel == null) throw new NullException(() => audioFileOutputChannel);

            if (audioFileOutputChannel.Outlet != null)
            {
                if (audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Remove(audioFileOutputChannel);
                }
            }

            audioFileOutputChannel.Outlet = outlet;

            if (audioFileOutputChannel.Outlet != null)
            {
                if (!audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Contains(audioFileOutputChannel))
                {
                    audioFileOutputChannel.Outlet.AsAudioFileOutputChannels.Add(audioFileOutputChannel);
                }
            }
        }
        
        public static void LinkTo(this Operator op, Patch patch)
        {
            if (op == null) throw new NullException(() => op);

            if (op.Patch != null)
            {
                if (op.Patch.Operators.Contains(op))
                {
                    op.Patch.Operators.Remove(op);
                }
            }

            op.Patch = patch;

            if (op.Patch != null)
            {
                if (!op.Patch.Operators.Contains(op))
                {
                    op.Patch.Operators.Add(op);
                }
            }
        }

        public static void LinkTo(this Patch patch, Document document)
        {
            if (patch == null) throw new NullException(() => patch);

            if (patch.Document != null)
            {
                if (patch.Document.Patches.Contains(patch))
                {
                    patch.Document.Patches.Remove(patch);
                }
            }

            patch.Document = document;

            if (patch.Document != null)
            {
                if (!patch.Document.Patches.Contains(patch))
                {
                    patch.Document.Patches.Add(patch);
                }
            }
        }

        public static void LinkTo(this Curve curve, Document document)
        {
            if (curve == null) throw new NullException(() => curve);

            if (curve.Document != null)
            {
                if (curve.Document.Curves.Contains(curve))
                {
                    curve.Document.Curves.Remove(curve);
                }
            }

            curve.Document = document;

            if (curve.Document != null)
            {
                if (!curve.Document.Curves.Contains(curve))
                {
                    curve.Document.Curves.Add(curve);
                }
            }
        }

        public static void LinkTo(this Sample sample, Document document)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.Document != null)
            {
                if (sample.Document.Samples.Contains(sample))
                {
                    sample.Document.Samples.Remove(sample);
                }
            }

            sample.Document = document;

            if (sample.Document != null)
            {
                if (!sample.Document.Samples.Contains(sample))
                {
                    sample.Document.Samples.Add(sample);
                }
            }
        }

        public static void LinkTo(this AudioFileOutput audioFileOutput, Document document)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (audioFileOutput.Document != null)
            {
                if (audioFileOutput.Document.AudioFileOutputs.Contains(audioFileOutput))
                {
                    audioFileOutput.Document.AudioFileOutputs.Remove(audioFileOutput);
                }
            }

            audioFileOutput.Document = document;

            if (audioFileOutput.Document != null)
            {
                if (!audioFileOutput.Document.AudioFileOutputs.Contains(audioFileOutput))
                {
                    audioFileOutput.Document.AudioFileOutputs.Add(audioFileOutput);
                }
            }
        }

        public static void LinkInstrumentToDocument(this Document instrument, Document document)
        {
            if (instrument == null) throw new NullException(() => instrument);

            if (instrument.AsInstrumentInDocument != null)
            {
                if (instrument.AsInstrumentInDocument.Instruments.Contains(instrument))
                {
                    instrument.AsInstrumentInDocument.Instruments.Remove(instrument);
                }
            }

            instrument.AsInstrumentInDocument = document;

            if (instrument.AsInstrumentInDocument != null)
            {
                if (!instrument.AsInstrumentInDocument.Instruments.Contains(instrument))
                {
                    instrument.AsInstrumentInDocument.Instruments.Add(instrument);
                }
            }
        }

        public static void LinkEffectToDocument(this Document effect, Document document)
        {
            if (effect == null) throw new NullException(() => effect);

            if (effect.AsEffectInDocument != null)
            {
                if (effect.AsEffectInDocument.Effects.Contains(effect))
                {
                    effect.AsEffectInDocument.Effects.Remove(effect);
                }
            }

            effect.AsEffectInDocument = document;

            if (effect.AsEffectInDocument != null)
            {
                if (!effect.AsEffectInDocument.Effects.Contains(effect))
                {
                    effect.AsEffectInDocument.Effects.Add(effect);
                }
            }
        }

        public static void LinkToReferringDocument(this DocumentReference documentReference, Document document)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            if (documentReference.ReferringDocument != null)
            {
                if (documentReference.ReferringDocument.DocumentReferences.Contains(documentReference))
                {
                    documentReference.ReferringDocument.DocumentReferences.Remove(documentReference);
                }
            }

            documentReference.ReferringDocument = document;

            if (documentReference.ReferringDocument != null)
            {
                if (!documentReference.ReferringDocument.DocumentReferences.Contains(documentReference))
                {
                    documentReference.ReferringDocument.DocumentReferences.Add(documentReference);
                }
            }
        }

        public static void LinkToReferencedDocument(this DocumentReference documentReference, Document referencedDocument)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            documentReference.ReferencedDocument = referencedDocument;
            // No inverse property.
        }

        public static void LinkToMainPatch(this Document document, Patch mainPatch)
        {
            if (document == null) throw new NullException(() => document);

            document.MainPatch = mainPatch;
            // No inverse property.
        }
    }
}
