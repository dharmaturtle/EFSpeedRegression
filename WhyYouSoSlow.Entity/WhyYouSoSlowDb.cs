using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhyYouSoSlow.Entity
{
    public partial class WhyYouSoSlowDb : DbContext
    {
        public virtual DbSet<AcquiredCommentEntity> AcquiredComment { get; set; }
        public virtual DbSet<CommentEntity> Comment { get; set; }
        public virtual DbSet<BlogEntity> Blog { get; set; }
        public virtual DbSet<PostEntity> Post { get; set; }
        public virtual DbSet<PostInstanceEntity> PostInstance { get; set; }
        public virtual DbSet<TagEntity> Tag { get; set; }
        public virtual DbSet<Tag_AcquiredCommentEntity> Tag_AcquiredComment { get; set; }
        public virtual DbSet<UserEntity> User { get; set; }

        public WhyYouSoSlowDb(DbContextOptions<WhyYouSoSlowDb> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AcquiredCommentEntity>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CommentId });

                entity.HasIndex(e => e.CommentId);

                entity.HasIndex(e => e.MetaId);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.AcquiredComments)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcquiredComment_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AcquiredComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcquiredComment_User");
            });

            modelBuilder.Entity<CommentEntity>(entity =>
            {
                entity.HasIndex(e => e.TemplateId);

                entity.HasIndex(e => new { e.PostInstanceId, e.TemplateId, e.Index })
                    .HasName("AK_Comment")
                    .IsUnique();

                entity.HasOne(d => d.PostInstance)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_PostInstance");
            });

            modelBuilder.Entity<BlogEntity>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_User");
            });

            modelBuilder.Entity<PostEntity>(entity =>
            {
                entity.HasIndex(e => e.BlogId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Blog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<PostInstanceEntity>(entity =>
            {
                entity.HasIndex(e => e.Hash)
                    .HasName("AK_PostInstance_AcquireHash")
                    .IsUnique();

                entity.HasIndex(e => e.PostId);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostInstances)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostInstance_Post");
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.Name })
                    .HasName("AK_Tag__UserId_Name")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_User");
            });

            modelBuilder.Entity<Tag_AcquiredCommentEntity>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.UserId, e.CommentId });

                entity.HasIndex(e => new { e.UserId, e.CommentId });

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Tag_AcquiredComments)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_AcquiredComment_Tag");

                entity.HasOne(d => d.AcquiredComment)
                    .WithMany(p => p.Tag_AcquiredComments)
                    .HasForeignKey(d => new { d.UserId, d.CommentId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_AcquiredComment_AcquiredComment");
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.DisplayName)
                    .HasName("AK_User__DisplayName")
                    .IsUnique();

            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}

