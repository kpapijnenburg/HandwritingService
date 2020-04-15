using HandwritingService.Domain;
using KPA.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HandwritingService.DAL.Repositories
{
    public class HandwritingRepository : Repository<HandwritingContext, Handwriting>
    {
        public HandwritingRepository(HandwritingContext context): base(context)
        {

        }

    }
}
