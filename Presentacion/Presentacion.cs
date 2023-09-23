using BLL;
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
            int idPaciente;
            int tipoAfilacion;
            Double salarioDevengado;
            Double valorHospitalizacion;
            Double valorCuotaModeradora;
            Double Tarifa;

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
                    tipoAfilacion = int.Parse(Console.ReadLine());

                 
                    Console.WriteLine("Salario Devengado : ");
                    salarioDevengado = Double.Parse(Console.ReadLine());
                    
                    
                    Console.WriteLine("Valor del servicio de Hospitalizacion : ");
                    valorHospitalizacion = Double.Parse(Console.ReadLine());

                    LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
                    Tarifa = liquidacionCuotaModeradoraService.CalcularTarifa(salarioDevengado,tipoAfilacion);
                    valorCuotaModeradora = liquidacionCuotaModeradoraService.CalcularCuotaModeradora(salarioDevengado,valorHospitalizacion, Tarifa, tipoAfilacion);

                    Console.WriteLine("El valor de la Cuota Moderadora del Paciente es : " + valorCuotaModeradora );
                }
                else if (op == 2)
                {
                   


                }
                else if (op == 3)
                {
                    

                }

            } while (op != 4);
        }
    }
}
