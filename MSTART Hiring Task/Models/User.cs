using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTART_Hiring_Task.Models;

public partial class User
{
    [Display(Name ="Id")]
    public int Id { get; set; }

    public DateTime ServerDateTime { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public DateTime UpdateDateTimeUtc { get; set; }
    [Required(ErrorMessage ="cant be blank")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "cant be blank")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "cant be blank")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "cant be blank")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "cant be blank")]
    public UserStatus Status { get; set; }
    public Gender gender { get; set; }
    [Display(Name = "Id")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public Account? Account { get; set; } // Reference to the Account model
    public enum UserStatus
    {
        Active,
        Inactive,
        Deleted
    }
    public enum Gender
    {
        Male,
        Female,
        
    }
}
