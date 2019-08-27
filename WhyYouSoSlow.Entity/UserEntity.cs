using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class UserEntity
    {
        public UserEntity()
        {
            AcquiredComments = new HashSet<AcquiredCommentEntity>();
            Blogs = new HashSet<BlogEntity>();
            Posts = new HashSet<PostEntity>();
            Tags = new HashSet<TagEntity>();
        }

        public int Id { get; set; }

        [StringLength(32)]
        public string DisplayName {
            get => _DisplayName;
            set {
                if (value.Length > 32) throw new ArgumentOutOfRangeException($"String too long! It was {value.Length} long, and DisplayName has a maximum length of 32. Attempted value: {value}");
                _DisplayName = value;
            }
        }
        private string _DisplayName;

        [InverseProperty("User")]
        public virtual ICollection<AcquiredCommentEntity> AcquiredComments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<BlogEntity> Blogs { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<PostEntity> Posts { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}

