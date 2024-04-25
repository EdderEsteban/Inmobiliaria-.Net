using System.Diagnostics;
using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_.Net.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InmuebleController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult ListadoTodosInmuebles()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var lista = repo.ListarTodosInmuebles();
        return View(lista);
    }

    public IActionResult ListadoInmueblesDisponibles()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var lista = repo.ListarInmueblesDisponibles();
        return View(lista);
    }


public IActionResult ListadoInmueblesAlquilados()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var lista = repo.ListarInmueblesAlquilados();
        return View(lista);
    }
    [HttpGet]
    public IActionResult CrearInmueble()
    {
        //Enviar la lista de tipos de inmueble
        RepositorioInmueble repo = new RepositorioInmueble();
        var listTipos = repo.ListarTiposInmueble();
        ViewBag.tipos = listTipos;

        //Enviar la lista de propietarios
        RepositorioPropietario repoProp = new RepositorioPropietario();
        var listPropietarios = repoProp.ListarPropietarios();
        ViewBag.propietarios = listPropietarios;

        return View();
    }

    [HttpPost]
    public IActionResult GuardarInmueble(Inmueble inmueble)
    {
        Console.WriteLine(inmueble);
        if (ModelState.IsValid) //Asegurarse q es valido el modelo
        {
            RepositorioInmueble repo = new RepositorioInmueble();
            repo.GuardarNuevo(inmueble);
            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }
        return View();
    }

    public IActionResult EditarInmueble(int id)
    {
        //Enviar la lista de tipos de inmueble
        RepositorioInmueble repo = new RepositorioInmueble();
        var listTipos = repo.ListarTiposInmueble();
        ViewBag.tipos = listTipos;

        //Enviar la lista de propietarios
        RepositorioPropietario repoProp = new RepositorioPropietario();
        var listPropietarios = repoProp.ListarPropietarios();
        ViewBag.propietarios = listPropietarios;

        var inmueble = repo.ObtenerInmueble(id);

        return View(inmueble);
    }

    public IActionResult ModificarInmueble(Inmueble inmueble)
    {
        if (ModelState.IsValid)
        {
            RepositorioInmueble repo = new RepositorioInmueble();
            RepositorioContrato repoContrato = new RepositorioContrato();

            // Obtener todos los contratos asociados al inmueble
            var contratosDelInmueble = repoContrato.ListarContratosPorInmueble(
                inmueble.Id_inmueble
            );

            // Verificar si existen contratos asociados al inmueble
            if (contratosDelInmueble.Count > 0)
            {
                // Si hay contratos, ver si Inmueble.Disponible es 1 o 0
                Inmueble inmuebleOriginal = repo.ObtenerInmueble(inmueble.Id_inmueble); // Obtener el inmueble original de la BD
                if (inmuebleOriginal != null)
                {
                    if (inmuebleOriginal.Disponible == true)
                    {
                        // Permitir la modificación de todos los campos
                        repo.ActualizarInmueble(inmueble);
                    }
                    else
                    {
                        // Permitir la modificación de todos los campos excepto Disponible
                        inmueble.Disponible = inmuebleOriginal.Disponible; // Mantener el valor original de Disponible
                        repo.ActualizarInmuebleExceptoDisponible(inmueble);
                        TempData["AlertMessage"] =
                            "Se actualizó los datos excepto Disponible, ya que el inmueble tiene un contrato activo.";
                    }
                }
                else
                {
                    // No se encontro el inmueble original en la BD
                    return View("EditarInmueble", inmueble);
                }
            }
            else
            {
                // No hay contratos asociados, permitir la modificación de todos los campos
                repo.ActualizarInmueble(inmueble);
            }

            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }

        return View("EditarInmueble", inmueble);
    }

    public IActionResult EliminarInmueble(int id)
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        repo.EliminarInmueble(id);
        return RedirectToAction(nameof(ListadoTodosInmuebles));
    }

    public IActionResult DetallesInmueble(int id)
    {
        //Buscar el Inmueble
        RepositorioInmueble repo = new RepositorioInmueble();
        var inmueble = repo.ObtenerInmueble(id);

        //Enviar el Contratos
        RepositorioContrato repoContrato = new RepositorioContrato();
        var contrato = repoContrato.ObtenerContratoInmueble(inmueble.Id_inmueble);
        ViewBag.contrato = contrato;

        //Enviar el propietarios
        RepositorioPropietario repoProp = new RepositorioPropietario();
        var propietario = repoProp.ObtenerPropietario(inmueble.Id_propietario);
        ViewBag.propietario = propietario;

        //Enviar el Inquilino
        if(contrato != null)
        {
        RepositorioInquilino repoInquil = new RepositorioInquilino();
        var inquilino = repoInquil.ObtenerInquilino(contrato.Id_inquilino);
        ViewBag.inquilino = inquilino;
        }

        return View(inmueble);
    }
}
