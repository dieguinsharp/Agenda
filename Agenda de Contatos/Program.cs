using System;
using System.IO;
using System.Collections.Generic;
namespace Agenda_de_Contatos {
    class Program {
        static void Main(string[] args) {
            string loop = "";
            do {
                Console.Clear();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Bem vindo a sua Agenda Telefonica!");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("[1] Adicionar Contatos [1]");
                Console.WriteLine("[2] Mostrar Agenda --- [2]");
                Console.WriteLine("[3] Editar Numero ---- [3]");
                Console.WriteLine("[4] Editar Nome ------ [4]");
                Console.WriteLine("[5] Sair ------------- [5]");
                Console.WriteLine("--------------------------");
                Console.Write("Digite uma das opções:");
                int resp = Convert.ToInt32(Console.ReadLine());

                agendaTelefonica c1 = new agendaTelefonica();
                string nome = "", numero = "";

                switch (resp) {
                    case 1:
                        Console.WriteLine("--------------------------");
                        Console.Write("Digite o nome do contato:");
                        nome = Console.ReadLine();
                        Console.WriteLine("--------------------------");
                        Console.Write("Digite o numero do contato:");
                        numero = Console.ReadLine();
                        c1.adicionarContatos(nome, numero);
                        break;
                    case 2:
                        Console.Clear();
                        c1.mostrarAgenda();
                        break;
                    case 3:
                        Console.WriteLine("--------------------------");
                        Console.Write("Informe o nome do contato:");
                        nome = Console.ReadLine();
                        c1.editarContatos(nome, numero);
                        break;
                    case 4:
                        Console.WriteLine("--------------------------");
                        Console.Write("Informe o nome do contato:");
                        nome = Console.ReadLine();
                        c1.editarContatos(nome, "null");
                        break;
                    case 5:
                        Console.Clear();
                        break;
                }
                Console.WriteLine("------------------------------------");
                Console.Write("Deseja continuar usando sua agenda?");
                loop = Console.ReadLine();
            } while (loop != "N" && loop != "n");
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Obrigado por usar nosso console XD.");
            Console.WriteLine("-----------------------------------");
        }     
    }
    class agendaTelefonica {
        bool erro = false;
        public void adicionarContatos(string nome, string numero) {
            try {
                StreamWriter adc = new StreamWriter("agenda.txt", true);
                adc.Write(nome + Environment.NewLine);
                adc.Write(numero + Environment.NewLine);
                adc.Close();
            } catch (Exception) {
                Console.WriteLine("Ouve um erro, por favor, revise seus dados e tente novamente.");
                erro = true;
            }
            if (erro == false) {
                Console.WriteLine("Contatos adicionados a sua lista de Contatos!");
            }
        }
        public void mostrarAgenda() {

            StreamReader verAgenda = new StreamReader("agenda.txt");
            string[] conteudo = verAgenda.ReadToEnd().Split(Environment.NewLine);

            List<string> agenda = new List<string>();

            for (int cont=0; cont < File.ReadAllLines("agenda.txt").Length; cont++) {
                agenda.Add(conteudo[cont]);
            }

            for (int cont = 0; cont < agenda.Count; cont++) {
                Console.WriteLine(agenda[cont] + " > " + agenda[cont + 1]);
                cont++;
            }


            /*try {
                Console.WriteLine("--- Contatos atuais da Agenda ---");
                Console.WriteLine(File.ReadAllText("agenda.txt"));
                Console.WriteLine("---------------------------------");
            } catch (FileNotFoundException) {
                Console.WriteLine("O arquivo da agenda não existe.");
            } */
        }     
        public void editarContatos(string nome, string numero) {

            try {
                StreamReader mostrar = new StreamReader("agenda.txt");
                string[] conteudo = mostrar.ReadToEnd().Split(Environment.NewLine);

                List<string> lista = new List<string>();

                for (int cont = 0; cont < (File.ReadAllLines("agenda.txt").Length); cont++) {
                    lista.Add(conteudo[cont]);
                }

                mostrar.Close();

                bool erro = true;

                for (int cont = 0; cont < lista.Count; cont++) {
                    if (lista[cont] == nome) {
                        if (numero == "null") {
                            Console.Write("Nome de contato achado, agora informe o novo nome para ele:");
                            lista[cont] = Console.ReadLine();
                            cont = lista.Count;
                            erro = false;
                        } else {
                            Console.Write("Nome de contato achado, agora informe o novo numero para " + nome + ":");
                            lista[cont + 1] = Console.ReadLine();
                            cont = lista.Count;
                            erro = false;
                        }
                        
                    }
                }
                if (erro == false) {
                    File.Delete("agenda.txt");

                    StreamWriter sobrescrever = new StreamWriter("agenda.txt");
                    for (int cont = 0; cont < lista.Count; cont++) {
                        sobrescrever.Write(lista[cont].ToString() + Environment.NewLine);
                    }
                    sobrescrever.Close();
                } else {
                    Console.WriteLine("Não achamos um contato com o nome " + nome + ", verifique e tente novamente.");
                }
            } catch (FileNotFoundException) {
                Console.WriteLine("O arquivo da agenda não existe! Tente novamente.");
            }                   
        }
    }
}
