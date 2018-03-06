using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Aggregates;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class SampleRepository : RepositoryBase<Sample, int>, ISampleRepository
	{
		public SampleRepository(IContext context)
			: base(context)
		{ }

		/// <summary> base does nothing </summary>
		public virtual byte[] TryGetBytes(int sampleID) => throw new RepositoryMethodNotImplementedException();

		public byte[] GetBytes(int sampleID)
		{
			byte[] bytes = TryGetBytes(sampleID);
			if (bytes == null)
			{
				throw new NotFoundException<byte[]>(new { sampleID });
			}
			return bytes;
		}

		/// <summary> base does nothing </summary>
		public virtual void SetBytes(int sampleID, byte[] bytes) => throw new RepositoryMethodNotImplementedException();
	}
}
