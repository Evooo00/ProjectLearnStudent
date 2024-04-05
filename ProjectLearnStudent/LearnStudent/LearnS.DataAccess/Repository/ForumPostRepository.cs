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
    public class ForumPostRepository : Repository<ForumPost>, IForumPostRepository
    {
        private ApplicationDbContext _db;

        public ForumPostRepository(ApplicationDbContext db) : base(db)
        {
            _db =db;
        }

        public void Update(ForumPost obj)
        {
            _db.ForumPosts.Update(obj);
        }
    }
}
