#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;


namespace LetterCounter.Models;

public class TextInput
{
    
    [Key]
    public int TextInputId {get; set;}
    // validations for text input length
    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Text {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    
}