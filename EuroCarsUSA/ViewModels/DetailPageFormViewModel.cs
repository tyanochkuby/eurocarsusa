using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EuroCarsUSA.Data.Attributes;
using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels;

public class DetailPageFormViewModel
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(40)]
    public string? Name { get; set; }

    [EmailAddress]
    [AtLeastOneProperty("Email", "PhoneNumber", ErrorMessage = "You must provide either an Email or a Phone Number.")]
    public string? Email { get; set; }

    [Phone]
    [MaxLength(9)]
    public string? PhoneNumber { get; set; }

    [ForeignKey("Car")]
    public Guid CarId { get; set; }
    public Car? Car { get; set; }

    [MaxLength(200)]
    public string? Message { get; set; }

    public string RecaptchaToken { get; set; }

}
