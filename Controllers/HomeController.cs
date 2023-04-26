using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LetterCounter.Models;



namespace LetterCounter.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;
    // Inject the DB context into the controller
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    // -------Render the Index Page---------
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View("Index");
    }

    // ------Post method to send text to the database--------
    [HttpPost("/text/create")]
    public IActionResult TextToDB(TextInput newText)
    {
        // Check to ensure the text passes the validations
        if (ModelState.IsValid)
        {
            // Add the text input to the database 
            _context.TextInputs.Add(newText);
            // Save the changes
            _context.SaveChanges();
            // Redirect to the CountLetters method to display the result
            return RedirectToAction("CountLetters", new { TextInputId = newText.TextInputId });
        }
        else
        {
            // if the text did not pass validations, return the Index method which will show the validation error message
            return Index();
        }
    }

    // Get Text Input from Database and Analyze the number of characters + Render the result
    [HttpGet("/text/{TextInputId}/result")]
    public IActionResult CountLetters(int TextInputId)
    {
        // Obtain the text entry from the database
        TextInput? newInput = _context.TextInputs.FirstOrDefault(i => i.TextInputId == TextInputId);
        // Create a new dictonary to keep track of each character occuring in the submission
        Dictionary<char, int> CounterDict = new Dictionary<char, int>();

        // Use a for each loop to loop through the string
        foreach (char c in newInput.Text)
        {
            // Check if the character is a letter
            if (char.IsLetter(c))
            {
                // Create a 'key' variable with the letter, also making it case insensitive
                char key = char.ToLower(c);
                // check if the key is already present in the dictonary
                if (CounterDict.ContainsKey(key))
                {
                    // If the key is present increment the associated value
                    CounterDict[key]++;
                }
                else
                {
                    // If the key is not already in the dictonary, add the key and set the value to 1 
                    CounterDict.Add(key, 1);
                }
            }
        }
        // 
        TextViewModel viewModel = new TextViewModel
        {
            // Sort the dictonary by the keys
            CounterDict = CounterDict.OrderBy(k => k.Key).ToDictionary(x => x.Key, x => x.Value),
            AllInputs = _context.TextInputs.Where(i => i.TextInputId != TextInputId).ToList(),
            TextInput = newInput

        };
        return View("Result", viewModel);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

