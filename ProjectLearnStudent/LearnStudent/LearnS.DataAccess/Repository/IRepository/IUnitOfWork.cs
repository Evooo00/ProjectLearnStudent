using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISectionRepository Section { get; }

        IAvatarsUploadRepository AvatarsUpload {  get; }
        ILearningMaterialsRepository LearningMaterials { get; }

        IQuizRepository Quiz { get; }

        IQuestionRepository Question { get; }

        IApplicationUserRepository User { get; }
        IExampleTaskRepository ExampleTask { get;}
        IForumPostRepository ForumPost { get; }
        IForumThreadRepository ForumThread { get; }
        IForumCommentRepository ForumComment { get; }
        IForumRatingRepository ForumRating { get; }
        IAvatarPurchaseRepositroy AvatarPurchase {  get; }

        void Save();
        Task<List<ForumThread>> GetAllThreadsAsync(bool includeUser);


    }
}
