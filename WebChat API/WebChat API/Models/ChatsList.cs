using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class ChatsList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private long id = 0;
        private string firstUser = string.Empty;
        private string secondUser = string.Empty;

        [JsonIgnore]
        public virtual User? FUsers { get; set; }
        [JsonIgnore]
        public virtual User? SUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message>? Messages { get; set; }

        public long ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string FirstUser
        {
            get { return this.firstUser; }
            set { this.firstUser = value; }
        }
        public string SecondUser
        {
            get { return this.secondUser; }
            set { this.secondUser = value; }
        }
    }
}