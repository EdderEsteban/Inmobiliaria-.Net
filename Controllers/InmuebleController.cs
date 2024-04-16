using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;

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

    public IActionResult ListadoInmuebles()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var lista = repo.ListarInmueblesActivos();
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
        if (ModelState.IsValid)//Asegurarse q es valido el modelo
        {
            Console.WriteLine("Modelo Validado");
            RepositorioInmueble repo = new RepositorioInmueble();
            repo.GuardarNuevo(inmueble);
            return RedirectToAction(nameof(ListadoTodosInmuebles));

        }
        return View();

    }

    /*public IActionResult EditarInquilino(int id)
    {
        if (id > 0)
        {
            RepositorioInquilino repo = new RepositorioInquilino();
            var inquilino = repo.ObtenerInquilino(id);
            return View(inquilino);
        }
        else
        {
            return View();
        }
    }

    public IActionResult ModificarInquilino(Inquilino inquilino)
    {
         if (ModelState.IsValid)//Asegurarse q es valido el modelo
        {
            RepositorioInquilino repo = new RepositorioInquilino();
            repo.ActualizarInquilino(inquilino);
            return RedirectToAction(nameof(ListadoInquilinos));
        }
        return View("EditarInquilino",inquilino);
    }*/

    public IActionResult EliminarInquilino(int id)
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        repo.EliminarInmueble(id);
        return RedirectToAction(nameof(ListadoTodosInmuebles)); //Cambiar
    }

    public IActionResult Delete(int id, bool estado)
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        repo.CambiarEstadoInmueble(id);
        return RedirectToAction(nameof(ListadoInmuebles)); //cambiar
    }
}