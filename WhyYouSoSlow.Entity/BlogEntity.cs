using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class BlogEntity
    {
        public BlogEntity()
        {
            Posts = new HashSet<PostEntity>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name {
            get => _Name;
            set {
                if (value.Length > 100) throw new ArgumentOutOfRangeException($"String too long! It was {value.Length} long, and Name has a maximum length of 100. Attempted value: {value}");
                _Name = value;
            }
        }
        private string _Name;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Blogs")]
        public virtual UserEntity User { get; set; }
        [InverseProperty("Blog")]
        public virtual ICollection<PostEntity> Posts { get; set; }
    }
}
