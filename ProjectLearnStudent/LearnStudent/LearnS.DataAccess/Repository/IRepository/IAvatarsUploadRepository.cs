using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IAvatarsUploadRepository : IRepository<AvatarsUpload>
    {
        Task<AvatarsUpload> FindAsync(int id);
        void Update(AvatarsUpload obj);

    }
}
