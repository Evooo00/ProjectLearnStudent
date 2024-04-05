using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnS.Models;

namespace LearnS.Models.ViewModels
{
    public class AvatarsUploadVM
    {
        public AvatarsUpload AvatarsUpload { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AvatarsList { get; set; }

        

    }
}
