using System.Data.Common;

namespace Scaner_Automata
{
    public partial class Form1 : Form
    {
        private string TextoDummy = "(X1+B2);\r\n(Y1+B3*C4)+D;\r\n(((VAR2+X1)));\r\n(PESO+(CARGO*DIF2));\r\n((X2+45e78) * (CARGO/ABONO) - (PORC*12.55)) - INT;\r\n456.78* (12.34*3.56E45) +B2;";
        Automata automata = new Automata();
        string lineas;
        int numero;

        public Form1()
        {
            InitializeComponent();
            //var algo = new Automata();
            //var another = algo.EscanearTexto(TextoDummy);
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            numero = 0;
            dgvId.Rows.Clear();
            dgvCons.Rows.Clear();
            dgvLexica.Rows.Clear();

            if (TextoDummy != null)
            {
                var a = automata.EscanearTexto(TextoDummy, lblMensaje);

                foreach (RegistroDinamico obj in a.RegistrosDinamicos)
                {
                    foreach (int linea in obj.LineasEnDondeAparece)
                    {
                        lineas += linea.ToString() + " ";
                    }

                    dgvId.Rows.Add(obj.IdentificadorTexto, obj.Valor, lineas);
                    lineas = "";

                }

                foreach (RegistroConstante obj in a.RegistrosConstantes)
                {
                    dgvCons.Rows.Add(obj.ConstanteTexto, obj.Valor, obj.LineaEnDondeAparece);
                }

                foreach (RegistroLexico obj in a.RegistrosLexicos)
                {
                    numero++;
                    dgvLexica.Rows.Add(numero, obj.LineaNum, obj.Token, obj.Tipo, obj.Codigo);
                }

                if (a.huboErrores == false)
                {
                    lblMensaje.Text = "Mensaje | 1:100 Sin errores.";
                }

            }
        }


    }
}
