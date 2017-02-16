using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class InterpolationTypeRepository : DefaultRepositories.InterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Block");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Line");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Stripe");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Cubic");
            RepositoryHelper.EnsureEnumEntity(this, 5, "Hermite");

            InterpolationType blockInterpolationType = Get(1);
            blockInterpolationType.SortOrder = 1;

            InterpolationType lineInterpolationType = Get(2);
            lineInterpolationType.SortOrder = 3;

            InterpolationType stripeInterpolationType = Get(3);
            stripeInterpolationType.SortOrder = 2;

            InterpolationType curveInterpolationType = Get(4);
            curveInterpolationType.SortOrder = 4;

            InterpolationType hermiteInterpolationType = Get(5);
            hermiteInterpolationType.SortOrder = 5;
        }
    }
}