﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToIDAndNameExtensions
    {
        public static IDAndName ToIDAndDisplayName(this AudioFileFormat entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndName(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }

        public static IDAndName ToIDAndDisplayName(this Dimension entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = ResourceHelper.GetDisplayName(entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this DimensionEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayName(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndName(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndDisplayName(this InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this InterpolationTypeEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayName(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this NodeType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this OperatorType operatorType)
        {
            if (operatorType == null) throw new NullException(() => operatorType);

            var viewModel = new IDAndName
            {
                ID = operatorType.ID,
                Name = ResourceHelper.GetPropertyDisplayName(operatorType.Name)
            };

            return viewModel;
        }

        public static IDAndName ToIDAndName(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndDisplayName(this ResampleInterpolationTypeEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayName(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this CollectionRecalculationEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayName(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this SpeakerSetupEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayName(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndName(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndName(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndDisplayName(this SampleDataType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayName(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndName(this Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDAndName ToIDAndDisplayNamePlural(this ScaleTypeEnum enumValue)
        {
            string displayName = ResourceHelper.GetDisplayNamePlural(enumValue);

            return new IDAndName
            {
                ID = (int)enumValue,
                Name = displayName
            };
        }

        public static IDAndName ToIDAndDisplayNamePlural(this ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = ResourceHelper.GetDisplayNamePlural(entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }
    }
}