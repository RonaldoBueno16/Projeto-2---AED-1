using Projeto_2___AED_1.src.Funcionarios;
using System.Collections.Generic;
using System;
using Projeto_2___AED_1.src.Services;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

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

            Console.WriteLine("\nMostrando pacientes");
            foreach(Paciente paciente in pacientes)
            {
                Console.WriteLine(paciente.GetNome());
            }

            Console.WriteLine("\nMostrando secretarias");
            foreach (Secretaria secretaria in secretarias)
            {
                Console.WriteLine(secretaria.GetName());
            }

            Console.WriteLine("\nMostrando medicos");
            foreach (Medico medico in medicos)
            {
                Console.WriteLine(medico.GetName());
            }

            ReturnERROR:
            Console.WriteLine("==========================================");
            Console.WriteLine("=============[Menu principal]=============\n");
            Console.WriteLine("1 - Entrar como secretária");
            Console.WriteLine("2 - Entrar como médico");
            Console.WriteLine("3 - Entrar como administrador do sistema");
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
        }

        private static void MenuSecretaria()
        {
            VoltarLista:
            Console.WriteLine("==========================================");
            Console.WriteLine("=============[Menu SECRETÁRIA]=============\n");
            Console.WriteLine("1 - Cadastrar paciente");
            Console.WriteLine("2 - Agendar consulta");
            Console.WriteLine("3 - Visualizar agenda");
            Console.WriteLine("4 - Voltar");
            Console.Write("\nDigite a opção desejada: ");

            string optionSTR = Console.ReadLine();
            int option;
            bool isNumber = Int32.TryParse(optionSTR, out option);
            if (!isNumber)
            {
                Console.Clear();
                goto VoltarLista;
            }

            switch(option)
            {
                case 1:
                    {
                        Console.WriteLine("\n# Cadastrando paciente\n");

                        VoltarNome:
                        Console.Write("Digite o nome do paciente: ");
                        string nome = Console.ReadLine();
                        if (nome == "")
                        {
                            if (TentarNovamente("Você digitou um nome errado, deseja continuar (Y/N) ?\n"))
                                goto VoltarNome;
                        }

                        VoltarCPF:
                        Console.Write("Digite o CPF do paciente: ");
                        string cpf = Console.ReadLine();
                        if (cpf == "")
                        {
                            if (TentarNovamente("Você digitou um CPF errado, deseja continuar? (Y/N)\n"))
                                goto VoltarCPF;
                        }

                        VoltarNascimento:
                        Console.Write("Digite a data de nascimento do paciente: ");
                        string nascimento = Console.ReadLine();
                        if (nascimento == "")
                        {
                            if (TentarNovamente("Você digitou uma data errada, deseja continuar? (Y/N)\n"))
                                goto VoltarNascimento;
                        }

                        VoltarPlano:
                        Console.Write("O paciente possuí plano de saúde? (Y/N): ");
                        string plano = Console.ReadLine();
                        if (plano == "" || (plano != "Y" && plano != "N"))
                        {
                            if (TentarNovamente("Você digitou uma informação inválida, deseja continuar? (Y/N)\n"))
                                goto VoltarPlano;
                        }
                        bool possui_plano;
                        if (plano.ToUpper() == "Y")
                            possui_plano = true;
                        else
                            possui_plano = false;

                        new Paciente(nome, long.Parse(cpf), nascimento, possui_plano, true);
                        Console.WriteLine("# PACIENTE CADASTRADO COM SUCESSO! PRESSIONE QUALQUER TECLA PARA CONTINUAR");
                        Console.ReadKey(true);
                        goto VoltarLista;
                    }
                case 2:
                    {

                        break;
                    }
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
