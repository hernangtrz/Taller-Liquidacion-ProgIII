using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Presentacion
    {
        static void Main(string[] args)
        {
            int numeroLiquidacion;
            String fechaLiquidacion = DateTime.UtcNow.ToString("MM-dd-yyyy");
            int idPaciente;
            String tipoAfilacion;
            Double salarioDevengado = 0;
            Double valorHospitalizacion;
            Double valorCuotaModeradora;
            Double Tarifa;
            int contador = 0;

            int op = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Liquidacion Cuota Moderadora");
                Console.WriteLine("1. Calcular Cuota Moderadora");
                Console.WriteLine("2. Consultar Todas Las Liquidaciones de Cuota Moderadora");
                Console.WriteLine("3. Consulta por Tipo de Afiliacion");
                Console.WriteLine("4. Consulta Total de Liquidaciones de Cuota Moderadora POr Tipo de Afiliacion");
                Console.WriteLine("5. Consulta por Fecha");
                Console.WriteLine("6. Consulta por Palabras");
                Console.WriteLine("7. Salir");

                op = int.Parse(Console.ReadLine());

                if (op == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese los Siguientes Datos ");
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Numero de Liquidacion : ");
                    numeroLiquidacion = int.Parse(Console.ReadLine());
                    Console.WriteLine("Identificacion del Paciente : ");
                    idPaciente = int.Parse(Console.ReadLine());
                    Console.WriteLine("Tipo de Afiliacion : ");
                    tipoAfilacion = Console.ReadLine();

                    if(String.Equals(tipoAfilacion, "Regimen contributivo", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Salario Devengado : ");
                        salarioDevengado = Double.Parse(Console.ReadLine());
                    }
                    
                    
                    
                    Console.WriteLine("Valor del servicio de Hospitalizacion : ");
                    valorHospitalizacion = Double.Parse(Console.ReadLine());

                    LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora(numeroLiquidacion, fechaLiquidacion,idPaciente,tipoAfilacion,salarioDevengado,valorHospitalizacion);
                    Tarifa = liquidacion.CalcularTarifa(salarioDevengado,tipoAfilacion);
                    valorCuotaModeradora = liquidacion.CalcularCuotaModeradora(salarioDevengado,valorHospitalizacion, Tarifa, tipoAfilacion);
                    LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
                    string message = liquidacionCuotaModeradoraService.Guardar(liquidacion);

                    Console.WriteLine("El valor de la Cuota Moderadora del Paciente es : " + valorCuotaModeradora );
                    Console.ReadKey();
                }
                else if (op == 2)
                {
                    
                    Console.Clear();
                    Console.WriteLine("Liquidaciones realizadas");
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------------------------------------------------------");
                    Console.WriteLine();
                    LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
                    foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
                    {
                        Console.WriteLine($"Identificación del paciente : {liquidacion.idPaciente}");
                        Console.WriteLine($"Tipo de afiliacion : {liquidacion.tipoAfilacion}");
                        Console.WriteLine($"Salario devengado por el paciente : {liquidacion.salarioDevengado:C}");
                        Console.WriteLine($"Valor servicio de hospitalizacion : {liquidacion.valorHospitalizacion:C}");
                        Console.WriteLine($"Tarifa Aplicada : {liquidacion.tarifa} años");
                        Console.WriteLine($"Valor liquidado real de la cuota moderadora : {liquidacion.valorLiquidoRealCuotaModeradora}");
                        Console.WriteLine($"¿Aplico Tope Maximo?: {liquidacion.pasoTopeMaximo}");
                        Console.WriteLine($"Valor de La Cuota Moderadora : {liquidacion.valorCuotaModeradora}");

                        Console.WriteLine("-----------------------------------------------------------------------------------------");

                        Console.WriteLine();
                       
                    }
                    Console.ReadKey();

                }
                else if (op == 3)
                {
                    Console.Clear();
                    String Regimen;
                    int contadorRegimen=0;
                    Console.WriteLine("Consulta por Tipo de Afiliacion");
                    Console.WriteLine("Digite el tipo de Regimen del paciente : ");
                    Regimen=Console.ReadLine();
                    Console.WriteLine("------------------------------------------------------------------------------------");
                    LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
                    foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
                    {
                        contador++;
                        if(string.Equals(Regimen ,liquidacion.tipoAfilacion, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Identificación del paciente : {liquidacion.idPaciente}");
                            Console.WriteLine($"Tipo de afiliacion : {liquidacion.tipoAfilacion}");
                            Console.WriteLine($"Salario devengado por el paciente : {liquidacion.salarioDevengado:C}");
                            Console.WriteLine($"Valor servicio de hospitalizacion : {liquidacion.valorHospitalizacion:C}");
                            Console.WriteLine($"Tarifa Aplicada : {liquidacion.tarifa} años");
                            Console.WriteLine($"Valor liquidado real de la cuota moderadora : {liquidacion.valorLiquidoRealCuotaModeradora}");
                            Console.WriteLine($"¿Aplico Tope Maximo?: {liquidacion.pasoTopeMaximo}");
                            Console.WriteLine($"Valor de La Cuota Moderadora : {liquidacion.valorCuotaModeradora}");
                            contadorRegimen++;
                        }
                        Console.WriteLine();
                        Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Las liquidaaciones totales realizadas son : " + contador);
                    Console.WriteLine("Las liquidaciones totales del regimen " + Regimen + " son : " + contadorRegimen);
                    Console.ReadKey();

                }

            } while (op != 4);
        }
    }
}
