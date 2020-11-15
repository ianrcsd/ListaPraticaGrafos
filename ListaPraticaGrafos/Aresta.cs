namespace ListaPraticaGrafos
{
    public class Aresta : IDados
    {
        //private int id;
        private int peso;
        private int direcao;
        private Vertice verticeInicial;
        private Vertice verticeFinal;
        
        public Aresta(int peso, int direcao, Vertice vI, Vertice vF)
        {
            //this.id = id;
            this.peso = peso;
            this.direcao = direcao;
            this.verticeInicial = vI;
            this.verticeFinal = vF;
        }
        //public int GetId()
        //{
        //    return id;
        //}

        //public void SetId(int id)
        //{
        //    this.id = id;
        //}

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
        //precisa ver a validação do grafo não dirigido. Pensei em colocar 0
        public void SetDirecao(int direcao)
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
    }
}