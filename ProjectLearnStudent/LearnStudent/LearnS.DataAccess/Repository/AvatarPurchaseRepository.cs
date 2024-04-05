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
    public class AvatarPurchaseRepositroy : Repository<AvatarPurchase>, IAvatarPurchaseRepositroy
    {
        private ApplicationDbContext _db;
        public AvatarPurchaseRepositroy(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(AvatarPurchase obj)
        {
            _db.AvatarPurchases.Update(obj);
        }
        public async Task<AvatarsUpload> FindAsync(int id)
        {
            return await _db.AvatarsUploads.FindAsync(id);
        }
    }
}