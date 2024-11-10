using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Text;

namespace Scaner_Automata
{
  
    internal class Automata
    {
        private int valorDeConstantes = 200;
        private int valorDeDinamicos = 100;

        private int[,] tablaTransiciones;

        private TipoChar tipoCharActual;
        private int estadoActual;

        private StringBuilder bufferIdentificador;
        private StringBuilder bufferConstante;

        // Conjuntos y diccionarios
        private (char, int) SignoDolar = ('$', 199);
        private (char, int) CadenaVacia = (' ', 99);
        private Dictionary<char, int> Delimitadores = new Dictionary<char, int>()
        {
            { '(', 50 },
            { ')', 51 },
            {  ';', 52 }
        };
        private Dictionary<char, int> Operadores = new Dictionary<char, int>()
        {
            { '+', 70 },
            { '-', 71 },
            {  '*', 72 },
            {  '/', 73 }
        };
        private Dictionary<int, string> ErroresDisponibles = new()
        {
            {100, "Sin errores"},
            {101, "Símbolo desconocido" },
            {102, "Elemento Inválido" }
        };
        private Dictionary<Token, int> Tipos = new()
        {
            {Token.Delimitadores, 5 },
            {Token.Operadores,  7},
            {Token.CadenaVacia, 2 },
            {Token.Identificador, 1 },
            {Token.SignoDolar, 4 },
            {Token.Constante, 3 },
            {Token.Regla, 6 }
        };


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
            this.tipoCharActual = TipoChar.EspacioBlanco; //TipoChar por defecto
            this.bufferConstante = new StringBuilder();
            this.bufferIdentificador = new StringBuilder();
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

        private bool elEstadoEsValido(int state)
        {
            int[] valids = { 1, 2, 3, 4, 8, 9, 10};
            return valids.Contains(state);
        }

        public AutomataResult EscanearTexto(string allText)
        {
            //Reinicia el automata
            this.estadoActual = 0;
            this.valorDeDinamicos = 100;
            this.valorDeConstantes = 200;
            this.bufferConstante.Clear();
            this.bufferIdentificador.Clear();

            var toReturn = new AutomataResult();

            //Seteando los datos y buffers para realizar los cálculos
            string[] lines = allText.Replace(" ", "").Split('\n'); //Es necesario considerar espacios en blanco?

            //Realiza el análisis
            for (int actualLineIndex = 0; actualLineIndex < lines.Length; actualLineIndex ++)
            {
                foreach(char c in lines[actualLineIndex])
                {
                    //Guardar valores anteriores
                    TipoChar tipoCharAnterior = tipoCharActual;
                    int estadoAnterior = estadoActual;

                    //Actualizar con los nuevos valores dada la transición
                    tipoCharActual = this.CategorizarCaracter(c);
                    estadoActual = this.tablaTransiciones[estadoActual, (int)tipoCharActual];


                    /// TiposChars (Separación): Lógica de separación de tokens usando delimitadores y operadores
                    if (tipoCharActual == TipoChar.Delimitador)
                    {
                        toReturn.RegistrosLexicos.Add(new RegistroLexico
                        {
                            LineaNum = actualLineIndex + 1,
                            Codigo = this.Delimitadores[c],
                            Token = c.ToString(),
                            Tipo = this.Tipos[Token.Delimitadores]
                        });

                        if (elEstadoEsValido(estadoAnterior))
                        {
                            if (bufferConstante.ToString() != String.Empty)
                            {
                                ///El texto anterior al separador era una constante (Número) válido
                                toReturn.RegistrosConstantes.Add(new RegistroConstante
                                {
                                    ConstanteTexto = bufferConstante.ToString(),
                                    LineaEnDondeAparece = actualLineIndex + 1,
                                    Valor = this.valorDeConstantes++
                                });


                                ///Limpia como el "cache" de la palabra almacenada para dar cabida al siguiente
                                bufferConstante.Clear();
                            }
                            else if (bufferIdentificador.ToString() != String.Empty)
                            {
                                ///El texto anterior al separador era un identificador válido
                                ///
                                ///Checar si no se encuentra ya este identificador
                                RegistroDinamico? identificadorExistente =
                                    toReturn.RegistrosDinamicos.Find(rd => rd.IdentificadorTexto == bufferIdentificador.ToString());

                                //Si? Agregar solamente la línea nueva
                                if (identificadorExistente != null)
                                {
                                    identificadorExistente.LineasEnDondeAparece.Add(actualLineIndex + 1);
                                }
                                //No? Crear un registro nuevo y la línea en la que se encuentra
                                else
                                {
                                    RegistroDinamico nuevoIdentificador = new RegistroDinamico
                                    {
                                        IdentificadorTexto = bufferIdentificador.ToString(),
                                        Valor = this.valorDeDinamicos++
                                    };
                                    nuevoIdentificador.LineasEnDondeAparece.Add(actualLineIndex + 1);
                                    toReturn.RegistrosDinamicos.Add(nuevoIdentificador);
                                }

                                //Limpia el cache de la palabra almacenada previa
                                bufferConstante.Clear();
                            }

                            bufferConstante.Clear();
                            bufferIdentificador.Clear();
                        }
                    }

                    if (tipoCharActual == TipoChar.Operador)
                    {
                        toReturn.RegistrosLexicos.Add(new RegistroLexico
                        {
                            LineaNum = actualLineIndex + 1,
                            Codigo = this.Operadores[c],
                            Token = c.ToString(),
                            Tipo = this.Tipos[Token.Operadores]
                        });


                        if (elEstadoEsValido(estadoAnterior))
                        {
                            if (bufferConstante.ToString() != String.Empty)
                            {
                                ///El texto anterior al separador era una constante (Número) válido
                                toReturn.RegistrosConstantes.Add(new RegistroConstante
                                {
                                    ConstanteTexto = bufferConstante.ToString(),
                                    LineaEnDondeAparece = actualLineIndex + 1,
                                    Valor = this.valorDeConstantes++
                                });

                                ///Limpia como el "cache" de la palabra almacenada para dar cabida al siguiente
                                bufferConstante.Clear();
                            }
                            else if (bufferIdentificador.ToString() != String.Empty)
                            {
                                ///El texto anterior al separador era un identificador válido
                                ///
                                ///Checar si no se encuentra ya este identificador
                                RegistroDinamico? identificadorExistente =
                                    toReturn.RegistrosDinamicos.Find(rd => rd.IdentificadorTexto == bufferIdentificador.ToString());

                                //Si? Agregar solamente la línea nueva
                                if (identificadorExistente != null)
                                {
                                    identificadorExistente.LineasEnDondeAparece.Add(actualLineIndex + 1);
                                }
                                //No? Crear un registro nuevo y la línea en la que se encuentra
                                else
                                {
                                    RegistroDinamico nuevoIdentificador = new RegistroDinamico
                                    {
                                        IdentificadorTexto = bufferIdentificador.ToString(),
                                        Valor = this.valorDeDinamicos++
                                    };
                                    nuevoIdentificador.LineasEnDondeAparece.Add(actualLineIndex + 1);
                                    toReturn.RegistrosDinamicos.Add(nuevoIdentificador);
                                }

                                //Limpia el cache de la palabra almacenada previa
                                bufferConstante.Clear();
                            }
                        }

                         bufferConstante.Clear();
                         bufferIdentificador.Clear();

                    }

                  

                    // TiposChars (Independientes): Estos son los que por si sólos son válidos
                    if (tipoCharActual == TipoChar.Letra)
                        bufferIdentificador.Append(c);

                    if (
                            tipoCharActual == TipoChar.Digito || 
                            tipoCharActual == TipoChar.Exponencial || 
                            tipoCharActual == TipoChar.PuntoFlotante
                       )
                    {
                        ///Checa si el buffer identificador tiene chars dentro
                        ///         (Significa que se está analizando un identificador actualmente)
                        ///o de lo contrario significa que se está analizando una constante actualmente  
                        /// ""Solo se usa un buffer a la vez""

                        if (bufferIdentificador.ToString() != String.Empty)
                            bufferIdentificador.Append(c);
                        else
                            bufferConstante.Append(c);
                    }



                }
            }

            return toReturn;
        }
    }
}



   //if (sb.ToString() != String.Empty)
   //                     {
   //                         //Checar si no se encuentra ya este identificador
   //                         RegistroDinamico? identificadorExistente =
   //                             toReturn.RegistrosDinamicos.Find(rd => rd.IdentificadorTexto == sb.ToString());

   //                         //Si? Agregar solamente la línea nueva
   //                         if (identificadorExistente != null)
   //                         {
   //                             identificadorExistente.LineasEnDondeAparece.Add(actualLineIndex + 1);
   //                         }
   //                         //No? Crear un registro nuevo y la línea en la que se encuentra
   //                         else
   //                         {
   //                             RegistroDinamico nuevoIdentificador = new RegistroDinamico
   //                             {
   //                                 IdentificadorTexto = sb.ToString(),
   //                                 Valor = this.valorDeDinamicos++
   //                             };
   //                             nuevoIdentificador.LineasEnDondeAparece.Add(actualLineIndex + 1);
   //                             toReturn.RegistrosDinamicos.Add(nuevoIdentificador);
   //                         }

   //                         //Limpia el cache de la palabra almacenada previa
   //                         sb.Clear();
   //                     }

   //                     //Continua con la lógica normal de "Delimitador"

