namespace Kavosh.Domain.Entities
{
    public class LoginUser : BaseEntity
    {
        public string Username { get; set; }    
        public string Password { get; set; }    
        public string FullName { get; set; }    

    }
}       