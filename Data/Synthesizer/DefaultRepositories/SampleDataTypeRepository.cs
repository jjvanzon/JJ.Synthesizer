using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class SampleDataTypeRepository : RepositoryBase<SampleDataType, int>, ISampleDataTypeRepository
	{
		public SampleDataTypeRepository(IContext context)
			: base(context)
		{ }
	}
}
