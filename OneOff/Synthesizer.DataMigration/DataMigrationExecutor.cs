using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal static class DataMigrationExecutor
    {
        // General

        public static bool MustAssertWarningIncrease { get; set; } = true;

        public static void AssertAllDocuments(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void ShowAllWarnings(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                var documentManager = new DocumentManager(repositories);

                IResult totalResult = new VoidResult { Successful = true };

                IList<Document> documents = repositories.DocumentRepository.GetAll();
                foreach (Document document in documents)
                {
                    IResult result = documentManager.GetWarningsRecursive(document);
                    string messagePrefix = ValidationHelper.GetMessagePrefix(document);

                    totalResult.Combine(result, messagePrefix);
                }

                totalResult.Assert();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void ReapplyAllPatches(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void DeleteOrphanedEntityPositions(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);
                int rowsAffected = entityPositionManager.DeleteOrphans();

                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

                context.Commit();

                progressCallback($"{MethodBase.GetCurrentMethod().Name} finished. {rowsAffected} rows affected.");
            }

        }

        // Specific Migrations

        // ...

        // Helpers

        private static void Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(
            OperatorTypeEnum operatorTypeEnum,
            RepositoryWrapper repositories,
            Action<string> progressCallback)
        {
            var documentManager = new DocumentManager(repositories);
            documentManager.RefreshSystemDocumentIfNeeded(documentManager.GetSystemDocument());
            Patch systemPatch = documentManager.GetSystemPatch(operatorTypeEnum);
            IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

            for (int i = 0; i < operators.Count; i++)
            {
                Operator op = operators[i];
                op.LinkToUnderlyingPatch(systemPatch);
                op.UnlinkOperatorType();

                string progressMessage = $"Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
                progressCallback(progressMessage);
            }
        }

        private static void AssertDocuments_AndReapplyUnderlyingPatches(
            RepositoryWrapper repositories,
            Action<string> progressCallback)
        {
            IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

            AssertDocuments_AndReapplyUnderlyingPatches(rootDocuments, repositories, progressCallback, MustAssertWarningIncrease);
        }

        private static void AssertDocuments_AndReapplyUnderlyingPatches(
            IList<Document> rootDocuments,
            RepositoryWrapper repositories,
            Action<string> progressCallback,
            bool mustAssertWarningIncrease = true)
        {
            var documentManager = new DocumentManager(repositories);

            IResult totalResult = new VoidResult { Successful = true };
            for (int i = 0; i < rootDocuments.Count; i++)
            {
                Document document = rootDocuments[i];

                string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{document.Name}'.";
                progressCallback(progressMessage);

                // Warnings Before
                IResult warningResultBefore = documentManager.GetWarningsRecursive(document);

                // Reapply UnderlyingPatches

                // 'Get' through the manager will execute side-effects of reapplying underlying patches.
                // Not that the migration should not have written fully correct data,
                // but you can at any time change an underlying patch from a library,
                // and then the dependent documents will not be immediately updated.
                // So at any time 'invalid' data (non-updated operators)
                // can be in the database and that would be no problem.
                // However if you just asser the documents,
                // it will just complain about mismatches all over the place.

                documentManager.Get(document.ID);

                // Validate
                IResult saveResult = documentManager.Save(document);

                // Collect Results
                totalResult.Combine(saveResult, ValidationHelper.GetMessagePrefix(document));

                // Warnings After
                IResult warningResultAfter = documentManager.GetWarningsRecursive(document);

                // Compare Warnings
                IList<string> additionalWarningTexts = warningResultAfter.Messages
                                                                         .Except(warningResultBefore.Messages)
                                                                         .ToArray();
                if (mustAssertWarningIncrease)
                {
                    if (additionalWarningTexts.Count != 0)
                    {
                        var additionalWarningResult = new VoidResult
                        {
                            Successful = false,
                            Messages = additionalWarningTexts
                        };

                        totalResult.Combine(additionalWarningResult, "Warning: " + ValidationHelper.GetMessagePrefix(document));
                    }
                }
            }

            try
            {
                totalResult.Assert();
            }
            catch
            {
                progressCallback("Exception while validating documents.");
                throw;
            }
        }
    }
}