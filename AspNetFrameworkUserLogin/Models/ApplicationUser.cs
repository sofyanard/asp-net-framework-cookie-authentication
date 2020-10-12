using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetFrameworkUserLogin.Models
{
    [Table("RefUser")]
    public class ApplicationUser
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserName")]
        [Required]
        [Display(Name = "User Name")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Column("FullName")]
        [Required]
        public string FullName { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string Password { get; set; }
    }

    class UserRegisterModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}