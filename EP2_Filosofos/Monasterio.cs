using System;
using System.Windows.Forms;

namespace EP2_Filosofos
{
    public partial class Monasterio : Form
    {
        #region Propriedades
        internal Filosofo f1;
        internal Filosofo f2;
        internal Filosofo f3;
        #endregion
        #region Construtores e Inicializadores
        public Monasterio()
        {
            InitializeComponent();
        }
        private void Monasterio_Load(object sender, EventArgs e)
        {
            Inicializa();
        }
        #endregion
        #region Eventos UI
        private void btnA_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnF1A":
                    f1.Acao();
                    break;
                case "btnF2A":
                    f2.Acao();
                    break;
                case "btnF3A":
                    f3.Acao();
                    break;
            }
            AtualizaLabels();
        }
        private void resetarEstadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inicializa();
        }
        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Sobre().ShowDialog();
        }
        #endregion
        #region Metodos Auxiliares
        public void AtualizaLabels()
        {
            string[] maosFilosofo;
            
            lblF1E.Text = f1.ExibeEstadoFun();
            maosFilosofo = VerificaMaos(f1);
            lblF1MD.Text = maosFilosofo[1];
            lblF1ME.Text = maosFilosofo[0];
            btnF1A.Text = f1.ExibeEstadoAcaoFun();

            lblF2E.Text = f2.ExibeEstadoFun();
            maosFilosofo = VerificaMaos(f2);
            lblF2MD.Text = maosFilosofo[1];
            lblF2ME.Text = maosFilosofo[0];
            btnF2A.Text = f2.ExibeEstadoAcaoFun();

            lblF3E.Text = f3.ExibeEstadoFun();
            maosFilosofo = VerificaMaos(f3);
            lblF3MD.Text = maosFilosofo[1];
            lblF3ME.Text = maosFilosofo[0];
            btnF3A.Text = f3.ExibeEstadoAcaoFun();
        }
        static string[] VerificaMaos(Filosofo filosofo)
        {
            string[] descMaos = new string[2];

            for (var i = 0; i < filosofo.Garfos.Length; i++)
            {
                if (filosofo.Garfos[i])
                {
                    descMaos[i] = String.Format("Garfo {0}", (filosofo.isSujo[i] ? "Sujo" : "Limpo"));
                }
            }
            for (var i = 0; i < filosofo.Tokens.Length; i++)
            {
                if (filosofo.Tokens[i])
                {
                    descMaos[i] = String.IsNullOrEmpty(descMaos[i]) ? "Token" : String.Format("{0}, Token", descMaos[i]);
                }
            }
            return descMaos;
        }
        public void Inicializa()
        {
            f1 = new Filosofo(Filosofo.Estados.Dormindo, new bool[2] { true, true }, new bool[2] { false, false }, new bool[2] { true, true });
            f2 = new Filosofo(Filosofo.Estados.Dormindo, new bool[2] { false, true }, new bool[2] { true, false }, new bool[2] { false, true });
            f3 = new Filosofo(Filosofo.Estados.Dormindo, new bool[2] { false, false }, new bool[2] { true, true }, new bool[2] { false, false });

            f1.VizinhoDireita = f2;
            f1.VizinhoEsquerda = f3;
            f2.VizinhoDireita = f3;
            f2.VizinhoEsquerda = f1;
            f3.VizinhoDireita = f1;
            f3.VizinhoEsquerda = f2;

            AtualizaLabels();
        }
        #endregion
    }
}
