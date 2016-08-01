RAISERROR (
'RUN THE C# UTILITY: JJ.OneOff.Synthesizer.DataMigration.
Select the option Migrate_Operator_SpeedUp_ToSquash and next
run Migrate_Operator_SlowDown_ToStretch.
', 11, -1); 

--For this you need to get the changeset 855 and paste this into the DataMigrationExecutor:

--public static void Migrate_Operator_SpeedUp_ToSquash(Action<string> progressCallback)
--{
--    if (progressCallback == null) throw new NullException(() => progressCallback);

--    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

--    using (IContext context = PersistenceHelper.CreateContext())
--    {
--        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

--        var patchManager = new PatchManager(new PatchRepositories(repositories));

--        IList<Operator> operators = repositories.OperatorRepository
--                                                .GetAll()
--                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SpeedUp)
--                                                .ToArray();

--        for (int i = 0; i < operators.Count; i++)
--        {
--            Operator op = operators[i];

--            op.SetOperatorTypeEnum(OperatorTypeEnum.Narrower, repositories.OperatorTypeRepository);

--            Inlet originInlet = patchManager.CreateInlet(op);
--            originInlet.ListIndex = 2;

--            patchManager.Patch = op.Patch;
--            VoidResult result = patchManager.SaveOperator(op);
--            ResultHelper.Assert(result);

--            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
--            progressCallback(progressMessage);
--        }

--        AssertDocuments(repositories, progressCallback);

--        //throw new Exception("Temporarily not committing, for debugging.");
--        context.Commit();
--    }

--    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
--}

--public static void Migrate_Operator_SlowDown_ToStretch(Action<string> progressCallback)
--{
--    if (progressCallback == null) throw new NullException(() => progressCallback);

--    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

--    using (IContext context = PersistenceHelper.CreateContext())
--    {
--        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

--        var patchManager = new PatchManager(new PatchRepositories(repositories));

--        IList<Operator> operators = repositories.OperatorRepository
--                                                .GetAll()
--                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SlowDown)
--                                                .ToArray();

--        for (int i = 0; i < operators.Count; i++)
--        {
--            Operator op = operators[i];

--            op.SetOperatorTypeEnum(OperatorTypeEnum.Stretch, repositories.OperatorTypeRepository);

--            Inlet originInlet = patchManager.CreateInlet(op);
--            originInlet.ListIndex = 2;

--            patchManager.Patch = op.Patch;
--            VoidResult result = patchManager.SaveOperator(op);
--            ResultHelper.Assert(result);

--            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
--            progressCallback(progressMessage);
--        }

--        AssertDocuments(repositories, progressCallback);

--        //throw new Exception("Temporarily not committing, for debugging.");
--        context.Commit();
--    }

--    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
--}
