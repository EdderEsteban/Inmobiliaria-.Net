using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;

namespace Inmobiliaria_.Net.Controllers
{
    public class ContratoController : Controller
    {
        private readonly ILogger<ContratoController> _logger;

        public ContratoController(ILogger<ContratoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListadoContratos()
        {
            //Enviar la lista de Inmuebles Disponibles
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            var listaInmueblesDisponibles = repoInmueble.ListarTodosInmuebles();
            ViewBag.inmueblesDisponibles = listaInmueblesDisponibles;

            // Enviar la lista de Inquilinos
            RepositorioInquilino repoInquilino = new RepositorioInquilino();
            var listaInquilinos = repoInquilino.ListarInquilinos();
            ViewBag.inquilinos = listaInquilinos;

            //Enviar la lista de Contratos
            RepositorioContrato repo = new RepositorioContrato();
            var lista = repo.ListarContratos();
            return View(lista);
        }

        [HttpGet]
        public IActionResult CrearContrato()
        {
            //Enviar la lista de Inmuebles Disponibles
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            var listaInmueblesDisponibles = repoInmueble.ListarInmueblesDisponibles();
            ViewBag.inmueblesDisponibles = listaInmueblesDisponibles;

            // Enviar la lista de Inquilinos
            RepositorioInquilino repoInquilino = new RepositorioInquilino();
            var listaInquilinos = repoInquilino.ListarInquilinos();
            ViewBag.inquilinos = listaInquilinos;
            
            return View();
        }

        [HttpPost]
        public IActionResult GuardarContrato(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                RepositorioContrato repo = new RepositorioContrato();
                repo.GuardarNuevo(contrato);
                
                RepositorioInmueble repoinmueble = new RepositorioInmueble();
                repoinmueble.CambiarEstadoInmueble(contrato.Id_inmueble);
                
                return RedirectToAction(nameof(ListadoContratos));
            }
            return View("CrearContrato", contrato);
        }

        [HttpGet]
        public IActionResult EditarContrato(int id)
        {
            if (id > 0)
            {
                RepositorioContrato repo = new RepositorioContrato();
                var contrato = repo.ObtenerContrato(id);
                return View(contrato);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult ModificarContrato(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                RepositorioContrato repo = new RepositorioContrato();
                repo.ActualizarContrato(contrato);
                return RedirectToAction(nameof(ListadoContratos));
            }
            return View("EditarContrato", contrato);
        }

        [HttpGet]
        public IActionResult EliminarContrato(int id)
        {
            RepositorioContrato repo = new RepositorioContrato();
            repo.EliminarContrato(id);
            return RedirectToAction(nameof(ListadoContratos));
        }
    }
}
