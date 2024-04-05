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
    public class ForumRatingRepository : Repository<ForumRating>, IForumRatingRepository
    {
        private ApplicationDbContext _db;

        public ForumRatingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ForumRating obj)
        {
            _db.ForumRatings.Update(obj);
        }

        public IEnumerable<ForumRating> Find(Expression<Func<ForumRating, bool>> expression)
        {
           
            return _db.ForumRatings.Where(expression).ToList();
        }
    }
}
