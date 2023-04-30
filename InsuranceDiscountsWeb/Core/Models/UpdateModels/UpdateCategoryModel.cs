using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateCategoryModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? CategoryName { get; set; }
    }
}
