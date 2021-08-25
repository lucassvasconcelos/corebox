using System;

namespace CoreBox.UnitTests
{
    public class PessoaDataModel : DataModel<PessoaDataModel, Guid>
    {
        public string Nome { get; set; }
        public virtual Documento Documento { get; set; }
    }
}