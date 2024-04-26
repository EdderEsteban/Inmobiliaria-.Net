using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_.Net.Controllers
{
    public class PagoController : Controller
    {
        private readonly ILogger<PagoController> _logger;

        public PagoController(ILogger<PagoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListadoPagos()
        {
            //Lista de Pagos
            RepositorioPago repo = new RepositorioPago();
            var lista = repo.ListarPagos();

            //Enviar la lista de Inmuebles
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            var listaInmuebles = repoInmueble.ListarTodosInmuebles();
            ViewBag.inmuebles = listaInmuebles;

            // Enviar la lista de Inquilinos
            RepositorioInquilino repoInquilino = new RepositorioInquilino();
            var listaInquilinos = repoInquilino.ListarInquilinos();
            ViewBag.inquilinos = listaInquilinos;

            //Enviar la lista de Contratos
            RepositorioContrato repoContratos = new RepositorioContrato();
            var listaContratos = repoContratos.ListarContratos();
            ViewBag.contratos = listaContratos;

            return View(lista);
        }

        [HttpGet]
        public IActionResult CrearPago(int id)
        {
            //Enviar el Inmueble
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            var inmueblexId = repoInmueble.ObtenerInmueble(id);
            ViewBag.inmueble = inmueblexId;

            //Enviar la lista de Contratos
            RepositorioContrato repoContratos = new RepositorioContrato();
            var contratoxId = repoContratos.ObtenerContratoInmueble(id);
            ViewBag.contratos = contratoxId;
            
            // Enviar la lista de Inquilinos
            RepositorioInquilino repoInquilino = new RepositorioInquilino();
            var inquilinoxId = repoInquilino.ObtenerInquilino(contratoxId.Id_inquilino);
            ViewBag.inquilino = inquilinoxId;

            return View();
        }

         [HttpPost]
         public IActionResult GuardarPago(Pago pago)
         {
             if (ModelState.IsValid)
             {
                 RepositorioPago repo = new RepositorioPago();
                 repo.GuardarNuevo(pago);
                return RedirectToAction(nameof(ListadoPagos));
             }
             return View("CrearPago", pago);
         }
 
       /*  [HttpGet]
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
         }*/
 
         [HttpGet]
         public IActionResult EliminarPago(int id)
         {
             RepositorioPago repo = new RepositorioPago();
             repo.EliminarPago(id);
             return RedirectToAction(nameof(ListadoPagos));
         }
         
    }
}
