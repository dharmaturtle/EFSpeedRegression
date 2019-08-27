using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class PostEntity
    {
        public PostEntity()
        {
            PostInstances = new HashSet<PostInstanceEntity>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Description {
            get => _Description;
            set {
                if (value.Length > 100) throw new ArgumentOutOfRangeException($"String too long! It was {value.Length} long, and Description has a maximum length of 100. Attempted value: {value}");
                _Description = value;
            }
        }
        private string _Description;
        public int BlogId { get; set; }

        [ForeignKey("BlogId")]
        [InverseProperty("Posts")]
        public virtual BlogEntity Blog { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public virtual UserEntity User { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<PostInstanceEntity> PostInstances { get; set; }
    }
}
