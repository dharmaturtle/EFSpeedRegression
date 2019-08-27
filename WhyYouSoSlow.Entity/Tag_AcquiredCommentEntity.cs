using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class Tag_AcquiredCommentEntity
    {
        public int TagId { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }

        [ForeignKey("UserId,CommentId")]
        [InverseProperty("Tag_AcquiredComments")]
        public virtual AcquiredCommentEntity AcquiredComment { get; set; }
        [ForeignKey("TagId")]
        [InverseProperty("Tag_AcquiredComments")]
        public virtual TagEntity Tag { get; set; }
    }
}
