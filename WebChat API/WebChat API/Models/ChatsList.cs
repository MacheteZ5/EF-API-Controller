using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class ChatsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private long id = 0;
        private string name = string.Empty;
        private string description = string.Empty;
        private bool active = true;
        private DateTime? dateCreated = DateTime.MinValue;

        [JsonIgnore]
        public virtual IEnumerable<Room>? rooms { get; set; }

        public long ID
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