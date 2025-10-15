using System;
using System.Drawing;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
List<IMostrable> items = new List<IMostrable>();
Estudiante estudiante1 = new Estudiante("Torres Goméz", "001", "Desarollo y gestión de software");
Estudiante estudiante2 = new Estudiante("Maria Castañeda", "002", "Desarollo y gestión de software");
EstudianteBecado estudiante3 = new EstudianteBecado("Mario Hugo", "003", "Desarollo y gestión de software aplicada a empresas", 17.2);
Materia mat1 = new Materia("Informática", "Info002", 35, 10, 2);
Materia mat2 = new Materia("Matemática", "mat001", 25, 5, 0);
Materia mat3 = new Materia("Desarollo de software", "desa003", 40, 3, 1);
Calificacion cal1 = new Calificacion(estudiante1, mat1, 75);
Calificacion cal2 = new Calificacion(estudiante1, mat1, 75);
Calificacion cal3 = new Calificacion(estudiante3, mat3, 95);

items.Add(mat1);
items.Add(mat2);
items.Add(mat3);
items.Add(estudiante1);
items.Add(estudiante2);
items.Add(estudiante3);
items.Add(cal1);
items.Add(cal2);
items.Add(cal3);


foreach (IMostrable i in items) { 
    i.MostrarDatos(); 
}


Console.WriteLine($"El promedio del estudiante {estudiante1.Nombre} es de {estudiante1.CalcularPromedio()}");
Console.WriteLine($"La carga semanal de unala materia de {mat1.Nombre} con 12 horas es de {mat1.CalcularCargaSemanal(12)}");
Console.WriteLine($"Los puntos por calificación del estudiante {cal1.Estudiante.Nombre} son de {cal1.CalcularPuntos()}");
Console.WriteLine("La matrícula por descuento del becado es de {0:##.##}", estudiante3.CalcularMatriculaConDescuento(230.00));

public interface IMostrable { void MostrarDatos(); }

public class Estudiante : IMostrable
{

    // Atributos
    private string nombre;

    private string id;

    private string carrera;

    private List<Calificacion> calificaciones;

    // Propiedades

    public string Nombre { get; set; }

    public string Id { get; set; }

    public string Carrera { get; set; }

    public List<Calificacion> Calificaciones { get; }

    // Constructores

    public Estudiante(string nombre, string id, string carrera)
    {
        this.Nombre = nombre;
        this.Id = id;
        this.Carrera = carrera;
        this.Calificaciones = new List<Calificacion>();
    }

    public Estudiante()
    {

    }

    // Métodos
    public double CalcularPromedio()
    {
        double NotaTotal = 0, CreditosTotales = 0;
        foreach(var c in Calificaciones)
        {
            NotaTotal += c.Nota;
            CreditosTotales += c.Materia.Creditos;
        }
        return (NotaTotal * CreditosTotales) / CreditosTotales;
    }

    public void MostrarDatos() => Console.WriteLine($"El nombre del {this.Id} de nombre {this.Nombre} tiene un promedio de {this.CalcularPromedio()} en la carrera de {this.Carrera}");
    
        
}

public class Materia : IMostrable{

    // Atributos privados

    private string nombre;

    private string codigo;

    private int creditos;

    private int cupos;

    private int inscritos;

    // Propiedades

    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public int Creditos
    {
        get; set; } // (> 0)

    public int Cupos { get; set; } // (≥ 0)

    public int Inscritos { get; set; } // (≥ 0, ≤ Cupos)

    // Constructores

    public Materia(string nombre, string codigo, int creditos, int cupos = 0, int inscritos = 0)
    {
        try
        {
            this.Nombre = nombre;
            this.Codigo = codigo;
            this.Creditos = creditos;
            this.Cupos = cupos;
            this.Inscritos = inscritos;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error, parámetros incorrectos" + ex.ToString());
        }

    }

    public Materia()
    {

    }

    // Métodos

    public int CalcularCargaSemanal(int horasPorCredito) =>  Creditos * horasPorCredito;

    public void MostrarDatos() => Console.WriteLine($"La materia {this.Nombre} código {this.Codigo} tiene {this.Creditos} créditos, {this.Cupos} cupos y {this.Inscritos} estudiantes inscritos");
}

public class Calificacion : IMostrable
{

    // Atributos privados

    private Estudiante estudiante;

    private Materia materia;

    private double nota; //(rango 0–100)

    // Propiedades

    public Estudiante Estudiante { get; set; }

    public Materia Materia { get; set; }

    public double Nota { get; set; } //(0 ≤ Nota ≤ 100)

    // Constructor

    public Calificacion(Estudiante estudiante, Materia materia, double nota)
    {
        try { 
            this.Estudiante = estudiante;
            this.Materia = materia;
            this.Nota = nota;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error, parámetros incorrectos" + ex.ToString());
        }
    }

    // Métodos

    public double CalcularPuntos() => Nota * Materia.Creditos;

    public void MostrarDatos() => Console.WriteLine($"El estudiante nombre {this.Estudiante.Nombre} de la materia de {this.Materia.Nombre} tiene una nota de {this.Nota}");

}

public class EstudianteBecado : Estudiante{ 

    //  Atributo privado adicional

    private double porcentajeBeca; //(0–100)

    //  Propiedad

    public double PorcentajeBeca { get; set; } //(0 ≤ valor ≤ 100)

    // Constructor
    public EstudianteBecado(string nombre, string id, string carrera, double PorcentajeBeca) : base(nombre, id, carrera)
    {
        this.PorcentajeBeca = PorcentajeBeca;
    }


    //  Métodos

    public double CalcularMatriculaConDescuento(double matriculaBase)
    {
        try
        {
            return matriculaBase * (1 - (PorcentajeBeca / 100.0));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error, parámetros incorrectos" + ex.ToString());
        }
        return 0.0;

    }
    public void MostrarDatos()
    {
        Console.WriteLine($"El nombre del {this.Id} de nombre {this.Nombre} tiene un promedio de {this.CalcularPromedio()} en la carrera de {this.Carrera}, tiene una beca del {this.PorcentajeBeca}");//(incluye % de beca y, si corresponde, matrícula con descuento calculada en Main;
    }
}


