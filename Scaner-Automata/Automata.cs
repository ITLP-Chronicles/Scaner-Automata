using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using System.Text;

namespace Scaner_Automata
{
  
    internal class Automata
    {
        private readonly int[,] tablaTransiciones;

        private TipoChar tipoCharActual;
        private int estadoActual;

        // Manejo de constantes
        private const int ValorIdentificadorDefault = 100;
        private const int ValorConstanteDefault = 200;
        private int valorConstanteActual;
        private int valorIdentificadorActual;

        // Mini-Almacenes para almacenar identificadores y constantes
        private StringBuilder bufferIdentificador;
        private StringBuilder bufferConstante;
        private AutomataResult results;

        // Conjuntos y diccionarios
        private readonly (char, int) SignoDolar = ('$', 199);
        private readonly (char, int) CadenaVacia = (' ', 99);
        private readonly Dictionary<char, int> Delimitadores;
        private readonly Dictionary<char, int> Operadores;
        private readonly Dictionary<int, string> ErroresDisponibles;
        private readonly Dictionary<Token, int> Tipos;

        public Automata()
        {
           this.tablaTransiciones = new int[,]
            {
                {1, 2, 3, 4, 5, 0, 2, 6 },
                {1, 1, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 7, 0, 8, 6 },
                {1, 2, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 1, 0, 5, 6 },
                {1, 5, 3, 4, 1, 0, 5, 6 },
                {1, 2, 3, 4, 1, 0, 2, 6 },
                {5, 10,3, 4, 5, 0, 5, 6 },
                {5, 9, 3, 4, 5, 0, 5, 6 },
                {5, 9, 3, 4, 7, 0, 5, 6 },
                {5, 10, 3, 4, 5, 0, 5, 9 }
            }; 

            this.estadoActual = 0;
            this.tipoCharActual = TipoChar.EspacioBlanco; //TipoChar por defecto
            this.bufferConstante = new StringBuilder();
            this.bufferIdentificador = new StringBuilder();
            this.valorConstanteActual = ValorConstanteDefault;
            this.valorIdentificadorActual = ValorIdentificadorDefault;
            this.results = new AutomataResult();

            //---------------- Inicializando Diccionarios ------------------
            this.Tipos = new()
            {
                {Token.Delimitadores, 5 },
                {Token.Operadores,  7},
                {Token.CadenaVacia, 2 },
                {Token.Identificador, 1 },
                {Token.SignoDolar, 4 },
                {Token.Constante, 3 },
                {Token.Regla, 6 }
            };
            this.ErroresDisponibles = new()
            {
                {100, "Sin errores"},
                {101, "Símbolo desconocido" },
                {102, "Elemento Inválido" }
            };
            this.Operadores = new()
            {
                { '+', 70 },
                { '-', 71 },
                {  '*', 72 },
                {  '/', 73 }
            };
            this.Delimitadores = new()
            {
                { '(', 50 },
                { ')', 51 },
                {  ';', 52 }
            };
        }


        public AutomataResult EscanearTexto(string allText)
        {
            this.Resetear();
            string[] lineas = allText.Replace(" ", "").Replace("\r", "").Split('\n');

            //Realiza el análisis
            for (int lineaActualIndex = 0; lineaActualIndex < lineas.Length; lineaActualIndex ++)
            {
                foreach(char c in lineas[lineaActualIndex])
                {
                    TipoChar tipoCharAnterior = tipoCharActual;
                    int estadoAnterior = estadoActual;

                    tipoCharActual = this.CategorizarCaracter(c);
                    estadoActual = this.tablaTransiciones[estadoActual, (int)tipoCharActual];

                    /// TiposChars (Separación): Lógica de separación de tokens usando delimitadores y operadores
                    if (tipoCharActual == TipoChar.Delimitador || tipoCharActual == TipoChar.Operador)
                    {
                        if (elEstadoEsValido(estadoAnterior))
                        {
                            //El texto anterior es una constante válida
                            if (bufferConstante.Length != 0)
                            {
                                this.AddNuevoConstante(bufferConstante.ToString(), lineaActualIndex + 1); 
                            }
                            ///El texto anterior al separador es un identificador válido
                            
                            else if (bufferIdentificador.Length != 0)
                            {
                                this.AddNuevoIdentificador(bufferIdentificador.ToString(), lineaActualIndex + 1);
                            }

                            //No entraría en ningún if si lo anterior es un operador o delimitador.
                        }
                        else if (estadoAnterior != 0)
                        {
                            string actualErrorText = bufferConstante.Length != 0 ? bufferConstante.ToString() : bufferIdentificador.ToString();

                            this.results.Errores.Add(new RegistroError
                            {
                                CodigoError = 102,
                                DescripcionError = "Elemento Inválido",
                                ErrorTexto = actualErrorText,
                                LineaEnDondeAparece = lineaActualIndex + 1
                            }); 
                        }

                        //ResetBuffers
                        this.bufferConstante.Clear();
                        this.bufferIdentificador.Clear();
                    }


                    /// ------------------  Handlers individuales para cada TipoChar -------------------
                    switch (tipoCharActual)
                    {
                        case TipoChar.Delimitador: {
                                this.results.RegistrosLexicos.Add(new RegistroLexico
                                {
                                    LineaNum = lineaActualIndex + 1,
                                    Codigo = this.Delimitadores[c],
                                    Token = c.ToString(),
                                    Tipo = this.Tipos[Token.Delimitadores]
                                });
                                break;
                            }
                        case TipoChar.Operador: {
                                this.results.RegistrosLexicos.Add(new RegistroLexico
                                {
                                    LineaNum = lineaActualIndex + 1,
                                    Codigo = this.Operadores[c],
                                    Token = c.ToString(),
                                    Tipo = this.Tipos[Token.Operadores]
                                });
                                break;
                            }
                        case TipoChar.Letra: {
                                bufferIdentificador.Append(c);
                                break;
                            }
                        case TipoChar.Digito:
                        case TipoChar.Exponencial:
                        case TipoChar.PuntoFlotante: {
                                ///Checa si el buffer identificador tiene chars dentro
                                ///         (Significa que se está analizando un identificador actualmente)
                                ///o de lo contrario significa que se está analizando una constante actualmente  
                                /// ""Solo se usa un buffer a la vez""

                                if (bufferIdentificador.ToString() != String.Empty)
                                    bufferIdentificador.Append(c);
                                else
                                    bufferConstante.Append(c);

                                break;
                            }
                        case TipoChar.Desconocido: {
                                this.results.Errores.Add(new RegistroError
                                {
                                    LineaEnDondeAparece = lineaActualIndex + 1,
                                    CodigoError = 101,
                                    DescripcionError = "Símbolo desconocido",
                                    ErrorTexto = c.ToString()
                                });
                                break;
                            }
                    }
                } //Aquí acaba el foreach

                /// Checa si se terminó la línea actual en un estado válido (no se dejó
                /// algo cortado, etc, entre otras validaciones al final de la línea
                if (!elEstadoEsValido(estadoActual))
                {
                    string actualErrorText = bufferConstante.Length != 0 ? bufferConstante.ToString() : bufferIdentificador.ToString();
                    this.results.Errores.Add(new RegistroError
                    {
                        CodigoError = 102,
                        DescripcionError = "Elemento Inválido",
                        ErrorTexto = actualErrorText,
                        LineaEnDondeAparece = lineaActualIndex + 1
                    });

                    bufferConstante.Clear();
                    bufferIdentificador.Clear();
                }
                else
                {
                    //El texto anterior es una constante válida
                    if (bufferConstante.Length != 0)
                        this.AddNuevoConstante(bufferConstante.ToString(), lineaActualIndex + 1);

                    ///El texto anterior al separador es un identificador válido
                    else if (bufferIdentificador.Length != 0)
                        this.AddNuevoIdentificador(bufferIdentificador.ToString(), lineaActualIndex + 1);
                }
            }
            return this.results;
        }
        
       private void Resetear()
        {
            this.estadoActual = 0;
            this.valorIdentificadorActual = ValorIdentificadorDefault;
            this.valorConstanteActual = ValorConstanteDefault;
            this.bufferConstante.Clear();
            this.results = new AutomataResult();
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

        private void AddNuevoConstante(string constanteText, int noLineaEncontrado)
        {
            //Toca añadirlo en la tabla de constantes
            this.results.RegistrosConstantes.Add(new RegistroConstante
            {
                ConstanteTexto = constanteText,
                LineaEnDondeAparece = noLineaEncontrado,
                Valor = this.valorConstanteActual++
            });

            //Toca añadirlo a la tabla léxica en general también
            this.results.RegistrosLexicos.Add(new RegistroLexico
            {
                LineaNum = noLineaEncontrado,
                Codigo = this.valorConstanteActual,
                Tipo = this.Tipos[Token.Constante],
                Token = constanteText
            });
        }

        private void AddNuevoIdentificador(string identificadorText, int noLineaEncontrado)
        {
            //------- Lógica para añadirlo solamente a la tabla de Identificadores ------

            ///Checar si no se encuentra ya este identificador
            RegistroDinamico? identificadorExistente = this.results.RegistrosDinamicos
                .Find(rd => rd.IdentificadorTexto == bufferIdentificador.ToString());

            int valorDeEsteIdentificador;

            //Se encontró que ya existía? Agregar solamente la línea nueva
            if (identificadorExistente != null) {
                identificadorExistente.LineasEnDondeAparece.Add(noLineaEncontrado);
                valorDeEsteIdentificador = identificadorExistente.Valor;
            }
            //No? Crear un registro nuevo y la línea en la que se encuentra de paso
            else
            {
                //Toca añadirlo a la tabla de identificadores
                var nuevoIdentificador = new RegistroDinamico
                {
                    IdentificadorTexto = identificadorText,
                    Valor = this.valorIdentificadorActual++
                };

                nuevoIdentificador.LineasEnDondeAparece.Add(noLineaEncontrado);
                this.results.RegistrosDinamicos.Add(nuevoIdentificador);
                valorDeEsteIdentificador = this.valorIdentificadorActual;
            }

            // ---------------- Lógica para añadirlo también en la tabla léxica ---------

            this.results.RegistrosLexicos.Add(new RegistroLexico()
                {
                    LineaNum = noLineaEncontrado,
                    Codigo = valorDeEsteIdentificador,
                    Tipo = this.Tipos[Token.Identificador],
                    Token = identificadorText
                }
            );
        }
    }
}

