using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToIDNameExtensions
    {
        public static IDName ToIDName(this AudioFileFormat entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this SampleDataType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this Document entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this NodeType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }

        public static IDName ToIDName(this InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDName
            {
                ID = entity.ID,
                Name = entity.Name
            };
        }
    }
}
