using Projeto_2___AED_1.src.Funcionarios;
using Projeto_2___AED_1.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_2___AED_1.src.Consultas
{
    class Consulta
    {
        private long id;
        private Medico medico;
        private Paciente paciente;

        public long GetID()
        {
            return this.id;
        }

        public Medico GetMedico()
        {
            return this.medico;
        }

        public Consulta(Paciente pac, Medico med)
        {
            DBConnect connection = new DBConnect();

            this.medico = med;
            this.paciente = pac;

            this.id = connection.Insert("INSERT INTO consultas(medicos_crm, pacientes_matricula) VALUES("+med.GetCRM()+", "+pac.GetID()+")");
        }

        public Paciente GetPaciente()
        {
            return this.paciente;
        }
    }
}
