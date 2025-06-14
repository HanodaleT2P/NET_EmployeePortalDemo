using System.ComponentModel.DataAnnotations;

namespace EmployeePortalDemo.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    
}


   
}
