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
    public class ExampleTaskRepository : Repository<ExampleTasks>, IExampleTaskRepository
    {
        private ApplicationDbContext _db;

        public ExampleTaskRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ExampleTasks obj)
        {
            _db.ExampleTasks.Update(obj);
        }
    }
}
