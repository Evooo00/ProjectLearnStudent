using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnS.Models.ViewModels
{
    public class QuizVM
    {
        public Quiz Quiz { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SectionList { get; set; }

        public List<Question> Questions { get; set; }

    }

}