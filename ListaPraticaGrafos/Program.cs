using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPraticaGrafos
{
    class Program
    {
        static void NaoDirigido(string path)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
            string linha;
            int id = 0;         
            while ((linha = reader.ReadLine()) != null)
            
                if (linha.Length == 1)
                {                   
                    GrafoNaoDirigido gND;                    
                    continue;
                }           

                
                Vertice vertice1;
                Vertice vertice2;
                Aresta aresta;
                List<Aresta> arestas = new List<Aresta>();
                List<Vertice> vertices = new List<Vertice>();
                String[] dados = linha.Split(';');

                
                
                vertice1 = new Vertice(Convert.ToInt32(dados[0]));
                vertice2 = new Vertice(Convert.ToInt32(dados[1]));

                aresta = new Aresta(Convert.ToInt32(dados[2]), 0, vertice1, vertice2);

                arestas.Add(aresta);                
                

                System.Console.WriteLine(linha.Length);
            

                //// Aresta a = new Aresta(id++, int.Parse(dados[2]), 0, dados[0], dados[1]);

                // arestas.Add(a);
                // Vertice v = new Vertice(int.Parse(dados[0]), arestas);

            }
            reader.Close();
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
