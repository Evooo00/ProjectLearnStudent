using LearnS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LearnS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<AvatarsUpload> AvatarsUploads { get; set; }

        public DbSet<LearningMaterials> LearningMaterials { get; set; }
        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<ExampleTasks> ExampleTasks { get; set; }

        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ForumRating> ForumRatings { get; set; }

        public DbSet<AvatarPurchase> AvatarPurchases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ForumThread>()
             .HasMany(thread => thread.ForumPosts)
             .WithOne(post => post.ForumThread)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumPost>()
                 .HasMany(fp => fp.ForumRatings)
                 .WithOne(fr => fr.ForumPost)
                 .HasForeignKey(fr => fr.ForumPostId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumPost>()
        .HasMany(fp => fp.ForumComments)
        .WithOne(fc => fc.ForumPost)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumComment>()
                .HasOne(fc => fc.ForumPost)
                .WithMany(fp => fp.ForumComments)
                .HasForeignKey(fc => fc.ForumPostId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<ForumRating>()
                .HasOne(fr => fr.ForumPost)
                .WithMany(fp => fp.ForumRatings)
                .HasForeignKey(fr => fr.ForumPostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AvatarPurchase>()
                .HasOne(ap => ap.User)
                .WithMany(u => u.AvatarPurchases)
                .HasForeignKey(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvatarPurchase>()
             .HasOne(ap => ap.Avatar)
             .WithMany()
             .HasForeignKey(ap => ap.AvatarId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Math", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Automatics", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Physics", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Section>().HasData(
                new Section
                {
                    Id = 1,
                    Title = "Statystyka",
                    Description = "Statystyka opisowa",
                    Author = "eTrapez",
                    CategoryId = 1
                },
                  new Section
                  {
                      Id = 2,
                      Title = "Statystyka",
                      Description = "Przedziały ufności. Estymacja przedziałowe.",
                      Author = "eTrapez",
                      CategoryId = 1
                  },
                    new Section
                    {
                        Id = 3,
                        Title = "Statystyka",
                        Description = "Parametryczne testy istotności",
                        Author = "eTrapez",
                        CategoryId = 1
                    }


                );
  

         

             modelBuilder.Entity<AvatarsUpload>().HasData(
    new AvatarsUpload
    {
        Id = 1,
        Name = "test",
        ImageUrl = "",
        CoinsValue = 10,
     
    },
    new AvatarsUpload
    {
        Id = 2, 
        Name = "kot",
        ImageUrl = "",
        CoinsValue = 20,
    }
);


            modelBuilder.Entity<LearningMaterials>().HasData(
                new LearningMaterials
                {
                    Id = 1,
                    Title = "statystyka",
                    Description = "test213",
                    Author = "eTrapez",
                    SectionId = 2

                }
                );


            modelBuilder.Entity<Quiz>().HasData(
         new Quiz
         {
             Id = 1,
             Title = "Quiz matematyka test",
             SectionId = 2,
             Points = 100,
             Coins = 100,
         }
     );
            modelBuilder.Entity<Question>().HasData(
        new Question
        {
            Id = 1,
            QuestionText = "Ile to 22 + 2?",
            QuizId = 1,
            Answer1 = "24",
            Answer2 = "34",
            Answer3 = "44",
            Answer4 = "54",
            IsCorrect = 1
        }
    );
            modelBuilder.Entity<ExampleTasks>().HasData(
                new ExampleTasks
                {
                    Id = 1,
                    Title = "Zadania z statystyki",
                    ExampleTask = "Ile to 2 + 2?",
                    Solution = "2 + 2 to 4 xD",
                    SectionId = 2

                }
                );
            modelBuilder.Entity<ForumThread>().HasData(
             new ForumThread
             {
                 Id = 1,
                 Title = "Tytuł testowy 1",
                 Content = "zawartość testowa",
                 CreatedAt = DateTime.Now,
                 UserId = "f096fef9-cdf0-4298-81b1-52925b2ef44d",




             }
             );
            modelBuilder.Entity<ForumPost>().HasData(
             new ForumPost
             {
                 Id = 1,
                 Content = "zawartość testowa",
                 CreatedAt = DateTime.Now,
                 ForumThreadId = 1,
                 UserId = "f096fef9-cdf0-4298-81b1-52925b2ef44d",


             }
             );
            modelBuilder.Entity<ForumRating>().HasData(
               new ForumRating
               {
                   Id = 1,
                   Value = 1,
                   ForumPostId = 1,
                   UserId = "f096fef9-cdf0-4298-81b1-52925b2ef44d",



               }
               );
            modelBuilder.Entity<ForumComment>().HasData(
        new ForumComment
        {
            Id = 1,
            Content = "komentarz",
            CreatedAt = DateTime.Now,
            ForumPostId = 1,
            UserId = "f096fef9-cdf0-4298-81b1-52925b2ef44d"
        }
    );

            modelBuilder.Ignore<CultureInfo>();
        }
    }
}
