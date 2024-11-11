using System.Data.Common;
using System.Text;

namespace Scaner_Automata
{
    public partial class Form1 : Form
    {
        private string TextoDummy = "(X1+B2);\r\n(Y1+B3*C4)+D;\r\n(((VAR2+X1)));\r\n(PESO+(CARGO*DIF2));\r\n((X2+45e78) * (CARGO/ABONO) - (PORC*12.55)) - INT;\r\n456.78* (12.34*3.56E45) +B2;";

        Automata automata;

        public Form1()
        {
            InitializeComponent();
            this.automata = new();

            txtTexto.Text = "5.5.5";
            //this.btnLeer_Click(new object { }, new EventArgs());
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            dgvId.Rows.Clear();
            dgvCons.Rows.Clear();
            dgvLexica.Rows.Clear();

            AutomataResult a = automata.EscanearTexto(txtTexto.Text);
            bool hayErrores = a.Errores.Count > 0;

            List<string> buffer = new();
            foreach (RegistroDinamico obj in a.RegistrosDinamicos)
            {
                foreach (int linea in obj.LineasEnDondeAparece)
                    buffer.Add(linea.ToString());

                dgvId.Rows.Add(obj.IdentificadorTexto, obj.Valor, String.Join(", ", buffer));

                buffer.Clear();
            }

            foreach (RegistroConstante obj in a.RegistrosConstantes)
                dgvCons.Rows.Add(obj.ConstanteTexto, obj.Valor, obj.LineaEnDondeAparece);

            for (int registroActualIndex = 0; registroActualIndex < a.RegistrosLexicos.Count; registroActualIndex++)
            {
                RegistroLexico obj = a.RegistrosLexicos[registroActualIndex];
                dgvLexica.Rows.Add(registroActualIndex + 1, obj.LineaNum, obj.Token, obj.Tipo, obj.Codigo);
            }
        }

        private string GenerarTextoError(RegistroError re) => @$"1:{re.CodigoError} {re.DescripcionError}";
    }
}
