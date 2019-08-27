using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class TagEntity
    {
        public TagEntity()
        {
            Tag_AcquiredComments = new HashSet<Tag_AcquiredCommentEntity>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name {
            get => _Name;
            set {
                if (value.Length > 250) throw new ArgumentOutOfRangeException($"String too long! It was {value.Length} long, and Name has a maximum length of 250. Attempted value: {value}");
                _Name = value;
            }
        }
        private string _Name;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Tags")]
        public virtual UserEntity User { get; set; }
        [InverseProperty("Tag")]
        public virtual ICollection<Tag_AcquiredCommentEntity> Tag_AcquiredComments { get; set; }
    }
}
