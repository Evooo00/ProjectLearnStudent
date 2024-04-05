using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository
{
    public class ForumThreadRepository : Repository<ForumThread>, IForumThreadRepository
    {
        private ApplicationDbContext _db;

        public ForumThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<ForumThread>> GetAllThreadsAsync(bool includeUser = false)
        {
            if (includeUser)
            {
                return await _db.ForumThreads.Include(ft => ft.User).ToListAsync();
            }
            else
            {
                return await _db.ForumThreads.ToListAsync();
            }
        }

        public void Update(ForumThread obj)
        {
            _db.ForumThreads.Update(obj);
        }
       
    }
}
