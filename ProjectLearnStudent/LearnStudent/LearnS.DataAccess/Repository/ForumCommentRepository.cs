using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository
{
    public class ForumCommentRepository : Repository<ForumComment>, IForumCommentRepository
    {
        private ApplicationDbContext _db;

        public ForumCommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ForumComment obj)
        {
            _db.ForumComments.Update(obj);
        }
        public IEnumerable<ForumComment> Find(Expression<Func<ForumComment, bool>> expression)
        {
            return _db.ForumComments.Where(expression).ToList();
        }
    }
}
