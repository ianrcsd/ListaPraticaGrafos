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
        static List<Grafo> LerArquivo(string path)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
           // string linha;
            int id = 0;
            //Boolean ehPrimeiro = true;
            List<Vertice> vertices = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
            List<Grafo> grafos = new List<Grafo>();
          

            string[] linha = File.ReadAllLines(path);

            Grafo grafo = null; 
            for (int i = 0; i < linha.Count(); i++)
            {
                int aux = 1;
                if (i == linha.Count()-1)  //Verifica se é o ultimo item do arquvio para não da erro  ao executar o i+1            
                    aux = 0;
                int direcao = 0;                
                String[] dados = linha[i].Split(';');             

                if (dados.Count() == 1)
                {
                    id++;
                    if (linha[i +aux].Split(';').Count() == 3)
                    {                        
                        grafo = new GrafoNaoDirigido(id);
                        grafo.EhDirigido = false;
                    }
                    else
                    {
                        grafo = new GrafoDirigido(id);                        
                    }
                    vertices.Clear();
                    arestas.Clear();
                    grafos.Add(grafo);
                    continue;
                }
             

                Vertice vertice1;
                Vertice vertice2;
                Aresta aresta;




                // Validar o ID do vertice, criar somente se o ID não existir 
                // Se o vertice já existir eu preciso buscar ele para adicionar na aresta ou no grafo
                // Find buscando o vertive pelo ID, caso o find retorne eu apenas adiciono a aresta no vertice , 
                // se não retornar eu instancio um novo vértice.          
               

                vertice1 = vertices.Find(v => v.GetId() == Convert.ToInt32(dados[0]));
                vertice2 = vertices.Find(v => v.GetId() == Convert.ToInt32(dados[1]));

                if (vertice1 == null)
                {                     
                    vertice1 = new Vertice(int.Parse(dados[0]));                    
                    vertices.Add(vertice1);                    
                    grafo.setVertice(vertice1);                    
                }
                if (vertice2 == null)
                {
                    vertice2 = new Vertice(Convert.ToInt32(dados[1]));
                    vertices.Add(vertice2);
                    grafo.setVertice(vertice2);
                }

                if (dados.Count() == 4)
                {
                    direcao = Convert.ToInt32(dados[3]);
                }
                

                aresta = new Aresta(Convert.ToInt32(dados[2]), direcao, vertice1, vertice2);
                vertice1.SetArestas(aresta);
                vertice2.SetArestas(aresta);


  
                grafo.setAresta(aresta);


            }

            return grafos;

            
        }
        static void Main(string[] args)
        {
            //string naoDirigido = System.IO.File.ReadAllText(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\não-dirigido.txt");
            //string diridigo = System.IO.File.ReadAllText(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\dirigido.txt");


            Console.WriteLine("");

            List<Grafo> listaGrafos = LerArquivo(@"C:\Users\ianrc\Documents\PUC\4 - Semestre\Grafos\ListaPraticaGrafos\Grafos.txt");



            foreach (Grafo g in listaGrafos)
            {
                if (!g.EhDirigido)
                {
                    GrafoNaoDirigido gnd = (GrafoNaoDirigido)g;

                    foreach (Vertice v in g.vertices)
                    {
                        //Console.WriteLine("O Vértice {0} tem Grau {1} ", v.GetId(), gnd.GetGrau(v));
                        //Console.WriteLine("O Vértice {0} Isolado -> {1} ", v.GetId(), gnd.IsIsolado(v));
                        //Console.WriteLine("O Vértice {0} Pendente -> {1} ", v.GetId(), gnd.IsPendente(v));
                        //Console.WriteLine(gnd.GetAGMPrim(v).ToString());
                    }



                    //Console.WriteLine("Conexo ->" + gnd.IsConexo());
                    //Console.WriteLine("Euleriano ->" + gnd.IsEuleriano());
                    //Console.WriteLine("IsRegular ->" + gnd.IsRegular());
                    //Console.WriteLine("IsUnicursal ->" + gnd.IsUnicursal());
                    //Console.WriteLine("IsNulo ->" + gnd.IsNulo());
                    //Console.WriteLine("IsCompleto ->" + gnd.IsCompleto());
                    //Console.WriteLine("getCutVertices -> " + gnd.GetCutVertices());
                    Console.WriteLine("=====================================");
                }
                else
                {
                    GrafoDirigido gd = (GrafoDirigido)g;
                    //Console.WriteLine("HasCiclo ->" + gd.HasCiclo());
                    foreach (Vertice v in g.vertices)
                    {
                        Console.WriteLine(" Vértice {0} Grau de Entrada {1} ", v.GetId(), gd.GetGrauEntrada(v));
                        Console.WriteLine(" Vértice {0} Grau de Saida {1} ", v.GetId(), gd.GetGrauSaida(v));
                    }
                    Console.WriteLine("=====================================");

                }

            }

            Console.ReadKey();

        }
    }
}

