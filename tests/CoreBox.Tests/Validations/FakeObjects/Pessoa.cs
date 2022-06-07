using CoreBox.Extensions;
using CoreBox.Tests.Validations.Validator;

namespace CoreBox.Tests.Validations;

public class Pessoa
{
    public string TipoPessoa { get; set; }
    public string Documento { get; set; }
    public Sexo? Sexo { get; set; }

    public Pessoa(string tipoPessoa, string documento, int? sexo = 1)
    {
        TipoPessoa = tipoPessoa;
        Documento = documento;
        Sexo = (Sexo?)sexo;

        this.ValidateAndThrow(new PessoaValidator());
    }
}