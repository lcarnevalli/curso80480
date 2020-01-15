using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    public abstract class EmpresaBase
    {
        public EmpresaBase(int cnpjInjetado)
        {
            this.CNPJ = cnpjInjetado;
        }
        public Int64 CNPJ { get; private set; }
        public abstract bool ValidarCNPJ();
        public virtual bool ValidarCNPJV2() {
            return true; 
        }
    }

    public class EmpresaLTDA : EmpresaBase
    {

        public EmpresaLTDA()
            :base(0032)
        {

        }

        public sealed override bool ValidarCNPJ()
        {
            throw new NotImplementedException();
        }
        public override bool ValidarCNPJV2()
        {
            return base.ValidarCNPJV2();
        }
    }

    public class EmpresaLTDASimples : EmpresaLTDA {
        public EmpresaLTDASimples()
        {
            
        }
    }



}
