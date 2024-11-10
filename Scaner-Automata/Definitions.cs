using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<int> LineasEnDondeAparece = new();
    }

    public record RegistroConstante
    {
        public string ConstanteTexto = "";
        public int Valor;
        public int LineaEnDondeAparece;
    }
    public record RegistroError
    {
        public int LineaEnDondeAparece;
        public int CodigoError;
        public string DescripcionError = "";
    }

    public record AutomataResult
    {
        public List<RegistroLexico> RegistrosLexicos = new();
        public List<RegistroDinamico> RegistrosDinamicos = new();
        public List<RegistroConstante> RegistrosConstantes = new();
    }

}
