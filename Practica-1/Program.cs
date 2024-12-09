using System;
using System.Collections.Generic;

namespace Practica_1
{
    //creamos el struct
    public struct Alumno
    {
        public string Nombre;
        public int Materia_1;
        public int Materia_2;
        public int Materia_3;
        public int Materia_4;

        public Alumno(string nombre, int materia_1, int materia_2, int materia_3, int materia_4)
        {
            Nombre = nombre;
            Materia_1 = materia_1;
            Materia_2 = materia_2;
            Materia_3 = materia_3;
            Materia_4 = materia_4;
        }
    }
    internal class Program
    {
        //lista donde se guardaran los datos mas adelante
        List<List<Alumno>> listasAlumnos = new List<List<Alumno>>();
        //creamos un arreglo utilizando como tipo de dato el struct
        static Alumno[] datosDeAlumnos = new Alumno[6];
        //creamos el arreglo y agregamos las materias de forma implicita
        static string[] materias = { "Programacion", "Base de Datos", "Matemáticas", "S. Operativos" };
        //creamos la matriz cuyos tipos de datos son, el arreglo como "filas" y la cantidad de alumnos como "columnas"
        static int[,] notas = new int[materias.Length, datosDeAlumnos.Length];
        //tipos de datos autilizar 
        static int opcion;

        static void Main(string[] args)
        {
            do
            {
                Mensaje();
                opcion = int.Parse(Console.ReadLine());
                Menu();
            } while (opcion != 0);
        }
        static void Mensaje()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al programa. Seleccione una opcion...\n");
            Console.WriteLine("1: Ingresar nombres de alumnos");
            Console.WriteLine("2: Ordenar nombres alfabeticamente");
            Console.WriteLine("3: Ingresar notas");
            Console.WriteLine("4: Ver las notas");
            Console.WriteLine("5: Convertir a lista dinamica");
            Console.WriteLine("6: Ver el promedio");
            Console.WriteLine("0: Para salir\n");
        }
        static void Menu()
        {
            Console.Clear();

            switch (opcion)
            {
                case 1:
                    IngresarNombres(datosDeAlumnos);
                    break;
                case 2:
                    OrdenarNombres(datosDeAlumnos);
                    break;
                case 3:
                    IngresarNotas(datosDeAlumnos, materias, notas);
                    break;
                case 4:
                    VerNotas(materias, notas);
                    break;
                case 5:
                    ConvertirEnListas(notas);
                    break;
                case 6:
                    //CalcularPromedios(listasAlumnos);
                    break;
                case 0:
                    Console.WriteLine("Fin del programa.");
                    break;
                default:
                    Console.WriteLine("Opcion inválida. Ingrese una opcion del menú");
                    break;
            }
        }
        static bool ValidarLetra(char letra)
        {
            return letra == 'm' || letra == 'n';
        }
        static void IngresarNombres(Alumno[] datosDeAlumnos)
        {
            Console.Clear();
            bool continuarIngresandro = true;

            while (continuarIngresandro)
            {
                Console.WriteLine("Ingrese 'm' para volver al menú 'n' para ingresar datos");
                char letra = char.ToLower(Console.ReadLine()[0]);

                if (!ValidarLetra(letra))
                {
                    Console.WriteLine("Opcion incorrecta. Ingrese 'm' o 'n'");
                }
                else if (letra == 'm')
                {
                    continuarIngresandro = false;
                }
                else if (letra == 'n')
                {
                    for (int n = 0; n < datosDeAlumnos.Length; n++)
                    {
                        Console.Write($"Ingrese el nombre para el Alumno {n}: ");
                        string nombre = Console.ReadLine();

                        //verficamos de la siguiente manera con el siguiente metodo que indica que el espacio está vacío
                        while (string.IsNullOrWhiteSpace(nombre))
                        {
                            //en caso de estar vacío se solicita al usuario que ingrese el dato si o si
                            Console.WriteLine("El espacio no puede estar vacío.");
                            Console.Write($"Ingrese el nombre para el indice {n}: ");
                            nombre = Console.ReadLine();
                        }
                        //una vez que verificamos que no se encuentre vacío el espacio, se guarda el dato solicitado
                        datosDeAlumnos[n].Nombre = nombre;
                    }
                }
            }
        }
        static void OrdenarNombres(Alumno[] datosDeAlumnos)
        {
            //Mostramos los nombres ingresados
            Console.Clear();

            //string[] nombres = { "Matias", "Laura", "Camila", "Celi", "Tefi", "Alejandro", "Pato" };

            Console.WriteLine("Los nombres ingresados son:");
            foreach (var nombre in datosDeAlumnos)
            {
                Console.WriteLine(nombre.Nombre + " ");
            }
            //ordenamos con el método bubble sort
            for (int n = 0; n < datosDeAlumnos.Length - 1; n++)
            {
                //el segundo for compara los elementos adyacentes
                for (int j = 0; j < datosDeAlumnos.Length - n - 1; j++)
                {
                    //lo siguiente compara y ordena en caso de que los elementos esten desordenados
                    if (string.Compare(datosDeAlumnos[j].Nombre, datosDeAlumnos[j + 1].Nombre) > 0)
                    {
                        //intercambia en caso de estar desordenado
                        string temp = datosDeAlumnos[j].Nombre;
                        datosDeAlumnos[j].Nombre = datosDeAlumnos[j + 1].Nombre;
                        datosDeAlumnos[j + 1].Nombre = temp;
                    }
                }
            }
            //mostramos los nombres ordenados
            Console.WriteLine("\nLos nombres ingresados ordenados son: ");
            foreach (var nombre in datosDeAlumnos)
            {
                Console.WriteLine(nombre.Nombre + " ");
            }
            Console.WriteLine("\nPresione 'enter' para volver al menú");
            Console.ReadKey();
        }
        static void IngresarNotas(Alumno[] datosDeAlumnos, string[] materias, int[,] notas)
        {
            Console.Clear();
            Console.WriteLine(">>Ingresar Notas<<");
            //recorremos la matriz para cargar las notas
            for (int i = 0; i < 4; i++)
            {
                //ingresamos las notas en las columnas
                Console.Write($"\nMateria: {materias[i]} ");
                for (int j = 0; j < 6; j++)
                {
                    notas[i, j] = ValidarNotas(j + 1, materias[i]);
                }
            }

            Console.WriteLine("\nPresione 'enter' para volver al menú");
            Console.ReadKey();
        }
        static int ValidarNotas(int alumno, string materia)
        {
            while (true)
            {
                Console.Write($"\nNota del Alumno {alumno} en {materia} (1-10): ");
                int nota;
                if (!int.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
                {
                    Console.Write("Nota incorrecra. Ingrese entre 1-10: ");
                }
                else
                {
                    return nota;
                }
            }
        }
        static void VerNotas(string[] materias, int[,] notas)
        {
            Console.Clear();
            Console.WriteLine("'--Las notas de las materias son--'");
            //recorremos las filas de la matriz
            Console.WriteLine("\nMaterias\tAlumno_1\tAlumno_2\tAlumno_3\tAlumno_4\tAlumno_5\tAlumno_6");
            for (int i = 0; i < materias.Length; i++)
            {
                //mostramos las materias
                Console.Write(materias[i] + "\t");
                //recorremos las columnas
                for (int j = 0; j < 6; j++)
                {
                    //mostramos las notas
                    Console.Write(notas[i, j] + "\t\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPresione 'enter' para volver al menú");
            Console.ReadKey();
        }
        static void ConvertirEnListas(int[,] notas)
        {
            Console.Clear();
            //creamos la lista donde vamos a gaurdar los datos convertidos
            List<List<int>> listasAlumnos = new List<List<int>>();
            //recorremos las columnas de la matriz
            for (int j = 0; j < 6; j++)
            {
                //creamos una sublista donde se guardan las materias
                List<int> notasAlumnos = new List<int>();
                //luego recorremos las filas de la matriz
                for (int i = 0; i < 4; i++)
                {
                    //traspasamos los datos de la matriz a una lista dinamica
                    notasAlumnos.Add(notas[i, j]);
                }
                //una vez echo el traspaso de datos. Lo guardamos en la lista
                listasAlumnos.Add(notasAlumnos);
            }
            //luego mostramos los datos de la lista dinamica
            Console.WriteLine("\nLas notas son:");
            for (int j = 0; j < listasAlumnos.Count; j++)
            {
                Console.WriteLine($"Alumno {j + 1}: ");
                foreach (var nota in listasAlumnos[j])
                {
                    Console.WriteLine(nota + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPresione 'enter' para volver al menú");
            Console.ReadKey();
        }
        /*
        static void CalcularPromedios(List<List<Alumno>> listasAlumnos)
        {
            Console.WriteLine("\nPromedios de notas:");
            for (int j = 0; j < listasAlumnos.Count; j++)
            {
                double suma = 0;
                int cantidadNotas = 0;

                foreach (var alumno in listasAlumnos[j])
                {
                    suma += alumno.Materia_1 + alumno.Materia_2 + alumno.Materia_3 + alumno.Materia_4;
                    cantidadNotas++;
                }

                double promedio = cantidadNotas > 0 ? suma / cantidadNotas : 0;
                Console.WriteLine($"Alumno {j + 1}: {promedio:F2}"); // Formato a 2 decimales
            }
            Console.WriteLine("\nPresione 'enter' para volver al menú");
            Console.ReadKey();
        }*/
    }
}
