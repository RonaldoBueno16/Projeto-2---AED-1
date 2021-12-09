using Projeto_2___AED_1.src.Funcionarios;
using System.Collections.Generic;
using System;
using Projeto_2___AED_1.src.Services;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Projeto_2___AED_1.src.Consultas;

namespace Projeto_2___AED_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConnect connect = new DBConnect();
            List<Medico> medicos = connect.CarregarMedicos();
            List<Secretaria> secretarias = connect.CarregarSecretarias();
            List<Paciente> pacientes = connect.CarregarPacientes();
            List<Consulta> consultas = new List<Consulta>();
            
            ReturnERROR:
            Console.WriteLine("==========================================");
            Console.WriteLine("=============[Menu principal]=============\n");
            Console.WriteLine("1 - Entrar como secretária");
            Console.WriteLine("2 - Entrar como médico");
            Console.Write("\nDigite a opção desejada: ");

            string optionSTR = Console.ReadLine();
            int option;
            bool isNumber = Int32.TryParse(optionSTR, out option);
            if (!isNumber)
            {
                Console.Clear();
                goto ReturnERROR;
            }

            Console.WriteLine(option);

            switch (option)
            {
                case 1:
                    Console.Clear();
                    MenuSecretaria();
                    Console.Clear();
                    goto ReturnERROR;
                case 2:
                    Console.Clear();
                    MenuMedico();
                    Console.Clear();
                    goto ReturnERROR;
                case 3:
                    Console.WriteLine("Entrar como administrador");
                    break;
                default:
                    Console.Clear();
                    goto ReturnERROR;
            }
            
            void MenuSecretaria()
            {
                VoltarLista:
                Console.WriteLine("==========================================");
                Console.WriteLine("=============[Menu SECRETÁRIA]=============\n");
                Console.WriteLine("1 - Cadastrar paciente");
                Console.WriteLine("2 - Agendar consulta");
                Console.WriteLine("3 - Visualizar agenda");
                Console.WriteLine("4 - Voltar");
                Console.Write("\nDigite a opção desejada: ");

                optionSTR = Console.ReadLine();
                isNumber = Int32.TryParse(optionSTR, out option);
                if (!isNumber)
                {
                    Console.Clear();
                    goto VoltarLista;
                }

                switch (option)
                {
                    case 1: //Cadastrar paciente
                        {
                            Console.WriteLine("\n# Cadastrando paciente\n");

                            VoltarNome:
                            Console.Write("Digite o nome do paciente: ");
                            string nome = Console.ReadLine();
                            if (nome == "")
                            {
                                if (TentarNovamente("Você digitou um nome errado, deseja continuar (Y/N) ?\n"))
                                    goto VoltarNome;
                                else
                                    goto VoltarLista;
                            }

                            VoltarCPF:
                            Console.Write("Digite o CPF do paciente: ");
                            string cpf = Console.ReadLine();
                            if (cpf == "")
                            {
                                if (TentarNovamente("Você digitou um CPF errado, deseja continuar? (Y/N)\n"))
                                    goto VoltarCPF;
                                else
                                    goto VoltarLista;
                            }

                            VoltarNascimento:
                            Console.Write("Digite a data de nascimento do paciente: ");
                            string nascimento = Console.ReadLine();
                            if (nascimento == "")
                            {
                                if (TentarNovamente("Você digitou uma data errada, deseja continuar? (Y/N)\n"))
                                    goto VoltarNascimento;
                                else
                                    goto VoltarLista;
                            }

                            VoltarPlano:
                            Console.Write("O paciente possuí plano de saúde? (Y/N): ");
                            string plano = Console.ReadLine();
                            if (plano == "" || (plano != "Y" && plano != "N"))
                            {
                                if (TentarNovamente("Você digitou uma informação inválida, deseja continuar? (Y/N)\n"))
                                    goto VoltarPlano;
                                else
                                    goto VoltarLista;
                            }
                            bool possui_plano;
                            if (plano.ToUpper() == "Y")
                                possui_plano = true;
                            else
                                possui_plano = false;

                            Paciente paciente = new Paciente(nome, long.Parse(cpf), nascimento, possui_plano, true);
                            Console.WriteLine("# PACIENTE CADASTRADO COM SUCESSO! PRESSIONE QUALQUER TECLA PARA CONTINUAR\n");
                            pacientes.Add(paciente);
                            Console.ReadKey(true);
                            Console.Clear();
                            goto VoltarLista;
                        }
                    case 2: //Agendar consulta
                        {
                            Console.WriteLine("\nPacientes cadastrados:");
                            foreach(Paciente paciente in pacientes)
                            {
                                Console.WriteLine("Matricula:["+paciente.GetID()+"] - Paciente:["+paciente.GetNome()+"]");
                            }
                            Console.Write("\nDigite a matricula do paciente que você deseja marcar a consulta: ");
                            optionSTR = Console.ReadLine();
                            isNumber = Int32.TryParse(optionSTR, out option);
                            if (!isNumber)
                            {
                                Console.WriteLine("Você digitou algo inválido!");
                                Console.ReadKey();
                                Console.Clear();
                                goto VoltarLista;
                            }

                            Paciente pacienteEX = GetPaciente(option);
                            if(pacienteEX == null)
                            {
                                Console.WriteLine("Você digitou um paciente inválido!");
                                Console.ReadKey(true);
                                Console.Clear();
                                goto VoltarLista;
                            }
                            else
                            {
                                Console.WriteLine("\n- Marcando consulta para o paciente: " + pacienteEX.GetNome());
                                foreach(Medico medico in medicos)
                                {
                                    Console.WriteLine("CRM:["+medico.GetCRM()+"] - Médico:["+medico.GetName()+"]");
                                }
                                Console.Write("\nDigite o CRM do médico que vai atende-lo: ");

                                optionSTR = Console.ReadLine();
                                isNumber = Int32.TryParse(optionSTR, out option);
                                if (!isNumber)
                                {
                                    Console.WriteLine("Você digitou algo inválido!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    goto VoltarLista;
                                }

                                Medico medico_AUX = GetMedico(option);

                                Consulta consulta = new Consulta(pacienteEX, medico_AUX);
                                consultas.Add(consulta);
                                Console.WriteLine("A consulta do paciente "+pacienteEX.GetNome()+" com o médico "+medico_AUX.GetName()+" foi marcada com sucesso!");
                                Console.ReadKey(true);
                                Console.Clear();
                                goto VoltarLista;
                            }
                        }
                    case 3: //Listar a agenda dos médicos
                        {
                            Console.WriteLine("- Médicos disponíveis:");
                            foreach (Medico medico in medicos)
                            {
                                Console.WriteLine("CRM:[" + medico.GetCRM() + "] - Médico:[" + medico.GetName() + "]");
                            }
                            Console.Write("\nDigite o CRM do médico para visualizar a agenda: ");

                            optionSTR = Console.ReadLine();
                            isNumber = Int32.TryParse(optionSTR, out option);
                            if (!isNumber)
                            {
                                Console.WriteLine("Você digitou algo inválido!");
                                Console.ReadKey();
                                goto VoltarLista;
                            }

                            Medico medico_AUX = GetMedico(option);
                            if(medico_AUX == null)
                            {
                                Console.WriteLine("Médico não encontrado");
                                Console.ReadKey(true);
                                goto VoltarLista;
                            }
                            else
                            {
                                int count = 0;
                                Console.WriteLine("- Listando as consultas do médico: " + medico_AUX.GetName());
                                foreach(Consulta consulta in consultas)
                                {
                                    if(consulta.GetMedico() == medico_AUX)
                                    {
                                        Console.WriteLine("- "+consulta.GetID()+": "+consulta.GetPaciente().GetNome()+"");
                                        count++;
                                    }
                                }
                                if (count == 0)
                                    Console.Write("\nEsse médico não tem nenhuma consulta marcada!");
                                else
                                    Console.Write("\nEsse médico tem " + count + " consultas marcadas!");

                                Console.ReadKey(true);
                                Console.Clear();
                                goto VoltarLista;
                            }
                        }
                }
            }

            Medico GetMedico(long CRM)
            {
                foreach(Medico medico in medicos)
                {
                    if(medico.GetCRM() == CRM)
                    {
                        return medico;
                    }
                }
                return null;
            }

            Paciente GetPaciente(long id)
            {
                foreach(Paciente paciente in pacientes)
                {
                    if(paciente.GetID() == id)
                    {
                        return paciente;
                    }
                }
                return null;
            }
        }
        

        private static bool TentarNovamente(string message)
        {
            Console.Write(message);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key.ToString().ToUpper() == "Y")
                return true;
            else
                return false;
        }

        private static void MenuMedico()
        {
            ReturnERROR:
            Console.WriteLine("==========================================");
            Console.WriteLine("==============[Menu MÉDICO]===============\n");
            Console.WriteLine("1 - Visualizar agenda");
            Console.WriteLine("2 - Voltar");
            Console.Write("\nDigite a opção desejada: ");

            string optionSTR = Console.ReadLine();
            int option;
            bool isNumber = Int32.TryParse(optionSTR, out option);
            if (!isNumber)
            {
                Console.Clear();
                goto ReturnERROR;
            }
        }
    }
}
