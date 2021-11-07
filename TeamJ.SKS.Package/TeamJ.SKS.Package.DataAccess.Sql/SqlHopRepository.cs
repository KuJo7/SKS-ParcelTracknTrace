using System;
using System.Collections.Generic;
using System.Linq;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlHopRepository : IHopRepository
    {
        private readonly IContext _context;

        public SqlHopRepository(IContext context)
        {
            _context = context;
        }

        public void Create(DALHop dalHop)
        {
            _context.Hops.Add(dalHop);
            _context.SaveChanges();
        }

        public void Update(DALHop dalHop)
        {
            _context.Hops.Update(dalHop);
            _context.SaveChanges();
        }

        public void Delete(DALHop dalHop)
        {
            _context.Hops.Remove(dalHop);
            _context.SaveChanges();
        }

        public IEnumerable<DALHop> GetAllHops()
        {
            return _context.Hops;
        }

        public DALHop GetByCode(string code)
        {
            return _context.Hops.Find(code);
        }

        public IEnumerable<DALHop> GetByHopType(string hopType)
        {
            return _context.Hops.Where(hop => hop.HopType == hopType);
        }

        public IEnumerable<DALHop> GetByLevel(int level)
        {
            return _context.Hops.Where(hop => hop.Level == level);
        }

        //Person GetSinglePersonByFirstName(string searchPattern);
        //Person GetSinglePersonByLastName(string searchPattern);
        //ICollection<Person> GetAllPeopleWithEmptyCompany();
    }
}
