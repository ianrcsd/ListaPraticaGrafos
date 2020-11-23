namespace ListaPraticaGrafos
{
    public class Aresta : IDados
    {
        //private int id;
        private int peso;
        private int direcao;
        private Vertice verticeInicial;
        private Vertice verticeFinal;
        private bool emUso;
        
        public Aresta(int peso, int direcao, Vertice vI, Vertice vF)
        {
            this.peso = peso;
            this.direcao = direcao;
            this.verticeInicial = vI;
            this.verticeFinal = vF;
            this.emUso = false;
        }
        public int GetPeso()
        {
            return peso;
        }

        public void SetPeso(int peso)
        {
            this.peso = peso;
        }

        public int GetDirecao()
        {
            return direcao;
        }
        public void SetDirecao(int direcao) //somente para grafos dirigidos
        {
            this.direcao = direcao;
        }

        public Vertice GetVerticeInicial()
        {
            return this.verticeInicial;
        }

        public void SetVerticeInicial(Vertice v)
        {
            this.verticeInicial = v;
        }

        public Vertice GetVerticeFinal()
        {
            return this.verticeFinal;
        }

        public void SetVerticeFinal(Vertice v)
        {
            if (v != GetVerticeInicial())
                this.verticeFinal = v;
            else
                this.verticeFinal = null;
        }

        public bool GetEmUso()
        {
            return emUso;
        }

        public void SetEmUso(bool uso)
        {
            this.emUso = uso;
        }
    }
}