using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class LiquidacionCuotaModeradoraGUI
    {
        LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
        int numeroLiquidacion;
        String fechaLiquidacion = DateTime.UtcNow.ToString("MM-dd-yyyy");
        int idPaciente;
        String tipoAfilacion;
        Double salarioDevengado = 0;
        Double valorHospitalizacion;
        Double valorCuotaModeradora;
        Double Tarifa;
        Double sumadorLiquidaciones = 0;
        Double sumadorLiquidacionesRegimen1 = 0;
        Double sumadorLiquidacionesRegimen2 = 0;
        String validaRegimen = "regimen contributivo";
        String regimen;

        public void Menu()
        {
        int op = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Liquidacion Cuota Moderadora");
                Console.WriteLine("1. Calcular Cuota Moderadora");
                Console.WriteLine("2. Consultar Todas Las Liquidaciones de Cuota Moderadora");
                Console.WriteLine("3. Consulta por Tipo de Afiliacion");
                Console.WriteLine("4. Consulta Total de Liquidaciones de Cuota Moderadora Por Tipo de Afiliacion");
                Console.WriteLine("5. Consulta por Fecha");
                Console.WriteLine("6. Consulta por Palabras");
                Console.WriteLine("7. Eliminar liquidacion");
                Console.WriteLine("8. Modificar liquidacion");
                Console.WriteLine("9. Salir");

                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        calcularCuotaModeradora();
                        break;
                    case 2:
                        ConsultarLiquidaciones();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Consulta por Tipo de Afiliacion");
                        Console.WriteLine("Digite el tipo de Regimen del paciente : ");
                        regimen = Console.ReadLine();
                        ConsultarPorRegimen(regimen);
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        TotalCuotasLiquidadas();
                        Console.ReadKey();
                        break;
                    case 5:
                        ConsultarPorFecha();
                        break;
                    case 6:
                        //ConsultarPorPalabras();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Eliminar liquidacion");
                        Console.WriteLine("Digite el numero de la liquidacion a eliminar: ");
                        int liquidacionAEliminar = int.Parse(Console.ReadLine());
                        EliminarLiquidacion(liquidacionAEliminar);
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Modificar liquidacion");
                        Console.WriteLine("Digite el numero de la liquidacion a modificar: ");
                        int liquidacionAModificar = int.Parse(Console.ReadLine());
                        ModificarLiquidacion(liquidacionAModificar);
                        Console.ReadKey();
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (op != 7);

         }

        public int LiquidacionesTotales()
        {
            int contador = 0;
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                contador++;
            }
            return contador;
        }
        public int LiquidacionesRegimenContributivo()
        {
            int contador = 0;
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                if(liquidacion.tipoAfilacion == "regimen contributivo")
                {
                    contador++;
                }
            }
            return contador;
        }
        public int LiquidacionesRegimenSubsidiado()
        {
            int contador = 0;
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                if (liquidacion.tipoAfilacion == "regimen subsidiado")
                {
                    contador++;
                }
            }
            return contador;
        }

        public void ConsultarPorNumero(int numero)
        {
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                if (numero == liquidacion.numeroLiquidacion)
                {
                    Console.WriteLine($"Numero de la liquidacion : {liquidacion.numeroLiquidacion}");
                    Console.WriteLine($"Identificación del paciente : {liquidacion.idPaciente}");
                    Console.WriteLine($"Tipo de afiliacion : {liquidacion.tipoAfilacion}");
                    Console.WriteLine($"Salario devengado por el paciente : {liquidacion.salarioDevengado:C}");
                    Console.WriteLine($"Valor servicio de hospitalizacion : {liquidacion.valorHospitalizacion:C}");
                    Console.WriteLine($"Tarifa Aplicada : {liquidacion.tarifa}");
                    Console.WriteLine($"Valor liquidado real de la cuota moderadora : {liquidacion.valorLiquidoRealCuotaModeradora}");
                    Console.WriteLine($"¿Aplico Tope Maximo?: {liquidacion.pasoTopeMaximo}");
                    Console.WriteLine($"Valor de La Cuota Moderadora : {liquidacion.valorCuotaModeradora}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                }

            }
        }

        public void calcularCuotaModeradora()
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

            if (String.Equals(tipoAfilacion, "Regimen contributivo", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Salario Devengado : ");
                salarioDevengado = Double.Parse(Console.ReadLine());
            }



            Console.WriteLine("Valor del servicio de Hospitalizacion : ");
            valorHospitalizacion = Double.Parse(Console.ReadLine());

            LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora(numeroLiquidacion, fechaLiquidacion, idPaciente, tipoAfilacion, salarioDevengado, valorHospitalizacion);
            Tarifa = liquidacion.CalcularTarifa(salarioDevengado, tipoAfilacion);
            valorCuotaModeradora = liquidacion.CalcularCuotaModeradora(salarioDevengado, valorHospitalizacion, Tarifa, tipoAfilacion);
            string message = liquidacionCuotaModeradoraService.Guardar(liquidacion);

            Console.WriteLine("El valor de la Cuota Moderadora del Paciente es : " + valorCuotaModeradora);
            Console.ReadKey();
        }
        public void ConsultarLiquidaciones()
        {
            Console.WriteLine("Liquidaciones realizadas");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine();
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
        public void ConsultarPorRegimen(String regimen)
        {
            Console.WriteLine();
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                if (string.Equals(regimen, liquidacion.tipoAfilacion, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Identificación del paciente : {liquidacion.idPaciente}");
                    Console.WriteLine($"Tipo de afiliacion : {liquidacion.tipoAfilacion}");
                    Console.WriteLine($"Salario devengado por el paciente : {liquidacion.salarioDevengado:C}");
                    Console.WriteLine($"Valor servicio de hospitalizacion : {liquidacion.valorHospitalizacion:C}");
                    Console.WriteLine($"Tarifa Aplicada : {liquidacion.tarifa}");
                    Console.WriteLine($"Valor liquidado real de la cuota moderadora : {liquidacion.valorLiquidoRealCuotaModeradora}");
                    Console.WriteLine($"¿Aplico Tope Maximo?: {liquidacion.pasoTopeMaximo}");
                    Console.WriteLine($"Valor de La Cuota Moderadora : {liquidacion.valorCuotaModeradora}");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                }

            }
            Console.WriteLine();
            Console.WriteLine("Las liquidaciones totales realizadas son : " + LiquidacionesTotales());
            Console.WriteLine("Las liquidaciones totales del regimen contributivo son: " + LiquidacionesRegimenContributivo());
            Console.WriteLine("Las liquidaciones totales del regimen subsidiado son: " + LiquidacionesRegimenSubsidiado());


        }
        public void TotalCuotasLiquidadas()
        {
            Double totalCuotasLiquidadas = 0;
            Double totalLiquidadoContributivo = 0;
            Double totalLiquidadoSubsidiado = 0;

            Console.WriteLine("------------------------------------------------------------------------------------");
                foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
                {
                    totalCuotasLiquidadas += liquidacion.valorCuotaModeradora;
                    if(liquidacion.tipoAfilacion == "regimen contributivo")
                    {
                        totalLiquidadoContributivo += liquidacion.valorCuotaModeradora;
                    }
                    else if (liquidacion.tipoAfilacion == "regimen subsidiado")
                    {
                        totalLiquidadoSubsidiado += liquidacion.valorCuotaModeradora;
                    }
                }
                Console.WriteLine("El valor total de todas las liquidaciones realizadas es :  " + totalCuotasLiquidadas);
                Console.WriteLine("El valor total de las liquidaciones del regimen contributivo es: "+ totalLiquidadoContributivo);
                Console.WriteLine("El valor total de las liquidaciones del regimen subsidiado es: " + totalLiquidadoSubsidiado);
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------------------------------------");

        }
        public void ConsultarPorFecha()
        {
            String mes;
            int numMes;
            int año;
            DateTime fecha;
            String[] meses = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
            int contadorRegimenContributivo = 0;
            int contadorRegimenSubsidiado = 0;
            int contadorTotalLiquidaciones = 0;
            Double totalCuotasLiquidadas = 0;
            Double totalLiquidadoContributivo = 0;
            Double totalLiquidadoSubsidiado = 0;
            Console.Clear();
            Console.WriteLine("Filtrar las liquidaciones realizadas en un mes y año especifico");
            Console.WriteLine("Digite el mes en que se realizo la liquidacion(es): ");
            mes = Console.ReadLine();
            numMes = Array.IndexOf(meses, mes.ToLower()) + 1;
            Console.WriteLine("Digite el año en que se realizo la liquidacion(es): ");
            año = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Liquidacion(es) del mes " + mes.ToLower() + " del año " + año) ;
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine();
            foreach (var item in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
                fecha = DateTime.ParseExact(item.fechaLiquidacion, "MM-dd-yyyy", null);
                if (fecha.Month == numMes && fecha.Year == año)
                {
                    if(item.tipoAfilacion == "regimen contributivo")
                    {
                        contadorRegimenContributivo++;
                        totalLiquidadoContributivo += item.valorCuotaModeradora;
                    }
                    else if(item.tipoAfilacion == "regimen subsidiado")
                    {
                        contadorRegimenSubsidiado++;
                        totalLiquidadoSubsidiado += item.valorCuotaModeradora;

                    }
                    ConsultarPorNumero(item.numeroLiquidacion);
                    contadorTotalLiquidaciones++;
                    totalCuotasLiquidadas += item.valorCuotaModeradora;
                }

            }
            Console.WriteLine();
            Console.WriteLine("Las liquidaciones totales realizadas son : " + contadorTotalLiquidaciones);
            Console.WriteLine("Las liquidaciones totales del regimen contributivo son: " + contadorRegimenContributivo);
            Console.WriteLine("Las liquidaciones totales del regimen subsidiado son: " + contadorRegimenSubsidiado);
            Console.WriteLine("El valor total de todas las liquidaciones realizadas es :  " + totalCuotasLiquidadas);
            Console.WriteLine("El valor total de las liquidaciones del regimen contributivo es: " + totalLiquidadoContributivo);
            Console.WriteLine("El valor total de las liquidaciones del regimen subsidiado es: " + totalLiquidadoSubsidiado);
            Console.ReadKey();
        }

        public void EliminarLiquidacion(int numLiquidacion)
        {
            Console.WriteLine(liquidacionCuotaModeradoraService.Eliminar(numLiquidacion));
        }

        public void ModificarLiquidacion(int numLiquidacion)
        {
            Double nuevoValorHospitalizacion;
            foreach (var liquidacion in liquidacionCuotaModeradoraService.ConsultarTodos())
            {
               if(liquidacion.numeroLiquidacion == numLiquidacion)
                {
                    Console.WriteLine("Va a modificar la liquidacion numero: " + liquidacion.numeroLiquidacion);
                    Console.WriteLine("Digite el nuevo valor de hospitalizacion: ");
                    nuevoValorHospitalizacion = Double.Parse(Console.ReadLine());
                    Console.WriteLine(liquidacionCuotaModeradoraService.Modificar(numLiquidacion, nuevoValorHospitalizacion));

                    LiquidacionCuotaModeradora liquidacionCuotaModeradora = liquidacionCuotaModeradoraService.Buscar(numLiquidacion);
                    Console.WriteLine("El nuevo valor de la hospitalizacion es: " + liquidacionCuotaModeradora.valorHospitalizacion);
                    Console.WriteLine("La nueva cuota moderadora es: " + liquidacionCuotaModeradora.valorCuotaModeradora);
                }
            }
        }
    }
}
       