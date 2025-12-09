using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class StatusMessage
    {
        private long id = 0;
        private string name = string.Empty;
        private string description = string.Empty;
        private bool vigente = true;
        private DateTime fecTransac = DateTime.Now;
        [JsonIgnore]
        public virtual IEnumerable<Message> Messages { get; set; } = new List<Message>();

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
        public bool Vigente
        {
            get { return this.vigente; }
            set { this.vigente = value; }
        }
        public DateTime FecTransac
        {
            get { return this.fecTransac; }
            set { this.fecTransac = value; }
        }
    }
}