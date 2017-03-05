using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        [NotNull] public static string GetMessagePrefix([NotNull] AudioFileOutput entity) => GetMessagePrefix(ResourceFormatter.AudioFileOutput, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] AudioOutput entity) => GetMessagePrefix(ResourceFormatter.AudioOutput, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Curve entity) => GetMessagePrefix(ResourceFormatter.Curve, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Document entity) => GetMessagePrefix(ResourceFormatter.Document, entity.Name);

        [NotNull] public static string GetMessagePrefix_ForLowerDocumentReference([NotNull] DocumentReference lowerDocumentReference)
        {
            return GetMessagePrefix(ResourceFormatter.LowerDocument, GetIdentifier_ForLowerDocumentReference(lowerDocumentReference));
        }

        [NotNull] public static string GetMessagePrefix([NotNull] Inlet entity) => GetMessagePrefix(ResourceFormatter.Inlet, GetIdentifier(entity));
        /// <param name="number">1-based</param>
        [NotNull] public static string GetMessagePrefix([NotNull] Node entity, int number) => GetMessagePrefix(ResourceFormatter.Node, GetIdentifier(entity, number));

        [NotNull]
        public static string GetMessagePrefix(
            [NotNull] Operator entity,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] IPatchRepository patchRepository)
        {
            string operatorTypeDisplayName = ResourceFormatter.GetOperatorTypeDisplayName(entity);
            string identifier = GetIdentifier(entity, sampleRepository, curveRepository, patchRepository);
            return GetMessagePrefix(operatorTypeDisplayName, identifier);
        }

        [NotNull] public static string GetMessagePrefix([NotNull] Outlet entity) => GetMessagePrefix(ResourceFormatter.Outlet, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Patch entity) => GetMessagePrefix(ResourceFormatter.Patch, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Sample entity) => GetMessagePrefix(ResourceFormatter.Sample, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Scale entity) => GetMessagePrefix(ResourceFormatter.Scale, GetIdentifier(entity));
        [NotNull] public static string GetMessagePrefix([NotNull] Tone entity) => GetMessagePrefix(ResourceFormatter.Tone, GetIdentifier(entity));

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