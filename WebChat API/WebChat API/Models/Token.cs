namespace WebChat_API.Models
{
    public class Token
    {
        private string tokenString = string.Empty;
        private DateTime expirationTime = DateTime.MinValue;

        public string TokenString
        {
            get { return this.tokenString; }
            set { this.tokenString = value; }
        }
        public DateTime ExpirationTime
        {
            get { return this.expirationTime; }
            set { this.expirationTime = value; }
        }
    }
}
