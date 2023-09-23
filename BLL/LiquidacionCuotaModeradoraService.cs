using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        public string Guardar(LiquidacionCuotaModeradora liquidacionCuotaModeradora)
        {
            try
            {

                if (liquidacionCuotaModeradoraRepository.Buscar(liquidacionCuotaModeradora.numeroLiquidacion) == null)
                {
                    liquidacionCuotaModeradoraRepository.Guardar(liquidacionCuotaModeradora);
                    return $"se han guardado Satisfactoriamente los datos del establecimiento: {liquidacionCuotaModeradora.numeroLiquidacion} ";
                }
                else
                {
                    return $"Lo sentimos, con la Identificación {liquidacionCuotaModeradora.numeroLiquidacion} ya se encuentra registrada";
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }
        }

        public string Eliminar(int numLiquidacion)
        {
            try
            {
                if (liquidacionCuotaModeradoraRepository.Buscar(numLiquidacion) != null)
                {
                    liquidacionCuotaModeradoraRepository.Eliminar(numLiquidacion);
                    return ($"se han Eliminado Satisfactoriamente los datos del establecimiento con Identificación: {numLiquidacion} ");
                }
                else
                {
                    return ($"Lo sentimos, no se encuentra registrada un establecimiento con Identificacion {numLiquidacion}");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }

        }
        public List<LiquidacionCuotaModeradora> ConsultarTodos()
        {
            List<LiquidacionCuotaModeradora> establecimientos = liquidacionCuotaModeradoraRepository.ConsultarTodos();
            return establecimientos;
        }

        public class ConsultaCuotaModeradoraResponse
        {
            public List<LiquidacionCuotaModeradora> liquidaciones { get; set; }
            public string Message { get; set; }
            public bool Encontrado { get; set; }

            public ConsultaCuotaModeradoraResponse(List<LiquidacionCuotaModeradora> Liquidaciones)
            {
                liquidaciones = new List<LiquidacionCuotaModeradora>();
                Liquidaciones = liquidaciones;
                Encontrado = true;
            }
            public ConsultaCuotaModeradoraResponse(string message)
            {
                Message = message;
                Encontrado = false;
            }
        }


    }









}

