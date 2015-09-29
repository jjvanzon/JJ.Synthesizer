using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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

        public static IDAndName ToIDAndDisplayName(this ChildDocumentType entity)
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

        public static IDAndName ToIDAndName(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                ID = entity.ID,
                Name = entity.Name
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

        public static IDAndName ToIDAndName(this ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string displayName = PropertyDisplayNames.ResourceManager.GetString(entity.Name);

            return new IDAndName
            {
                ID = entity.ID,
                Name = displayName
            };
        }
    }
}
