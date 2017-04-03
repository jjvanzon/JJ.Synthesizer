using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        [NotNull] public static string GetMessagePrefix([NotNull] AudioFileOutput entity) => GetMessagePrefix(ResourceFormatter.AudioFileOutput, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] AudioOutput entity) => GetMessagePrefix(ResourceFormatter.AudioOutput, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Curve entity) => GetMessagePrefix(ResourceFormatter.Curve, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Document entity) => GetMessagePrefix(ResourceFormatter.Document, entity.Name);

        [NotNull] public static string GetMessagePrefix_ForLowerDocumentReference([NotNull] DocumentReference lowerDocumentReference)
        {
            return GetMessagePrefix(ResourceFormatter.LowerDocument, GetUserFriendlyIdentifier_ForLowerDocumentReference(lowerDocumentReference));
        }

        [NotNull] public static string GetMessagePrefix([NotNull] Inlet entity) => GetMessagePrefix(ResourceFormatter.Inlet, GetUserFriendlyIdentifier(entity));
        
        /// <param name="number">1-based</param>
        [NotNull] public static string GetMessagePrefix([NotNull] Node entity, int number) => GetMessagePrefix(ResourceFormatter.Node, GetUserFriendlyIdentifier(entity, number));

        [NotNull]
        public static string GetMessagePrefix(
            [NotNull] Operator entity,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] IPatchRepository patchRepository)
        {
            return GetMessagePrefix(ResourceFormatter.Operator, GetUserFriendlyIdentifier(entity, sampleRepository, curveRepository, patchRepository));
        }

        [NotNull] public static string GetMessagePrefix([NotNull] Outlet entity) => GetMessagePrefix(ResourceFormatter.Outlet, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Patch entity) => GetMessagePrefix(ResourceFormatter.Patch, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Sample entity) => GetMessagePrefix(ResourceFormatter.Sample, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Scale entity) => GetMessagePrefix(ResourceFormatter.Scale, GetUserFriendlyIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Tone entity) => GetMessagePrefix(ResourceFormatter.Tone, GetUserFriendlyIdentifier(entity));

        // Helpers

        /// <summary> Uses the name in the message or otherwise the only the entityTypeDisplayName. </summary>
        [NotNull]
        private static string GetMessagePrefix(string entityTypeDisplayName, [CanBeNull] string identifier)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return $"{entityTypeDisplayName}: ";
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                return $"{entityTypeDisplayName} {identifier}: ";
            }
        }
    }
}