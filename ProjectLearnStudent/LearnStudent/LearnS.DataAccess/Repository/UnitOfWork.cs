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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ISectionRepository Section { get; private set; }

        public IAvatarsUploadRepository AvatarsUpload { get; private set; }
        public ILearningMaterialsRepository LearningMaterials { get; private set; }

        public IQuizRepository Quiz { get; private set; }

        public IQuestionRepository Question { get; private set; }

        public IApplicationUserRepository User { get; private set; }
        public IExampleTaskRepository ExampleTask { get; private set; }

        public IForumPostRepository ForumPost { get; private set; }
        public IForumThreadRepository ForumThread { get; private set; }
        public IForumCommentRepository ForumComment { get; private set; }
        public IForumRatingRepository ForumRating { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
       public IAvatarPurchaseRepositroy AvatarPurchase { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Section = new SectionRepository(_db);
            AvatarsUpload = new AvatarsUploadRepository(_db);
            LearningMaterials = new LearningMaterialsRepository(_db);
            Quiz = new QuizRepository(_db);
            Question = new QuestionRepository(_db);
            User = new ApplicationUserRepository(_db);
            ExampleTask = new ExampleTaskRepository(_db);
            ForumPost = new ForumPostRepository(_db);
            ForumThread = new ForumThreadRepository(_db);
            ForumComment = new ForumCommentRepository(_db);
            ForumRating = new ForumRatingRepository(_db);
            AvatarPurchase = new AvatarPurchaseRepositroy(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task<List<ForumThread>> GetAllThreadsAsync(bool includeUser)
        {
            if (includeUser)
            {
                return await _db.ForumThreads.Include(t => t.User).ToListAsync();
            }
            else
            {
                return await _db.ForumThreads.ToListAsync();
            }
        }

    }
}
