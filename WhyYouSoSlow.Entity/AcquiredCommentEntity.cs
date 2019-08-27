using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class AcquiredCommentEntity
    {
        public AcquiredCommentEntity()
        {
            Tag_AcquiredComments = new HashSet<Tag_AcquiredCommentEntity>();
        }

        public int UserId { get; set; }
        public int CommentId { get; set; }
        public byte CommentState { get; set; }
        public short Meta { get; set; }
        public short Meta2 { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Created { get; set; }
        public int MetaId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CommentId")]
        [InverseProperty("AcquiredComments")]
        public virtual CommentEntity Comment { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("AcquiredComments")]
        public virtual UserEntity User { get; set; }
        [InverseProperty("AcquiredComment")]
        public virtual ICollection<Tag_AcquiredCommentEntity> Tag_AcquiredComments { get; set; }
    }
}
