using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class Message
    {
        private long id = 0;
        private long contactID = 0;
        private string content = string.Empty;
        private int statusMessageID = 0;
        private DateTime? fecTransac = DateTime.Now;
        [JsonIgnore]
        public virtual StatusMessage? statusMessage { get; set; }
        [JsonIgnore]
        public virtual ChatsList? chatsList { get; set; }
        public long ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public long ContactID
        {
            get { return this.contactID; }
            set { this.contactID = value; }
        }
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        public int StatusMessageID
        {
            get { return this.statusMessageID; }
            set { this.statusMessageID = value; }
        }
        public DateTime? FecTransac
        {
            get { return this.fecTransac; }
            set { this.fecTransac = value; }
        }
    }
}