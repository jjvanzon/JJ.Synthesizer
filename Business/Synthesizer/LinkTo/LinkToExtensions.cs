using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable InvertIf

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

		public static void LinkTo(this AudioFileOutput audioFileOutput, Outlet outlet)
		{
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

			if (audioFileOutput.Outlet != null)
			{
				if (audioFileOutput.Outlet.InAudioFileOutputs.Contains(audioFileOutput))
				{
					audioFileOutput.Outlet.InAudioFileOutputs.Remove(audioFileOutput);
				}
			}

			audioFileOutput.Outlet = outlet;

			if (audioFileOutput.Outlet != null)
			{
				if (!audioFileOutput.Outlet.InAudioFileOutputs.Contains(audioFileOutput))
				{
					audioFileOutput.Outlet.InAudioFileOutputs.Add(audioFileOutput);
				}
			}
		}

		public static void LinkTo(this Document document, AudioOutput audioOutput)
		{
			if (document == null) throw new NullException(() => document);
			document.AudioOutput = audioOutput ?? throw new NullException(() => audioOutput);

			// No inverse property.
		}

		public static void LinkToHigherDocument(this DocumentReference documentReference, Document higherDocument)
		{
			if (documentReference == null) throw new NullException(() => documentReference);

			if (documentReference.HigherDocument != null)
			{
				if (documentReference.HigherDocument.LowerDocumentReferences.Contains(documentReference))
				{
					documentReference.HigherDocument.LowerDocumentReferences.Remove(documentReference);
				}
			}

			documentReference.HigherDocument = higherDocument;

			if (documentReference.HigherDocument != null)
			{
				if (!documentReference.HigherDocument.LowerDocumentReferences.Contains(documentReference))
				{
					documentReference.HigherDocument.LowerDocumentReferences.Add(documentReference);
				}
			}
		}

		public static void LinkToLowerDocument(this DocumentReference documentReference, Document lowerDocument)
		{
			if (documentReference == null) throw new NullException(() => documentReference);

			if (documentReference.LowerDocument != null)
			{
				if (documentReference.LowerDocument.HigherDocumentReferences.Contains(documentReference))
				{
					documentReference.LowerDocument.HigherDocumentReferences.Remove(documentReference);
				}
			}

			documentReference.LowerDocument = lowerDocument;

			if (documentReference.LowerDocument != null)
			{
				if (!documentReference.LowerDocument.HigherDocumentReferences.Contains(documentReference))
				{
					documentReference.LowerDocument.HigherDocumentReferences.Add(documentReference);
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

		public static void LinkTo(this Inlet inlet, Outlet inputOutlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			if (inlet.InputOutlet != null)
			{
				if (inlet.InputOutlet.ConnectedInlets.Contains(inlet))
				{
					inlet.InputOutlet.ConnectedInlets.Remove(inlet);
				}
			}

			inlet.InputOutlet = inputOutlet;

			if (inlet.InputOutlet != null)
			{
				if (!inlet.InputOutlet.ConnectedInlets.Contains(inlet))
				{
					inlet.InputOutlet.ConnectedInlets.Add(inlet);
				}
			}
		}

		public static void LinkTo(this MidiMapping midiMapping, Document document)
		{
			if (midiMapping == null) throw new NullException(() => midiMapping);

			if (midiMapping.Document != null)
			{
				if (midiMapping.Document.MidiMappings.Contains(midiMapping))
				{
					midiMapping.Document.MidiMappings.Remove(midiMapping);
				}
			}

			midiMapping.Document = document;

			if (midiMapping.Document != null)
			{
				if (!midiMapping.Document.MidiMappings.Contains(midiMapping))
				{
					midiMapping.Document.MidiMappings.Add(midiMapping);
				}
			}
		}

		public static void LinkTo(this MidiMappingElement midiMappingElement, EntityPosition entityPosition)
		{
			if (midiMappingElement == null) throw new ArgumentNullException(nameof(midiMappingElement));

			midiMappingElement.EntityPosition = entityPosition;

			// No inverse property.
		}

		public static void LinkTo(this MidiMappingElement midiMappingElement, MidiMapping midiMapping)
		{
			if (midiMappingElement == null) throw new NullException(() => midiMappingElement);

			if (midiMappingElement.MidiMapping != null)
			{
				if (midiMappingElement.MidiMapping.MidiMappingElements.Contains(midiMappingElement))
				{
					midiMappingElement.MidiMapping.MidiMappingElements.Remove(midiMappingElement);
				}
			}

			midiMappingElement.MidiMapping = midiMapping;

			if (midiMappingElement.MidiMapping != null)
			{
				if (!midiMappingElement.MidiMapping.MidiMappingElements.Contains(midiMappingElement))
				{
					midiMappingElement.MidiMapping.MidiMappingElements.Add(midiMappingElement);
				}
			}
		}

		public static void LinkTo(this MidiMappingElement midiMappingElement, Scale scale)
		{
			if (midiMappingElement == null) throw new NullException(() => midiMappingElement);

			if (midiMappingElement.Scale != null)
			{
				if (midiMappingElement.Scale.MidiMappingElements.Contains(midiMappingElement))
				{
					midiMappingElement.Scale.MidiMappingElements.Remove(midiMappingElement);
				}
			}

			midiMappingElement.Scale = scale;

			if (midiMappingElement.Scale != null)
			{
				if (!midiMappingElement.Scale.MidiMappingElements.Contains(midiMappingElement))
				{
					midiMappingElement.Scale.MidiMappingElements.Add(midiMappingElement);
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

		public static void LinkToUnderlyingPatch(this Operator derivedOperator, Patch underlyingPatch)
		{
			if (derivedOperator == null) throw new NullException(() => derivedOperator);

			derivedOperator.UnderlyingPatch = underlyingPatch;
			
			// No inverse property
		}

		public static void LinkTo(this Operator op, Curve curve)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));

			op.Curve = curve;

			// No inverse property.
		}

		public static void LinkTo(this Operator op, EntityPosition entityPosition)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));

			op.EntityPosition = entityPosition;

			// No inverse property.
		}

		public static void LinkTo(this Operator op, Sample sample)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));

			op.Sample = sample;

			// No inverse property.
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

		public static void LinkTo(this Scale scale, Document document)
		{
			if (scale == null) throw new NullException(() => scale);

			if (scale.Document != null)
			{
				if (scale.Document.Scales.Contains(scale))
				{
					scale.Document.Scales.Remove(scale);
				}
			}

			scale.Document = document;

			if (scale.Document != null)
			{
				if (!scale.Document.Scales.Contains(scale))
				{
					scale.Document.Scales.Add(scale);
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

		public static void LinkTo(this AudioOutput audioOutput, SpeakerSetup speakerSetup)
		{
			if (audioOutput == null) throw new NullException(() => audioOutput);

			audioOutput.SpeakerSetup = speakerSetup;

			// No inverse property.
		}

		public static void LinkTo(this IInletOrOutlet inletOrOutlet, Dimension dimension)
		{
			if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

			inletOrOutlet.Dimension = dimension;

			// No inverse property.
		}

		public static void LinkTo(this MidiMappingElement midiMappingElement, Dimension dimension)
		{
			if (midiMappingElement == null) throw new NullException(() => midiMappingElement);

			midiMappingElement.StandardDimension = dimension;

			// No inverse property.
		}

		public static void LinkTo(this Node node, NodeType nodeType)
		{
			if (node == null) throw new NullException(() => node);

			node.NodeType = nodeType;

			// No inverse property.
		}

		public static void LinkTo(this Operator op, Dimension dimension)
		{
			if (op == null) throw new NullException(() => op);

			op.StandardDimension = dimension;

			// No inverse property.
		}

		public static void LinkTo(this Patch patch, Dimension dimension)
		{
			if (patch == null) throw new NullException(() => patch);

			patch.StandardDimension = dimension;

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