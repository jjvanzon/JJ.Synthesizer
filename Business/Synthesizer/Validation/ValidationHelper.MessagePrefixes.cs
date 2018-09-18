using System.Linq;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Validation
{
	public static partial class ValidationHelper
	{
		public static string GetMessagePrefix(AudioFileOutput entity)
			=> GetMessagePrefix(
				ResourceFormatter.AudioFileOutput,
				GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(AudioOutput entity)
			=> GetMessagePrefix(ResourceFormatter.AudioOutput, GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(Curve entity) => GetMessagePrefix(ResourceFormatter.Curve, GetUserFriendlyIdentifier(entity));
		public static string GetMessagePrefix(Document entity) => GetMessagePrefix(ResourceFormatter.Document, entity.Name);

		public static string GetMessagePrefix_ForLowerDocumentReference(DocumentReference lowerDocumentReference)
			=> GetMessagePrefix(
				ResourceFormatter.Library,
				GetUserFriendlyIdentifier_ForLowerDocumentReference(lowerDocumentReference));

		public static string GetMessagePrefix_ForHigherDocumentReference(DocumentReference higherDocumentReference)
			=> GetMessagePrefix(
				ResourceFormatter.HigherDocument,
				GetUserFriendlyIdentifier_ForHigherDocumentReference(higherDocumentReference));

		// ReSharper disable once UnusedParameter.Global
		public static string GetMessagePrefix(EntityPosition entityPosition) => ResourceFormatter.EntityPosition;

		public static string GetMessagePrefix(MidiMappingGroup midiMapping)
			=> GetMessagePrefix(
				ResourceFormatter.MidiMappingGroup,
				GetUserFriendlyIdentifier(midiMapping));

		public static string GetMessagePrefix(MidiMapping midiMapping)
			=> $"{ResourceFormatter.MidiMapping} '{GetUserFriendlyIdentifier(midiMapping)}': ";

		/// <summary> Only returns a prefix if higherPatch is actually in another document than lowerPatch. </summary>
		public static string TryGetHigherDocumentPrefix(Patch lowerPatch, Patch higherPatch)
		{
			if (lowerPatch.Document == higherPatch.Document)
			{
				return null;
			}

			DocumentReference documentReference = higherPatch.Document
			                                                 .LowerDocumentReferences
			                                                 .FirstOrDefault(x => x.LowerDocument.ID == lowerPatch.Document.ID);

			string higherDocumentPrefix = GetMessagePrefix_ForHigherDocumentReference(documentReference);

			return higherDocumentPrefix;
		}

		public static string GetMessagePrefix(IInletOrOutlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string entityTypeName = entity.GetType().Name;

			string entityTypeDisplayName;

			switch (entity)
			{
				case Inlet _:
					entityTypeDisplayName = ResourceFormatter.Inlet;
					break;

				case Outlet _:
					entityTypeDisplayName = ResourceFormatter.Outlet;
					break;

				default:
					entityTypeDisplayName = entityTypeName;
					break;
			}

			return GetMessagePrefix(entityTypeDisplayName, GetUserFriendlyIdentifier(entity));
		}

		/// <param name="number">1-based</param>
		public static string GetMessagePrefix(Node entity, int number)
			=> GetMessagePrefix(ResourceFormatter.Node, GetUserFriendlyIdentifier(entity, number));

		public static string GetMessagePrefix(Operator entity, ICurveRepository curveRepository)
			=> GetMessagePrefix(ResourceFormatter.Operator, GetUserFriendlyIdentifier(entity, curveRepository));

		public static string GetMessagePrefix(Patch entity) => GetMessagePrefix(ResourceFormatter.Patch, GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(Sample entity) => GetMessagePrefix(ResourceFormatter.Sample, GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(Scale entity) => GetMessagePrefix(ResourceFormatter.Scale, GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(Tone entity) => GetMessagePrefix(ResourceFormatter.Tone, GetUserFriendlyIdentifier(entity));

		// Helpers

		/// <summary> Uses the name in the message or otherwise the only the entityTypeDisplayName. </summary>
		private static string GetMessagePrefix(string entityTypeDisplayName, string identifier)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (string.IsNullOrWhiteSpace(identifier))
			{
				return $"{entityTypeDisplayName}: ";
			}

			return $"{entityTypeDisplayName} {identifier}: ";
		}
	}
}