using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace CoreBox.Validations;

public class CpfValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly int _validLength = 11;
    protected int[] FirstMultiplierCollection => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    protected int[] SecondMultiplierCollection => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    public override string Name => "CpfValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        string val = value as string ?? string.Empty;
        val = Regex.Replace(val, "[^a-zA-Z0-9]", "");

        if (IsValidLength(val) || AllDigitsAreEqual(val) || value == null) return false;

        var cpf = val.Select(x => (int)char.GetNumericValue(x)).ToArray();
        var digits = GetDigits(cpf);

        return val.EndsWith(digits);
    }

    private bool IsValidLength (string value) => !string.IsNullOrWhiteSpace(value) && value.Length != _validLength;

    private static bool AllDigitsAreEqual (string value) => value.All(x => x == value.FirstOrDefault());

    private string GetDigits(int[] cpf)
    {
        int first = CalculateValue(FirstMultiplierCollection, cpf);
        int second = CalculateValue(SecondMultiplierCollection, cpf);
        return $"{CalculateDigit(first)}{CalculateDigit(second)}";
    }

    private static int CalculateValue(int[] weight, int[] numbers)
    {
        int sum = 0;
        for (int i = 0; i < weight.Length; i++) sum += weight[i] * numbers[i];
        return sum;
    }

    private static int CalculateDigit(int sum)
    {
        int modResult = (sum % 11);
        return modResult < 2 ? 0 : 11 - modResult;
    }
}