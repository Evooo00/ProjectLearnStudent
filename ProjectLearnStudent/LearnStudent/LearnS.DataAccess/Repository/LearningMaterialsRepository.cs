using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository
{
    public class LearningMaterialsRepository : Repository<LearningMaterials>, ILearningMaterialsRepository
    {
        private ApplicationDbContext _db;
        public LearningMaterialsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(LearningMaterials obj)
        {
            _db.LearningMaterials.Update(obj);
        }
    }
}
