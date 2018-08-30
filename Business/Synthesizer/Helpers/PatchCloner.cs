using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Helpers
{
    internal class PatchCloner
    {
        private readonly RepositoryWrapper _repositories;
        private Dictionary<Operator, Operator> _operatorDictionary;
        private Dictionary<Inlet, Inlet> _inletDictionary;
        private Dictionary<Outlet, Outlet> _outletDictionary;

        public PatchCloner(RepositoryWrapper repositories) => _repositories = repositories;

        public Patch CloneWithRelatedEntities(Patch sourcePatch)
        {
            if (sourcePatch == null) throw new ArgumentNullException(nameof(sourcePatch));

            _operatorDictionary = new Dictionary<Operator, Operator>();
            _inletDictionary = new Dictionary<Inlet, Inlet>();
            _outletDictionary = new Dictionary<Outlet, Outlet>();

            Patch destPatch = Clone(sourcePatch);

            foreach (Operator sourceOperator in sourcePatch.Operators)
            {
                Operator destOperator = CloneRecursive(sourceOperator);
                destOperator.LinkTo(destPatch);
            }

            return destPatch;
        }

        private Operator CloneRecursive(Operator sourceOperator)
        {
            if (_operatorDictionary.TryGetValue(sourceOperator, out Operator destOperator))
            {
                return destOperator;
            }

            destOperator = Clone(sourceOperator);
            _operatorDictionary[sourceOperator] = destOperator;

            foreach (Inlet sourceInlet in sourceOperator.Inlets)
            {
                Inlet destInlet = CloneRecursive(sourceInlet);
                destInlet.LinkTo(destOperator);
            }

            foreach (Outlet sourceOutlet in sourceOperator.Outlets)
            {
                Outlet destOutlet = CloneRecursive(sourceOutlet);
                destOutlet.LinkTo(destOperator);
            }

            if (sourceOperator.Sample != null)
            {
                Sample destSample = CloneWithBytes(sourceOperator.Sample);
                destOperator.LinkTo(destSample);
            }

            if (sourceOperator.Curve != null)
            {
                Curve destCurve = CloneWithRelatedEntities(sourceOperator.Curve);
                destOperator.LinkTo(destCurve);
            }

            if (sourceOperator.EntityPosition != null)
            {
                EntityPosition entityPosition = Clone(sourceOperator.EntityPosition);
                destOperator.LinkTo(entityPosition);
            }

            return destOperator;
        }

        private Inlet CloneRecursive(Inlet sourceInlet)
        {
            if (_inletDictionary.TryGetValue(sourceInlet, out Inlet destInlet))
            {
                return destInlet;
            }

            destInlet = Clone(sourceInlet);
            _inletDictionary[sourceInlet] = destInlet;

            if (sourceInlet.InputOutlet != null)
            {
                Outlet destOutlet = CloneRecursive(sourceInlet.InputOutlet);
                destInlet.LinkTo(destOutlet);
            }

            return destInlet;
        }

        private Outlet CloneRecursive(Outlet sourceOutlet)
        {
            if (_outletDictionary.TryGetValue(sourceOutlet, out Outlet destOutlet))
            {
                return destOutlet;
            }

            destOutlet = Clone(sourceOutlet);
            _outletDictionary[sourceOutlet] = destOutlet;

            Operator destOperator = CloneRecursive(sourceOutlet.Operator);
            destOutlet.LinkTo(destOperator);

            foreach (Inlet sourceInlet in sourceOutlet.ConnectedInlets)
            {
                Inlet destInlet = CloneRecursive(sourceInlet);
                destInlet.LinkTo(destOutlet);
            }

            return destOutlet;
        }

        private Curve CloneWithRelatedEntities(Curve sourceCurve)
        {
            if (sourceCurve == null) throw new ArgumentNullException(nameof(sourceCurve));

            Curve destCurve = Clone(sourceCurve);

            foreach (Node sourceNode in sourceCurve.Nodes)
            {
                Node destNode = Clone(sourceNode);
                destNode.LinkTo(destCurve);
            }

            return destCurve;
        }

        private Sample CloneWithBytes(Sample sourceSample)
        {
            Sample destSample = Clone(sourceSample);

            CloneBytes(sourceSample, destSample);

            return destSample;
        }

        private void CloneBytes(Sample sourceSample, Sample destSample)
        {
            byte[] sourceBytes = _repositories.SampleRepository.GetBytes(sourceSample.ID);
            var destBytes = new byte[sourceBytes.Length];
            Array.Copy(sourceBytes, destBytes, sourceBytes.Length);
            _repositories.SampleRepository.SetBytes(destSample.ID, destBytes);
        }

        private Patch Clone(Patch source)
        {
            var dest = new Patch
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name,
                GroupName = source.GroupName,
                Hidden = source.Hidden,
                HasDimension = source.HasDimension,
                CustomDimensionName = source.CustomDimensionName
            };

            _repositories.PatchRepository.Insert(dest);

            dest.LinkTo(source.Document);
            dest.LinkTo(source.StandardDimension);

            return dest;
        }

        private Operator Clone(Operator source)
        {
            var dest = new Operator
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name,
                CustomDimensionName = source.CustomDimensionName,
                Data = source.Data,
                HasDimension = source.HasDimension
            };

            _repositories.OperatorRepository.Insert(dest);

            dest.LinkTo(source.StandardDimension);
            dest.LinkToUnderlyingPatch(source.UnderlyingPatch);

            return dest;
        }

        private Inlet Clone(Inlet source)
        {
            var dest = new Inlet
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name,
                Position = source.Position,
                DefaultValue = source.DefaultValue,
                IsRepeating = source.IsRepeating,
                RepetitionPosition = source.RepetitionPosition,
                IsObsolete = source.IsObsolete,
                NameOrDimensionHidden = source.NameOrDimensionHidden,
                WarnIfEmpty = source.WarnIfEmpty
            };

            _repositories.InletRepository.Insert(dest);

            dest.LinkTo(source.Dimension);

            return dest;
        }

        private Outlet Clone(Outlet source)
        {
            var dest = new Outlet
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name,
                Position = source.Position,
                IsRepeating = source.IsRepeating,
                RepetitionPosition = source.RepetitionPosition,
                IsObsolete = source.IsObsolete,
                NameOrDimensionHidden = source.NameOrDimensionHidden
            };

            _repositories.OutletRepository.Insert(dest);

            dest.LinkTo(source.Dimension);

            return dest;
        }

        private Curve Clone(Curve source)
        {
            var dest = new Curve
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name
            };

            _repositories.CurveRepository.Insert(dest);

            return dest;
        }

        private Node Clone(Node source)
        {
            var dest = new Node
            {
                ID = _repositories.IDRepository.GetID(),
                X = source.X,
                Y = source.Y,
                Slope = source.Slope
            };

            _repositories.NodeRepository.Insert(dest);

            dest.LinkTo(source.InterpolationType);

            return dest;
        }

        private Sample Clone(Sample source)
        {
            var dest = new Sample
            {
                ID = _repositories.IDRepository.GetID(),
                Name = source.Name,
                Amplifier = source.Amplifier,
                TimeMultiplier = source.TimeMultiplier,
                SamplingRate = source.SamplingRate,
                BytesToSkip = source.BytesToSkip
            };

            _repositories.SampleRepository.Insert(dest);

            dest.LinkTo(source.AudioFileFormat);
            dest.LinkTo(source.InterpolationType);
            dest.LinkTo(source.SampleDataType);
            dest.LinkTo(source.SpeakerSetup);

            return dest;
        }

        private EntityPosition Clone(EntityPosition source)
        {
            var dest = new EntityPosition
            {
                ID = _repositories.IDRepository.GetID(),
                X = source.X,
                Y = source.Y
            };

            _repositories.EntityPositionRepository.Insert(dest);

            return dest;
        }
    }
}