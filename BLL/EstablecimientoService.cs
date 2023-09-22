using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EstablecimientoService
    {
        private readonly EstablecimientoRepository establecimientoRepository;
        public EstablecimientoService()
        {
            establecimientoRepository = new EstablecimientoRepository();
        }

        public Double CalcularGanacias(Double valorIngresosAnuales, Double valorGastosAnuales)
        {
            return valorIngresosAnuales - valorGastosAnuales;
        }

        public Double PasarUvt(Double gananciasPesos)
        {

            return gananciasPesos / 25000;
        }
        public string Guardar(Establecimiento establecimiento)
        {
            try
            {

                if (establecimientoRepository.Buscar(establecimiento.identificacion) == null)
                {
                    establecimientoRepository.Guardar(establecimiento);
                    return $"se han guardado Satisfactoriamente los datos del establecimiento: {establecimiento.nombre} ";
                }
                else
                {
                    return $"Lo sentimos, con la Identificación {establecimiento.identificacion} ya se encuentra registrada";
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }
        }
        public string Eliminar(string identificacion)
        {
            try
            {
                if (establecimientoRepository.Buscar(identificacion) != null)
                {
                    establecimientoRepository.Eliminar(identificacion);
                    return ($"se han Eliminado Satisfactoriamente los datos del establecimiento con Identificación: {identificacion} ");
                }
                else
                {
                    return ($"Lo sentimos, no se encuentra registrada un establecimiento con Identificacion {identificacion}");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }

        }
        public List<Establecimiento> ConsultarTodos()
        {
                List<Establecimiento> establecimientos = establecimientoRepository.ConsultarTodos();
                return establecimientos;
        }

        public class ConsultaEstablecimientoResponse
        {
            public List<Establecimiento> Establecimientos { get; set; }
            public string Message { get; set; }
            public bool Encontrado { get; set; }

            public ConsultaEstablecimientoResponse(List<Establecimiento> establecimientos)
            {
                Establecimientos = new List<Establecimiento>();
                Establecimientos = establecimientos;
                Encontrado = true;
            }
            public ConsultaEstablecimientoResponse(string message)
            {
                Message = message;
                Encontrado = false;
            }
        }


    }
}
