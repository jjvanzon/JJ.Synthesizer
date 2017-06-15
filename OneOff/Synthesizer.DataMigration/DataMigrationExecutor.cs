using System;
using System.Collections.Generic;
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
    internal class DataMigrationExecutor
    {
        private class InletOrOutletTuple
        {
            public InletOrOutletTuple(
                OperatorTypeEnum operatorTypeEnum,
                int listIndex,
                DimensionEnum dimensionEnum)
            {
                OperatorTypeEnum = operatorTypeEnum;
                ListIndex = listIndex;
                DimensionEnum = dimensionEnum;
            }

            public OperatorTypeEnum OperatorTypeEnum { get; }
            public int ListIndex { get; }
            public DimensionEnum DimensionEnum { get; }
        }

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

        public static void Migrate_AndOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                DocumentManager documentManager = new DocumentManager(repositories);
                Patch systemPatch = documentManager.GetSystemPatch(OperatorTypeEnum.And);

                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.And);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];
                    op.LinkToUnderlyingPatch(systemPatch);
                    op.UnlinkOperatorType();

                    foreach (Inlet inlet in op.Inlets)
                    {
                        inlet.WarnIfEmpty = true;
                    }

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_SineOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                DocumentManager documentManager = new DocumentManager(repositories);

                const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Sine;
                Patch systemPatch = documentManager.GetSystemPatch(operatorTypeEnum);

                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];
                    op.LinkToUnderlyingPatch(systemPatch);
                    op.UnlinkOperatorType();

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_ComparativeOperators_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Equal, repositories, progressCallback);
                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.GreaterThan, repositories, progressCallback);
                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.GreaterThanOrEqual, repositories, progressCallback);
                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LessThan, repositories, progressCallback);
                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LessThanOrEqual, repositories, progressCallback);
                Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.NotEqual, repositories, progressCallback);

                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        private static void Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(
            OperatorTypeEnum operatorTypeEnum,
            RepositoryWrapper repositories,
            Action<string> progressCallback)
        {
            DocumentManager documentManager = new DocumentManager(repositories);

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

        // Helpers

        private static void AssertDocuments_AndReapplyUnderlyingPatches(RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

            AssertDocuments_AndReapplyUnderlyingPatches(rootDocuments, repositories, progressCallback);
        }

        private static void AssertDocuments_AndReapplyUnderlyingPatches(IList<Document> rootDocuments, RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IResult totalResult = new VoidResult { Successful = true };
            for (int i = 0; i < rootDocuments.Count; i++)
            {
                Document document = rootDocuments[i];

                string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{document.Name}'.";
                progressCallback(progressMessage);

                // Validate
                var documentManager = new DocumentManager(repositories);

                // 'Get' will execute side-effects of reapplying underlying patches.
                // Not that the migration should not have written fully correct data,
                // but you can at any time change an underlying patch from a library,
                // and then the dependent documents will not be immediately updated.
                // So at any time 'invalid' data (non-updated operators)
                // can be in the database and that would be no problem.
                // However if you just asser the documents,
                // it will just complain about mismatches all over the place.

                documentManager.Get(document.ID);
                IResult result = documentManager.Save(document);
                string messagePrefix = ValidationHelper.GetMessagePrefix(document);
                totalResult.Combine(result, messagePrefix);
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