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

        public Grafo(List<Vertice> v, List<Aresta> a)
        {
            vertices = v;
            arestas = a;
        }
    }
}
