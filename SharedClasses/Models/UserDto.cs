namespace SharedClasses.Models
{
    public static class UserDto
    {
        public class Index
        {
            public string UserId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Blocked { get; set; }
        }
    }
}
