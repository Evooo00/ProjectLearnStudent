using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository
{
    public class SectionRepository : Repository<Section>, ISectionRepository 
    {
        private ApplicationDbContext _db;
        public SectionRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

      
        
        public void Update(Section obj)
        {
            _db.Sections.Update(obj);
        }
    }
}
