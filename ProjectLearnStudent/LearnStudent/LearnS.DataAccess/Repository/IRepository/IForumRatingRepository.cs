using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IForumRatingRepository : IRepository<ForumRating>
    {
        IEnumerable<ForumRating> Find(Expression<Func<ForumRating, bool>> expression);
        void Update(ForumRating obj);
    }
}
