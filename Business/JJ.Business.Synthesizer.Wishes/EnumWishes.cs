using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;

// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    public static class EnumWishes
    {
                
        public static SampleDataTypeEnum ToEnum(this SampleDataType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (SampleDataTypeEnum)entity.ID;
        }

        public static AudioFileFormatEnum ToEnum(this AudioFileFormat entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (AudioFileFormatEnum)entity.ID;
        }

        public static SpeakerSetupEnum ToEnum(this SpeakerSetup entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (SpeakerSetupEnum)entity.ID;
        }

        public static InterpolationTypeEnum ToEnum(this InterpolationType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (InterpolationTypeEnum)entity.ID;
        }

        
        public static void SetNodeTypeEnum(this CurveInWrapper wrapper, NodeTypeEnum interpolationTypeEnum, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SetNodeTypeEnum(wrapper.Curve, interpolationTypeEnum, context);
        }

        public static void SetNodeTypeEnum(this CurveIn curveIn, NodeTypeEnum interpolationTypeEnum, IContext context = null)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            SetNodeTypeEnum(curveIn.Curve, interpolationTypeEnum, context);
        }

        /// <summary>
        /// Sets the node type (the type of interpolation) for a curve as a whole.
        /// This sets the node type of all the curve nodes.
        /// </summary>
        public static void SetNodeTypeEnum(this Curve curve, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            var nodeTypeRepository = CreateRepository<INodeTypeRepository>(context);
            curve.Nodes.ForEach(x => x.SetNodeTypeEnum(nodeTypeEnum, nodeTypeRepository));
        }

        public static NodeTypeEnum TryGetNodeTypeEnum(this CurveIn curveIn)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            return TryGetNodeTypeEnum(curveIn.Curve);
        }

        /// <summary>
        /// Gets the node type (the type of interpolation) for a curve as a whole.
        /// This only works if all (but the last) node are set to the same node type.
        /// Otherwise, NodeTypeEnum.Undefined is returned.
        /// </summary>
        public static NodeTypeEnum TryGetNodeTypeEnum(this Curve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));

            IList<NodeTypeEnum> distinctNodeTypeEnums = curve.Nodes.Select(x => x.GetNodeTypeEnum()).Distinct().ToArray();

            if (distinctNodeTypeEnums.Count == 1) return distinctNodeTypeEnums[0];
            else return NodeTypeEnum.Undefined;
        }

        // Helpers

        /// <summary>
        /// Creates a new repository, of the given interface type TInterface.
        /// If the context isn't provided, a brand new one is created, based on the settings from the config file.
        /// Depending on the use-case, creating a new context like that each time can be problematic.
        /// </summary>
        private static TInterface CreateRepository<TInterface>(IContext context = null)
            => PersistenceHelper.CreateRepository<TInterface>(context ?? PersistenceHelper.CreateContext());

    }
}
