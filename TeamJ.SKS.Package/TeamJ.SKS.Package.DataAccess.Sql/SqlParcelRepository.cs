using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlParcelRepository : IParcelRepository
    {
        private readonly IContext _context;

        public SqlParcelRepository(IContext context)
        {
            _context = context;
        }

        public void Create(DALParcel dalParcel)
        {
            _context.Parcels.Add(dalParcel);
            _context.SaveChanges();
        }

        public void Delete(DALParcel dalParcel)
        {
            _context.Parcels.Remove(dalParcel);
            _context.SaveChanges();
        }

        public void Update(DALParcel dalParcel)
        {
            _context.Parcels.Update(dalParcel);
            _context.SaveChanges();
        }

        public DALParcel GetById(string trackingID) 
        {
            return _context.Parcels.Find(trackingID);
        }

        public DALParcel GetByCode(string code)
        {
            return _context.Parcels.Find(code);
        }

        public IEnumerable<DALParcel> GetByState(DALStateEnum state)
        {
            return _context.Parcels.Where(parcel => parcel.State == state);
        }

        //Person GetSinglePersonByFirstName(string searchPattern);
        //Person GetSinglePersonByLastName(string searchPattern);
        //ICollection<Person> GetAllPeopleWithEmptyCompany();
    }
}
