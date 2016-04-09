using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.LinkTo
{
    public static class LinkToExtensions
    {
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

        public static void LinkToParentDocument(this Document childDocument, Document parentDocument)
        {
            if (childDocument == null) throw new NullException(() => childDocument);

            if (childDocument.ParentDocument != null)
            {
                if (childDocument.ParentDocument.ChildDocuments.Contains(childDocument))
                {
                    childDocument.ParentDocument.ChildDocuments.Remove(childDocument);
                }
            }

            childDocument.ParentDocument = parentDocument;

            if (childDocument.ParentDocument != null)
            {
                if (!childDocument.ParentDocument.ChildDocuments.Contains(childDocument))
                {
                    childDocument.ParentDocument.ChildDocuments.Add(childDocument);
                }
            }
        }

        public static void LinkToDependentDocument(this DocumentReference documentReference, Document dependentDocument)
        {
            // DocumentReference -> DependentDocument
            // Document -> DependentDocuments

            if (documentReference == null) throw new NullException(() => documentReference);

            if (documentReference.DependentDocument != null)
            {
                if (documentReference.DependentDocument.DependentDocuments.Contains(documentReference))
                {
                    documentReference.DependentDocument.DependentDocuments.Remove(documentReference);
                }
            }

            documentReference.DependentDocument = dependentDocument;

            if (documentReference.DependentDocument != null)
            {
                if (!documentReference.DependentDocument.DependentDocuments.Contains(documentReference))
                {
                    documentReference.DependentDocument.DependentDocuments.Add(documentReference);
                }
            }
        }

        public static void LinkToDependentOnDocument(this DocumentReference documentReference, Document dependentOnDocument)
        {
            // DocumentReference -> DependentOnDocument
            // Document -> DependentOnDocuments

            if (documentReference == null) throw new NullException(() => documentReference);

            if (documentReference.DependentOnDocument != null)
            {
                if (documentReference.DependentOnDocument.DependentOnDocuments.Contains(documentReference))
                {
                    documentReference.DependentOnDocument.DependentOnDocuments.Remove(documentReference);
                }
            }

            documentReference.DependentOnDocument = dependentOnDocument;

            if (documentReference.DependentOnDocument != null)
            {
                if (!documentReference.DependentOnDocument.DependentOnDocuments.Contains(documentReference))
                {
                    documentReference.DependentOnDocument.DependentOnDocuments.Add(documentReference);
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

        public static void LinkTo(this Scale sample, Document document)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.Document != null)
            {
                if (sample.Document.Scales.Contains(sample))
                {
                    sample.Document.Scales.Remove(sample);
                }
            }

            sample.Document = document;

            if (sample.Document != null)
            {
                if (!sample.Document.Scales.Contains(sample))
                {
                    sample.Document.Scales.Add(sample);
                }
            }
        }

        public static void LinkTo(this Tone tone, Scale scale)
        {
            if (tone == null) throw new NullException(() => tone);

            if (tone.Scale != null)
            {
                if (tone.Scale.Tones.Contains(tone))
                {
                    tone.Scale.Tones.Remove(tone);
                }
            }

            tone.Scale = scale;

            if (tone.Scale != null)
            {
                if (!tone.Scale.Tones.Contains(tone))
                {
                    tone.Scale.Tones.Add(tone);
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

        // Enum-Like Entities

        public static void LinkTo(this AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.SpeakerSetup = speakerSetup;

            // No inverse property.
        }

        public static void LinkTo(this AudioFileOutput audioFileOutput, SampleDataType sampleDataType)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.SampleDataType = sampleDataType;

            // No inverse property.
        }

        public static void LinkTo(this AudioFileOutput audioFileOutput, AudioFileFormat audioFileFormat)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            audioFileOutput.AudioFileFormat = audioFileFormat;

            // No inverse property.
        }

        public static void LinkTo(this Inlet inlet, Dimension dimension)
        {
            if (inlet == null) throw new NullException(() => inlet);

            inlet.Dimension = dimension;

            // No inverse property.
        }

        public static void LinkTo(this Node node, NodeType nodeType)
        {
            if (node == null) throw new NullException(() => node);

            node.NodeType = nodeType;

            // No inverse property.
        }

        public static void LinkTo(this Operator op, OperatorType operatorType)
        {
            if (op == null) throw new NullException(() => op);

            op.OperatorType = operatorType;

            // No inverse property.
        }

        public static void LinkTo(this Outlet outlet, OutletType outletType)
        {
            if (outlet == null) throw new NullException(() => outlet);

            outlet.OutletType = outletType;

            // No inverse property.
        }

        public static void LinkTo(this Sample sample, SpeakerSetup speakerSetup)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.SpeakerSetup = speakerSetup;

            // No inverse property.
        }

        public static void LinkTo(this Sample sample, InterpolationType interpolationType)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.InterpolationType = interpolationType;

            // No inverse property.
        }

        public static void LinkTo(this Sample sample, SampleDataType sampleDataType)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.SampleDataType = sampleDataType;

            // No inverse property.
        }

        public static void LinkTo(this Sample sample, AudioFileFormat audioFileFormat)
        {
            if (sample == null) throw new NullException(() => sample);

            sample.AudioFileFormat = audioFileFormat;

            // No inverse property.
        }

        public static void LinkTo(this Scale scale, ScaleType scaleType)
        {
            if (scale == null) throw new NullException(() => scale);

            scale.ScaleType = scaleType;

            // No inverse property.
        }
    }
}
