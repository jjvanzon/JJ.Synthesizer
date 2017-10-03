using System.Linq;
using System.Text;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Framework.Mathematics;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable RedundantCaseLabel

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ToViewModelHelper
    {
        public const string DIMENSION_KEY_EMPTY = "";
        public const string STANDARD_DIMENSION_KEY_PREFIX = "0C26ADA8-0BFC-484C-BF80-774D055DAA3F-StandardDimension-";
        public const string CUSTOM_DIMENSION_KEY_PREFIX = "5133584A-BA76-42DB-BD0E-42801FCB96DF-CustomDimension-";

        private static readonly bool _idsVisible = CustomConfigurationManager.GetSection<ConfigurationSection>().IDsVisible;

        // Dimensions

        public static string GetDimensionKey(Operator op)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            string customDimensionName = op.GetCustomDimensionNameWithFallback();
            if (!string.IsNullOrEmpty(customDimensionName))
            {
                return $"{CUSTOM_DIMENSION_KEY_PREFIX}{customDimensionName}";
            }
            else
            {
                return GetDimensionKey(op.GetStandardDimensionEnumWithFallback());
            }
        }

        public static string GetDimensionKey(DimensionEnum standardDimensionEnum)
        {
            if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                return $"{STANDARD_DIMENSION_KEY_PREFIX}{standardDimensionEnum}";
            }

            return DIMENSION_KEY_EMPTY;
        }

        public static string TryGetDimensionName(Operator op)
        {
            var sb = new StringBuilder();

            string customDimensionName = op.GetCustomDimensionNameWithFallback();
            DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnumWithFallback();

            if (NameHelper.IsFilledIn(customDimensionName))
            {
                sb.Append(customDimensionName);
            }
            else if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                sb.Append(ResourceFormatter.GetDisplayName(standardDimensionEnum));
            }
            else if (op.DimensionIsInherited())
            {
                sb.Append(ResourceFormatter.Dimension);
            }

            if (op.DimensionIsInherited())
            {
                sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
            }

            return sb.ToString();
        }

        public static bool MustStyleDimension(Operator entity)
        {
            switch (entity.GetOperatorTypeEnum())
            {
                case OperatorTypeEnum.GetPosition:
                case OperatorTypeEnum.SetPosition:
                    return false;
            }

            return entity.HasDimension;
        }

        // Document

        public static bool GetCanAddToInstrument(DocumentTreeNodeTypeEnum selectedNodeType)
        {
            switch (selectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.LibraryPatch:
                case DocumentTreeNodeTypeEnum.Patch:
                    return true;

                case DocumentTreeNodeTypeEnum.AudioOutput:
                case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                case DocumentTreeNodeTypeEnum.Curves:
                case DocumentTreeNodeTypeEnum.Libraries:
                case DocumentTreeNodeTypeEnum.Library:
                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                case DocumentTreeNodeTypeEnum.PatchGroup:
                case DocumentTreeNodeTypeEnum.Samples:
                case DocumentTreeNodeTypeEnum.Scales:
                default:
                    return false;
            }
        }

        public static bool GetCanCreateNew(DocumentTreeNodeTypeEnum selectedNodeType, bool patchDetailsVisible)
        {
            if (!patchDetailsVisible)
            {
                return false;
            }

            switch (selectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.LibraryPatch:
                case DocumentTreeNodeTypeEnum.Patch:
                    return true;

                case DocumentTreeNodeTypeEnum.AudioOutput:
                case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                case DocumentTreeNodeTypeEnum.Curves:
                case DocumentTreeNodeTypeEnum.Libraries:
                case DocumentTreeNodeTypeEnum.Library:
                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                case DocumentTreeNodeTypeEnum.PatchGroup:
                case DocumentTreeNodeTypeEnum.Samples:
                case DocumentTreeNodeTypeEnum.Scales:
                default:
                    return false;
            }
        }

        public static bool GetCanPlay(DocumentTreeNodeTypeEnum selectedNodeType)
        {
            switch (selectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.AudioOutput:
                case DocumentTreeNodeTypeEnum.Library:
                case DocumentTreeNodeTypeEnum.LibraryPatch:
                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                case DocumentTreeNodeTypeEnum.Patch:
                case DocumentTreeNodeTypeEnum.PatchGroup:
                case DocumentTreeNodeTypeEnum.Samples:
                case DocumentTreeNodeTypeEnum.Libraries:
                    return true;

                case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                case DocumentTreeNodeTypeEnum.Curves:
                case DocumentTreeNodeTypeEnum.Scales:
                default:
                    return false;
            }
        }

        public static bool GetCanOpenExternally(DocumentTreeNodeTypeEnum selectedNodeType)
        {
            switch (selectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.Library:
                case DocumentTreeNodeTypeEnum.LibraryPatch:
                    return true;

                case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                case DocumentTreeNodeTypeEnum.AudioOutput:
                case DocumentTreeNodeTypeEnum.Curves:
                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                case DocumentTreeNodeTypeEnum.Libraries:
                case DocumentTreeNodeTypeEnum.Patch:
                case DocumentTreeNodeTypeEnum.PatchGroup:
                case DocumentTreeNodeTypeEnum.Samples:
                case DocumentTreeNodeTypeEnum.Scales:
                default:
                    return false;
            }
        }

        // Inlet

        public static bool GetInletVisible(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet != null)
            {
                return true;
            }

            Operator op = inlet.Operator;

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                    return false;

                case OperatorTypeEnum.Squash:
                    if (inlet.GetDimensionEnumWithFallback() == DimensionEnum.Origin)
                    {
                        if (op.GetStandardDimensionEnumWithFallback() == DimensionEnum.Time)
                        {
                            return false;
                        }
                    }
                    break;
            }

            return true;
        }

        public static string GetInletCaption(
            Inlet inlet,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository)
        {
            var sb = new StringBuilder();

            if (!inlet.NameOrDimensionHidden)
            {
                // Name or Dimension
                OperatorWrapper wrapper = EntityWrapperFactory.CreateOperatorWrapper(
                    inlet.Operator,
                    curveRepository,
                    sampleRepository);
                string inletDisplayName = wrapper.GetInletDisplayName(inlet);
                sb.Append(inletDisplayName);

                // RepetitionPosition
                if (inlet.RepetitionPosition.HasValue)
                {
                    sb.Append($" {inlet.RepetitionPosition + 1}");
                }
            }

            // DefaultValue
            if (inlet.InputOutlet == null)
            {
                if (inlet.DefaultValue.HasValue)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(' ');
                    }
                    sb.AppendFormat("= {0:0.####}", inlet.DefaultValue.Value);
                }
            }

            // IsObsolete
            if (inlet.IsObsolete)
            {
                AppendObsoleteFlag(sb);
            }

            // ID
            if (_idsVisible)
            {
                sb.Append($" ({inlet.ID})");
            }

            return sb.ToString();
        }

        public static float? TryGetConnectionDistance(Inlet entity, EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.InputOutlet == null)
            {
                return null;
            }

            Operator operator1 = entity.Operator;
            Operator operator2 = entity.InputOutlet.Operator;

            EntityPosition entityPosition1 = entityPositionManager.GetOrCreateOperatorPosition(operator1.ID);
            EntityPosition entityPosition2 = entityPositionManager.GetOrCreateOperatorPosition(operator2.ID);

            float distance = Geometry.AbsoluteDistance(
                entityPosition1.X,
                entityPosition1.Y,
                entityPosition2.X,
                entityPosition2.Y);

            return distance;
        }

        // Node

        public static string GetNodeCaption(Node entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return $"{entity.X:0.####}, {entity.Y:0.####}";
        }

        // Operator

        /// <summary>
        /// A Number Operator can be considered 'owned' by another operator if
        /// it is the only operator it is connected to.
        /// In that case it is convenient that the Number Operator moves along
        /// with the operator it is connected to.
        /// In the Vector Graphics we accomplish this by making the Number Operator Rectangle a child of the owning Operator's Rectangle. 
        /// But also in the MoveOperator action we must move the owned operators along with their owner.
        /// </summary>
        public static bool GetOperatorIsOwned(Operator entity)
        {
            // ReSharper disable once InvertIf
            if (entity.Outlets.Count > 0)
            {
                bool isOwned = entity.GetOperatorTypeEnum() == OperatorTypeEnum.Number &&
                               // Make sure the connected inlets are all of the same operator.
                               entity.Outlets[0].ConnectedInlets.Select(x => x.Operator).Distinct().Count() == 1;

                return isOwned;
            }

            return false;
        }

        public static bool GetOperatorIsSmaller(Operator entity) => entity.GetOperatorTypeEnum() == OperatorTypeEnum.Number;

        public static string GetOperatorCaption(
            Operator op,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            string operatorCaption;

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.AverageOverDimension:
                case OperatorTypeEnum.AverageOverInlets:
                    operatorCaption = ResourceFormatter.Average;
                    break;

                case OperatorTypeEnum.ClosestOverDimension:
                case OperatorTypeEnum.ClosestOverInlets:
                    operatorCaption = ResourceFormatter.Closest;
                    break;

                case OperatorTypeEnum.ClosestOverDimensionExp:
                case OperatorTypeEnum.ClosestOverInletsExp:
                    operatorCaption = ResourceFormatter.ClosestExp;
                    break;

                case OperatorTypeEnum.Curve:
                    operatorCaption = GetOperatorCaption_ForCurve(op, curveRepository);
                    break;

                case OperatorTypeEnum.GetPosition:
                    operatorCaption = GetOperatorCaption_ForGetPosition(op);
                    break;

                case OperatorTypeEnum.MaxOverDimension:
                case OperatorTypeEnum.MaxOverInlets:
                    operatorCaption = ResourceFormatter.Max;
                    break;

                case OperatorTypeEnum.MinOverDimension:
                case OperatorTypeEnum.MinOverInlets:
                    operatorCaption = ResourceFormatter.Min;
                    break;

                case OperatorTypeEnum.Number:
                    operatorCaption = GetOperatorCaption_ForNumber(op);
                    break;

                case OperatorTypeEnum.PatchInlet:
                    operatorCaption = GetOperatorCaption_ForPatchInlet(op);
                    break;

                case OperatorTypeEnum.PatchOutlet:
                    operatorCaption = GetOperatorCaption_ForPatchOutlet(op);
                    break;

                case OperatorTypeEnum.RangeOverDimension:
                case OperatorTypeEnum.RangeOverOutlets:
                    operatorCaption = ResourceFormatter.Range;
                    break;

                case OperatorTypeEnum.Sample:
                    operatorCaption = GetOperatorCaption_ForSample(op, sampleRepository);
                    break;

                case OperatorTypeEnum.SetPosition:
                    operatorCaption = GetOperatorCaption_ForSetPosition(op);
                    break;

                case OperatorTypeEnum.SortOverDimension:
                case OperatorTypeEnum.SortOverInlets:
                    operatorCaption = ResourceFormatter.Sort;
                    break;

                case OperatorTypeEnum.SumOverDimension:
                    operatorCaption = ResourceFormatter.Sum;
                    break;

                default:
                    operatorCaption = GetOperatorCaption_ForOtherOperators(op);
                    break;
            }

            if (_idsVisible)
            {
                operatorCaption += $" ({op.ID})";
            }

            return operatorCaption;
        }

        private static string GetOperatorCaption_ForCurve(Operator op, ICurveRepository curveRepository)
        {
            string operatorTypeDisplayName = ResourceFormatter.Curve;

            // Use Operator.Name
            if (!string.IsNullOrWhiteSpace(op.Name))
            {
                return $"{operatorTypeDisplayName}: {op.Name}";
            }

            // Use Curve.Name
            var wrapper = new Curve_OperatorWrapper(op, curveRepository);
            Curve underlyingEntity = wrapper.Curve;
            if (!string.IsNullOrWhiteSpace(underlyingEntity?.Name))
            {
                return $"{operatorTypeDisplayName}: {underlyingEntity.Name}";
            }

            // Use OperatorTypeDisplayName
            string caption = operatorTypeDisplayName;
            return caption;
        }

        /// <summary> Value Operator: display name and value or only value. </summary>
        private static string GetOperatorCaption_ForNumber(Operator op)
        {
            var wrapper = new Number_OperatorWrapper(op);
            string formattedValue = wrapper.Number.ToString("0.####");

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (string.IsNullOrWhiteSpace(op.Name))
            {
                return formattedValue;
            }
            else
            {
                return $"{op.Name}: {formattedValue}";
            }
        }

        private static string GetOperatorCaption_ForPatchInlet(Operator op)
        {
            var sb = new StringBuilder();

            var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);

            Inlet inlet = wrapper.Inlet;

            string name = inlet.GetNameWithFallback();
            DimensionEnum dimensionEnum = inlet.GetDimensionEnumWithFallback();

            // Use OperatorType DisplayName
            sb.Append(ResourceFormatter.Inlet);

            // Try Use Operator Name
            if (!string.IsNullOrWhiteSpace(name))
            {
                sb.Append($": {name}");
            }
            // Try Use Dimension
            else if (dimensionEnum != DimensionEnum.Undefined)
            {
                sb.Append($": {ResourceFormatter.GetDisplayName(dimensionEnum)}");
            }
            // Try Say 'Dimension'
            else if (inlet.DimensionIsInherited())
            {
                sb.AppendFormat($": {ResourceFormatter.Dimension}");
            }
            // Try Use List Index
            else
            {
                sb.Append($" {inlet.Position}");
            }

            if (inlet.DimensionIsInherited())
            {
                sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
            }

            // Try Use DefaultValue
            double? defaultValue = inlet.DefaultValue;
            if (defaultValue.HasValue)
            {
                sb.Append($" = {defaultValue.Value}");
            }

            return sb.ToString();
        }

        private static string GetOperatorCaption_ForPatchOutlet(Operator op)
        {
            var sb = new StringBuilder();

            var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);
            Outlet outlet = wrapper.Outlet;

            string name = outlet.GetNameWithFallback();
            DimensionEnum dimensionEnum = outlet.GetDimensionEnumWithFallback();

            // Use OperatorType DisplayName
            sb.Append(ResourceFormatter.Outlet);

            // Try Use Operator Name
            if (!string.IsNullOrWhiteSpace(name))
            {
                sb.Append($": {name}");
            }
            // Try Use Dimension
            else if (dimensionEnum != DimensionEnum.Undefined)
            {
                sb.Append($": {ResourceFormatter.GetDisplayName(dimensionEnum)}");
            }
            // Try Say 'Dimension'
            else if (outlet.DimensionIsInherited())
            {
                sb.AppendFormat($": {ResourceFormatter.Dimension}");
            }
            // Try Use List Index
            else
            {
                sb.Append($" {outlet.Position}");
            }

            // Inherited
            if (outlet.DimensionIsInherited())
            {
                sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
            }

            return sb.ToString();
        }

        private static string GetOperatorCaption_ForSample(Operator op, ISampleRepository sampleRepository)
        {
            string operatorTypeDisplayName = ResourceFormatter.Sample;

            // Use Operator.Name
            if (!string.IsNullOrWhiteSpace(op.Name))
            {
                return $"{operatorTypeDisplayName}: {op.Name}";
            }

            // Use Sample.Name
            var wrapper = new Sample_OperatorWrapper(op, sampleRepository);
            Sample underlyingEntity = wrapper.Sample;
            if (!string.IsNullOrWhiteSpace(underlyingEntity?.Name))
            {
                return $"{operatorTypeDisplayName}: {underlyingEntity.Name}";
            }

            // Use OperatorType DisplayName
            string caption = operatorTypeDisplayName;
            return caption;
        }

        private static string GetOperatorCaption_ForGetPosition(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(
                op,
                ResourceFormatter.GetPositionWithPlaceholder("{0}")); // HACK: Method delegated to will replace placeholder.
        }

        private static string GetOperatorCaption_ForSetPosition(Operator op)
        {
            return GetOperatorCaption_WithDimensionPlaceholder(
                op,
                ResourceFormatter.SetPositionWithPlaceholder("{0}")); // HACK: Method delegated to will replace placeholder.
        }

        private static string GetOperatorCaption_WithDimensionPlaceholder(Operator op, string operatorTypeDisplayNameWithPlaceholder)
        {
            var sb = new StringBuilder();

            DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnumWithFallback();
            string customDimensionName = op.GetCustomDimensionNameWithFallback();

            if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                // StandardDimension
                sb.AppendFormat(operatorTypeDisplayNameWithPlaceholder, ResourceFormatter.GetDisplayName(standardDimensionEnum));
            }
            else if (NameHelper.IsFilledIn(customDimensionName))
            {
                // CustomDimensionName
                sb.AppendFormat(operatorTypeDisplayNameWithPlaceholder, customDimensionName);
            }
            else
            {
                // No Dimension FilledIn
                sb.Append(ResourceFormatter.GetDisplayName(op));
            }

            // Inherited
            if (op.DimensionIsInherited())
            {
                sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
            }

            // Use Operator.Name
            if (!string.IsNullOrWhiteSpace(op.Name))
            {
                sb.Append($": {op.Name}");
            }

            return sb.ToString();
        }

        private static string GetOperatorCaption_ForOtherOperators(Operator op) => ResourceFormatter.GetDisplayName(op);

        // Outlet

        public static bool GetOutletVisible(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

            bool outletVisible = operatorTypeEnum != OperatorTypeEnum.PatchOutlet;
            return outletVisible;
        }

        public static string GetOutletCaption(Outlet outlet, ISampleRepository sampleRepository, ICurveRepository curveRepository)
        {
            if (outlet == null) throw new NullException(() => outlet);

            switch (outlet.Operator.GetOperatorTypeEnum())
            {
                case OperatorTypeEnum.RangeOverOutlets:
                    return GetOutletCaption_ForRangeOverOutlets(outlet);

                default:
                    return GetOutletCaption_ForOtherOperatorType(outlet, sampleRepository, curveRepository);
            }
        }

        private static string GetOutletCaption_ForRangeOverOutlets(Outlet outlet)
        {
            var sb = new StringBuilder();

            double? from = outlet.Operator.Inlets
                                 .Where(x => x.GetDimensionEnum() == DimensionEnum.From)
                                 .Select(x => x.TryGetConstantNumber())
                                 .FirstOrDefault();
            if (from.HasValue)
            {
                double? step = outlet.Operator.Inlets
                                     .Where(x => x.GetDimensionEnum() == DimensionEnum.Step)
                                     .Select(x => x.TryGetConstantNumber())
                                     .FirstOrDefault();
                if (step.HasValue)
                {
                    int listIndex = outlet.Operator.Outlets.IndexOf(outlet);

                    double value = from.Value + step.Value * listIndex;

                    sb.Append(value.ToString("0.####"));
                }
            }

            if (outlet.IsObsolete)
            {
                AppendObsoleteFlag(sb);
            }

            if (_idsVisible)
            {
                sb.Append($" ({outlet.ID})");
            }

            return sb.ToString();
        }

        private static string GetOutletCaption_ForOtherOperatorType(
            Outlet outlet,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository)
        {
            var sb = new StringBuilder();

            if (!outlet.NameOrDimensionHidden)
            {
                // Dimension or Name
                OperatorWrapper wrapper = EntityWrapperFactory.CreateOperatorWrapper(outlet.Operator, curveRepository, sampleRepository);
                string inletDisplayName = wrapper.GetOutletDisplayName(outlet);
                sb.Append(inletDisplayName);

                // RepetitionPosition
                if (outlet.RepetitionPosition.HasValue)
                {
                    sb.Append($" {outlet.RepetitionPosition + 1}");
                }
            }

            // IsObsolete
            if (outlet.IsObsolete)
            {
                AppendObsoleteFlag(sb);
            }

            // ID
            if (_idsVisible)
            {
                sb.Append($" ({outlet.ID})");
            }

            return sb.ToString();
        }

        public static float? TryGetAverageConnectionDistance(Outlet entity, EntityPositionManager entityPositionManager)
        {
            if (entity == null) throw new NullException(() => entity);

            int connectedInletCount = entity.ConnectedInlets.Count;

            if (connectedInletCount == 0)
            {
                return null;
            }

            Operator operator1 = entity.Operator;

            float aggregate = 0f;

            foreach (Inlet connectedInlet in entity.ConnectedInlets)
            {
                Operator operator2 = connectedInlet.Operator;

                EntityPosition entityPosition1 = entityPositionManager.GetOrCreateOperatorPosition(operator1.ID);
                EntityPosition entityPosition2 = entityPositionManager.GetOrCreateOperatorPosition(operator2.ID);

                float distance = Geometry.AbsoluteDistance(
                    entityPosition1.X,
                    entityPosition1.Y,
                    entityPosition2.X,
                    entityPosition2.Y);

                aggregate += distance;
            }

            aggregate /= connectedInletCount;

            return aggregate;
        }

        // Tone

        public static string GetToneGridEditNumberTitle(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return ResourceFormatter.GetScaleTypeDisplayNameSingular(entity);
        }

        // Helpers

        private static void AppendObsoleteFlag(StringBuilder sb)
        {
            if (sb.Length != 0)
            {
                sb.Append(' ');
            }

            sb.AppendFormat("({0})", ResourceFormatter.IsObsolete);
        }
    }
}
