namespace Scaner_Automata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var algo = new Automata();

            var another = algo.EscanearTexto("asdf");
            var myList = another.RegistrosLexicos;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
