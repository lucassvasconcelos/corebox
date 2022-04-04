using System.Text.RegularExpressions;

namespace CoreBox.Extensions;

public static class StringExtensions
{
    public static bool IsEmail(this string email)
    {
        try
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsCpf(this string cpf)
    {
        try
        {
            cpf = cpf.OnlyStringNumbers();

            if (cpf.Length != 11)
                return false;

            int soma = 0;
            string tempCpf = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * (new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 })[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            string digito = resto.ToString();
            tempCpf = tempCpf + digito;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * (new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 })[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsCnpj(this string cnpj)
    {
        try
        {
            cnpj = cnpj.OnlyStringNumbers();

            if (cnpj.Length != 14)
                return false;

            int soma = 0;
            string tempCnpj = cnpj.Substring(0, 12);

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * (new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 })[i];

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * (new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 })[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string OnlyStringNumbers(this string text)
        => new String(text.Trim().Where(Char.IsDigit).ToArray());
}