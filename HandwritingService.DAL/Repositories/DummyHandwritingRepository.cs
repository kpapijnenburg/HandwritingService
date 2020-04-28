using HandwritingService.Domain;
using KPA.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingService.DAL.Repositories
{
    public class DummyHandwritingRepository : IRepository<Handwriting>
    {
        public List<Handwriting> Handwritings { get; set; }

        public DummyHandwritingRepository()
        {
            Handwritings = new List<Handwriting>();
        }

        public Task<Handwriting> Create(Handwriting entity)
        {
            var count = Handwritings.Count;
            entity.Id = count += 1;

            Handwritings.Add(entity);

            return Task.FromResult(entity);
        }

        public Task Delete(Handwriting entity)
        {
            throw new NotImplementedException();
        }

        public Task<Handwriting> Find(Expression<Func<Handwriting, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Handwriting>> FindAll(Expression<Func<Handwriting, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Handwriting>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Handwriting> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Handwriting> Update(Handwriting entity)
        {
            throw new NotImplementedException();
        }
    }
}
