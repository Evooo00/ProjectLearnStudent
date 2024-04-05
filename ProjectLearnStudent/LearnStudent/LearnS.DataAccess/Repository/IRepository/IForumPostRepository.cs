using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IForumPostRepository : IRepository<ForumPost>
    {
        void Update(ForumPost obj);
    }
}
