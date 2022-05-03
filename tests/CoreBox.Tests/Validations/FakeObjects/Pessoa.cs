using CoreBox.Extensions;
using CoreBox.Tests.Validations.Validator;

namespace CoreBox.Tests.Validations;

public class Pessoa
{
    public string TipoPessoa { get; set; }
    public string Documento { get; set; }

    public Pessoa(string tipoPessoa, string documento)
    {
        TipoPessoa = tipoPessoa;
        Documento = documento;

        this.ValidateAndThrow(new PessoaValidator());
    }
}