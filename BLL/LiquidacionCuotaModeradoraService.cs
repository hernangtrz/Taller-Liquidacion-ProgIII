using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LiquidacionCuotaModeradoraService
    {
        private readonly LiquidacionCuotaModeradoraRepository liquidacionCuotaModeradoraRepository;

        public LiquidacionCuotaModeradoraService()
        {
            liquidacionCuotaModeradoraRepository = new LiquidacionCuotaModeradoraRepository();
        }

        public Double CalcularTarifa(Double salarioDevengado, int tipoAfiliacion)
        {
            Double tarifa = 0;

            if (tipoAfiliacion == 1)
            {
                if (salarioDevengado > 2320000)
                {
                    tarifa = 0.15;
                }
                if (salarioDevengado < 2320000 && salarioDevengado > 5800000)
                {
                    tarifa = 0.20;
                }
                if (salarioDevengado > 5800000)
                {
                    tarifa = 0.25;
                }

            }else if (tipoAfiliacion == 2)
            {
                tarifa = 0.05;
            }


            return tarifa;
        }


        public Double CalcularCuotaModeradora(Double salarioDevengado,Double valorHospitalizacion, Double tarifa, int tipoAfiliacion)
        {
            Double cuota=0;

            if (tipoAfiliacion == 1)
            {
                if (salarioDevengado < 2320000)
                {
                    cuota = valorHospitalizacion * tarifa;
                    if ((valorHospitalizacion * tarifa) > 250000)
                    {
                        cuota = 250000;
                    }
                }

                if (salarioDevengado < 2320000 && salarioDevengado > 5800000)
                {
                    cuota = valorHospitalizacion * tarifa;
                    if ((valorHospitalizacion * tarifa) > 900000)
                    {
                        cuota = 900000;
                    }
                }
                if (salarioDevengado > 5800000)
                {
                    cuota = valorHospitalizacion * tarifa;
                    if ((valorHospitalizacion * tarifa) > 1500000)
                    {
                        cuota = 1500000;
                    }
                }

            }else if(tipoAfiliacion == 2)
            {
                cuota = valorHospitalizacion * 0.05;
                if(cuota > 200000)
                {
                    cuota = 200000;
                }
            }

            
            return cuota;
        }

    }

    
}
