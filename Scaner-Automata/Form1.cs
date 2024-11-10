namespace Scaner_Automata
{
    public partial class Form1 : Form
    {
        private string TextoDummy = "(X1+B2);\r\n(Y1+B3*C4)+D;\r\n(((VAR2+X1)));\r\n(PESO+(CARGO*DIF2));\r\n((X2+45.78) * (CARGO/ABONO) - (PORC*12.55)) - INT;\r\n456.78* (12.34*3.56E45) +B2;";
        Automata automata = new Automata();
        string lineas;

        public Form1()
        {
            InitializeComponent();
            var algo = new Automata();
            var another = algo.EscanearTexto(TextoDummy);
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            dgvId.Rows.Clear();
            dgvCons.Rows.Clear();
            dgvLexica.Rows.Clear();

            if (txtTexto.Text != null)
            {
                var a = automata.EscanearTexto(txtTexto.Text);

                foreach (RegistroDinamico obj in a.RegistrosDinamicos)
                {
                    foreach (int linea in obj.LineasEnDondeAparece)
                    {
                        lineas += linea.ToString() + " ";
                    }

                    dgvId.Rows.Add(obj.IdentificadorTexto, obj.Valor, lineas);
                    lineas = "";

                }

            }
        }
    }
}
