using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSTART_Hiring_Task.Models;

public partial class Account
{
    [Key]
    [Display(Name = "AccountId")]
    public int AccountId { get; set; }

    public int UserId { get; set; }

    public DateTime ServerDateTime { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public DateTime UpdateDateTimeUtc { get; set; }
    [Required(ErrorMessage = "cant be blank")]
    public string AccountNumber { get; set; } = null!;
    [Required(ErrorMessage = "cant be blank")]
    public decimal Balance { get; set; }
    [Required(ErrorMessage = "cant be blank")]
    
    public Currency currency { get; set; }

    public AccountStatus Status { get; set; }

    public virtual User? User { get; set; }

    // Currency struct
    public enum Currency
    {
        USD,
        EUR,
        GBP
    }
}

    // AccountStatus enum
    public enum AccountStatus
    {
        Active,
        Inactive,
        Deleted
    }


