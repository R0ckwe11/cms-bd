using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace cms_bd.Models;

public class ValidationResult
{
    public int Status { get; }

    public Dictionary<string, string[]> Errors { get; }

    public ValidationResult(ModelStateDictionary modelState, int Status)
    {
        this.Status = Status;
        Errors = new Dictionary<string, string[]>();
        foreach(var key in modelState.Keys) {
            Errors[key] = modelState[key].Errors.Select(e => e.ErrorMessage).ToArray();
        }
    }
}