using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Models
{
    public class Note
    {
        public Note()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Note title is required.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
