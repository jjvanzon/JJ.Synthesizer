using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Framework.Mathematics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Resources;
using System.IO;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter
    {
        private IPatchRepository _patchRepository;
        private IOperatorRepository _operatorRepository;
        private IOperatorTypeRepository _operatorTypeRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;
        private IEntityPositionRepository _entityPositionRepository;
        private IIDRepository _idRepository;

        private PatchManager _patchManager;
        private EntityPositionManager _entityPositionManager;

        public PatchDetailsViewModel ViewModel { get; set; }

        public PatchDetailsPresenter(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository,
            IIDRepository idRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
            _operatorTypeRepository = operatorTypeRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
            _idRepository = idRepository;

            _entityPositionManager = new EntityPositionManager(_entityPositionRepository, _idRepository);

            _patchManager = new PatchManager(
                _patchRepository,
                _operatorRepository, 
                _operatorTypeRepository, 
                _inletRepository, 
                _outletRepository, 
                _curveRepository, 
                _sampleRepository,
                _documentRepository,
                _entityPositionRepository,
                _idRepository);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            ViewModel.Visible = false;
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Patch patch = ViewModel.ToEntity(_patchRepository, _operatorRepository, _operatorTypeRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            IValidator validator = new PatchValidator_Recursive(patch, _curveRepository, _sampleRepository, _documentRepository, alreadyDone: new HashSet<object>());
            if (!validator.IsValid)
            {
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                ViewModel.Successful = false;
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        public void MoveOperator(int operatorID, float centerX, float centerY)
        {
            AssertViewModel();

            OperatorViewModel operatorViewModel = ViewModel.Entity.Operators
                                                                  .Where(x => x.ID == operatorID)
                                                                  .Single();
            operatorViewModel.CenterX = centerX;
            operatorViewModel.CenterY = centerY;
        }

        public void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            AssertViewModel();

            InletViewModel inletViewModel = ViewModel.Entity.Operators
                                                            .SelectMany(x => x.Inlets)
                                                            .Where(x => x.ID == inletID)
                                                            .Single();

            OutletViewModel inputOutletViewModel = ViewModel.Entity.Operators
                                                                   .SelectMany(x => x.Outlets)
                                                                   .Where(x => x.ID == inputOutletID)
                                                                   .Single();
            inletViewModel.InputOutlet = inputOutletViewModel;
        }

        public void SelectOperator(int operatorID)
        {
            SetSelectedOperator(operatorID);
        }
        
        /// <summary>
        /// Deletes the selected operator. Does not delete anything if no operator is selected.
        /// </summary>
        public void DeleteOperator()
        {
            AssertViewModel();

            if (ViewModel.SelectedOperator != null)
            {
                int operatorID = ViewModel.SelectedOperator.ID;

                int listIndex = ViewModel.Entity.Operators.IndexOf(x => x.ID == operatorID);

                OperatorViewModel operatorViewModel = ViewModel.Entity.Operators[listIndex];

                // Unlink related operator's inlets to which the input operator is connected.
                IList<InletViewModel> relatedInletViewModels =  GetConnectedInletViewModels(ViewModel.Entity.Operators, operatorViewModel);
                foreach (InletViewModel relatedInletViewModel in relatedInletViewModels)
                {
                    relatedInletViewModel.InputOutlet = null;
                }

                // Unlink op.Inlets.InputOutlet
                operatorViewModel.Inlets.ForEach(x => x.InputOutlet = null);
                // Unlink op.Inlets
                operatorViewModel.Inlets = new List<InletViewModel>();

                // Unlink op.Outlet[..].Operator
                operatorViewModel.Outlets.ForEach(x => x.Operator = null);
                // Unlink op.Outlets
                operatorViewModel.Outlets = new List<OutletViewModel>();

                ViewModel.Entity.Operators.RemoveAt(listIndex);
            }
        }

        /// <summary>
        /// Gets related operator's inlets to which the input operator is connected
        /// (at the ViewModel level).
        /// </summary>
        private IList<InletViewModel> GetConnectedInletViewModels(IList<OperatorViewModel> allOperatorViewModels, OperatorViewModel inputOperatorViewModel)
        {
            // TODO: This makes operating on the view model to execute the delete action quite expensive.
            // Is it possible and less expensive to do a partial ToEntity and operate on the entity model?
            var list = new List<InletViewModel>();

            foreach (OperatorViewModel operatorViewModel in allOperatorViewModels)
            {
                foreach (InletViewModel inletViewModel in operatorViewModel.Inlets)
                {
                    if (inletViewModel.InputOutlet != null)
                    {
                        if (inletViewModel.InputOutlet.Operator.ID == inputOperatorViewModel.ID)
                        {
                            list.Add(inletViewModel);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// NOTE: Do a rollback after this action,
        /// because for performance reasons it does not produce a complete state in the context.
        /// </summary>
        public void SetValue(string value)
        {
            AssertViewModel();

            if (ViewModel.SelectedOperator == null)
            {
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorFirst
                });

                return;
            }

            if (ViewModel.SelectedOperator.OperatorTypeID != (int)OperatorTypeEnum.Value)
            {
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectedOperatorMustBeValueOperator
                });

                return;
            }

            ViewModel.SelectedValue = value;

            Operator op = ViewModel.SelectedOperator.ToEntityWithInletsAndOutlets(_operatorRepository, _operatorTypeRepository, _inletRepository, _outletRepository);
            op.Data = value;

            IValidator validator = new OperatorValidator_Value(op); // TODO: Low priority: Do this with a manager, so you can hide complexity (hide the validator) and decrease the degree of coupling.
            if (!validator.IsValid)
            {
                ViewModel.ValidationMessages.AddRange(validator.ValidationMessages.ToCanonical());
                ViewModel.Successful = false;
            }
            else
            {
                ViewModel.SelectedOperator.Value = value;
                ViewModel.SelectedOperator.Caption = value;
            }

            // TODO: Low priority: Clearing validation messages at appropriate times in other actions and in this action.
            // TODO: Low priority: And setting Successful = true for that matter.
        }

        /// <summary>
        /// Returns the output file path.
        /// This action is quite a hack.
        /// TODO: It should not be a hack and also this action is way too dependent on infrastructure.
        /// </summary>
        public string Play(RepositoryWrapper repositoryWrapper)
        {
            AssertViewModel();

            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            double duration = config.PatchPlayDurationInSeconds;
            string sampleFilePath = config.PatchPlayHackedSampleFilePath;
            string outputFilePath = config.PatchPlayHackedAudioFileOutputFilePath;

            Patch patch = ToEntity(ViewModel);

            VoidResult result = DoPlay(duration, sampleFilePath, outputFilePath, patch, repositoryWrapper);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;

            return outputFilePath;
        }

        private VoidResult DoPlay(double duration, string sampleFilePath, string outputFilePath, Patch patch, RepositoryWrapper repositoryWrapper)
        {
            var result = new VoidResult
            {
                Messages = new List<Message>()
            };

            Operator patchOutlet = patch.Operators
                                        .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
                                        .FirstOrDefault();

            Operator sampleOperator = patch.Operators
                                           .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
                                           .FirstOrDefault();

            if (patchOutlet == null)
            {
                result.Successful = false;
                result.Messages.Add(new Message
                {
                    PropertyKey = PropertyNames.PatchOutlet,
                    Text = PresentationMessages.AddPatchOutletToPlayASound
                });
                return result;
            }

            Sample_OperatorWrapper sampleOperatorWrapper = null;

            if (sampleOperator != null)
            {
                // TODO: Refactor out dependency on file system.
                if (!File.Exists(sampleFilePath))
                {
                    result.Successful = false;
                    result.Messages.Add(new Message
                    {
                        PropertyKey = PropertyNames.Patch,
                        Text = PresentationMessageFormatter.SampleFileDoesNotExistWithLocation(Path.GetFullPath(sampleFilePath))
                    });
                    return result;
                }

                SampleManager sampleManager = CreateSampleManager(repositoryWrapper);

                using (Stream sampleStream = new FileStream(sampleFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Sample sample = sampleManager.CreateSample(sampleStream);

                    sampleOperatorWrapper = new Sample_OperatorWrapper(sampleOperator, _sampleRepository);
                    sampleOperatorWrapper.Sample = sample;
                }
            }

            AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(repositoryWrapper);

            AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.FilePath = outputFilePath;
            audioFileOutput.Duration = duration;

            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
            Outlet outlet = patchOutletWrapper.Result;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

            audioFileOutputManager.Execute(audioFileOutput);

            if (sampleOperatorWrapper != null)
            {
                sampleOperatorWrapper.Sample = null;
            }

            return new VoidResult
            {
                Successful = true,
                Messages = new List<Message>()
            };
        }

        // Helpers

        private Patch ToEntity(PatchDetailsViewModel userInput)
        {
            Patch patch = userInput.ToEntity(
                _patchRepository,
                _operatorRepository,
                _operatorTypeRepository,
                _inletRepository,
                _outletRepository,
                _entityPositionRepository);

            return patch;
        }

        /// <summary>
        /// The SelectedOperator is non-persisted data.
        /// This method sets the selected operator in the view model.
        /// It uses the Operator's ID for this.
        /// It goes through all the operators in the view model,
        /// setting IsSelected to false unless it is the selected operator,
        /// and sets the details view model's SelectedOperator property.
        /// </summary>
        private void SetSelectedOperator(int operatorID)
        {
            ViewModel.SelectedOperator = null;
            ViewModel.SelectedValue = null;

            foreach (OperatorViewModel operatorViewModel in ViewModel.Entity.Operators)
            {
                if (operatorViewModel.ID == operatorID)
                {
                    operatorViewModel.IsSelected = true;
                    ViewModel.SelectedOperator = operatorViewModel;
                    if (operatorViewModel.OperatorTypeID == (int)OperatorTypeEnum.Value)
                    {
                        ViewModel.SelectedValue = operatorViewModel.Value;
                    }
                }
                else
                {
                    operatorViewModel.IsSelected = false;
                }
            }
        }

        private SampleManager CreateSampleManager(RepositoryWrapper repositoryWrapper)
        {
            var sampleRepositories = new SampleRepositories(repositoryWrapper);
            var manager = new SampleManager(sampleRepositories);
            return manager;
        }

        private AudioFileOutputManager CreateAudioFileOutputManager(RepositoryWrapper repositoryWrapper)
        {
            var manager = new AudioFileOutputManager(
                repositoryWrapper.AudioFileOutputRepository,
                repositoryWrapper.AudioFileOutputChannelRepository,
                repositoryWrapper.SampleDataTypeRepository,
                repositoryWrapper.SpeakerSetupRepository,
                repositoryWrapper.AudioFileFormatRepository,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.SampleRepository,
                repositoryWrapper.DocumentRepository,
                repositoryWrapper.IDRepository);

            return manager;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
