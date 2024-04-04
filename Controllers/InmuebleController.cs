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

    public IActionResult ListadoInmuebles()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var lista = repo.ListarInmuebles();
        return View(lista);
    }

        public IActionResult CrearInmueble()
    {
        RepositorioInmueble repo = new RepositorioInmueble();
        var listado = repo.ListarTiposInmueble();
        return View(listado);
    }

    /*public IActionResult GuardarInquilino(Inquilino inquilino)
    {
        if (ModelState.IsValid)//Asegurarse q es valido el modelo
        {
            RepositorioInquilino repo = new RepositorioInquilino();
            repo.GuardarNuevo(inquilino);
            return RedirectToAction(nameof(ListadoInquilinos));
        }
        return View("CrearInquilino", inquilino);
    }

    public IActionResult EditarInquilino(int id)
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
    }
 
    public IActionResult EliminarInquilino(int id)
    {
            RepositorioInquilino repo = new RepositorioInquilino();
            repo.EliminarInquilino(id);
            return RedirectToAction(nameof(ListadoInquilinos));
    }*/
}