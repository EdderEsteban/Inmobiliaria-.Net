using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult CrearContratoId(int id)
        {
            //Enviar la lista de Inmuebles Disponibles
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            var inmueble = repoInmueble.ObtenerInmueble(id);
            ViewBag.inmueble = inmueble;

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

                return RedirectToAction("ListadoInmueblesAlquilados", "Inmueble");
            }
            return View("CrearContrato", contrato);
        }

        [HttpGet]
        public IActionResult EditarContrato(int id)
        {
            if (id > 0)
            {
                // Obtener el contrato específico según el ID
                RepositorioContrato repoContrato = new RepositorioContrato();
                var contrato = repoContrato.ObtenerContrato(id);

                // Verificar si el contrato existe
                if (contrato != null)
                {
                    //Enviar el Inmueble
                    RepositorioInmueble repo = new RepositorioInmueble();
                    var inmueble = repo.ObtenerInmueble(contrato.Id_inmueble);
                    ViewBag.inmueble = inmueble;

                    //Enviar el Inquilino
                    RepositorioInquilino repoInquil = new RepositorioInquilino();
                    var inquilino = repoInquil.ObtenerInquilino(contrato.Id_inquilino);
                    ViewBag.inquilino = inquilino;

                    return View(contrato);
                }
                else
                {
                    // Manejar el caso en que el contrato no existe
                    return NotFound();
                }
            }
            else
            {
                // Manejar el caso en que el ID no es válido
                return BadRequest();
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
