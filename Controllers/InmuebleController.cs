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

    public IActionResult GuardarInmueble(Inmueble inmueble)
    {
        foreach (var prop in typeof(Inmueble).GetProperties())
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(inmueble);

            Console.WriteLine($"{propName}: {propValue}");
        }
        
        //if (ModelState.IsValid)//Asegurarse q es valido el modelo
        //{
            Console.WriteLine("Modelo Validado");
            RepositorioInmueble repo = new RepositorioInmueble();
            repo.GuardarNuevo(inmueble);
            return RedirectToAction(nameof(ListadoInmuebles));
            //return View();
        //}
        //return RedirectToAction(nameof(ListadoInmuebles));
        //return View();
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
    }
 
    public IActionResult EliminarInquilino(int id)
    {
            RepositorioInquilino repo = new RepositorioInquilino();
            repo.EliminarInquilino(id);
            return RedirectToAction(nameof(ListadoInquilinos));
    }*/
}