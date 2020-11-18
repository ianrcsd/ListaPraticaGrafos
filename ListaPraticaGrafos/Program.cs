using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ListaPraticaGrafos
{
    class Program
    {
        static void NaoDirigido(string path)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
           // string linha;
            int id = 0;
            List<Vertice> vertices = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
          

            string[] linha = File.ReadAllLines(path);

            for (int i = 0; i < linha.Count(); i++)
            {
             
                if (i == linha.Count()-1)  //Verifica se é o ultimo item do arquvio para não da erro  ao executar o i+1            
                    continue; 

                if (linha.Length == 1)
                {
                    GrafoNaoDirigido gND;
                    continue;
                }
                Console.WriteLine(linha[i]);

                Console.WriteLine("O proximo é {0}", linha[i + 1]);

                

                Vertice vertice1;
                Vertice vertice2;
                Aresta aresta;


                String[] dados = linha[i].Split();



                vertice1 = new Vertice(Convert.ToInt32(dados[0]));
                vertice2 = new Vertice(Convert.ToInt32(dados[1]));

                aresta = new Aresta(Convert.ToInt32(dados[2]), 0, vertice1, vertice2);

                arestas.Add(aresta);



            }
            //foreach (string s in readText)
            //{
            //    Console.WriteLine(s);
            //}
            //Console.WriteLine("Ocounte " +  );

            //while ((linha = reader.ReadLine()) != null)
            //{
            //    System.Console.WriteLine(linha.Length);

            //    if (linha.Length == 1)
            //    {
            //        GrafoNaoDirigido gND;
            //        continue;
            //    }


            //    Vertice vertice1;
            //    Vertice vertice2;
            //    Aresta aresta;


            //    String[] dados = linha.Split(';');



            //    vertice1 = new Vertice(Convert.ToInt32(dados[0]));
            //    vertice2 = new Vertice(Convert.ToInt32(dados[1]));

            //    aresta = new Aresta(Convert.ToInt32(dados[2]), 0, vertice1, vertice2);

            //    arestas.Add(aresta);






            //    //// Aresta a = new Aresta(id++, int.Parse(dados[2]), 0, dados[0], dados[1]);

            //    // arestas.Add(a);
            //    // Vertice v = new Vertice(int.Parse(dados[0]), arestas);

            //}
            //reader.Close();
        }
        static void Main(string[] args)
        {
            string naoDirigido = System.IO.File.ReadAllText(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\não-dirigido.txt");
            string diridigo = System.IO.File.ReadAllText(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\dirigido.txt");


            // Console.WriteLine(diridigo);

            NaoDirigido(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\não-dirigido.txt");

            Console.ReadKey();

        }
    }
}

