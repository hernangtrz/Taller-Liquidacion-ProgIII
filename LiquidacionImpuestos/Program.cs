using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.EstablecimientoService;

namespace LiquidacionImpuestos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String identificacion;
            String nombre;
            Double valorIngresosAnuales;
            Double valorGastosAnuales;
            int tiempoFuncionamiento;
            String tipoResponsabilidad;
            Double gananciasPesos;
            Double gananciasUVT;
            Double valorImpuesto;
            int op = 0;

            do
            {
                Console.WriteLine("Menu Calculo de Liquidacion");
                Console.WriteLine("1. CalcularImpuesto");
                Console.WriteLine("2. Consultar liquidaciones");
                Console.WriteLine("3. Eliminar liquidaciones");
                Console.WriteLine("4. Salir");

                op = int.Parse(Console.ReadLine());

                if (op == 1)
                {
                    String id;
                    Console.WriteLine("Digite la identificacion del establecimiento : ");
                    identificacion = Console.ReadLine();
                    id = identificacion;



                    Console.WriteLine("Digite el nombre del establecimiento : ");
                    nombre = Console.ReadLine();

                    Console.WriteLine("Digite el valor de los ingresos anuales : ");
                    valorIngresosAnuales = Double.Parse(Console.ReadLine());

                    Console.WriteLine("Digite el valor de los gastos anuales : ");
                    valorGastosAnuales = Double.Parse(Console.ReadLine());

                    Console.WriteLine("Digite el tiempo de funcionamiento en años : ");
                    tiempoFuncionamiento = int.Parse(Console.ReadLine());

                    Console.WriteLine("El establecimiento es responsable de IVA [S/N] : ");
                    tipoResponsabilidad = Console.ReadLine();

                    EstablecimientoService establecimientoService = new EstablecimientoService();
                    gananciasPesos = establecimientoService.CalcularGanacias(valorIngresosAnuales,valorGastosAnuales);
                    gananciasUVT = establecimientoService.PasarUvt(gananciasPesos);
                    Establecimiento establecimiento = new Establecimiento(identificacion, nombre, valorIngresosAnuales, valorGastosAnuales, tiempoFuncionamiento, tipoResponsabilidad, gananciasPesos, gananciasUVT);
                    valorImpuesto = establecimiento.CalcularImpuesto();
                    string message = establecimientoService.Guardar(establecimiento);
                    Console.WriteLine($"el impuesto del establecimiento es :  {valorImpuesto} " + message);
                    
                }
                else if (op == 2)
                {
                    Console.WriteLine("Liquidaciones realizadas");
                    EstablecimientoService establecimientoService = new EstablecimientoService();
                    foreach (var establecimiento in establecimientoService.ConsultarTodos())
                    {
                        Console.WriteLine($"Identificación: {establecimiento.identificacion}");
                        Console.WriteLine($"Nombre: {establecimiento.nombre}");
                        Console.WriteLine($"Ingresos Anuales: {establecimiento.valorIngresosAnuales:C}");
                        Console.WriteLine($"Gastos Anuales: {establecimiento.valorGastosAnuales:C}");
                        Console.WriteLine($"Tiempo de Funcionamiento: {establecimiento.tiempoFuncionamiento} años");
                        Console.WriteLine($"Tipo de Responsabilidad: {establecimiento.tipoResponsabilidad}");
                        Console.WriteLine($"Ganancias en pesos: {establecimiento.gananciasPesos}");
                        Console.WriteLine($"Ganancias en UVT: {establecimiento.gananciasUVT}");
                        Console.WriteLine($"Valor Impuesto: {establecimiento.valorImpuesto}");
                        Console.WriteLine();
                    }
                    

                }

            } while (op != 4);
        }
    }
}
