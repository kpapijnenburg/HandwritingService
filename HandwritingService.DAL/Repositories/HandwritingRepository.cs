using HandwritingService.Domain;
using KPA.Database.Abstractions;

namespace HandwritingService.DAL.Repositories
{
    public class HandwritingRepository : Repository<HandwritingContext, Handwriting>
    {
        public HandwritingRepository(HandwritingContext context): base(context)
        {

        }
    }
}
