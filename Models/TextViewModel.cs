#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;


namespace LetterCounter.Models;
// using a viewmodel for the dictonary, list, and input. 
public class TextViewModel
{
    public TextInput? TextInput { get; set; }
    public List<TextInput> AllInputs { get; set; }
    public Dictionary<char, int> CounterDict { get; set; }

}