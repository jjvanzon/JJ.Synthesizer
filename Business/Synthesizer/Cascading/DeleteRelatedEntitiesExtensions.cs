using System;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Cascading
{
	/// <summary> Deletes related entities that are inherently part of the entity. </summary>
	public static class DeleteRelatedEntitiesExtensions
	{
		public static void DeleteRelatedEntities(this Document document, RepositoryWrapper repositories)
		{
			if (document == null) throw new NullException(() => document);
			if (repositories == null) throw new NullException(() => repositories);

			foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs.ToArray())
			{
				audioFileOutput.UnlinkRelatedEntities();
				repositories.AudioFileOutputRepository.Delete(audioFileOutput);
			}

			// Order-Dependence:
			// AudioOutput is omitted here.
			// You need to postpone deleting this 1-to-1 related entity till after deleting the document, 
			// or ORM will try to update Document.AudioOutputID to null and crash.

			foreach (DocumentReference documentReference in document.LowerDocumentReferences.ToArray())
			{
				documentReference.UnlinkRelatedEntities();
				repositories.DocumentReferenceRepository.Delete(documentReference);
			}

			foreach (MidiMapping midiMapping in document.MidiMappings.ToArray())
			{
				midiMapping.DeleteRelatedEntities(repositories.MidiMappingElementRepository, repositories.EntityPositionRepository);
				midiMapping.UnlinkRelatedEntities();
				repositories.MidiMappingRepository.Delete(midiMapping);
			}

			foreach (Patch patch in document.Patches.ToArray())
			{
				patch.DeleteRelatedEntities(repositories);
				patch.UnlinkRelatedEntities();
				repositories.PatchRepository.Delete(patch);
			}

			foreach (Scale scale in document.Scales.ToArray())
			{
				scale.DeleteRelatedEntities(repositories.ToneRepository);
				scale.UnlinkRelatedEntities();
				repositories.ScaleRepository.Delete(scale);
			}
		}

		public static void DeleteRelatedEntities(this Curve curve, INodeRepository nodeRepository)
		{
			if (curve == null) throw new NullException(() => curve);
			if (nodeRepository == null) throw new NullException(() => nodeRepository);

			foreach (Node node in curve.Nodes.ToArray())
			{
				node.UnlinkRelatedEntities();
				nodeRepository.Delete(node);
			}
		}

		public static void DeleteRelatedEntities(
			this MidiMapping midiMapping,
			IMidiMappingElementRepository midiMappingElementRepository,
			IEntityPositionRepository entityPositionRepository)
		{
			if (midiMapping == null) throw new NullException(() => midiMapping);
			if (midiMappingElementRepository == null) throw new NullException(() => midiMappingElementRepository);

			foreach (MidiMappingElement midiMappingElement in midiMapping.MidiMappingElements.ToArray())
			{
				midiMappingElement.UnlinkRelatedEntities();
				midiMappingElementRepository.Delete(midiMappingElement);

				// Order-Dependence:
				// You need to postpone deleting this 1-to-1 related entity till after deleting the MidiMappingElement, 
				// or ORM will try to update MidiMappingElement.EntityPositionID to null and crash.
				if (midiMappingElement.EntityPosition != null)
				{
					entityPositionRepository.Delete(midiMappingElement.EntityPosition);
				}
			}
		}

		public static void DeleteRelatedEntities(this Operator op, RepositoryWrapper repositories)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));
			if (repositories == null) throw new NullException(() => repositories);

			if (op.Curve != null)
			{
				op.Curve.DeleteRelatedEntities(repositories.NodeRepository);
				repositories.CurveRepository.Delete(op.Curve);
			}

			// Order-Dependence:
			// EntityPosition is omitted here.
			// You need to postpone deleting this 1-to-1 related entity till after deleting the Operator, 
			// or ORM will try to update Operator.EntityPositionID to null and crash.

			foreach (Inlet inlet in op.Inlets.ToArray())
			{
				inlet.UnlinkRelatedEntities();
				repositories.InletRepository.Delete(inlet);
			}

			if (op.Sample != null)
			{
				op.Sample.UnlinkRelatedEntities();
				repositories.SampleRepository.Delete(op.Sample);
			}

			foreach (Outlet outlet in op.Outlets.ToArray())
			{
				outlet.UnlinkRelatedEntities();
				repositories.OutletRepository.Delete(outlet);
			}
		}

		public static void DeleteRelatedEntities(this Patch patch, RepositoryWrapper repositories)
		{
			if (patch == null) throw new NullException(() => patch);
			if (repositories == null) throw new NullException(() => repositories);

			foreach (Operator op in patch.Operators.ToArray())
			{
				op.DeleteRelatedEntities(repositories);
				op.UnlinkRelatedEntities();
				repositories.OperatorRepository.Delete(op);

				// Order-Dependence:
				// You need to postpone deleting this 1-to-1 related entity till after deleting the Operator, 
				// or ORM will try to update Operator.EntityPositionID to null and crash.
				if (op.EntityPosition != null)
				{
					repositories.EntityPositionRepository.Delete(op.EntityPosition);
				}
			}
		}

		public static void DeleteRelatedEntities(this Scale scale, IToneRepository toneRepository)
		{
			if (scale == null) throw new NullException(() => scale);

			foreach (Tone tone in scale.Tones.ToArray())
			{
				toneRepository.Delete(tone);
			}
		}
	}
}
