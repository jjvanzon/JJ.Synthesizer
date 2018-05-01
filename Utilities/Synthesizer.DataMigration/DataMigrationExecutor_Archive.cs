namespace JJ.Utilities.Synthesizer.DataMigration
{
	// ReSharper disable once UnusedMember.Global
	internal class DataMigrationExecutor_Archive
	{
		//private class InletOrOutletTuple
		//{
		//	public InletOrOutletTuple(
		//		OperatorTypeEnum operatorTypeEnum,
		//		int listIndex,
		//		DimensionEnum dimensionEnum)
		//	{
		//		OperatorTypeEnum = operatorTypeEnum;
		//		ListIndex = listIndex;
		//		DimensionEnum = dimensionEnum;
		//	}

		//	public OperatorTypeEnum OperatorTypeEnum { get; }
		//	public int ListIndex { get; }
		//	public DimensionEnum DimensionEnum { get; }
		//}

		//private const int DEFAULT_FREQUENCY = 440;

		//public static void MigrateSineVolumes(Action<string> progressCallback)
		//{
		//	throw new NotSupportedException();

		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> sineOperators = repositories.OperatorRepository
		//													.GetAll()
		//													.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sine)
		//													.ToArray();
		//		for (int i = 0; i < sineOperators.Count; i++)
		//		{
		//			Operator sineOperator = sineOperators[i];

		//			patchFacade.Patch = sineOperator.Patch;

		//			var sine = new Sine_OperatorWrapper(sineOperator);
		//			throw new Exception("Process cannot be run anymore, since the data has been migrated so that there are no more volumes anymore.");
		//			//if (sine.Volume != null)
		//			{
		//				var multiply = patchFacade.Multiply();

		//				foreach (Inlet inlet in sine.Result.ConnectedInlets.ToArray())
		//				{
		//					inlet.LinkTo(multiply.Result);
		//				}

		//				//multiply.A = sine.Volume;
		//				multiply.B = sine.Result;

		//				//sine.Volume = null;

		//				VoidResult result = patchFacade.SavePatch();
		//				ResultHelper.Assert(result);
		//			}

		//			string progressMessage = String.Format("Migrated sine {0}/{1}.", i + 1, sineOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();

		//		progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//	}
		//}

		//public static void AddSampleOperatorFrequencyInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> sampleOperators = repositories.OperatorRepository
		//													  .GetAll()
		//													  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
		//													  .ToArray();

		//		for (int i = 0; i < sampleOperators.Count; i++)
		//		{
		//			Operator sampleOperator = sampleOperators[i];

		//			patchFacade.Patch = sampleOperator.Patch;

		//			var sampleOperatorWrapper = new Sample_OperatorWrapper(sampleOperator, repositories.SampleRepository);

		//			if (sampleOperator.Inlets.Count == 0)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(sampleOperator);

		//				var numberWrapper = patchFacade.Number(DEFAULT_FREQUENCY);

		//				sampleOperatorWrapper.Frequency = numberWrapper;

		//				VoidResult result = patchFacade.SavePatch();
		//				ResultHelper.Assert(result);
		//			}

		//			string progressMessage = String.Format("Migrated sample operator {0}/{1}.", i + 1, sampleOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();

		//		progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//	}
		//}

		///// <summary>
		///// Renames all the patches, so overwrites all of the names.
		///// Currently (2015-11-21) this is acceptable since no patch has a specific name.
		///// They are just called 'Patch 1', 'Patch 2', etc.
		///// The new names become 'Patch 1', etc. too, but always with the English term for Patch, not Dutch.
		///// </summary>
		//public static void MakePatchNamesUnique(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	CultureHelper.SetThreadCultureName("en-US");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();

		//		for (int i = 0; i < rootDocuments.Count; i++)
		//		{
		//			Document rootDocument = rootDocuments[i];

		//			int patchNumber = 1;

		//			// Traverse patches in a very specific order, so that the numbers do not seem all mixed up.
		//			//IEnumerable<Document> childDocuments = rootDocument.ChildDocuments.OrderBy(x => x.ChildDocumentType.ID).ThenBy(x => x.Name);
		//			IEnumerable<Document> childDocuments = rootDocument.ChildDocuments.OrderBy(x => x.GroupName).ThenBy(x => x.Name);
		//			foreach (Document childDocument in childDocuments)
		//			{
		//				foreach (Patch patch in childDocument.Patches)
		//				{
		//					string newName = String.Format("{0} {1}", ResourceFormatter.Patch, patchNumber);
		//					patch.Name = newName;

		//					patchNumber++;
		//				}
		//			}

		//			foreach (Patch patch in rootDocument.Patches)
		//			{
		//				string newName = String.Format("{0} {1}", ResourceFormatter.Patch, patchNumber);
		//				patch.Name = newName;

		//				patchNumber++;
		//			}

		//			string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		///// <summary>
		///// Renames curves and samples so they are unique within a root documant and its child documents.
		///// Only renames ones with actual duplicate names.
		///// </summary>
		//public static void MakeCurveNamesAndSampleNamesUnique(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var documentFacade = new DocumentFacade(repositories);

		//		IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
		//		for (int i = 0; i < rootDocuments.Count; i++)
		//		{
		//			Document rootDocument = rootDocuments[i];

		//			// Rename duplicate Curves
		//			var duplicateCurveGroups = rootDocument.EnumerateSelfAndParentAndTheirChildren()
		//												   .SelectMany(x => x.Curves)
		//												   .GroupBy(x => x.Name)
		//												   .Where(x => x.Count() > 1);

		//			foreach (var duplicateCurveGroup in duplicateCurveGroups)
		//			{
		//				int curveNumber = 1;
		//				foreach (Curve curve in duplicateCurveGroup)
		//				{
		//					curve.Name = String.Format("{0} ({1})", curve.Name, curveNumber);
		//					curveNumber++;
		//				}
		//			}

		//			// Rename duplicate Samples
		//			var duplicateSampleGroups = rootDocument.EnumerateSelfAndParentAndTheirChildren()
		//												   .SelectMany(x => x.Samples)
		//												   .GroupBy(x => x.Name)
		//												   .Where(x => x.Count() > 1);

		//			foreach (var duplicateSampleGroup in duplicateSampleGroups)
		//			{
		//				int sampleNumber = 1;
		//				foreach (Sample sample in duplicateSampleGroup)
		//				{
		//					sample.Name = String.Format("{0} ({1})", sample.Name, sampleNumber);
		//					sampleNumber++;
		//				}
		//			}

		//			// Validate
		//			VoidResult result = documentFacade.ValidateRecursive(rootDocument);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		///// <summary>
		///// Puts every patch in its own child document and asserts that no child document has more than one patch.
		///// </summary>
		//public static void PutEachPatchInAChildDocument(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	CultureHelper.SetThreadCultureName("en-US");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var documentFacade = new DocumentFacade(repositories);
		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
		//		for (int i = 0; i < rootDocuments.Count; i++)
		//		{
		//			Document rootDocument = rootDocuments[i];

		//			// Move patches to their own child document.
		//			foreach (Patch patch in rootDocument.Patches.ToArray())
		//			{
		//				//Document newChildDocument = documentFacade.CreateChildDocument(rootDocument, ChildDocumentTypeEnum.Instrument);
		//				Document newChildDocument = documentFacade.CreateChildDocument(rootDocument);
		//				patch.LinkTo(newChildDocument);
		//			}

		//			// Give each child document minimally one patch.
		//			foreach (Document childDocument in rootDocument.ChildDocuments)
		//			{
		//				if (childDocument.Patches.Count == 0)
		//				{
		//					patchFacade.CreatePatch(childDocument);
		//					Patch newPatch = patchFacade.Patch;
		//				}
		//			}

		//			// Assert no child document has more than 1 patch.
		//			foreach (Document childDocument in rootDocument.ChildDocuments)
		//			{
		//				if (childDocument.Patches.Count > 1)
		//				{
		//					throw new Exception(String.Format("childDocument with Name '{0}' and ID '{1}' has more than one Patch.", childDocument.Name, childDocument.ID));
		//				}
		//			}

		//			// Validate
		//			VoidResult result = documentFacade.ValidateRecursive(rootDocument);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void ConvertUnderlyingDocumentIDsToUnderlyingPatchIDs(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> customOperators = repositories.OperatorRepository.GetAll()
		//																		 .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
		//																		 .ToArray();
		//		for (int i = 0; i < customOperators.Count; i++)
		//		{
		//			Operator customOperator = customOperators[i];

		//			var wrapper = new CustomOperator_OperatorWrapper(customOperator, repositories.PatchRepository);

		//			// Try using the UnderlyingPatchID as a DocumentID.
		//			int? underlyingEntityID = wrapper.UnderlyingPatchID;
		//			if (underlyingEntityID != null)
		//			{
		//				Document document = repositories.DocumentRepository.TryGet(underlyingEntityID.Value);
		//				if (document != null)
		//				{
		//					// It is a DocumentID. Make it the PatchID.
		//					if (document.Patches.Count != 1)
		//					{
		//						throw new NotEqualException(() => document.Patches.Count, 1);
		//					}

		//					wrapper.UnderlyingPatch = document.Patches[0];
		//				}
		//			}

		//			string progressMessage = String.Format("Migrated custom operator {0}/{1}.", i + 1, customOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void GivePatchesSameNameAsTheirDocuments(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Patch> patches = repositories.PatchRepository.GetAll().ToArray();

		//		for (int i = 0; i < patches.Count; i++)
		//		{
		//			Patch patch = patches[i];

		//			patch.Name = patch.Document.Name;

		//			string progressMessage = String.Format("Migrated Patch {0}/{1}.", i + 1, patches.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void ResaveCustomOperatorsToSet_InletDefaultValue_InletDimension_And_OutletDimension(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> customOperators = repositories.OperatorRepository
		//													  .GetAll()
		//													  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
		//													  .ToArray();

		//		for (int i = 0; i < customOperators.Count; i++)
		//		{
		//			Operator customOperator = customOperators[i];
		//			patchFacade.Patch = customOperator.Patch;

		//			VoidResult result = patchFacade.SaveOperator(customOperator);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated custom operator {0}/{1}.", i + 1, customOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void ResavePatchInletOperatorsToSet_InletDefaultValue_AndInletDimension(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> patchInlets = repositories.OperatorRepository
		//														  .GetAll()
		//														  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
		//														  .ToArray();

		//		for (int i = 0; i < patchInlets.Count; i++)
		//		{
		//			Operator patchInlet = patchInlets[i];
		//			patchFacade.Patch = patchInlet.Patch;

		//			VoidResult result = patchFacade.SaveOperator(patchInlet);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated PatchInlet Operator {0}/{1}.", i + 1, patchInlets.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Remove_PatchInletOperator_DataKeys_DefaultValue_AndDimensionEnum(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> patchInlets = repositories.OperatorRepository
		//														  .GetAll()
		//														  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
		//														  .ToArray();
		//		for (int i = 0; i < patchInlets.Count; i++)
		//		{
		//			Operator patchInlet = patchInlets[i];

		//			DataPropertyParser.RemoveKey(patchInlet, "DefaultValue");
		//			DataPropertyParser.RemoveKey(patchInlet, "DimensionEnum");

		//			string progressMessage = String.Format("Migrated PatchInlet Operator {0}/{1}.", i + 1, patchInlets.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_PatchOutlet_Dimension_FromDataProperty_ToPatchOutletOutlet(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> patchOutlets = repositories.OperatorRepository
		//														  .GetAll()
		//														  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
		//														  .ToArray();
		//		for (int i = 0; i < patchOutlets.Count; i++)
		//		{
		//			Operator patchOutlet = patchOutlets[i];

		//			string progressMessage = String.Format("Migrating PatchOutlet Operator {0}/{1}.", i + 1, patchOutlets.Count);
		//			progressCallback(progressMessage);

		//			var wrapper = new PatchOutlet_OperatorWrapper(patchOutlet);

		//			DimensionEnum dimensionEnum = DataPropertyParser.GetEnum<DimensionEnum>(patchOutlet, "DimensionEnum");

		//			wrapper.Result.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);

		//			// Make side-effects go off.
		//			patchFacade.Patch = patchOutlet.Patch;
		//			VoidResult result = patchFacade.SaveOperator(patchOutlet);
		//			try
		//			{
		//				ResultHelper.Assert(result);
		//			}
		//			catch
		//			{
		//				progressCallback("Exception!");
		//			}

		//			DataPropertyParser.RemoveKey(patchOutlet, "DimensionEnum");
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void MigrateResampleOperators_SetToDefaultInterpolationTypeInDataProperty(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Resample)
		//												.ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			patchFacade.Patch = op.Patch;

		//			var wrapper = new Resample_OperatorWrapper(op);
		//			wrapper.InterpolationType = InterpolationTypeEnum.Cubic;

		//			VoidResult result = patchFacade.SaveOperator(op);

		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_AggregateOperators_ParametersToInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> averageOperators = repositories.OperatorRepository
		//													   .GetAll()
		//													   .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Average)
		//													   .ToArray();
		//		for (int i = 0; i < averageOperators.Count; i++)
		//		{
		//			Operator op = averageOperators[i];

		//			bool mustMigrate = op.Inlets.Count == 1;
		//			if (!mustMigrate)
		//			{
		//				throw new Exception("Operator already migrated!");
		//			}

		//			patchFacade.Patch = op.Patch;

		//			// Create extra inlets.
		//			int newInletCount = 3;
		//			for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(op);
		//				inlet.ListIndex = inletIndex;
		//			}

		//			// Convert parameter to inlets.
		//			var averageWrapper = new Average_OperatorWrapper(op);

		//			double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
		//			var timeSliceDurationNumberWrapper = patchFacade.Number(timeSliceDurationValue);
		//			averageWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

		//			int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
		//			var sampleCountNumberWrapper = patchFacade.Number(sampleCountValue);
		//			averageWrapper.SampleCount = sampleCountNumberWrapper;

		//			VoidResult result = patchFacade.SaveOperator(op);

		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Step 1/4: Migrated Average {0}/{1}.", i + 1, averageOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		IList<Operator> minimumOperators = repositories.OperatorRepository
		//													   .GetAll()
		//													   .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Minimum)
		//													   .ToArray();
		//		for (int i = 0; i < minimumOperators.Count; i++)
		//		{
		//			Operator op = minimumOperators[i];

		//			bool mustMigrate = op.Inlets.Count == 1;
		//			if (!mustMigrate)
		//			{
		//				throw new Exception("Operator already migrated!");
		//			}

		//			patchFacade.Patch = op.Patch;

		//			// Create extra inlets.
		//			int newInletCount = 3;
		//			for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(op);
		//				inlet.ListIndex = inletIndex;
		//			}

		//			// Convert parameter to inlets.
		//			var minimumWrapper = new Minimum_OperatorWrapper(op);

		//			double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
		//			var timeSliceDurationNumberWrapper = patchFacade.Number(timeSliceDurationValue);
		//			minimumWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

		//			int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
		//			var sampleCountNumberWrapper = patchFacade.Number(sampleCountValue);
		//			minimumWrapper.SampleCount = sampleCountNumberWrapper;

		//			VoidResult result = patchFacade.SaveOperator(op);

		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Step 2/4: Migrated Minimum Operator {0}/{1}.", i + 1, minimumOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		IList<Operator> maximumOperators = repositories.OperatorRepository
		//													   .GetAll()
		//													   .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Maximum)
		//													   .ToArray();
		//		for (int i = 0; i < maximumOperators.Count; i++)
		//		{
		//			Operator op = maximumOperators[i];

		//			bool mustMigrate = op.Inlets.Count == 1;
		//			if (!mustMigrate)
		//			{
		//				throw new Exception("Operator already migrated!");
		//			}

		//			patchFacade.Patch = op.Patch;

		//			// Create extra inlets.
		//			int newInletCount = 3;
		//			for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(op);
		//				inlet.ListIndex = inletIndex;
		//			}

		//			// Convert parameter to inlets.
		//			var maximumWrapper = new Maximum_OperatorWrapper(op);

		//			double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
		//			var timeSliceDurationNumberWrapper = patchFacade.Number(timeSliceDurationValue);
		//			maximumWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

		//			int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
		//			var sampleCountNumberWrapper = patchFacade.Number(sampleCountValue);
		//			maximumWrapper.SampleCount = sampleCountNumberWrapper;

		//			VoidResult result = patchFacade.SaveOperator(op);

		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Step 3/4: Migrated Maximum Operator {0}/{1}.", i + 1, maximumOperators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_CacheOperators_ParametersToInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//													   .GetAll()
		//													   .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Cache)
		//													   .ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			bool mustMigrate = op.Inlets.Count == 1;
		//			if (!mustMigrate)
		//			{
		//				throw new Exception("Operator already migrated!");
		//			}

		//			patchFacade.Patch = op.Patch;

		//			// Create extra inlets.
		//			int newInletCount = 4;
		//			for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(op);
		//				inlet.ListIndex = inletIndex;
		//			}

		//			// Convert parameter to inlets.
		//			var wrapper = new Cache_OperatorWrapper(op);

		//			double startTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.StartTime);
		//			var startTimeNumberWrapper = patchFacade.Number(startTimeValue);
		//			wrapper.StartTime = startTimeNumberWrapper;

		//			double endTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.EndTime);
		//			var endTimeNumberWrapper = patchFacade.Number(endTimeValue);
		//			wrapper.EndTime = endTimeNumberWrapper;

		//			double samplingRateValue = DataPropertyParser.GetDouble(op, PropertyNames.SamplingRate);
		//			var samplingRateNumberWrapper = patchFacade.Number(samplingRateValue);
		//			wrapper.SamplingRate = samplingRateNumberWrapper;

		//			DataPropertyParser.RemoveKey(op, PropertyNames.StartTime);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.EndTime);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.SamplingRate);

		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_NumberOperators_FormatDataProperty(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Number)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			double number;
		//			if (!DoubleHelper.TryParse(op.Data, DataPropertyParser.FormattingCulture, out number))
		//			{
		//				throw new Exception("op.Data cannot be parsed to Double. Operator already migrated?");
		//			}

		//			op.Data = null;

		//			var wrapper = new Number_OperatorWrapper(op);
		//			wrapper.Number = number;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_ReferenceOperators_FormatDataProperty(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		{
		//			IList<Operator> curveOperators = repositories.OperatorRepository
		//														 .GetAll()
		//														 .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Curve)
		//														 .ToArray();
		//			for (int i = 0; i < curveOperators.Count; i++)
		//			{
		//				Operator op = curveOperators[i];

		//				int? curveID = null;
		//				if (!String.IsNullOrEmpty(op.Data))
		//				{
		//					curveID = Int32.Parse(op.Data);
		//				}

		//				op.Data = null;

		//				var wrapper = new Curve_OperatorWrapper(op, repositories.CurveRepository);
		//				wrapper.CurveID = curveID;

		//				patchFacade.Patch = op.Patch;
		//				VoidResult result = patchFacade.SaveOperator(op);
		//				ResultHelper.Assert(result);

		//				string progressMessage = String.Format("Step 1: Migrated Curve Operator {0}/{1}.", i + 1, curveOperators.Count);
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		{
		//			IList<Operator> sampleOperators = repositories.OperatorRepository
		//														  .GetAll()
		//														  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
		//														  .ToArray();
		//			for (int i = 0; i < sampleOperators.Count; i++)
		//			{
		//				Operator op = sampleOperators[i];

		//				int? sampleID = null;
		//				if (!String.IsNullOrEmpty(op.Data))
		//				{
		//					sampleID = Int32.Parse(op.Data);
		//				}

		//				op.Data = null;

		//				var wrapper = new Sample_OperatorWrapper(op, repositories.SampleRepository);
		//				wrapper.SampleID = sampleID;

		//				patchFacade.Patch = op.Patch;
		//				VoidResult result = patchFacade.SaveOperator(op);
		//				ResultHelper.Assert(result);

		//				string progressMessage = String.Format("Step 2: Migrated Sample Operator {0}/{1}.", i + 1, sampleOperators.Count);
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		{
		//			IList<Operator> customOperators = repositories.OperatorRepository
		//														   .GetAll()
		//														   .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
		//														   .ToArray();
		//			for (int i = 0; i < customOperators.Count; i++)
		//			{
		//				Operator op = customOperators[i];

		//				int? underlyingPatchID = null;
		//				if (!String.IsNullOrEmpty(op.Data))
		//				{
		//					underlyingPatchID = Int32.Parse(op.Data);
		//				}

		//				op.Data = null;

		//				var wrapper = new CustomOperator_OperatorWrapper(op, repositories.PatchRepository);
		//				wrapper.UnderlyingPatchID = underlyingPatchID;

		//				patchFacade.Patch = op.Patch;
		//				VoidResult result = patchFacade.SaveOperator(op);
		//				ResultHelper.Assert(result);

		//				string progressMessage = String.Format("Step 3: Migrated Custom Operator {0}/{1}.", i + 1, customOperators.Count);
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_SpectrumOperators_ParametersToInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Spectrum)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			int newInletCount = 4;

		//			bool mustMigrate = op.Inlets.Count != newInletCount;
		//			if (!mustMigrate)
		//			{
		//				throw new Exception("op.Inlets.Count == 4. Operator already migrated?");
		//			}

		//			patchFacade.Patch = op.Patch;

		//			// Create extra inlets.
		//			for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
		//			{
		//				Inlet inlet = patchFacade.CreateInlet(op);
		//				inlet.ListIndex = inletIndex;
		//			}

		//			// Convert parameter to inlets.
		//			var wrapper = new Spectrum_OperatorWrapper(op);

		//			double startTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.StartTime);
		//			var startTimeNumberWrapper = patchFacade.Number(startTimeValue);
		//			wrapper.StartTime = startTimeNumberWrapper;

		//			double endTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.EndTime);
		//			var endTimeNumberWrapper = patchFacade.Number(endTimeValue);
		//			wrapper.EndTime = endTimeNumberWrapper;

		//			double frequencyCountValue = DataPropertyParser.GetDouble(op, PropertyNames.FrequencyCount);
		//			var frequencyCountNumberWrapper = patchFacade.Number(frequencyCountValue);
		//			wrapper.FrequencyCount = frequencyCountNumberWrapper;

		//			DataPropertyParser.RemoveKey(op, PropertyNames.StartTime);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.EndTime);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.FrequencyCount);

		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Not committing yet, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_CurveOperators_SetDimensionParameter(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Curve)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new Curve_OperatorWrapper(op, repositories.CurveRepository);

		//			if (wrapper.Dimension != DimensionEnum.Undefined)
		//			{
		//				throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
		//			}
		//			wrapper.Dimension = DimensionEnum.Time;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_StretchOperators_SetDimensionParameter(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Stretch)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new Stretch_OperatorWrapper(op);

		//			if (wrapper.Dimension != DimensionEnum.Undefined)
		//			{
		//				throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
		//			}
		//			wrapper.Dimension = DimensionEnum.Time;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_SelectOperators_SetDimensionParameter(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Select)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new Select_OperatorWrapper(op);

		//			if (wrapper.Dimension != DimensionEnum.Undefined)
		//			{
		//				throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
		//			}
		//			wrapper.Dimension = DimensionEnum.Time;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_SetDimensionParameter_ForManyOperatorTypes(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
		//	{
		//		OperatorTypeEnum.Sine,
		//		OperatorTypeEnum.Sample,
		//		OperatorTypeEnum.Noise,
		//		OperatorTypeEnum.SawUp,
		//		OperatorTypeEnum.Square,
		//		OperatorTypeEnum.Triangle,
		//		OperatorTypeEnum.Pulse,
		//		OperatorTypeEnum.SawDown,
		//		OperatorTypeEnum.Delay,
		//		OperatorTypeEnum.SpeedUp,
		//		OperatorTypeEnum.SlowDown,
		//		OperatorTypeEnum.TimePower,
		//		OperatorTypeEnum.Earlier,
		//		OperatorTypeEnum.Squash,
		//		OperatorTypeEnum.Shift,
		//		OperatorTypeEnum.Loop,
		//		OperatorTypeEnum.Reverse,
		//		OperatorTypeEnum.Resample,
		//		OperatorTypeEnum.Random,
		//		OperatorTypeEnum.MinFollower,
		//		OperatorTypeEnum.MaxFollower,
		//		OperatorTypeEnum.AverageFollower,
		//		OperatorTypeEnum.Cache
		//	};

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll().ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new Select_OperatorWrapper(op);

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
		//			if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
		//			{
		//				continue;
		//			}

		//			if (wrapper.Dimension != DimensionEnum.Undefined)
		//			{
		//				throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
		//			}
		//			wrapper.Dimension = DimensionEnum.Time;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Document_CreateAudioOutput(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
		//		var audioOutputFacade = new AudioOutputFacade(
		//			repositories.AudioOutputRepository,
		//			repositories.SpeakerSetupRepository,
		//			repositories.IDRepository);

		//		IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
		//		for (int i = 0; i < rootDocuments.Count; i++)
		//		{
		//			Document rootDocument = rootDocuments[i];

		//			AudioOutput audioOutput = audioOutputFacade.Create(rootDocument);

		//			string progressMessage = String.Format("Migrated Document {0}/{1}.", i + 1, rootDocuments.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_BundleAndUnbundleOperators_SetInletAndOutletListIndexes_AndDimensionParameter(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Bundle ||
		//															x.GetOperatorTypeEnum() == OperatorTypeEnum.Unbundle)
		//												.ToArray();
		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			for (int j = 0; j < op.Outlets.Count; j++)
		//			{
		//				Outlet outlet = op.Outlets[j];
		//				outlet.ListIndex = j;
		//			}

		//			for (int j = 0; j < op.Inlets.Count; j++)
		//			{
		//				Inlet inlet = op.Inlets[j];
		//				inlet.ListIndex = j;
		//			}

		//			var wrapper = new GetPosition_OperatorWrapper(op);
		//			// This is the strangest if,
		//			// but this adds the Dimension key to the operator's data property,
		//			// without removing the original Dimension property.
		//			if (wrapper.Dimension == DimensionEnum.Undefined)
		//			{
		//				wrapper.Dimension = DimensionEnum.Undefined;
		//			}

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging purposes.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_SetRecalculateParameter_ForAggregatesOverDimension(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
		//	{
		//		OperatorTypeEnum.AverageOverDimension,
		//		OperatorTypeEnum.MaxOverDimension,
		//		OperatorTypeEnum.MinOverDimension,
		//		OperatorTypeEnum.SumOverDimension
		//	};

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll().ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new MaxOverDimension_OperatorWrapper(op);

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
		//			if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
		//			{
		//				continue;
		//			}

		//			if (wrapper.CollectionRecalculation != CollectionRecalculationEnum.Undefined)
		//			{
		//				throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
		//			}
		//			wrapper.CollectionRecalculation = CollectionRecalculationEnum.Continuous;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_RenameContinualToContinuous(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
		//	{
		//		OperatorTypeEnum.AverageOverDimension,
		//		OperatorTypeEnum.MaxOverDimension,
		//		OperatorTypeEnum.MinOverDimension,
		//		OperatorTypeEnum.SumOverDimension
		//	};

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll().ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
		//			if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
		//			{
		//				continue;
		//			}

		//			string value = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
		//			if (String.Equals(value, PropertyNames.Continual))
		//			{
		//				DataPropertyParser.SetValue(op, PropertyNames.Recalculation, PropertyNames.Continuous);
		//			}

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_RenameRecalculation_To_CollectionRecalculation(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
		//	{
		//		OperatorTypeEnum.AverageOverDimension,
		//		OperatorTypeEnum.ClosestOverDimension,
		//		OperatorTypeEnum.MaxOverDimension,
		//		OperatorTypeEnum.MinOverDimension,
		//		OperatorTypeEnum.SumOverDimension
		//	};

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll().ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
		//			if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
		//			{
		//				continue;
		//			}

		//			string value = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
		//			DataPropertyParser.SetValue(op, PropertyNames.CollectionRecalculation, value);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.Recalculation);

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily do not commit, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_MultiplyWithOrigin_ToMultiply(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.MultiplyWithOrigin)
		//												.ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new MultiplyWithOrigin_OperatorWrapper(op);

		//			if (wrapper.Origin != null)
		//			{
		//				continue;
		//			}

		//			op.SetOperatorTypeEnum(OperatorTypeEnum.Multiply, repositories.OperatorTypeRepository);

		//			Inlet originInlet = op.Inlets
		//								  .OrderBy(x => x.ListIndex)
		//								  .ElementAt(2);

		//			patchFacade.DeleteInlet(originInlet);

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_ResetOperators_AddListIndexDataKey(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Reset);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			var wrapper = new Reset_OperatorWrapper(op);

		//			if (wrapper.ListIndex.HasValue)
		//			{
		//				continue;
		//			}

		//			// Adds data key if it is not present yet.
		//			wrapper.ListIndex = null;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily do not commit, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_LowPassFilter_HighPassFilter_AddBandWidthInlet(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators1 = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.HighPassFilter);
		//		IList<Operator> operators2 = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.LowPassFilter);
		//		IList<Operator> operators = Enumerable.Concat(operators1, operators2).ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			switch (op.Inlets.Count)
		//			{
		//				case 3:
		//					continue;

		//				case 2:
		//					break;

		//				default:
		//					throw new EqualException(() => op.Inlets.Count, op.Inlets.Count);
		//			}

		//			Inlet bandWidthInlet = patchFacade.CreateInlet(op);
		//			bandWidthInlet.ListIndex = 2;
		//			bandWidthInlet.DefaultValue = 1.0;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Operator_SpeedUp_ToSquash(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SpeedUp)
		//												.ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			op.SetOperatorTypeEnum(OperatorTypeEnum.Squash, repositories.OperatorTypeRepository);

		//			Inlet originInlet = patchFacade.CreateInlet(op);
		//			originInlet.ListIndex = 2;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Operator_SlowDown_ToStretch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository
		//												.GetAll()
		//												.Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SlowDown)
		//												.ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			op.SetOperatorTypeEnum(OperatorTypeEnum.Stretch, repositories.OperatorTypeRepository);

		//			Inlet originInlet = patchFacade.CreateInlet(op);
		//			originInlet.ListIndex = 2;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Operator_Earlier_ToShiftAndNegative(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var x = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Earlier);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			x.Patch = op.Patch;

		//			var earlier = new Earlier_OperatorWrapper(op);

		//			// Insert number when TimeDifference not filled in.
		//			if (earlier.TimeDifference == null)
		//			{
		//				double defaultValue = earlier.TimeDifferenceInlet.DefaultValue ?? 0;
		//				earlier.TimeDifference = x.Number(defaultValue);
		//			}

		//			// Negate the time difference
		//			var negative = x.Negative(earlier.TimeDifference);
		//			earlier.TimeDifference = negative;

		//			// Change OperatorType
		//			op.SetOperatorTypeEnum(OperatorTypeEnum.Shift, repositories.OperatorTypeRepository);

		//			VoidResult result = x.SavePatch();
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Operator_Delay_To_Shift(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Delay);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			op.SetOperatorTypeEnum(OperatorTypeEnum.Shift, repositories.OperatorTypeRepository);

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SavePatch();
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Operator_Spectrum_AddDimensionDataKey(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Spectrum);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			if (!String.IsNullOrEmpty(op.Data))
		//			{
		//				continue;
		//			}

		//			var wrapper = new Spectrum_OperatorWrapper(op);
		//			wrapper.Dimension = DimensionEnum.Time;

		//			patchFacade.Patch = op.Patch;
		//			VoidResult result = patchFacade.SaveOperator(op);
		//			ResultHelper.Assert(result);

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_OperatorData_RenameDimensionHarmonicNumberToHarmonic(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			string dimension = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);

		//			if (String.Equals(dimension, PropertyNames.HarmonicNumber))
		//			{
		//				DataPropertyParser.SetValue(op, PropertyNames.Dimension, DimensionEnum.Harmonic);
		//			}

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		//AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_OperatorDimension_FromDataProperty_ToEntityReference(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			DimensionEnum dimensionEnum = DataPropertyParser.GetEnum<DimensionEnum>(op, PropertyNames.Dimension);
		//			op.SetStandardDimensionEnum(dimensionEnum, repositories.DimensionRepository);
		//			DataPropertyParser.RemoveKey(op, PropertyNames.Dimension);

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_SetDimension_OfInletsAndOutlets_OfStandardOperators(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		var inletTuples = new List<InletOrOutletTuple>();
		//		var outletTuples = new List<InletOrOutletTuple>();

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Select, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Volume));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Signal));

		//		Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> inletTupleDictionary = 
		//			inletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

		//		Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> outletTupleDictionary = 
		//			outletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

		//			IList<InletOrOutletTuple> inletTuples2;
		//			if (inletTupleDictionary.TryGetValue(operatorTypeEnum, out inletTuples2))
		//			{
		//				foreach (InletOrOutletTuple inletTuple in inletTuples2)
		//				{
		//					Inlet inlet = op.Inlets.Where(x => x.ListIndex == inletTuple.ListIndex).Single();
		//					inlet.SetDimensionEnum(inletTuple.DimensionEnum, repositories.DimensionRepository);
		//				}
		//			}

		//			IList<InletOrOutletTuple> outletTuples2;
		//			if (outletTupleDictionary.TryGetValue(operatorTypeEnum, out outletTuples2))
		//			{
		//				foreach (InletOrOutletTuple outletTuple in outletTuples2)
		//				{
		//					Outlet outlet = op.Outlets.Where(x => x.ListIndex == outletTuple.ListIndex).Single();
		//					outlet.SetDimensionEnum(outletTuple.DimensionEnum, repositories.DimensionRepository);
		//				}
		//			}

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_DeletePhaseShiftInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

		//			switch (operatorTypeEnum)
		//			{
		//				case OperatorTypeEnum.Random:
		//				case OperatorTypeEnum.SawDown:
		//				case OperatorTypeEnum.SawUp:
		//				case OperatorTypeEnum.Sine:
		//				case OperatorTypeEnum.Square:
		//				case OperatorTypeEnum.Triangle:
		//					{
		//						Inlet inlet = op.Inlets.Single(x => x.ListIndex == 1);
		//						patchFacade.DeleteInlet(inlet);
		//						break;
		//					}

		//				case OperatorTypeEnum.Pulse:
		//					{
		//						Inlet inlet = op.Inlets.Single(x => x.ListIndex == 2);
		//						patchFacade.DeleteInlet(inlet);
		//						break;
		//					}
		//			}

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_CustomOperators_ResaveToSetIsObsolete_OfInletsAndOutlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			patchFacade.Patch = op.Patch;
		//			patchFacade.SaveOperator(op);

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Move_CurvesSamplesAndPatch_FromChildDocuments_ToRootDocuments(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Document> rootDocuments = repositories.DocumentRepository.OrderByName();

		//		for (int i = 0; i < rootDocuments.Count; i++)
		//		{
		//			Document rootDocument = rootDocuments[i];

		//			var documentFacade = new DocumentFacade(repositories);

		//			HashSet<string> existingSampleNamesLowerCase = rootDocument.Samples
		//																	   .Select(x => x.Name.ToLower())
		//																	   .ToHashSet();

		//			foreach (Document childDocument in rootDocument.ChildDocuments.ToArray())
		//			{
		//				foreach (Curve curve in childDocument.Curves.ToArray())
		//				{
		//					curve.LinkTo(rootDocument);
		//				}

		//				foreach (Sample sample in childDocument.Samples.ToArray())
		//				{
		//					string newSampleName = sample.Name;
		//					int number = 2;
		//					while (existingSampleNamesLowerCase.Contains(newSampleName.ToLower()))
		//					{
		//						newSampleName = $"{sample.Name} ({number})";
		//						number++;
		//					}
		//					existingSampleNamesLowerCase.Add(newSampleName.ToLower());

		//					sample.Name = newSampleName;
		//					sample.LinkTo(rootDocument);
		//				}

		//				Patch patch = childDocument.Patches.Single();
		//				patch.LinkTo(rootDocument);

		//				//repositories.DocumentRepository.Delete(childDocument);

		//				VoidResult result = documentFacade.DeleteWithRelatedEntities(childDocument);
		//				result.Assert();
		//			}

		//			string progressMessage = String.Format("Migrated Document {0}/{1}.", i + 1, rootDocuments.Count);
		//			progressCallback(progressMessage);
		//		}

		//		// Flush to make NHibernate not accidently mistake the deleted child documents for root documents.
		//		// (for some reason that happens).
		//		context.Flush();

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_InterpolationType_LineRememberT0_To_LineRememberT1(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			string interpolationType = DataPropertyParser.TryGetString(op, PropertyNames.InterpolationType);

		//			if (String.Equals(interpolationType, "LineRememberT0"))
		//			{
		//				DataPropertyParser.SetValue(op, PropertyNames.InterpolationType, "LineRememberT1");
		//			}

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_InterpolationType_Rename_LineRememberT1_To_Line(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			string interpolationType = DataPropertyParser.TryGetString(op, PropertyNames.InterpolationType);

		//			if (String.Equals(interpolationType, "LineRememberT1"))
		//			{
		//				DataPropertyParser.SetValue(op, PropertyNames.InterpolationType, "Line");
		//			}

		//			string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_Bundle_AndUnbundle_ToInletsToDimension_AndDimensionToOutlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));
		//		var entityPositionFacade = new EntityPositionFacade(repositories.EntityPositionRepository, repositories.IDRepository);

		//		{
		//			IList<Operator> source_Bundle_Operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Bundle);

		//			for (int i = 0; i < source_Bundle_Operators.Count; i++)
		//			{
		//				Operator source_Bundle_Operator = source_Bundle_Operators[i];
		//				var source_Bundle_Wrapper = new Bundle_OperatorWrapper(source_Bundle_Operator);

		//				patchFacade.Patch = source_Bundle_Operator.Patch;

		//				InletsToDimension_OperatorWrapper dest_InletsToDimension_Wrapper = patchFacade.InletsToDimension(source_Bundle_Wrapper.Operands);
		//				dest_InletsToDimension_Wrapper.InterpolationType = InterpolationTypeEnum.Stripe;

		//				Operator dest_InletsToDimension_Operator = dest_InletsToDimension_Wrapper;
		//				dest_InletsToDimension_Operator.LinkTo(source_Bundle_Operator.StandardDimension);
		//				dest_InletsToDimension_Operator.CustomDimensionName = source_Bundle_Operator.CustomDimensionName;
		//				dest_InletsToDimension_Operator.Name = source_Bundle_Operator.Name;

		//				Outlet destOutlet = dest_InletsToDimension_Wrapper.Result;

		//				IList<Inlet> connectedInlets = source_Bundle_Wrapper.Result.ConnectedInlets.ToArray();
		//				foreach (Inlet connectedInlet in connectedInlets)
		//				{
		//					connectedInlet.LinkTo(destOutlet);
		//				}

		//				EntityPosition sourceEntityPosition = entityPositionFacade.GetOperatorPosition(source_Bundle_Operator.ID);
		//				EntityPosition destEntityPosition = entityPositionFacade.GetOrCreateOperatorPosition(dest_InletsToDimension_Operator.ID);
		//				destEntityPosition.X = sourceEntityPosition.X;
		//				destEntityPosition.Y = sourceEntityPosition.Y;

		//				patchFacade.DeleteOperatorWithRelatedEntities(source_Bundle_Operator);

		//				string progressMessage = String.Format("Migrated Bundle Operator {0}/{1}.", i + 1, source_Bundle_Operators.Count);
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		{
		//			IList<Operator> source_Unbundle_Operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Unbundle);

		//			for (int sourceOperatorIndex = 0; sourceOperatorIndex < source_Unbundle_Operators.Count; sourceOperatorIndex++)
		//			{
		//				Operator source_Unbundle_Operator = source_Unbundle_Operators[sourceOperatorIndex];
		//				var source_Unbundle_Wrapper = new Unbundle_OperatorWrapper(source_Unbundle_Operator);

		//				patchFacade.Patch = source_Unbundle_Operator.Patch;

		//				int outletCount = source_Unbundle_Operator.Outlets.Count;

		//				DimensionToOutlets_OperatorWrapper dest_DimensionToOutlets_Wrapper = patchFacade.DimensionToOutlets(
		//					source_Unbundle_Wrapper.Operand,
		//					source_Unbundle_Operator.GetStandardDimensionEnum(),
		//					source_Unbundle_Operator.CustomDimensionName,
		//					outletCount);
		//				dest_DimensionToOutlets_Wrapper.Operand = source_Unbundle_Wrapper.Operand;

		//				Operator dest_DimensionToOutlets_Operator = dest_DimensionToOutlets_Wrapper;
		//				dest_DimensionToOutlets_Operator.Name = source_Unbundle_Operator.Name;

		//				for (int outletIndex = 0; outletIndex < outletCount; outletIndex++)
		//				{
		//					Outlet sourceOutlet = source_Unbundle_Operator.Outlets[outletIndex];
		//					Outlet destOutlet = dest_DimensionToOutlets_Operator.Outlets[outletIndex];

		//					foreach (Inlet connectedInlet in sourceOutlet.ConnectedInlets.ToArray())
		//					{
		//						connectedInlet.LinkTo(destOutlet);
		//					}
		//				}

		//				EntityPosition sourceEntityPosition = entityPositionFacade.GetOperatorPosition(source_Unbundle_Operator.ID);
		//				EntityPosition destEntityPosition = entityPositionFacade.GetOrCreateOperatorPosition(dest_DimensionToOutlets_Operator.ID);
		//				destEntityPosition.X = sourceEntityPosition.X;
		//				destEntityPosition.Y = sourceEntityPosition.Y;

		//				patchFacade.DeleteOperatorWithRelatedEntities(source_Unbundle_Operator);

		//				string progressMessage = String.Format("Migrated Unbundle Operator {0}/{1}.", sourceOperatorIndex + 1, source_Unbundle_Operators.Count);
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		///// <summary>
		///// Back when default values for inlets were added to the PatchFacade.CreateOperator methods,
		///// The choice was made to NOT initialize them for already existing data,
		///// so that existing data would still behave the same, under the null-coalescing built into the visitor.
		///// But now the not-initialized default values of existing inlets are standing in my way.
		///// New visitors are being built and the choice is made not to introduce 2-level default values there,
		///// i.e. a possible default value in the entity model, then when no default value there,
		///// a hard-coded default value in the visitor. That would be 2 solutions for the same problem ('defaults').
		///// Instead the defaults in the entity model will be leading.
		///// If there was version tolerance with existing data to consider,
		///// the choice would be different, but since this system is not in production yet,
		///// it is OK to make potentially breaking changes.
		///// Probably nothing will be broken. It will probably fix more than it breaks.
		///// </summary>
		//public static void Migrate_SetInletDefaultValues(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	const double DEFAULT_FILTER_FREQUENCY = 1760.0;
		//	const double DEFAULT_FREQUENCY = 440.0;
		//	const double DEFAULT_AGGREGATE_FROM = 0.0;
		//	const double DEFAULT_AGGREGATE_TILL = 15.0;
		//	const double DEFAULT_BAND_WIDTH = 1.0;
		//	const double DEFAULT_DB_GAIN = 3.0;
		//	const double DEFAULT_DIFFERENCE = 1.0;
		//	const double DEFAULT_END_TIME = 1.0;
		//	const double DEFAULT_EXPONENT = 2.0;
		//	const double DEFAULT_FACTOR = 2.0;
		//	const double DEFAULT_PULSE_WIDTH = 0.5;
		//	const double DEFAULT_RANDOM_RATE = 16.0;
		//	const double DEFAULT_RANGE_FROM = 1.0;
		//	const double DEFAULT_RANGE_TILL = 16.0;
		//	const double DEFAULT_REVERSE_SPEED = 1.0;
		//	const double DEFAULT_SAMPLE_COUNT = 100.0;
		//	const double DEFAULT_SAMPLING_RATE = 44100.0;
		//	const double DEFAULT_SCALE_SOURCE_VALUE_A = -1.0;
		//	const double DEFAULT_SCALE_SOURCE_VALUE_B = 1.0;
		//	const double DEFAULT_SCALE_TARGET_VALUE_A = 1.0;
		//	const double DEFAULT_SCALE_TARGET_VALUE_B = 4.0;
		//	const double DEFAULT_TRANSITION_SLOPE = 1.0;
		//	const double DEFAULT_SLICE_LENGTH = 0.02;
		//	const double DEFAULT_SPECTRUM_FREQUENCY_COUNT = 256.0;
		//	const double DEFAULT_START_TIME = 0.0;
		//	const double DEFAULT_STEP = 1.0;
		//	const double MULTIPLICATIVE_IDENTITY = 1.0;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));
		//		int counter = 1;

		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.AllPassFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new AllPassFilter_OperatorWrapper(op);
		//				wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.AverageFollower);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new AverageFollower_OperatorWrapper(op);
		//				wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
		//				wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.AverageOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new AverageOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.BandPassFilterConstantPeakGain);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new BandPassFilterConstantPeakGain_OperatorWrapper(op);
		//				wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.BandPassFilterConstantTransitionGain);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new BandPassFilterConstantTransitionGain_OperatorWrapper(op);
		//				wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Cache);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Cache_OperatorWrapper(op);
		//				wrapper.StartInlet.DefaultValue = DEFAULT_START_TIME;
		//				wrapper.EndInlet.DefaultValue = DEFAULT_END_TIME;
		//				wrapper.SamplingRateInlet.DefaultValue = DEFAULT_SAMPLING_RATE;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.ClosestOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new ClosestOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.ClosestOverDimensionExp);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new ClosestOverDimensionExp_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Divide);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Divide_OperatorWrapper(op);
		//				wrapper.BInlet.DefaultValue = MULTIPLICATIVE_IDENTITY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.HighPassFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new HighPassFilter_OperatorWrapper(op);
		//				wrapper.MinFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.HighShelfFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new HighShelfFilter_OperatorWrapper(op);
		//				wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
		//				wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Interpolate);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Interpolate_OperatorWrapper(op);
		//				wrapper.SamplingRateInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Loop);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Loop_OperatorWrapper(op);
		//				wrapper.LoopStartMarkerInlet.DefaultValue = DEFAULT_START_TIME;
		//				wrapper.LoopEndMarkerInlet.DefaultValue = DEFAULT_END_TIME;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.LowPassFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new LowPassFilter_OperatorWrapper(op);
		//				wrapper.MaxFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.LowShelfFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new LowShelfFilter_OperatorWrapper(op);
		//				wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
		//				wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.MaxFollower);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new MaxFollower_OperatorWrapper(op);
		//				wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
		//				wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.MaxOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new MaxOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.MinFollower);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new MinFollower_OperatorWrapper(op);
		//				wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
		//				wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.MinOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new MinOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.MultiplyWithOrigin);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new MultiplyWithOrigin_OperatorWrapper(op);
		//				wrapper.AInlet.DefaultValue = MULTIPLICATIVE_IDENTITY;
		//				wrapper.BInlet.DefaultValue = MULTIPLICATIVE_IDENTITY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.NotchFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new NotchFilter_OperatorWrapper(op);
		//				wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.OneOverX);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new OneOverX_OperatorWrapper(op);
		//				wrapper.XInlet.DefaultValue = MULTIPLICATIVE_IDENTITY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.PeakingEQFilter);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new PeakingEQFilter_OperatorWrapper(op);
		//				wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
		//				wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;
		//				wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Pulse);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Pulse_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;
		//				wrapper.WidthInlet.DefaultValue = DEFAULT_PULSE_WIDTH;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Random);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Random_OperatorWrapper(op);
		//				wrapper.RateInlet.DefaultValue = DEFAULT_RANDOM_RATE;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.RangeOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new RangeOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_RANGE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_RANGE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.RangeOverOutlets);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new RangeOverOutlets_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_RANGE_FROM;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Reverse);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Reverse_OperatorWrapper(op);
		//				wrapper.SpeedInlet.DefaultValue = DEFAULT_REVERSE_SPEED;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Round);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Round_OperatorWrapper(op);
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Sample);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Sample_OperatorWrapper(op, repositories.SampleRepository);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.SawDown);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new SawDown_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.SawUp);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new SawUp_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Scaler);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Scaler_OperatorWrapper(op);
		//				wrapper.SourceValueAInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_A;
		//				wrapper.SourceValueBInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_B;
		//				wrapper.TargetValueAInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_A;
		//				wrapper.TargetValueBInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_B;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Shift);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Shift_OperatorWrapper(op);
		//				wrapper.DifferenceInlet.DefaultValue = DEFAULT_DIFFERENCE;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Sine);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Sine_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.SortOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new SortOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Spectrum);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Spectrum_OperatorWrapper(op);
		//				wrapper.StartInlet.DefaultValue = DEFAULT_START_TIME;
		//				wrapper.EndInlet.DefaultValue = DEFAULT_END_TIME;
		//				wrapper.FrequencyCountInlet.DefaultValue = DEFAULT_SPECTRUM_FREQUENCY_COUNT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Square);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Square_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Squash);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Squash_OperatorWrapper(op);
		//				wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Stretch);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Stretch_OperatorWrapper(op);
		//				wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.SumFollower);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new SumFollower_OperatorWrapper(op);
		//				wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
		//				wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.SumOverDimension);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new SumOverDimension_OperatorWrapper(op);
		//				wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
		//				wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
		//				wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.TimePower);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new TimePower_OperatorWrapper(op);
		//				wrapper.ExponentInlet.DefaultValue = DEFAULT_EXPONENT;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}
		//		{
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Triangle);
		//			foreach (Operator op in operators)
		//			{
		//				var wrapper = new Triangle_OperatorWrapper(op);
		//				wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

		//				progressCallback(String.Format("Migrated {0} {1}.", nameof(Operator), counter++));
		//			}
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");
		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		//public static void Migrate_OperatorType_Select_ToSetDimension(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		var patchFacade = new PatchFacade(new PatchRepositories(repositories));
		//		var entityPositionFacade = new EntityPositionFacade(repositories.EntityPositionRepository, repositories.IDRepository);

		//			IList<Operator> source_Select_Operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Select);

		//		for (int i = 0; i < source_Select_Operators.Count; i++)
		//		{
		//			Operator source_Select_Operator = source_Select_Operators[i];
		//			var source_Select_Wrapper = new Select_OperatorWrapper(source_Select_Operator);

		//			patchFacade.Patch = source_Select_Operator.Patch;

		//			SetDimension_OperatorWrapper dest_SetDimension_Wrapper = patchFacade.SetDimension(
		//				source_Select_Wrapper.Signal,
		//				source_Select_Wrapper.Position,
		//				source_Select_Operator.GetStandardDimensionEnum(),
		//				source_Select_Operator.CustomDimensionName);

		//			dest_SetDimension_Wrapper.Name = source_Select_Operator.Name;

		//			Outlet destOutlet = dest_SetDimension_Wrapper.PassThroughOutlet;

		//			IList<Inlet> connectedInlets = source_Select_Wrapper.Result.ConnectedInlets.ToArray();
		//			foreach (Inlet connectedInlet in connectedInlets)
		//			{
		//				connectedInlet.LinkTo(destOutlet);
		//			}

		//			EntityPosition sourceEntityPosition = entityPositionFacade.GetOperatorPosition(source_Select_Operator.ID);
		//			EntityPosition destEntityPosition = entityPositionFacade.GetOrCreateOperatorPosition(dest_SetDimension_Wrapper.WrappedOperator.ID);
		//			destEntityPosition.X = sourceEntityPosition.X;
		//			destEntityPosition.Y = sourceEntityPosition.Y;

		//			patchFacade.DeleteOperatorWithRelatedEntities(source_Select_Operator);

		//			string progressMessage = String.Format("Migrated {0} Operator {1}/{2}.", nameof(OperatorTypeEnum.Select), i + 1, source_Select_Operators.Count);
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
		//}

		// Helpers

		//private static void AssertDocuments(RepositoryWrapper repositories, Action<string> progressCallback)
		//{
		//	IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

		//	AssertDocuments(rootDocuments, repositories, progressCallback);
		//}

		//private static void AssertDocuments(IList<Document> rootDocuments, RepositoryWrapper repositories, Action<string> progressCallback)
		//{
		//	IResultDto totalResult = new VoidResultDto { Successful = true };
		//	for (int i = 0; i < rootDocuments.Count; i++)
		//	{
		//		Document rootDocument = rootDocuments[i];

		//		string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{rootDocument.Name}'.";
		//		progressCallback(progressMessage);

		//		// Validate
		//		var documentFacade = new DocumentFacade(repositories);
		//		VoidResultDto result = documentFacade.Save(rootDocument);
		//		totalResult.Combine(result);
		//	}

		//	try
		//	{
		//		ResultHelper.Assert(totalResult);
		//	}
		//	catch
		//	{
		//		string progressMessage = "Exception while validating documents.";
		//		progressCallback(progressMessage);
		//		throw;
		//	}
		//}


		//public static void Migrate_SetDimension_OfInletsAndOutlets_OfStandardOperators_SecondTimeAround(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		var inletTuples = new List<InletOrOutletTuple>();
		//		var outletTuples = new List<InletOrOutletTuple>();

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Absolute, 0, DimensionEnum.Number));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Absolute, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Add, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 2, DimensionEnum.Width));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.And, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 1, DimensionEnum.SliceLength));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 2, DimensionEnum.SampleCount));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 1, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 2, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 3, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.AverageOverInlets, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 2, DimensionEnum.Width));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 2, DimensionEnum.Width));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 1, DimensionEnum.Start));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 2, DimensionEnum.End));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 3, DimensionEnum.SamplingRate));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 0, DimensionEnum.PassThrough));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 1, DimensionEnum.Reset));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ChangeTrigger, 0, DimensionEnum.PassThrough));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 0, DimensionEnum.Input));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 1, DimensionEnum.Collection));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 2, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 3, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 4, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimension, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 0, DimensionEnum.Input));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 1, DimensionEnum.Collection));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 2, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 3, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 4, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverDimensionExp, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInlets, 0, DimensionEnum.Input));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInlets, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInletsExp, 0, DimensionEnum.Input));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ClosestOverInletsExp, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Curve, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.DimensionToOutlets, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 1, DimensionEnum.B));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 2, DimensionEnum.Origin));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Divide, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Equal, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 0, DimensionEnum.Low));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 1, DimensionEnum.High));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 2, DimensionEnum.Ratio));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Exponent, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThan, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GreaterThanOrEqual, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 2, DimensionEnum.BlobVolume));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 2, DimensionEnum.Slope));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 3, DimensionEnum.Decibel));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Hold, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Hold, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 0, DimensionEnum.Condition));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 1, DimensionEnum.Then));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 2, DimensionEnum.Else));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.If, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.InletsToDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 1, DimensionEnum.SamplingRate));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThan, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LessThanOrEqual, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Multiply, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 1, DimensionEnum.B));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 2, DimensionEnum.Origin));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MultiplyWithOrigin, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Negative, 0, DimensionEnum.Number));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Negative, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Number, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.OneOverX, 0, DimensionEnum.Number));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.OneOverX, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 0, DimensionEnum.Base));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 1, DimensionEnum.Exponent));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Power, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Subtract, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Not, 0, DimensionEnum.Number));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Not, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotEqual, 0, DimensionEnum.Number));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 0, DimensionEnum.A));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 1, DimensionEnum.B));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Or, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Noise, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 1, DimensionEnum.Width));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Frequency));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 2, DimensionEnum.BlobVolume));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 2, DimensionEnum.Slope));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 3, DimensionEnum.Decibel));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 2, DimensionEnum.Width));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 1, DimensionEnum.Frequency));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 2, DimensionEnum.Width));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 3, DimensionEnum.Decibel));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Sound));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 0, DimensionEnum.PassThrough));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 1, DimensionEnum.Reset));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.PulseTrigger, 0, DimensionEnum.PassThrough));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reset, 0, DimensionEnum.PassThrough));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reset, 0, DimensionEnum.PassThrough));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 0, DimensionEnum.PassThrough));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 1, DimensionEnum.Reset));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.ToggleTrigger, 0, DimensionEnum.PassThrough));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 1, DimensionEnum.Factor));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 0, DimensionEnum.PassThrough));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 1, DimensionEnum.Number));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SetDimension, 0, DimensionEnum.PassThrough));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 1, DimensionEnum.Difference));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 1, DimensionEnum.Factor));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 2, DimensionEnum.Origin));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 1, DimensionEnum.Factor));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 2, DimensionEnum.Origin));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 1, DimensionEnum.Exponent));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 2, DimensionEnum.Origin));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.GetPosition, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverInlets, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverInlets, 0, DimensionEnum.Number));

		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverInlets, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 1, DimensionEnum.SliceLength));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 2, DimensionEnum.SampleCount));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 1, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 2, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 3, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 1, DimensionEnum.SliceLength));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 2, DimensionEnum.SampleCount));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 1, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 2, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 3, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 0, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 1, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 2, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverOutlets, 0, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.RangeOverOutlets, 1, DimensionEnum.Step));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 1, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 2, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 3, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 1, DimensionEnum.SliceLength));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 2, DimensionEnum.SampleCount));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 1, DimensionEnum.From));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 2, DimensionEnum.Till));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 3, DimensionEnum.Step));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Random, 0, DimensionEnum.Rate));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Random, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 1, DimensionEnum.Step));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 2, DimensionEnum.Offset));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Sound));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 1, DimensionEnum.Start));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 2, DimensionEnum.End));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 3, DimensionEnum.FrequencyCount));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Volume));

		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 1, DimensionEnum.Skip));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 2, DimensionEnum.LoopStartMarker));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 3, DimensionEnum.LoopEndMarker));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 4, DimensionEnum.ReleaseEndMarker));
		//		inletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 5, DimensionEnum.NoteDuration));
		//		outletTuples.Add(new InletOrOutletTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));

		//		Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> inletTupleDictionary = inletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);
		//		Dictionary<OperatorTypeEnum, IList<InletOrOutletTuple>> outletTupleDictionary = outletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> operators = repositories.OperatorRepository.GetAll();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

		//			if (inletTupleDictionary.TryGetValue(operatorTypeEnum, out IList<InletOrOutletTuple> inletTuples2))
		//			{
		//				foreach (InletOrOutletTuple inletTuple in inletTuples2)
		//				{
		//					Inlet inlet = op.Inlets.Where(x => x.ListIndex == inletTuple.ListIndex).Single();
		//					inlet.SetDimensionEnum(inletTuple.DimensionEnum, repositories.DimensionRepository);
		//				}
		//			}

		//			if (outletTupleDictionary.TryGetValue(operatorTypeEnum, out IList<InletOrOutletTuple> outletTuples2))
		//			{
		//				foreach (InletOrOutletTuple outletTuple in outletTuples2)
		//				{
		//					Outlet outlet = op.Outlets.Where(x => x.ListIndex == outletTuple.ListIndex).Single();
		//					outlet.SetDimensionEnum(outletTuple.DimensionEnum, repositories.DimensionRepository);
		//				}
		//			}

		//			switch (operatorTypeEnum)
		//			{
		//				case OperatorTypeEnum.Add:
		//				case OperatorTypeEnum.AverageOverInlets:
		//				case OperatorTypeEnum.InletsToDimension:
		//				case OperatorTypeEnum.Multiply:
		//				case OperatorTypeEnum.MaxOverInlets:
		//				case OperatorTypeEnum.MinOverInlets:
		//				case OperatorTypeEnum.SortOverInlets:
		//					foreach (Inlet inlet in op.Inlets)
		//					{
		//						inlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
		//					}
		//					break;

		//				case OperatorTypeEnum.ClosestOverInlets:
		//				case OperatorTypeEnum.ClosestOverInletsExp:
		//					// Skip 1
		//					foreach (Inlet inlet in op.Inlets.Skip(1))
		//					{
		//						inlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
		//					}
		//					break;

		//				case OperatorTypeEnum.DimensionToOutlets:
		//				case OperatorTypeEnum.RangeOverOutlets:
		//					foreach (Outlet outlet in op.Outlets)
		//					{
		//						outlet.SetDimensionEnum(DimensionEnum.Item, repositories.DimensionRepository);
		//					}
		//					break;

		//			}

		//			// Cannot validate the operator, because it will do a recursive validation,
		//			// validating not yet migrated operators.

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_SetUnderlyingPatch_ForAbsoluteOperators(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
		//		var documentFacade = new DocumentFacade(repositories);

		//		Patch underlyingPatch = documentFacade.GetSystemPatch(OperatorTypeEnum.Absolute);
		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			DataPropertyParser.SetValue(op, "UnderlyingPatchID", underlyingPatch.ID);

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_Convert_SignalToSound_ForCustomOperators_PatchInlets_And_PatchOutlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> customOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);
		//		IList<Operator> patchInlets = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.PatchInlet);
		//		IList<Operator> patchOutlets = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.PatchOutlet);
		//		IList<Operator> operators = customOperators.Concat(patchInlets).Concat(patchOutlets).ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			foreach (Inlet inlet in op.Inlets)
		//			{
		//				if (inlet.GetDimensionEnum() == DimensionEnum.Signal)
		//				{
		//					inlet.SetDimensionEnum(DimensionEnum.Sound, repositories.DimensionRepository);
		//				}
		//			}

		//			foreach (Outlet outlet in op.Outlets)
		//			{
		//				if (outlet.GetDimensionEnum() == DimensionEnum.Signal)
		//				{
		//					outlet.SetDimensionEnum(DimensionEnum.Sound, repositories.DimensionRepository);
		//				}
		//			}

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_AddSystemDocument_AsLibrary_ToAllDocuments(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		DocumentFacade documentFacade = new DocumentFacade(repositories);

		//		Document systemDocument = documentFacade.GetSystemDocument();
		//		IList<Document> documents = repositories.DocumentRepository.GetAll();

		//		for (int i = 0; i < documents.Count; i++)
		//		{
		//			Document document = documents[i];

		//			if (document.ID == systemDocument.ID)
		//			{
		//				continue;
		//			}

		//			bool alreadyExists = document.LowerDocumentReferences
		//										 .Where(x => x.LowerDocument.ID == systemDocument.ID)
		//										 .Any();
		//			if (alreadyExists)
		//			{
		//				continue;
		//			}

		//			documentFacade.CreateDocumentReference(document, systemDocument);

		//			string progressMessage = $"Migrated {nameof(Document)} {i + 1}/{documents.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_UnderlyingPatch_DataKey_ToForeignKey(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> customOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);
		//		IList<Operator> absoluteOperators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);
		//		IList<Operator> operators = customOperators.Concat(absoluteOperators).ToArray();

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			int? underlyingPatchID = DataPropertyParser.TryGetInt32(op, "UnderlyingPatchID");
		//			DataPropertyParser.TryRemoveKey(op, "UnderlyingPatchID");

		//			if (!underlyingPatchID.HasValue)
		//			{
		//				op.UnlinkUnderlyingPatch();
		//			}
		//			else
		//			{
		//				Patch underlyingPatch = repositories.PatchRepository.Get(underlyingPatchID.Value);
		//				op.LinkToUnderlyingPatch(underlyingPatch);
		//			}

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_Clear_AbsoluteOperator_OperatorType(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Absolute);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			op.UnlinkOperatorType();

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_PatchInletOutlet_ListIndexes_FromDataProperty_ToInletAndOutlet(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		{
		//			const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchInlet;
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
		//			for (int i = 0; i < operators.Count; i++)
		//			{
		//				Operator op = operators[i];

		//				const string key = nameof(Inlet.ListIndex);
		//				int? listIndex = DataPropertyParser.TryGetInt32(op, key);
		//				if (!listIndex.HasValue)
		//				{
		//					throw new NullException(() => listIndex);
		//				}
		//				DataPropertyParser.TryRemoveKey(op, key);

		//				Inlet inlet = op.Inlets.Single();
		//				inlet.ListIndex = listIndex.Value;

		//				string progressMessage = $"Step 1: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		{
		//			const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchOutlet;
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
		//			for (int i = 0; i < operators.Count; i++)
		//			{
		//				Operator op = operators[i];

		//				const string key = nameof(Outlet.ListIndex);
		//				int? listIndex = DataPropertyParser.TryGetInt32(op, key);
		//				if (!listIndex.HasValue)
		//				{
		//					throw new NullException(() => listIndex);
		//				}
		//				DataPropertyParser.TryRemoveKey(op, key);

		//				Outlet outlet = op.Outlets.Single();
		//				outlet.ListIndex = listIndex.Value;

		//				string progressMessage = $"Step 2: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_PatchInletOutlet_Names_FromOperator_ToInletAndOutlet(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		{
		//			const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchInlet;
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
		//			for (int i = 0; i < operators.Count; i++)
		//			{
		//				Operator op = operators[i];
		//				Inlet inlet = op.Inlets.Single();

		//				if (string.IsNullOrEmpty(inlet.Name))
		//				{
		//					inlet.Name = op.Name;
		//				}
		//				op.Name = null;

		//				string progressMessage = $"Step 1: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		{
		//			const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.PatchOutlet;
		//			IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);
		//			for (int i = 0; i < operators.Count; i++)
		//			{
		//				Operator op = operators[i];
		//				Outlet outlet = op.Outlets.Single();

		//				// Check what's expected, so you do not accidentally overwrite.
		//				if (string.IsNullOrEmpty(outlet.Name))
		//				{
		//					outlet.Name = op.Name;
		//				}
		//				op.Name = null;

		//				string progressMessage = $"Step 2: Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//				progressCallback(progressMessage);
		//			}
		//		}

		//		AssertDocuments(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_AndOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
		//		DocumentFacade documentFacade = new DocumentFacade(repositories);
		//		Patch systemPatch = documentFacade.GetSystemPatch(OperatorTypeEnum.And);

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.And);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			op.LinkToUnderlyingPatch(systemPatch);
		//			op.UnlinkOperatorType();

		//			foreach (Inlet inlet in op.Inlets)
		//			{
		//				inlet.WarnIfEmpty = true;
		//			}

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_SineOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
		//		DocumentFacade documentFacade = new DocumentFacade(repositories);

		//		const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Sine;
		//		Patch systemPatch = documentFacade.GetSystemPatch(operatorTypeEnum);

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];
		//			op.LinkToUnderlyingPatch(systemPatch);
		//			op.UnlinkOperatorType();

		//			string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_ComparativeOperators_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Equal, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.GreaterThan, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.GreaterThanOrEqual, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LessThan, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LessThanOrEqual, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.NotEqual, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_Negative_Not_OneOverX(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Negative, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Not, repositories, progressCallback);
		//		//Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.OneOverX, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_Or_Power_Subtract(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Or, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Power, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Subtract, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_RenameResetOperatorDataKey_ListIndex_To_Position(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Reset);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			string value = DataPropertyParser.TryGetString(op, "ListIndex");
		//			DataPropertyParser.TryRemoveKey(op, "ListIndex");

		//			DataPropertyParser.SetValue(op, "Position", value);

		//			string progressMessage = $"Migrated {nameof(Operator)} {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_AddOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Add;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(operatorTypeEnum, repositories, progressCallback);

		//		IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

		//		for (int i = 0; i < operators.Count; i++)
		//		{
		//			Operator op = operators[i];

		//			op.Inlets.ForEach(x => x.IsRepeating = true);

		//			string progressMessage = $"Set Inlet.IsRepeating for {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//			progressCallback(progressMessage);
		//		}

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_MultiplyOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Multiply;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(operatorTypeEnum, repositories, progressCallback);
		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_MultiplyWithOrigin_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.MultiplyWithOrigin;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(operatorTypeEnum, repositories, progressCallback);
		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}


		//public static void Migrate_Divide_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Divide;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(operatorTypeEnum, repositories, progressCallback);
		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_If_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.If;

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(operatorTypeEnum, repositories, progressCallback);
		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_For_Noise_Pulse_SawDown_SawUp_Square_Triangle(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Noise, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Pulse, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SawDown, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SawUp, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Square, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Triangle, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForFilters(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.AllPassFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.BandPassFilterConstantPeakGain, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.BandPassFilterConstantTransitionGain, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.HighPassFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.HighShelfFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LowPassFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.LowShelfFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.NotchFilter, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.PeakingEQFilter, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForAggregatesOverInlets(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.AverageOverInlets, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ClosestOverInlets, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ClosestOverInletsExp, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MaxOverInlets, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MinOverInlets, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.RangeOverOutlets, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SortOverInlets, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_InletsToDimension_DimensionToOutlets_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.InletsToDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.DimensionToOutlets, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_AggregateFollowers_AndAggregatesOverDimensions_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.AverageFollower, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.AverageOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ClosestOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ClosestOverDimensionExp, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MaxFollower, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MaxOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MinFollower, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.MinOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.RangeOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SortOverDimension, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SumFollower, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SumOverDimension, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForChangeTrigger_PulseTrigger_ToggleTrigger_Hold_GetPosition_SetPosition_Round_Spectrum(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ChangeTrigger, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.PulseTrigger, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.ToggleTrigger, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Hold, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.GetPosition, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.SetPosition, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Round, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Spectrum, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForCache_Curve_Interpolate_Number_AndSample(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Cache, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Curve, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Interpolate, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Number, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Sample, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForExponent_Reverse_Scaler_Shift_Squash_Stretch_AndTimePower(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Exponent, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Reverse, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Scaler, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Shift, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Squash, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Stretch, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.TimePower, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}	

		//public static void Migrate_OperatorType_ToUnderlyingPatch_ForLoop_PatchInlet_PatchOutlet_Random_AndReset(Action<string> progressCallback)
		//{
		//	if (progressCallback == null) throw new NullException(() => progressCallback);

		//	progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

		//	using (IContext context = PersistenceHelper.CreateContext())
		//	{
		//		RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Loop, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.PatchInlet, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.PatchOutlet, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Random, repositories, progressCallback);
		//		Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(OperatorTypeEnum.Reset, repositories, progressCallback);

		//		AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

		//		//throw new Exception("Temporarily not committing, for debugging.");

		//		context.Commit();
		//	}

		//	progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
		//}

		// Helpers

		//private static void Migrate_OperatorType_ToUnderlingPatch_WithoutTransaction(
		//	OperatorTypeEnum operatorTypeEnum,
		//	RepositoryWrapper repositories,
		//	Action<string> progressCallback)
		//{
		//	var documentFacade = new DocumentFacade(repositories);
		//	documentFacade.RefreshSystemDocumentIfNeeded(documentFacade.GetSystemDocument());
		//	Patch systemPatch = documentFacade.GetSystemPatch(operatorTypeEnum);
		//	IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

		//	for (int i = 0; i < operators.Count; i++)
		//	{
		//		Operator op = operators[i];
		//		op.LinkToUnderlyingPatch(systemPatch);
		//		op.UnlinkOperatorType();

		//		string progressMessage = $"Migrated {operatorTypeEnum} {nameof(Operator)} {i + 1}/{operators.Count}.";
		//		progressCallback(progressMessage);
		//	}
		//}
	}
}