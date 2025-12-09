using System.Text.Json.Serialization;

namespace WebChat_API.Models
{
    public class User
    {
        private string userName = string.Empty;
        private string password = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private DateTime birthdate = DateTime.MinValue;
        private string email = string.Empty;
        private int? statusUserID = 0;

        [JsonIgnore]
        public virtual StatusUser? statusUser { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ChatsList>? firstChatsList { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ChatsList>? secondChatsList { get; set; }

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
        public DateTime Birthdate
        {
            get { return this.birthdate; }
            set { this.birthdate = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public int? StatusUserID
        {
            get { return this.statusUserID; }
            set { this.statusUserID = value; }
        }
    }
}