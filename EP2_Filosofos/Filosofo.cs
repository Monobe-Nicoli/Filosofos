using System.Collections.Generic;

namespace EP2_Filosofos
{
    class Filosofo
    {
        #region Propriedades
        public bool[] Garfos { get; set; }
        public bool[] Tokens { get; set; }
        public bool[] isSujo { get; set; }
        public List<int> Memoria { get; set; }
        public enum Estados
        {
            Comendo,
            Dormindo,
            Faminto
        }
        public Estados Estado { get; set; }
        public Filosofo VizinhoEsquerda { get; set; }
        public Filosofo VizinhoDireita { get; set; }
        #endregion
        #region Ações Primárias
        public void Acao()
        {
            switch (this.Estado)
            {
                case Estados.Comendo:
                    this.Estado = Estados.Dormindo;
                    foreach (var item in this.Memoria)
                    {
                        LiberarGarfo(item);
                    }
                    this.Memoria.Clear();
                    break;

                case Estados.Dormindo:
                    this.Estado = Estados.Faminto;
                    for (var i = 0; i < this.Garfos.Length; i++)
                    {
                        if (!this.Garfos[i])
                            this.SolicitarGarfo(i);
                    }
                    this.TentaComer();
                    break;

                case Estados.Faminto:
                    System.Windows.Forms.MessageBox.Show("Guenta ae tio! a força está contigo...", "Consciência suprema");
                    break;
            }
        }
        #endregion
        #region Ações Secundárias
        public void SolicitarGarfo(int posicao)
        {
            if (this.Estado == Estados.Faminto && this.Tokens[posicao] && !this.Garfos[posicao])
            {
                this.Tokens[posicao] = false;

                EnviarToken(posicao);
            }
        }
        public void LiberarGarfo(int posicao)
        {
            if (this.Estado != Estados.Comendo && this.Tokens[posicao] && this.isSujo[posicao])
            {
                EnviarGarfo(posicao);
                this.isSujo[posicao] = false;
                this.Garfos[posicao] = false;

                if (this.Estado == Estados.Faminto)
                {
                    if (this.Tokens[0])
                    {
                        this.EnviarToken(0);
                        this.Tokens[0] = false;
                    }
                    else if (this.Tokens[1])
                    {
                        this.EnviarToken(1);
                        this.Tokens[1] = false;
                    }
                }
            }
            else
            {
                this.Memoria.Add(posicao);
            }
        }
        #endregion
        #region Ações Terciárias
        public void EnviarToken(int posicao)
        {
            if (posicao == 0)
            {
                this.VizinhoEsquerda.Tokens[1] = true;
                this.VizinhoEsquerda.LiberarGarfo(1);
            }
            else
            {
                this.VizinhoDireita.Tokens[0] = true;
                this.VizinhoDireita.LiberarGarfo(0);
            }
        }
        public void EnviarGarfo(int posicao)
        {
            if (posicao == 0)
            {
                this.VizinhoEsquerda.Garfos[1] = true;
                this.VizinhoEsquerda.TentaComer();
            }
            else
            {
                this.VizinhoDireita.Garfos[0] = true;
                this.VizinhoDireita.TentaComer();
            }

        }
        #endregion
        #region Ações Quaternárias
        public void TentaComer()
        {
            if (this.Garfos[0] && this.Garfos[1])
            {
                this.Estado = Estados.Comendo;
                this.isSujo[0] = true;
                this.isSujo[1] = true;
            }
        }
        #endregion
        #region Métodos Auxiliares
        public string ExibeEstadoFun()
        {
            return this.Estado == Filosofo.Estados.Dormindo ? "ZzZZZZZZz" :
                this.Estado == Filosofo.Estados.Comendo ? "Nom nom nom" : 
                "#Starvation :(";
        }
        public string ExibeEstadoAcaoFun()
        {
            return this.Estado == Filosofo.Estados.Dormindo ? "Partiu rango" :
                this.Estado == Filosofo.Estados.Comendo ? "Partiu siesta" : 
                "Partiu fome";
        }
        #endregion
        #region Construtor
        public Filosofo(Estados estado, bool[] garfos, bool[] tokens, bool[] isSujo)
        {
            this.Estado = estado;
            this.Garfos = garfos;
            this.Tokens = tokens;
            this.isSujo = isSujo;
            this.Memoria = new List<int>();
            this.Memoria.Capacity = 2;
        }
        #endregion
    }
}