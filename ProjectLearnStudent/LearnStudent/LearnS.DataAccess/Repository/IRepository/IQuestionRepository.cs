using LearnS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public interface IQuestionRepository : IRepository<Question>
        {
            List<Question> GetAll(int quizId);
            void Update(Question obj);
        }
    }
}
