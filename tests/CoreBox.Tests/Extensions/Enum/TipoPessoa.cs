using System.ComponentModel;

namespace CoreBox.Tests.Extensions.Enum;

public enum TipoPessoa
{
    [Description("Pessoa Física")]
    Fisica,

    [Description("Pessoa Jurídica")]
    Juridica
}