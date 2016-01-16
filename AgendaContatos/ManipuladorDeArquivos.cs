using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgendaContatos
{
    public class ManipuladorDeArquivos
    {
        //"constante" onde vai ficar o arquivo. static para não instanciar
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";

        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();
            if (File.Exists(@EnderecoArquivo))
            {
                using (StreamReader sr = File.OpenText(@EnderecoArquivo))
                {
                    //Peek indica se tem um proximo caracter para ser lido, semelhante ao Next do Java, mas ele retorna -1 quando não tem mais linha.
                    while (sr.Peek() >= 0)
                    {
                        //le a linha do arquivo
                        string linha = sr.ReadLine();
                        //quebra ela com o separado ;
                        string[] linhaComSplit = linha.Split(';');
                        //le o vetor, cria o objeto e add na lista
                        if (linhaComSplit.Count() == 3)
                        {
                            Contato contato = new Contato();
                            contato.Nome = linhaComSplit[0];
                            contato.Email = linhaComSplit[1];
                            contato.NumeroTelefone = linhaComSplit[2];
                            contatosList.Add(contato);
                        }
                    }
                }
            }
            return contatosList;
        }

        public static void EscreverArquivo(List<Contato> contatosList)
        {
            //se não tiver o arquivo ele será criado
            //@ por ser um endereço
            //com o using não precisa colocar .Close() e poder liberar objetos da memoria
            using (StreamWriter sw = new StreamWriter(@EnderecoArquivo, false))
            {
                foreach (Contato contato in contatosList)
                {
                    string linha = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(linha);
                }
                //limpar buffer para escrever de novo
                sw.Flush();
            }

        }
    }
}
