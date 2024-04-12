using FluentValidation;
using FluentValidation.Validators;

namespace CoreBox.Validations;

public class EnumValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "EnumValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is null) 
            return false;

        string valueText = value.ToString()!;
        int val = (int)Enum.Parse(value.GetType(), valueText);
        var enumItems = Enum.GetValues(value.GetType());

        foreach (var item in enumItems)
        {
            if (val == (int)item)
                return true;
            
            continue;
        }

        return false;
    }
}