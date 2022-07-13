namespace PasswordManagementApi.Contracts
{
    public class GeneratePasswordResponse
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string ExpirationDate { get; set; }
    }
}
