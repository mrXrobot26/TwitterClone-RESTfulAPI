using Models.MyModels.ProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Post
{
    public class PostDTO
    {
        public string postContant { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}
