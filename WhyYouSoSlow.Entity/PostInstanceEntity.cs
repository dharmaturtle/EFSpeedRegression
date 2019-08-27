using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class PostInstanceEntity
    {
        public PostInstanceEntity()
        {
            Comments = new HashSet<CommentEntity>();
        }

        public int Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Modified { get; set; }
        public int PostId { get; set; }
        [Required]
        [MaxLength(32)]
        public byte[] Hash { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("PostInstances")]
        public virtual PostEntity Post { get; set; }
        [InverseProperty("PostInstance")]
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}
