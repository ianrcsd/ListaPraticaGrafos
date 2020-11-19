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
            List<Grafo> grafos = new List<Grafo>();
          

            string[] linha = File.ReadAllLines(path);
            

            for (int i = 0; i < linha.Count(); i++)
            {  
               
                if (i == linha.Count()-1)  //Verifica se é o ultimo item do arquvio para não da erro  ao executar o i+1            
                    continue;
                int direcao = 0;                
                String[] dados = linha[i].Split();
                Grafo grafo=null;

                if (linha.Length == 1)
                {
                    id++;
                    if (linha[i + 1].Length == 5)
                    {                        
                        grafo = new GrafoNaoDirigido(id);                       
                    }
                    else
                    {
                        grafo = new GrafoDirigido(id);
                        direcao = Convert.ToInt32(dados[3]);
                    }

                    grafos.Add(grafo);
                    continue;
                }
                Console.WriteLine(linha[i]);

                Console.WriteLine("O proximo é {0}", linha[i + 1]);

                Vertice vertice1;
                Vertice vertice2;
                Aresta aresta;




                // Validar o ID do vertice, criar somente se o ID não existir 
                // Se o vertice já existir eu preciso buscar ele para adicionar na aresta ou no grafo
                // Find buscando o vertive pelo ID, caso o find retorne eu apenas adiciono a aresta no vertice , 
                // se não retornar eu instancio um novo vértice.

                vertice1 = vertices.Find(vertice => vertice.GetId() == Convert.ToInt32(dados[0]));
                vertice2 = vertices.Find(vertice => vertice.GetId() == Convert.ToInt32(dados[1]));
                if (vertice1 == null)
                {
                    vertice1 = new Vertice(Convert.ToInt32(dados[0]));
                    grafo.setVertice(vertice1);                    
                }
                if (vertice2 == null)
                {
                    vertice2 = new Vertice(Convert.ToInt32(dados[1]));
                    grafo.setVertice(vertice2);
                }               
                
                aresta = new Aresta(Convert.ToInt32(dados[2]), direcao, vertice1, vertice2);
  
                grafo.setAresta(aresta);


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

