using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebChat_API.Models;

namespace WebChat_API.Contexts
{
    public class Context : DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<ChatsList> chatsList { get; set; }
        public DbSet<Message> message { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(user => user.UserName).HasName("UserName");
                entity.Property(user => user.Password).HasColumnName("Password");
                entity.Property(user => user.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(100);
                entity.Property(user => user.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(100);
                entity.Property(user => user.Birthdate).HasColumnName("BirthDate");
                entity.Property(user => user.Email).HasColumnName("Email");
                entity.Property(user => user.StatusUserID).IsRequired(false).HasColumnName("StatusID");
                entity.HasOne(user => user.statusUser).WithMany(status => status.Users).HasForeignKey(status => status.StatusUserID);
            });

            modelBuilder.Entity<ChatsList>(entity =>
            {
                entity.ToTable("ChatsList", trg => trg.HasTrigger("trg_ValidateChatsListCreation"));
                entity.HasKey(user => user.ID).HasName("ID");
                entity.Property(user => user.FirstUser).IsRequired().HasColumnName("FUser");
                entity.Property(user => user.SecondUser).IsRequired().HasColumnName("SUser");
                entity.HasOne(chatsList => chatsList.FUsers).WithMany(user => user.firstChatsList).HasForeignKey(x => x.FirstUser);
                entity.HasOne(chatsList => chatsList.SUsers).WithMany(user => user.secondChatsList).HasForeignKey(x => x.SecondUser);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");
                entity.HasKey(user => user.ID).HasName("ID");
                entity.Property(user => user.ContactID).HasColumnName("ContactID");
                entity.Property(user => user.Content).IsRequired().HasColumnName("Message");
                entity.Property(user => user.StatusMessageID).IsRequired().HasColumnName("StatusMessageID");
                entity.Property(user => user.FecTransac).IsRequired(false).HasColumnName("FecTransac");
                entity.HasOne(message => message.statusMessage).WithMany(status => status.Messages).HasForeignKey(status => status.ID);
                entity.HasOne(message => message.chatsList).WithMany(chatsList => chatsList.Messages).HasForeignKey(chatsList => chatsList.ID);
            });
        }
    }
}
