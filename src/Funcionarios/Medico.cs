using Projeto_2___AED_1.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_2___AED_1.src.Funcionarios
{
    class Medico:Funcionario
    {
        private int CRM;

        public Medico(string fName, long fCPF, double fSalario, int mCRM, bool register = false) : base(fName, fCPF, fSalario, register)
        {
            this.CRM = mCRM;

            if (register)
                RegisterMedico();
        }

        private void RegisterMedico()
        {
            DBConnect connection = new DBConnect();
            connection.Insert("INSERT INTO medicos(crm, funcionarios_matricula) VALUES(" + this.CRM+","+this.matricula+");");
        }
        
        public int GetCRM()
        {
            return this.CRM;
        }
        
    }
}
