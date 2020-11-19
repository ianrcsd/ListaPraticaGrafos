using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPraticaGrafos
{
    class Grafo
    {
        public List<Vertice> vertices;
        public List<Aresta> arestas;
        private int idGrafo;
        public int IdGrafo { get => idGrafo; set => idGrafo = value; }

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
    }
}
