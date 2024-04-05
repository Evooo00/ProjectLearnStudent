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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private ApplicationDbContext _db;
        public QuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      public List<Question> GetAll(int quizId)
{
    return _db.Questions.Where(q => q.QuizId == quizId).ToList();
}


        public void Update(Question obj)
        {
            _db.Questions.Update(obj);
        }
    }
}
