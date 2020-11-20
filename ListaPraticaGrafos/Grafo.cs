using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPraticaGrafos
{
    class Grafo
    {
        public List<Vertice> vertices = new List<Vertice>();
        public List<Aresta> arestas = new List<Aresta>();
        private bool ehDirigido = true;
        private int idGrafo;
        public int IdGrafo { get => idGrafo; set => idGrafo = value; }
        public bool EhDirigido { get => ehDirigido; set => ehDirigido = value; }

        public Grafo(List<Vertice> v, List<Aresta> a)
        {
            vertices = v;
            arestas = a;
        }
        public Grafo(int id)
        {
            idGrafo = id;
        }

        public void setVertice(Vertice v)
        {
            vertices.Add(v);
        }

        public void setAresta(Aresta a)
        {
            arestas.Add(a);
        }

        public override string ToString()
        {
            String arvore = "Arvore ";
            foreach (Vertice v in vertices)
            {
                arvore += "\nVertice " + v.GetId();
            }
            return arvore;
        }
    }
}
