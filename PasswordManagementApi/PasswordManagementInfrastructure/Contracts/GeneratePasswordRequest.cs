namespace PasswordManagementApi.Contracts
{
    public class GeneratePasswordRequest
    {
        public int UserId { get; set; }
        public string CreationDate { get; set; }
    }
}
