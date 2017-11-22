using System;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Cascading
{
	/// <summary> Deletes related entities that are inherently part of the entity. </summary>
	public static class DeleteRelatedEntitiesExtensions
	{
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

		public static void DeleteRelatedEntities(this Document document, RepositoryWrapper repositories)
		{
			if (document == null) throw new NullException(() => document);
			if (repositories == null) throw new NullException(() => repositories);

			foreach (Patch patch in document.Patches.ToArray())
			{
				patch.DeleteRelatedEntities(repositories);
				patch.UnlinkRelatedEntities();
				repositories.PatchRepository.Delete(patch);
			}

			if (document.AudioOutput != null)
			{
				document.AudioOutput.UnlinkRelatedEntities();
				repositories.AudioOutputRepository.Delete(document.AudioOutput);
			}

			foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs.ToArray())
			{
				audioFileOutput.UnlinkRelatedEntities();
				repositories.AudioFileOutputRepository.Delete(audioFileOutput);
			}

			foreach (Scale scale in document.Scales.ToArray())
			{
				scale.DeleteRelatedEntities(repositories.ToneRepository);
				scale.UnlinkRelatedEntities();
				repositories.ScaleRepository.Delete(scale);
			}

			foreach (DocumentReference documentReference in document.LowerDocumentReferences.ToArray())
			{
				documentReference.UnlinkRelatedEntities();
				repositories.DocumentReferenceRepository.Delete(documentReference);
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
			}
		}

		public static void DeleteRelatedEntities(this Operator op, RepositoryWrapper repositories)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));
			if (repositories == null) throw new NullException(() => repositories);

			foreach (Inlet inlet in op.Inlets.ToArray())
			{
				inlet.UnlinkRelatedEntities();
				repositories.InletRepository.Delete(inlet);
			}

			foreach (Outlet outlet in op.Outlets.ToArray())
			{
				outlet.UnlinkRelatedEntities();
				repositories.OutletRepository.Delete(outlet);
			}

			// Be null-tolerant to be able to get out of trouble if something is missing.
			EntityPosition entityPosition = repositories.EntityPositionRepository.TryGetByEntityTypeNameAndEntityID(typeof(OperatingSystem).Name, op.ID);
			if (entityPosition != null)
			{
				repositories.EntityPositionRepository.Delete(entityPosition);
			}

			if (op.Sample != null)
			{
				op.Sample.UnlinkRelatedEntities();
				repositories.SampleRepository.Delete(op.Sample);
			}

			if (op.Curve != null)
			{
				op.Curve.DeleteRelatedEntities(repositories.NodeRepository);
				repositories.CurveRepository.Delete(op.Curve);
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
