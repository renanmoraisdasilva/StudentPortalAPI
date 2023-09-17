using System.ComponentModel.DataAnnotations;

namespace StudentPortalAPI.Models
{
    public class User
    {
        public int UserId { get; private set; }

        [Required(ErrorMessage = "Username is required.")]        
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 255 characters.")]
        public string Username { get; private set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; private set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "Role is required.")]
        public UserRole Role { get; private set; }

        public List<Course>? Courses { get; private set; }
        public Student? StudentProfile { get; private set; }
        public Professor? ProfessorProfile { get; private set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow; // Initialize with current UTC time

        public static User Create(string username, string password, string email, UserRole role)
        {
            return new User(username, password, email, role);
        }

        public User()
        {
            // Default constructor for Entity Framework
        }

        private User(string username, string password, string email, UserRole role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }
    }

    public enum UserRole
    {
        Student,
        Teacher,
        Admin
    }
}
