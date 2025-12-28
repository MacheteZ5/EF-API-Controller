using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class User
    {
        private string userName = string.Empty;
        private string password = string.Empty;
        private string email = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private DateOnly birthdate = DateOnly.MinValue;
        private byte statusID = byte.MinValue;
        private DateTime? dateCreated = DateTime.MinValue;

        [JsonIgnore]
        public virtual StatusUser? statusUser { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Room>? rooms { get; set; }

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }
        public DateOnly Birthdate
        {
            get { return this.birthdate; }
            set { this.birthdate = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public byte StatusID
        {
            get { return this.statusID; }
            set { this.statusID = value; }
        }
        public DateTime? DateCreated
        {
            get { return this.dateCreated; }
            set { this.dateCreated = value; }
        }
    }
}