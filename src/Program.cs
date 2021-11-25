using Projeto_2___AED_1.src.Funcionarios;
using System.Collections.Generic;
using System;

namespace Projeto_2___AED_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Funcionarios marilia = new Secretaria("Marilia", 14698511126, 6000, true);
            Funcionarios anderson = new Medico("Cavalinho", 23211456963, 6000, 1663324, true);

            ReturnERROR:
            Console.WriteLine("==========================================");
            Console.WriteLine("=============[Menu principal]=============");
            Console.WriteLine("==========================================\n");
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
                    Console.WriteLine("Entrar como secretária");
                    MenuSecretaria();
                    break;
                case 2:
                    Console.WriteLine("Entrar como médico");
                    break;
                default:
                    Console.Clear();
                    goto ReturnERROR;
                    break;
            }
        }

        /*private static List<Funcionarios>[] ListarFuncionarios()
        {
            
        }*/

        private static void MenuSecretaria()
        {
            Console.WriteLine("asd");
        }
    }
}
