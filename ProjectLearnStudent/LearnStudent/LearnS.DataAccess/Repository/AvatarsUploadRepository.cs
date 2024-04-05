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
    public class AvatarsUploadRepository : Repository<AvatarsUpload>, IAvatarsUploadRepository
    {
        private ApplicationDbContext _db;
        public AvatarsUploadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(AvatarsUpload obj)
        {
            _db.AvatarsUploads.Update(obj);
        }
        public async Task<AvatarsUpload> FindAsync(int id)
        {
            return await _db.AvatarsUploads.FindAsync(id);
        }
    }
}
