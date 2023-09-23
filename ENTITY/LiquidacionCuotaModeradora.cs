using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class LiquidacionCuotaModeradora
    {
        public int numeroLiquidacion {  get; set; }
        public String fechaLiquidacion {  set; get; }     
        public int idPaciente { get; set; } 
        public String tipoAfilacion { get; set; }
        public Double salarioDevengado { get; set; }    
        public Double valorHospitalizacion { get; set; }    
        public Double valorCuotaModeradora { get; set; }
        public Double tarifa { get; set; }
        public Double valorLiquidoRealCuotaModeradora { get; set; }
        public String pasoTopeMaximo { get; set; } = "NO SE LE APLICO NINGUN TOPE";

        public LiquidacionCuotaModeradora()
        {
        }

        public LiquidacionCuotaModeradora(int numeroLiquidacion,String fechaLiquidacion, int idPaciente, String tipoAfilacion, double salarioDevengado, double valorHospitalizacion)
        {
            this.numeroLiquidacion = numeroLiquidacion;
            this.fechaLiquidacion = fechaLiquidacion;
            this.idPaciente = idPaciente;
            this.tipoAfilacion = tipoAfilacion;
            this.salarioDevengado = salarioDevengado;
            this.valorHospitalizacion = valorHospitalizacion;
            this.valorCuotaModeradora = valorCuotaModeradora;
            this.tarifa = tarifa;
            this.valorLiquidoRealCuotaModeradora = valorLiquidoRealCuotaModeradora;
            this.pasoTopeMaximo = pasoTopeMaximo;
        }

        public Double CalcularTarifa(Double salarioDevengado, String tipoAfiliacion)
        {

            if (String.Equals(tipoAfiliacion, "Regimen contributivo", StringComparison.OrdinalIgnoreCase))
            {
                if (salarioDevengado < 2320000)
                {
                    tarifa = 0.15;
                }
                if (salarioDevengado >= 2320000 && salarioDevengado <= 5800000)
                {
                    tarifa = 0.20;
                }
                if (salarioDevengado > 5800000)
                {
                    tarifa = 0.25;
                }

            }
            else if (String.Equals(tipoAfiliacion, "Regimen Subsidiado", StringComparison.OrdinalIgnoreCase))
            {
                tarifa = 0.05;
            }


            return tarifa;
        }

        public Double CalcularCuotaModeradora(Double salarioDevengado, Double valorHospitalizacion, Double tarifa, String tipoAfiliacion)
        {

            if (String.Equals(tipoAfiliacion, "Regimen contributivo", StringComparison.OrdinalIgnoreCase))
            {
                if (salarioDevengado < 2320000)
                {
                    valorCuotaModeradora = valorHospitalizacion * tarifa;
                    valorLiquidoRealCuotaModeradora = valorCuotaModeradora;
                    if (valorCuotaModeradora > 250000)
                    {
                        valorCuotaModeradora = 250000;
                        pasoTopeMaximo = "SE LE APLICO EL TOPE MAXIMO DE: " + valorCuotaModeradora;
                    }
                }

                if (salarioDevengado >= 2320000 && salarioDevengado <= 5800000)
                {
                    valorCuotaModeradora = valorHospitalizacion * tarifa;
                    valorLiquidoRealCuotaModeradora = valorCuotaModeradora;

                    if (valorCuotaModeradora > 900000)
                    {
                        valorCuotaModeradora = 900000;
                        pasoTopeMaximo = "SE LE APLICO EL TOPE MAXIMO DE: " + valorCuotaModeradora;
                    }
                }
                if (salarioDevengado > 5800000)
                {
                    valorCuotaModeradora = valorHospitalizacion * tarifa;
                    valorLiquidoRealCuotaModeradora = valorCuotaModeradora;

                    if (valorCuotaModeradora > 1500000)
                    {
                        valorCuotaModeradora = 1500000;
                        pasoTopeMaximo = "SE LE APLICO EL TOPE MAXIMO DE: " + valorCuotaModeradora;
                    }
                }

            }
            else if (String.Equals(tipoAfiliacion, "Regimen Subsidiado", StringComparison.OrdinalIgnoreCase))
            {
                valorCuotaModeradora = valorHospitalizacion * 0.05;
                valorLiquidoRealCuotaModeradora = valorCuotaModeradora;

                if (valorCuotaModeradora > 200000)
                {
                    valorCuotaModeradora = 200000;
                    pasoTopeMaximo = "SE LE APLICO EL TOPE MAXIMO DE: " + valorCuotaModeradora;
                }
            }


            return valorCuotaModeradora;
        }

    }


}
