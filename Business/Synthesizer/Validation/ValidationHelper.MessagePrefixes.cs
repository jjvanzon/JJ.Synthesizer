using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Validation
{
	public static partial class ValidationHelper
	{
		public static string GetMessagePrefix(AudioFileOutput entity) => GetMessagePrefix(
			ResourceFormatter.AudioFileOutput,
			GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(AudioOutput entity) =>
			GetMessagePrefix(ResourceFormatter.AudioOutput, GetUserFriendlyIdentifier(entity));

		public static string GetMessagePrefix(Curve entity) => GetMessagePrefix(ResourceFormatter.Curve, GetUserFriendlyIdentifier(entity));
		public static string GetMessagePrefix(Document entity) => GetMessagePrefix(ResourceFormatter.Document, entity.Name);

		public static string GetMessagePrefix_ForLowerDocumentReference(DocumentReference lowerDocumentReference) => GetMessagePrefix(
			ResourceFormatter.Library,
			GetUserFriendlyIdentifier_ForLowerDocumentReference(lowerDocumentReference));

		public static string GetMessagePrefix_ForHigherDocumentReference(DocumentReference higherDocumentReference) => GetMessagePrefix(
			ResourceFormatter.HigherDocument,
			GetUserFriendlyIdentifier_ForHigherDocumentReference(higherDocumentReference));

		public static string GetMessagePrefix(EntityPosition entityPosition) => ResourceFormatter.EntityPosition;

		public static string GetMessagePrefix(MidiMapping midiMapping) => GetMessagePrefix(
			ResourceFormatter.MidiMapping,
			GetUserFriendlyIdentifier(midiMapping));

		public static string GetMessagePrefix(MidiMappingElement midiMappingElement) =>
			$"{ResourceFormatter.MidiMappingElement}: {GetUserFriendlyIdentifierLong(midiMappingElement)}: ";

		/// <summary> Only returns a prefix if higherPatch is actually in another document than lowerPatch. </summary>
		public static string TryGetHigherDocumentPrefix(Patch lowerPatch, Patch higherPatch)
		{
			if (lowerPatch.Document == higherPatch.Document)
			{
				return null;
			}

			DocumentReference documentReference = higherPatch.Document
			                                                 .LowerDocumentReferences
			                                                 .Where(x => x.LowerDocument.ID == lowerPatch.Document.ID)
			                                                 .FirstOrDefault();

			string higherDocumentPrefix = GetMessagePrefix_ForHigherDocumentReference(documentReference);

			return higherDocumentPrefix;
		}

		public static string GetMessagePrefix(IInletOrOutlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			string entityTypeName = entity.GetType().Name;

			string entityTypeDisplayName;

			if (entity is Inlet)
			{
				entityTypeDisplayName = ResourceFormatter.Inlet;
			}
			else if (entity is Outlet)
			{
				entityTypeDisplayName = ResourceFormatter.Outlet;
			}
			else
			{
				entityTypeDisplayName = entityTypeName;
			}

			return GetMessagePrefix(entityTypeDisplayName, GetUserFriendlyIdentifier(entity));
		}

		/// <param name="number">1-based</param>
		public static string GetMessagePrefix(Node entity, int number) =>
			GetMessagePrefix(ResourceFormatter.Node, GetUserFriendlyIdentifier(entity, number));

		public static string GetMessagePrefix(Operator entity, ICurveRepository curveRepository) => GetMessagePrefix(
			ResourceFormatter.Operator,
			GetUserFriendlyIdentifier(entity, curveRepository));

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