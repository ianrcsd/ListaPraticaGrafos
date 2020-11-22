using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPraticaGrafos
{
    class GrafoNaoDirigido : Grafo
    {
        public GrafoNaoDirigido(List<Vertice> v, List<Aresta> a) : base(v, a) { }
        public GrafoNaoDirigido(int id) : base(id) { }

        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            bool adjacente = false;
            v1.GetArestas().ToArray();
            v2.GetArestas().ToArray();


            for (int i = 0; i < v1.GetArestas().Count; i++)
            {
                for (int j = 0; j < v2.GetArestas().Count; j++)
                {
                    if (v1.GetArestas()[i] == v2.GetArestas()[j])
                        adjacente = true;
                    break;
                }
            }
            return adjacente;
        }

        public int GetGrau(Vertice v1)
        {
            return v1.GetArestas().Count;
        }

        public bool IsIsolado(Vertice v1)
        {
            if (v1.GetArestas().Count == 0)
                return true;
            else
                return false;
        }

        public bool IsPendente(Vertice v1)
        {
            if (GetGrau(v1) == 1)
                return true;
            else
                return false;
        }

        public bool IsRegular()
        {
            bool regular = true;
            vertices.ToArray();

            for (int i = 0; i < vertices.Count; i++)
            {
                int aux = 1;
                if (i == vertices.Count() - 1)
                    aux = 0;
                if (GetGrau(vertices[i]) != GetGrau(vertices[i + aux]))
                {
                    regular = false;
                    break;
                }
            }
            return regular;

        }

        public bool IsNulo()
        {
            bool nulo = true;
            foreach (Vertice v in vertices)
            {
                if (!IsIsolado(v))
                    nulo = false;
                break;
            }
            return nulo;
        }

        public bool IsCompleto()
        {
            int numVertices = vertices.Count;
            int numArestas = arestas.Count;

            if (numArestas == ((numVertices * (numVertices - 1) / 2)))
                return true;
            else
                return false;
        }



        public bool IsEuleriano()
        {
            bool euleriano = true;

            foreach (Vertice v in vertices)
            {
                if (GetGrau(v) % 2 == 1)
                    euleriano = false;
            }
            return euleriano;
        }

        public bool IsUnicursal()
        {
            int soma = 0;

            foreach (Vertice v in vertices)
            {
                if (GetGrau(v) % 2 == 1)
                    soma++;
            }

            if (soma == 2)
                return true;
            else
                return false;
        }

        int componentes;

        public bool IsConexo()
        {

            if (Dfs() == 1)
                return true;
            else
                return false;
        }

        public int Dfs()
        {
            componentes = 0;
            foreach (Vertice v in vertices)
            {
                v.SetCor(0);
                v.SetPai(null);
            }
            int timeStamp = 0;


            foreach (Vertice v in vertices)
            {
                if (v.GetCor() == 0)
                {
                    Visitar(v, timeStamp);
                    componentes++;
                }
            }
            return componentes;
        }

        private void Visitar(Vertice v, int timeStamp)
        {
            timeStamp++;
            v.SetDescoberta(timeStamp);
            v.SetCor(1);

            foreach (Aresta a in v.GetArestas())
            {
                if (a.GetVerticeFinal().GetCor() == 0)
                {
                    a.GetVerticeFinal().SetPai(v);
                    v.GetFilhos().Add(a.GetVerticeInicial());
                    Visitar(a.GetVerticeFinal(), timeStamp);
                }
            }
            v.SetCor(2);
            timeStamp++;
            v.SetTermino(timeStamp);
        }

        public Grafo GetAGMPrim(Vertice v1)
        {
            List<Vertice> arvore = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
            Grafo prim;
            int maiorPeso = Int32.MaxValue;


            if (!IsConexo()) return null;

            Aresta menorAresta = null;

            if (arestas.Count == 0)
            {
                menorAresta = v1.GetMenorAresta();
                menorAresta.SetEmUso(true);
                arestas.Add(menorAresta);

            }

            while(menorAresta != null) {

                foreach (Aresta aresta in arestas) {
                    Aresta menorArestaInicial = aresta.GetVerticeInicial().GetMenorAresta();
                    Aresta menorArestaFinal = aresta.GetVerticeFinal().GetMenorAresta();

                    if (menorArestaFinal.GetPeso() < menorArestaInicial.GetPeso() && !menorArestaFinal.GetEmUso()) {
                        menorAresta = menorArestaFinal;
                    }
                    else if(!menorArestaInicial.GetEmUso()) {
                        menorAresta = menorArestaInicial;
                    } else {
                        menorAresta = null;
                    }

                }
                menorAresta.SetEmUso(true);
                arestas.Add(menorAresta);
            }



            return prim;
        }

        public Grafo GetAGMKruskal(Vertice v1)
        {
            List<Vertice> t = new List<Vertice>();
            List<Aresta> a = new List<Aresta>();
            Grafo kruskal;

            if (IsConexo())
            {

                arestas = BubbleSort(arestas);

                int[] chefes = Enumerable.Range(0, vertices.Count).ToArray();

                t.Add(v1);

                foreach (Aresta aresta in arestas)
                {
                    int chefeInicial = Chefe(aresta.GetVerticeInicial().GetId(), chefes);
                    int chefeFinal = Chefe(aresta.GetVerticeFinal().GetId(), chefes);

                    if (chefeInicial != chefeFinal)
                    {
                        a.Add(aresta);
                        Console.WriteLine(aresta.GetPeso());
                        t.Add(aresta.GetVerticeFinal());
                        chefes[chefeFinal] = chefeInicial;
                    }
                }

            }
            kruskal = new Grafo(t, a);

            return kruskal;

        }
        /// <summary>
        /// ordenação ascendente
        /// </summary>
        /// <param name = "vet" ></ param >
        public List<Aresta> BubbleSort(List<Aresta> a)
        {

            for (int i = 1; i < a.ToArray().Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (a[i].GetPeso() < a[j].GetPeso())
                    {
                        Aresta temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
            return a;
        }

        private int Chefe(int id, int[] chefes)
        {
            int chefe = id;
            if (id == chefes.Length)
            {
                chefe--;
                id--;
            }
            while (chefe != chefes[chefe])
            {
                chefe = chefes[chefe];
            }
            while (id != chefe)
            {
                int chefeAntigo = chefes[id];
                chefes[id] = chefe;
                id = chefeAntigo;
            }
            return chefe;
        }

        public int GetCutVertices()
        {
            int cutVertices = 0;
            if (IsConexo() && !IsCompleto()) //grafos completos não tem cut vertices
            {
                Dfs();
                foreach (Vertice v in vertices)
                {
                    if (v.GetPai() == null && v.GetFilhos().Count >= 2) //é uma raiz com mais de 2 filhos
                    {
                        cutVertices++;
                    }
                    if (v.GetPai() != null && v.GetFilhos() != null) //não é uma folha
                    {
                        cutVertices++;
                    }
                }
            }
            return cutVertices;
        }

    }
}
