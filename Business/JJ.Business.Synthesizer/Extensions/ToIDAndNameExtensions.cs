using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    internal static class ToIDAndNameExtensions
    {
        public static IDAndName ToIDAndName(this Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }

        public static IDAndName ToIDAndName(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }

        public static IDAndName ToIDAndName(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            return new IDAndName
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }
    }
}
