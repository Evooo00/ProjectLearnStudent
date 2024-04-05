using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IForumCommentRepository : IRepository<ForumComment>
    {
        IEnumerable<ForumComment> Find(Expression<Func<ForumComment, bool>> expression);
        void Update(ForumComment obj);
    }
}
