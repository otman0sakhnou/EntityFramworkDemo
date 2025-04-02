using System.ComponentModel.DataAnnotations;

namespace EntityFrameWorkTp.utils;

public class FutureDateAttribute : ValidationAttribute
{
       
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date <= DateTime.Now;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} ne peut pas être dans le futur";
    } 
}