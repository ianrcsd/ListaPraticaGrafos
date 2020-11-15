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

        bool IsIsolado(Vertice v1)
        {
            if (v1.GetArestas().Count == 0)
                return true;
            else
                return false;
        }

        bool IsPendente(Vertice v1)
        {
            if (GetGrau(v1) == 1)
                return true;
            else
                return false;
        }

        bool IsRegular()
        {
            bool regular = true;
            vertices.ToArray();

            for (int i = 0; i < vertices.Count; i++)
            {
                if (GetGrau(vertices[i]) != GetGrau(vertices[i++]))
                    regular = false;
            }
            return regular;

        }

        //bool IsNulo()
        //{
        //    if (v1.GetArestas().Count == 0)
        //        return true;
        //    else
        //        return false;
        //}

        bool IsCompleto()
        {
            int numVertices = vertices.Count;
            int numArestas = arestas.Count;

            if (numArestas == ((numVertices * (numVertices - 1) / 2)))
                return true;
            else
                return false;
        }

        bool IsConexo()
        {
            Dfs();
            if (componentes == 1)
                return true;
            else
                return false;
        }

        bool IsEuleriano()
        {
            bool euleriano = true;

            foreach (Vertice v in vertices)
            {
                if (GetGrau(v) % 2 == 1)
                    euleriano = false;
            }
            return euleriano;
        }

        bool IsUnicursal()
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
        public void Dfs()
        {
            componentes = 1;
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
            //return componentes;
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

        Grafo GetAGMPrim(Vertice v1)
        {
            List<Vertice> t = new List<Vertice>();
            List<Aresta> a = new List<Aresta>();
            Grafo prim;

            if (IsConexo())
            {
                t.Add(v1);
                while (t != vertices)
                {
                    v1.GetArestas().ToArray();
                    int menorPeso = Int32.MaxValue;
                    Vertice prox = v1;
                    Aresta aux = null;

                    for (int i = 0; i < v1.GetArestas().Count; i++)
                    {
                        if (v1.GetArestas()[i].GetPeso() > v1.GetArestas()[i++].GetPeso())
                        {
                            menorPeso = v1.GetArestas()[i++].GetPeso();
                            prox = v1.GetArestas()[i++].GetVerticeFinal();
                            aux = v1.GetArestas()[i++];
                        }
                        else if (v1.GetArestas()[i].GetPeso() == v1.GetArestas()[i++].GetPeso())
                        {
                            if (v1.GetArestas()[i].GetVerticeInicial().GetId() + v1.GetArestas()[i++].GetVerticeFinal().GetId() > v1.GetArestas()[i++].GetVerticeInicial().GetId() + v1.GetArestas()[i++].GetVerticeFinal().GetId())
                            {
                                menorPeso = v1.GetArestas()[i++].GetPeso();
                                prox = v1.GetArestas()[i++].GetVerticeFinal();
                                aux = v1.GetArestas()[i++];
                            }
                            else if (v1.GetArestas()[i].GetVerticeInicial().GetId() + v1.GetArestas()[i++].GetVerticeFinal().GetId() < v1.GetArestas()[i++].GetVerticeInicial().GetId() + v1.GetArestas()[i++].GetVerticeFinal().GetId())
                            {
                                menorPeso = v1.GetArestas()[i].GetPeso();
                                prox = v1.GetArestas()[i].GetVerticeFinal();
                                aux = v1.GetArestas()[i];
                            }
                            else
                            {
                                if (v1.GetArestas()[i].GetVerticeFinal().GetId() > v1.GetArestas()[i++].GetVerticeFinal().GetId())
                                {
                                    menorPeso = v1.GetArestas()[i++].GetPeso();
                                    prox = v1.GetArestas()[i++].GetVerticeFinal();
                                    aux = v1.GetArestas()[i++];
                                }
                                else
                                {
                                    menorPeso = v1.GetArestas()[i].GetPeso();
                                    prox = v1.GetArestas()[i].GetVerticeFinal();
                                    aux = v1.GetArestas()[i];
                                }
                            }
                        }
                    }
                    t.Add(prox);
                    a.Add(aux);
                    v1 = prox;
                }
            }
            prim = new Grafo(t, a);
            t.ToString();
            return prim;
        }

        Grafo GetAGMKruskal(Vertice v1)
        {
            List<Vertice> t = new List<Vertice>();
            List<Aresta> a = new List<Aresta>();
            Grafo kruskal;

            if (IsConexo())
            {
                arestas.Sort();

                int[] chefes = Enumerable.Range(0, vertices.Count).ToArray();

                t.Add(v1);

                foreach (Aresta aresta in arestas)
                {
                    int chefeInicial = Chefe(aresta.GetVerticeInicial().GetId(), chefes);
                    int chefeFinal = Chefe(aresta.GetVerticeFinal().GetId(), chefes);

                    if (chefeInicial != chefeFinal)
                    {
                        a.Add(aresta);
                        t.Add(aresta.GetVerticeFinal());
                        chefes[chefeFinal] = chefeInicial;
                    }
                }

            }
            kruskal = new Grafo(t, a);
            t.ToString();
            return kruskal;

        }
        /// <summary>
        /// ordenação ascendente
        /// </summary>
        /// <param name="vet"></param>
        //void BubbleSort(Aresta[] a)
        //{
        //    for (int i = 1; i < a.Length; i++)
        //    {
        //        for (int j = 0; j < i; j++)
        //        {
        //            if (a[i].GetPeso() < a[j].GetPeso())
        //            {
        //                Aresta temp = a[i];
        //                a[i] = a[j];
        //                a[j] = temp;
        //            }
        //        }
        //    }
        //}

        private int Chefe(int id, int[] chefes)
        {
            int chefe = id;
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

        int GetCutVertices()
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
                    if(v.GetPai()!=null && v.GetFilhos() != null) //não é uma folha
                    {
                        cutVertices++;
                    }
                }
            }
            return cutVertices;
        }


    }
}
