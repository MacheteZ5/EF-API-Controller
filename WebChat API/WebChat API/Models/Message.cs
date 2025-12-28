using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private long id = 0;
        private string content = string.Empty;
        private bool statusMessage = true;
        private DateTime? dateCreated = DateTime.MinValue;
        private string userNameRoom = string.Empty;
        private long chatsListIDRoom = 0;

        [JsonIgnore]
        public virtual Room? room { get; set; }

        public long ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public bool StatusMessage
        {
            get { return this.statusMessage; }
            set { this.statusMessage = value; }
        }
        public DateTime? DateCreated
        {
            get { return this.dateCreated; }
            set { this.dateCreated = value; }
        }
        public string UserNameRoom
        {
            get { return this.userNameRoom; }
            set { this.userNameRoom = value; }
        }
        public long ChatsListIDRoom
        {
            get { return this.chatsListIDRoom; }
            set { this.chatsListIDRoom = value; }
        }
    }
}