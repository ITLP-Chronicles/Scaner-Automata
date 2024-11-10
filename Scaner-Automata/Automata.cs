using System;
using System.Drawing.Drawing2D;

namespace Scaner_Automata
{
    public enum Token
    {
        Delimitadores = 50,
        Operadores = 70,
        CadenaVacia = 99,
        Identificador = 100,
        SignoDolar = 199,
        Constante = 200,
        Regla = 300
    }

    public enum TipoChar
    {
        Letra,
        Digito,
        Delimitador,
        Operador,
        Exponencial,
        EspacioBlanco,
        PuntoFlotante,
        Desconocido
    }

    public record RegistroLexico
    {
        public int LineaNum;
        public string Token = "";
        public int Tipo;
        public int Codigo;
    }

    public record RegistroDinamico
    {
        public string IdentificadorTexto = "";
        public int Valor;
        public List<int> LineasEnDondeAparece;
    }

    public record RegistroConstante
    {
        public string ConstanteTexto = "";
        public int Valor;
        public int LineaEnDondeAparece;
    }

    public record AutomataResult
    {
        public List<RegistroLexico> RegistrosLexicos = new();
        public List<RegistroDinamico> RegistrosDinamicos = new();
        public List<RegistroConstante> RegistrosConstantes = new();
    }

    internal class Automata
    {
        private int[,] tablaTransiciones;
        private int estadoActual;

        private Dictionary<char, int> Delimitadores = new Dictionary<char, int>()
        {
            { '(', 50 },
            { ')', 51 },
            {  ';', 52 }
        };

        private Dictionary<char, int> Operadores = new Dictionary<char, int>()
        {
            { '(', 70 },
            { '-', 71 },
            {  '*', 72 },
            {  '/', 73 }
        };

        private (char, int) SignoDolar = ('$', 199);
        private (char, int) CadenaVacia = (' ', 99);
        // Private Reglas?

        public Automata()
        {
            this.estadoActual = 0;
            this.tablaTransiciones = new int[,]
            {
                {1, 2, 3, 4, 5, 0, 2, 6 },
                {1, 1, 3, 4, 1, 0, 5, 6},
                {1, 2, 3, 4, 7, 0, 8, 6 },
                {1, 2, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 1, 0, 2, 6 },
                {5, 10,3, 4, 5, 0, 5, 6 },
                {5, 9, 3, 4, 5, 0, 5, 6 },
                {5, 9, 3, 4, 7, 0, 5, 6 },
                {5, 10, 3, 4, 5, 0, 5, 9 }
            };

        }

        private TipoChar CategorizarCaracter(char c)
        {
            if (c == '(' || c == ')' || c == ';') return TipoChar.Delimitador;
            if (c == '+' || c == '-' || c == '*' || c == '/') return TipoChar.Operador;
            if (c == '.') return TipoChar.PuntoFlotante;
            if (Char.IsDigit(c)) return TipoChar.Digito;
            if (Char.IsWhiteSpace(c)) return TipoChar.EspacioBlanco;

            if (c == 'e' || c == 'E') return TipoChar.Exponencial;
            if (Char.IsLetter(c) || c == '_') return TipoChar.Letra;

            return TipoChar.Desconocido;
        }

        public AutomataResult EscanearTexto(string allText)
        {
            AutomataResult toReturn = new();
            toReturn.RegistrosDinamicos = new List<RegistroDinamico>();
            string[] lines = allText.Split('\n');
            this.estadoActual = 0;

            foreach(string line in lines)
            {
                foreach(char c in line)
                {
                    TipoChar cToken = this.CategorizarCaracter(c);
                    estadoActual = this.tablaTransiciones[estadoActual, (int)cToken];

                    if (cToken == TipoChar.EspacioBlanco)
                    {
                        
                    } 
                }
            }
        }
    }
}
