using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class Room
    {
        private string userName = string.Empty;
        private long chatsListID = 0;
        private DateTime? dateCreated = DateTime.MinValue;

        [JsonIgnore]
        public virtual User? user { get; set; }
        [JsonIgnore]
        public virtual ChatsList? chatsList { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Message> messages { get; set; }

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        public long ChatsListID
        {
            get { return this.chatsListID; }
            set { this.chatsListID = value; }
        }
        public DateTime? DateCreated
        {
            get { return this.dateCreated; }
            set { this.dateCreated = value; }
        }
    }
}
