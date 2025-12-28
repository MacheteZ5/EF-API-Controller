using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class StatusUser
    {
        private byte id = 0;
        private string name = string.Empty;
        private string description = string.Empty;
        private bool active = true;
        private DateTime? dateCreated = DateTime.MinValue;

        [JsonIgnore]
        public virtual IEnumerable<User>? users { get; set; }

        public byte ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }
        public DateTime? DateCreated
        {
            get { return this.dateCreated; }
            set { this.dateCreated = value; }
        }
    }
}