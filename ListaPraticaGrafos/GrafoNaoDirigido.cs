﻿using System;
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

            for (int i = 0; i < v1.GetArestas().Count; i++) //analisa se os dois vértices compartilham a mesma aresta
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

        public bool IsIsolado(Vertice v1) // isolado é quando tem grau 0
        {
            if (v1.GetArestas().Count == 0)
                return true;
            else
                return false;
        }

        public bool IsPendente(Vertice v1) // isolado é quando tem grau 1
        {
            if (GetGrau(v1) == 1)
                return true;
            else
                return false;
        }

        public bool IsRegular() // se todos os vertices tem o mesmo grau
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

        public bool IsNulo() // ou trivial, todo os vértices com grau 1 (isolado)
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

        public bool IsCompleto() // implementei o teorema da soma dos vértices que prova se o grafo é completo
        {
            int numVertices = vertices.Count;
            int numArestas = arestas.Count;

            if (numArestas == ((numVertices * (numVertices - 1) / 2)))
                return true;
            else
                return false;
        }

        public bool IsEuleriano() //precisa ter todos os vértices de grau par
        {
            bool euleriano = true;

            foreach (Vertice v in vertices)
            {
                if (GetGrau(v) % 2 == 1)
                    euleriano = false;
            }
            return euleriano;
        }

        public bool IsUnicursal() //precisa ter dois vértices de grau par
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
        int isCutVertice = 0;

        public bool IsConexo() //se a busca em profundidde (dfs) retornar mais de 1 componente, não é conexo
        {

            if (Dfs() == 1)
                return true;
            else
                return false;
        }

        public int Dfs() //busca em profundidade
        {
            componentes = 0;
            foreach (Vertice v in vertices) //inicializa o grafo com todos o vertices brancos (0) e pai null
            {
                v.SetCor(0);
                v.SetPai(null);
            }
            int timeStamp = 0;

            foreach (Vertice v in vertices) // se o vertice for branco, visitar o adjacente
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
            v.SetCor(1); //assim que é visitado, o vértice é marcado de cinza(1)

            foreach (Aresta a in v.GetArestas())
            {
                if (a.GetVerticeFinal().GetCor() == 0)
                {
                    a.GetVerticeFinal().SetPai(v);
                    foreach (Aresta are in a.GetVerticeFinal().arestas)
                    {
                        if (are.GetVerticeFinal().GetCor() == 1)
                        {
                            isCutVertice++;
                        }
                    }
                    if (v.GetFilhos().Count == 0)
                    {
                        v.GetFilhos().Add(a.GetVerticeFinal());
                    }
                    else
                    {
                        foreach (Vertice ver in v.GetFilhos())
                        {
                            if (ver.GetId() == a.GetVerticeFinal().GetId()) break;
                            else
                            {
                                v.GetFilhos().Add(a.GetVerticeFinal());
                            }
                        }
                    }

                    Visitar(a.GetVerticeFinal(), timeStamp);
                }
            }
            v.SetCor(2); //marca o vértice como preto (2) depois de ter todas as arests visitadas
            timeStamp++;
            v.SetTermino(timeStamp);
        }

        public Grafo GetAGMPrim(Vertice v1)
        {
            List<Vertice> vertices = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
            int totalVertices = this.vertices.Count;
            Aresta menorAresta = null;

            if (!IsConexo()) return null;

            if (arestas.Count == 0)
            {
                menorAresta = v1.GetMenorArestaDisponivel();
                menorAresta.SetEmUso(true);
                arestas.Add(menorAresta);
                vertices.Add(v1);
                vertices.Add(menorAresta.GetVerticeFinal());

            }

            while (menorAresta != null)
            {
                menorAresta = null;
                foreach (Aresta aresta in arestas)
                {
                    Aresta menorArestaInicial = aresta.GetVerticeInicial().GetMenorArestaDisponivel();
                    Aresta menorArestaFinal = aresta.GetVerticeFinal().GetMenorArestaDisponivel();

                    if (vertices.Count == totalVertices) menorAresta = null;

                    else if (menorArestaInicial == null && menorArestaFinal != null)
                    {
                        if (menorAresta == null)
                            menorAresta = menorArestaFinal;
                        else if (menorAresta.GetPeso() > menorArestaFinal.GetPeso())
                            menorAresta = menorArestaFinal;
                    }
                    else if (menorArestaFinal == null && menorArestaInicial != null)
                    {
                        if (menorAresta == null)
                            menorAresta = menorArestaInicial;
                        else if (menorAresta.GetPeso() > menorArestaInicial.GetPeso())
                            menorAresta = menorArestaInicial;
                    }
                    else if (menorArestaFinal == null && menorArestaInicial == null) menorAresta = null;
                    else if (menorArestaFinal.GetPeso() < menorArestaInicial.GetPeso() && !menorArestaFinal.GetEmUso())
                    {
                        menorAresta = menorArestaFinal;
                    }
                    else if (!menorArestaInicial.GetEmUso())
                    {
                        menorAresta = menorArestaInicial;
                    }
                    else {
                        menorAresta = null;
                    }
                }

                if (menorAresta != null)
                {
                    menorAresta.SetEmUso(true);
                    arestas.Add(menorAresta);
                    vertices.Add(menorAresta.GetVerticeFinal());

                }
            }
            Grafo prim = new Grafo(vertices, arestas);

            return prim;
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

        public Grafo GetAGMKruskal(Vertice v1)
        {
            List<Vertice> vertices = new List<Vertice>();
            List<Aresta> arestas = new List<Aresta>();
            Grafo kruskal;

            if (!IsConexo()) return null;


            arestas = BubbleSort(this.arestas);

            int[] chefes = new int[this.vertices.Count];

            for (int i = 0; i < chefes.Length; i++)
            {
                chefes[i] = i;
            }

            vertices.Add(v1);

            foreach (Aresta aresta in arestas)
            {
                int chefeInicial = Chefe(aresta.GetVerticeInicial().GetId(), chefes);
                int chefeFinal = Chefe(aresta.GetVerticeFinal().GetId(), chefes);

                if (chefeInicial != chefeFinal)
                {
                    arestas.Add(aresta);
                    vertices.Add(aresta.GetVerticeFinal());
                    chefes[chefeFinal] = chefeInicial;
                }
            }
            kruskal = new Grafo(vertices, arestas);

            return kruskal;

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
