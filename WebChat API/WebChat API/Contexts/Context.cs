using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebChat_API.Models;

namespace WebChat_API.Contexts
{
    public class Context : DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<ChatsList> chatsList { get; set; }
        public DbSet<Room> room { get; set; }
        public DbSet<Message> message { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");
                entity.HasKey(user => user.UserName).HasName("UserName");
                entity.Property(user => user.Password).HasColumnName("Password");
                entity.Property(user => user.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(100);
                entity.Property(user => user.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(100);
                entity.Property(user => user.Birthdate).HasColumnName("BirthDate");
                entity.Property(user => user.Email).HasColumnName("Email");
                entity.Property(user => user.StatusID).IsRequired().HasColumnName("StatusID");
                entity.HasOne(user => user.statusUser).WithMany(status => status.users).HasForeignKey(user => user.StatusID);
            });

            modelBuilder.Entity<ChatsList>(entity =>
            {
                entity.ToTable("CHATSLIST");
                entity.HasKey(chatsList => chatsList.ID).HasName("ID");
                entity.Property(chatsList => chatsList.Name).IsRequired().HasColumnName("Name");
                entity.Property(chatsList => chatsList.Description).IsRequired().HasColumnName("Description");
                entity.Property(chatsList => chatsList.Active).IsRequired().HasColumnName("active");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("ROOM");
                entity.HasKey(room => new { room.UserName, room.ChatsListID });
                entity.Property(room => room.UserName).IsRequired().HasColumnName("UserName");
                entity.Property(room => room.ChatsListID).IsRequired().HasColumnName("ChatsListID");
                entity.HasOne(room => room.user).WithMany(users => users.rooms).HasForeignKey(user => user.UserName);
                entity.HasOne(room => room.chatsList).WithMany(chatsLists => chatsLists.rooms).HasForeignKey(chatsList => chatsList.ChatsListID);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("MESSAGE");
                entity.HasKey(message => message.ID).HasName("ID");
                entity.Property(message => message.Content).HasColumnName("Content");
                entity.Property(message => message.StatusMessage).IsRequired().HasColumnName("StatusMessage");
                entity.Property(message => message.UserNameRoom).IsRequired().HasColumnName("UserNameRoom");
                entity.Property(message => message.ChatsListIDRoom).IsRequired().HasColumnName("ChatsListIDRoom");
                entity.HasOne(message => message.room).WithMany(room => room.messages).HasForeignKey(message => new { message.UserNameRoom, message.ChatsListIDRoom });
            });
        }
    }
}
