using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhyYouSoSlow.Entity
{
    public partial class CommentEntity
    {
        public CommentEntity()
        {
            AcquiredComments = new HashSet<AcquiredCommentEntity>();
        }

        public int Id { get; set; }
        public int PostInstanceId { get; set; }
        public int TemplateId { get; set; }
        public byte? Index { get; set; }

        [ForeignKey("PostInstanceId")]
        [InverseProperty("Comments")]
        public virtual PostInstanceEntity PostInstance { get; set; }
        [InverseProperty("Comment")]
        public virtual ICollection<AcquiredCommentEntity> AcquiredComments { get; set; }
    }
}
