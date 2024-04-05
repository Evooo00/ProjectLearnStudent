using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IForumThreadRepository : IRepository<ForumThread>
    {
        void Update(ForumThread obj);
        
        Task<List<ForumThread>> GetAllThreadsAsync(bool includeUser = false);
    }
}
