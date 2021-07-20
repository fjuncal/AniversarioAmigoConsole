using System;
using System.Data;
using System.IO;
using aniversario_repository.Entity;
using aniversario_repository.RepositoryAmigo;

namespace AssessmentRafael
{
    class Program
    {
        
        static void Main(string[] args)
        {
            AmigoRepositorio amigoRepositorio = new AmigoRepositorio();
            Amigo amigo = new Amigo();
            
            int? opcao = null;
            var amigosAnivHoje = amigoRepositorio.GetAll();
            int contadorAmigosAnivHoje = 0;
            Console.WriteLine("------ANIVERSARIOS HOJE------");
            foreach (DataRow row in amigosAnivHoje.Rows)
            {
                if (Convert.ToDateTime(row["dataaniversario"]) == DateTime.Today)
                {
                    contadorAmigosAnivHoje++;
                    PegandoValoresTabela(row);
                    Console.WriteLine("------------------");
                }
            }
            Console.WriteLine("Quantidade de aniversariantes hoje: " + contadorAmigosAnivHoje);
            Console.WriteLine("##################################");
            while (opcao != 0)
            {
                opcao = Menu();
                
                FuncionalidadeMenu(opcao, amigo, amigoRepositorio);
                
            }
        }

        private static void PegandoValoresTabela(DataRow row)
        {
            int id = Convert.ToInt32(row["id"]);
            String nome = row["nome"].ToString();
            String sobrenome = row["sobrenome"].ToString();
            DateTime dataAniversario = Convert.ToDateTime(row["dataaniversario"]);

            Console.WriteLine("----AMIGO----");
            Console.WriteLine("id: " + id);
            Console.WriteLine("nome: " + nome);
            Console.WriteLine("Sobrenome: " + sobrenome);
            Console.WriteLine("DataAniversario: " + dataAniversario);
        }

        private static void FuncionalidadeMenu(int? opcao, Amigo amigo, AmigoRepositorio amigoRepositorio)
        {
            StreamWriter x;
            String path = "C:\\Users\\juncal\\RiderProjects\\AssessmentRafael\\AssessmentRafael\\arquivo.txt";
            
            if (opcao == 1)
            {
                Console.WriteLine("CADASTRAR ANIVERSARIO");

                FormularioAniversario(amigo);


                amigoRepositorio.Create(amigo);
                Console.WriteLine("----------CADASTRADO COM SUCESSO----------");
            }
            else if (opcao == 2)
            {
                Console.WriteLine("Informe o ID do Amigo que quer deletar:  ");
                var idAmigo = Convert.ToInt32(Console.ReadLine());

                amigoRepositorio.Delete(idAmigo);
            }
            else if (opcao == 3)
            {
                Console.WriteLine("EDITAR ANIVERSARIO");

                Console.WriteLine("Informe o ID do Amigo que quer editar:  ");
                var idAmigo = Convert.ToInt32(Console.ReadLine());

                FormularioAniversario(amigo);

                amigoRepositorio.Update(amigo, idAmigo);
            }
            else if (opcao == 4)
            {
                Console.WriteLine("Informe o ID do Amigo que quer buscar:  ");
                var idAmigo = Convert.ToInt32(Console.ReadLine());
                var amigoAniversario = amigoRepositorio.GetAmigo(idAmigo);

                foreach (DataRow row in amigoAniversario.Rows)
                {
                    PegandoValoresTabela(row);
                    DateTime dataAniversario = Convert.ToDateTime(row["dataaniversario"]);
                    CalculaDiferencaAniv(dataAniversario);
                    Console.WriteLine("------------------");
                }
            } else if (opcao == 5)
            {
                Console.WriteLine("Informe o nome ou alguma palavra chave do Amigo que quer buscar:  ");
                var nomeAmigo = Console.ReadLine();

                var amigoPalavraChave = amigoRepositorio.GetAmigoPalavraChave(nomeAmigo);
                foreach (DataRow row in amigoPalavraChave.Rows)
                {
                    PegandoValoresTabela(row);
                    DateTime dataAniversario = Convert.ToDateTime(row["dataaniversario"]);
                    CalculaDiferencaAniv(dataAniversario);
                    Console.WriteLine("------------------");
                }
            }
            else if (opcao == 6)
            {
                x = File.AppendText(path);
                var todosAmigos = amigoRepositorio.GetAll();
                foreach (DataRow row in todosAmigos.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    String nome = row["nome"].ToString();
                    String sobrenome = row["sobrenome"].ToString();
                    DateTime dataAniversario = Convert.ToDateTime(row["dataaniversario"]);
                    TimeSpan diffAniversario = dataAniversario.Subtract(DateTime.Today);

                    Console.WriteLine("----AMIGO----");
                    Console.WriteLine("id: " + id);
                    Console.WriteLine("nome: " + nome);
                    Console.WriteLine("Sobrenome: " + sobrenome);
                    Console.WriteLine("DataAniversario: " + dataAniversario);
                    Console.WriteLine("Dias que Faltam para o aniversario: " + diffAniversario.TotalDays + " dias");
                    Console.WriteLine("------------------");
                    
                    EscrevendoArquivo(x, id, nome, sobrenome, dataAniversario, diffAniversario);
                }
                x.Close();
            }
            else if (opcao == 7)
            {
                var aniversarioHoje = amigoRepositorio.GetAll();
                foreach (DataRow row in aniversarioHoje.Rows)
                {
                    if (Convert.ToDateTime(row["dataaniversario"]) == DateTime.Today)
                    {
                        PegandoValoresTabela(row);
                        DateTime dataAniversario = Convert.ToDateTime(row["dataaniversario"]);
                        CalculaDiferencaAniv(dataAniversario);
                    }
                }
            } else if (opcao > 7)
            {
                Console.WriteLine("Insira uma opção válida");
            }
        }

        private static void CalculaDiferencaAniv(DateTime dataAniversario)
        {
            TimeSpan diffAniversario = dataAniversario.Subtract(DateTime.Today);
            Console.WriteLine("Dias que Faltam para o aniversario: " + diffAniversario.TotalDays + " dias");
        }

        private static void EscrevendoArquivo(StreamWriter x, int id, string nome, string sobrenome, DateTime dataAniversario,
            TimeSpan diffAniversario)
        {
            x.WriteLine("id: " + id);
            x.WriteLine("nome: " + nome);
            x.WriteLine("Sobrenome: " + sobrenome);
            x.WriteLine("DataAniversario: " + dataAniversario);
            x.WriteLine("Dias que Faltam para o aniversario: " + diffAniversario.TotalDays + " dias");
            x.WriteLine("---------------------------------------------");
            x.WriteLine();
        }

        private static void FormularioAniversario(Amigo amigo)
        {
            Console.WriteLine("Nome: ");
            amigo.Nome = Console.ReadLine();

            Console.WriteLine("Sobrenome: ");
            amigo.Sobrenome = Console.ReadLine();

            Console.WriteLine("Data de Aniversário: DD/MM/2021 (Se ja tiver feito esse ano, colocar a data do ano que vem (2022))");
            amigo.DataAniversario = Convert.ToDateTime(Console.ReadLine());
        }

        private static int? Menu()
        {
            int? opcao;
            Console.WriteLine("-------------------------------" +
                              "\n MENU ANIVERSARIO AMIGO" +
                              "\n 1 - Cadastrar Aniversário" +
                              "\n 2 - Apagar Aniversário" +
                              "\n 3 - Editar Aniversário" +
                              "\n 4 - Buscar Aniversário por ID" +
                              "\n 5 - Buscar Aniversário por palavra chave" +
                              "\n 6 - Listar todos os aniversários" +
                              "\n 7 - Listar Aniversários de hoje" +
                              "\n 0 - SAIR");
            Console.WriteLine("Escolha uma opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());
            return opcao;
        }
    }
}