using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Projeto_2___AED_1.src.Funcionarios;

namespace Projeto_2___AED_1.src.Services
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.server = "198.100.155.70";
            this.database = "user_aed1";
            this.user = "user_aed1";
            this.password = "12345";

            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Falha ao se conectar com o servidor");
                        break;

                    case 1045:
                        Console.WriteLine("Usuário ou Senha inválidos");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public long Insert(string query)
        {
            if(this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, this.connection);

                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;

                this.CloseConnection();
                return id;
            }
            return -1;
        }

        public List<Funcionario> ListarMedicos()
        {
            List<Funcionario> listaDeRetorno = new List<Funcionario>();

            //

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM funcionarios a INNER JOIN medicos b ON b.funcionarios_matricula=a.matricula", this.connection);
                MySqlDataReader dtreader = cmd.ExecuteReader();

                while(dtreader.Read())
                {
                    Console.WriteLine(dtreader["matricula"]);
                    Console.WriteLine(dtreader["nome"]);
                    Console.WriteLine(dtreader["cpf"]);
                    Console.WriteLine(dtreader["salario"]);
                    Console.WriteLine(dtreader["crm"]);
                    Console.WriteLine(dtreader["funcionarios_matricula"]);
                }

                this.CloseConnection();
            }

            return listaDeRetorno;
        }

        public void Update(string query)
        {
            if (this.OpenConnection())
            {

                this.CloseConnection();
            }
        }
    }
}
