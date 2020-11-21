using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPraticaGrafos
{
    class GrafoDirigido : Grafo
    {

        public GrafoDirigido(List<Vertice> v, List<Aresta> a) : base(v, a)
        {
        }

        public GrafoDirigido(int id) : base(id)
        {

        }

        public bool isDirigido()
        {
            return true;
        }
        public int GetGrauEntrada(Vertice v1)
        {
            int count = 0;
            foreach (Aresta a in v1.GetArestas())
            {
                if (a.GetDirecao() == -1)
                    count++;
            }
            return count;
        }

        public int GetGrauSaida(Vertice v1)
        {
            int count = 0;
            foreach (Aresta a in v1.GetArestas())
            {
                if (a.GetDirecao() == 1)
                    count++;
            }
            return count;
        }
        public bool HasCiclo()
        {
           

            if (DfsModificado() == 1)
                return true;
            else
                return false;
        }

        public int DfsModificado()
        {
            int i = 0;
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
                    i = VisitarModificado(v, timeStamp);
                }
            }
            return i;
        }

        public int VisitarModificado(Vertice v, int timeStamp)
        {
            timeStamp++;
            v.SetDescoberta(timeStamp);
            v.SetCor(1);
            int i = 0;

            foreach (Aresta a in v.GetArestas())
            {
                if (a.GetDirecao() == 1)
                {
                    if (a.GetVerticeFinal().GetCor() == 0)
                    {
                        a.GetVerticeFinal().SetPai(v);
                        VisitarModificado(a.GetVerticeFinal(), timeStamp);
                    }
                    else if(a.GetVerticeFinal().GetCor() == 1)
                    {
                        i = 1;//tem ciclo
                    }                        
                }
                
            }
            v.SetCor(2);
            timeStamp++;
            v.SetTermino(timeStamp);
            return i;
        }
    }
}
