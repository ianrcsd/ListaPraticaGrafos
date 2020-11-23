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
            int id = 0;
            List<Vertice> vertices = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
            List<Grafo> grafos = new List<Grafo>();
            Grafo grafo = null;
            string[] linha = File.ReadAllLines(path);

            for (int i = 0; i < linha.Count(); i++)
            {
                int aux = 1;
                if (i == linha.Count() - 1)  //Verifica se é o ultimo item do arquvio para não da erro ao executar o i+1            
                    aux = 0;
                int direcao = 0;
                String[] dados = linha[i].Split(';');

                if (dados.Count() == 1)
                {
                    id++;
                    if (linha[i + aux].Split(';').Count() == 3)
                    {
                        grafo = new GrafoNaoDirigido(id);
                        grafo.SetEhDirigido(false);
                    }
                    else
                    {
                        grafo = new GrafoDirigido(id);
                        grafo.SetEhDirigido(true);
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
                // Se o vertice já existir buscar ele para adicionar na aresta ou no grafo
                // Find buscando o vertive pelo ID, caso o find retorne, apenas adiciona a aresta no vertice , 
                // se não retornar, instancia um novo vértice.     
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
        static void MenuDirigido(int op, GrafoDirigido gd)
        {
            int id = 0;
            switch (op)
            {
                case 1:
                    Console.WriteLine("Entre com o Id do vértice");
                    id = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gd.vertices)
                    {
                        if (v.GetId() == id)
                        {
                            Console.WriteLine("Vértice {0} Grau de Entrada {1} ", v.GetId(), gd.GetGrauEntrada(v));
                            break;
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("Entre com o Id do vértice");
                    id = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gd.vertices)
                    {
                        if (v.GetId() == id)
                        {
                            Console.WriteLine("Vértice {0} Grau de Saída {1} ", v.GetId(), gd.GetGrauSaida(v));
                        }
                    }
                    break;
                case 3:
                    if (gd.HasCiclo())
                    {
                        Console.WriteLine("O grafo {0} tem ciclo.", gd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não tem ciclo.", gd.GetIdGrafo());
                    }
                    break;
            }
        }
        static void MenuNaoDirigido(int op, GrafoNaoDirigido gnd)
        {
            int idVertice1 = 0;
            int idVertice2 = 0;
            Vertice v1 = null;
            Vertice v2 = null;

            switch (op)
            {
                case 1: //adjacentes
                    Console.WriteLine("Entre com o Id do vértice 1");
                    idVertice1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Entre com o Id do vértice 2");
                    idVertice2 = int.Parse(Console.ReadLine());

                    foreach (Vertice vertice in gnd.vertices)
                    {
                        if (vertice.GetId() == idVertice1)
                        {
                            v1 = vertice;
                        }
                    }
                    foreach (Vertice vertice in gnd.vertices)
                    {
                        if (vertice.GetId() == idVertice2)
                        {
                            v2 = vertice;
                        }
                    }

                    if (gnd.IsAdjacente(v1, v2))
                    {
                        Console.WriteLine("Os vértices {0} e {1} são adjacentes!", v1.GetId(), v2.GetId());
                    }
                    else
                    {
                        Console.WriteLine("Os vértices {0} e {1} não são adjacentes!", v1.GetId(), v2.GetId());
                    }
                    break;
                case 2: //grau
                    Console.WriteLine("Entre com o Id do vértice:");
                    idVertice1 = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gnd.vertices)
                    {
                        if (v.GetId() == idVertice1)
                        {
                            Console.WriteLine("Vértice {0} tem grau {1} ", v.GetId(), gnd.GetGrau(v));
                        }
                    }
                    break;
                case 3: //isolado
                    Console.WriteLine("Entre com o Id do vértice:");
                    idVertice1 = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gnd.vertices)
                    {
                        if (v.GetId() == idVertice1)
                        {
                            v1 = v;
                        }
                    }
                    if (gnd.IsIsolado(v1))
                    {
                        Console.WriteLine("Vértice {0} é isolado ", v1.GetId());
                    }
                    else
                    {
                        Console.WriteLine("Vértice {0} não é isolado ", v1.GetId());
                    }
                    break;
                case 4: //pendente
                    Console.WriteLine("Entre com o Id do vértice:");
                    idVertice1 = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gnd.vertices)
                    {
                        if (v.GetId() == idVertice1)
                        {
                            v1 = v;
                        }
                    }
                    if (gnd.IsPendente(v1))
                    {
                        Console.WriteLine("Vértice {0} é pendente ", v1.GetId());
                    }
                    else
                    {
                        Console.WriteLine("Vértice {0} não é pendente ", v1.GetId());
                    }
                    break;
                case 5: //regular                    
                    if (gnd.IsRegular())
                    {
                        Console.WriteLine("Vértice {0} é regular ", v1.GetId());
                    }
                    else
                    {
                        Console.WriteLine("Vértice {0} não é regular ", v1.GetId());
                    }
                    break;
                case 6: //nulo
                    if (gnd.IsNulo())
                    {
                        Console.WriteLine("O grafo {0} é nulo.", gnd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não é nulo.", gnd.GetIdGrafo());
                    }
                    break;
                case 7: //completo
                    if (gnd.IsCompleto())
                    {
                        Console.WriteLine("O grafo {0} é completo.", gnd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não é completo.", gnd.GetIdGrafo());
                    }
                    break;
                case 8: //conexo
                    if (gnd.IsConexo())
                    {
                        Console.WriteLine("O grafo {0} é conexo.", gnd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não é conexo.", gnd.GetIdGrafo());
                    }
                    break;
                case 9: //euleriano
                    if (gnd.IsEuleriano())
                    {
                        Console.WriteLine("O grafo {0} é euleriano.", gnd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não é euleriano.", gnd.GetIdGrafo());
                    }
                    break;
                case 10: //unicursal
                    if (gnd.IsUnicursal())
                    {
                        Console.WriteLine("O grafo {0} é unicursal.", gnd.GetIdGrafo());
                    }
                    else
                    {
                        Console.WriteLine("O grafo {0} não é unicursal.", gnd.GetIdGrafo());
                    }
                    break;
                case 11: //Prim
                    Console.WriteLine("Entre com o Id do vértice:");
                    idVertice1 = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gnd.vertices)
                    {
                        if (v.GetId() == idVertice1)
                        {
                            v1 = v;
                        }
                    }
                    Console.WriteLine(gnd.GetAGMPrim(v1));
                    break;
                case 12: //Kruskal
                    Console.WriteLine("Entre com o Id do vértice:");
                    idVertice1 = int.Parse(Console.ReadLine());
                    foreach (Vertice v in gnd.vertices)
                    {
                        if (v.GetId() == idVertice1)
                        {
                            v1 = v;
                        }
                    }
                    Console.WriteLine(gnd.GetAGMKruskal(v1));
                    break;
                case 13: //cut vertices
                    Console.WriteLine("O grafo {0} tem {1} cut vértices.", gnd.GetIdGrafo(), gnd.GetCutVertices());
                    break;
            }
        }

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            List<Grafo> listaGrafos = LerArquivo(@path + "\\..\\..\\..\\Grafos.txt");
            Grafo g = null;
            int op = 0;

            Console.WriteLine("Passe o Id do grafo que você deseja consultar: ");
            int id = int.Parse(Console.ReadLine());

            foreach (Grafo grafo in listaGrafos)
            {
                if (grafo.GetIdGrafo() == id)
                {
                    g = grafo;
                }
            }

            Console.Clear();

            if (g.GetEhDirigido())
            {
                GrafoDirigido gd = (GrafoDirigido)g;
                while (op != 4)
                {
                    Console.WriteLine("Grafo dirigido! \n");
                    Console.WriteLine("Entre com 1 para saber o grau de entrada de um vértice: \n ");
                    Console.WriteLine("Entre com 2 para saber o grau de saída de um vértice: \n ");
                    Console.WriteLine("Entre com 3 para saber o grafo tem um ciclo: \n ");
                    Console.WriteLine("Aperte 4 para sair: \n ");
                    op = int.Parse(Console.ReadLine());
                    Console.Clear();
                    MenuDirigido(op, gd);
                    Console.WriteLine("\n");
                }

            }
            else
            {
                GrafoNaoDirigido gnd = (GrafoNaoDirigido)g;
                while (op != 14)
                {
                    Console.WriteLine("Grafo não dirigido! \n");
                    Console.WriteLine("Entre com 1 para saber se dois vértices são adjacentes: \n");
                    Console.WriteLine("Entre com 2 para saber o grau um vértice: \n");
                    Console.WriteLine("Entre com 3 para saber o vértice é isolado: \n");
                    Console.WriteLine("Entre com 4 para saber o vértice é pendente: \n");
                    Console.WriteLine("Entre com 5 para saber se o grafo é regular: \n");
                    Console.WriteLine("Entre com 6 para saber se o grafo é nulo: \n");
                    Console.WriteLine("Entre com 7 para saber se o grafo é completo: \n");
                    Console.WriteLine("Entre com 8 para saber se o grafo é conexo: \n");
                    Console.WriteLine("Entre com 9 para saber se o grafo é euleriano: \n");
                    Console.WriteLine("Entre com 10 para saber se o grafo é unicursal: \n ");
                    Console.WriteLine("Entre com 11 para saber a AGM pelo algoritmo de Prim: \n ");
                    Console.WriteLine("Entre com 12 para saber a AGM pelo algoritmo de Kruskal: \n ");
                    Console.WriteLine("Entre com 13 para saber a quantidade de cut vértices \n ");
                    Console.WriteLine("Aperte 14 para sair: \n ");
                    op = int.Parse(Console.ReadLine());
                    Console.Clear();
                    MenuNaoDirigido(op, gnd);
                    Console.WriteLine("\n");

                }
            }
            Console.ReadKey();
        }
    }
}

