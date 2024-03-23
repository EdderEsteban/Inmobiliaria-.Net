using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;

namespace Inmobiliaria_.Net.Controllers;

public class PropietarioController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public PropietarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult ListadoPropietarios()
    {
        RepositorioPropietario repo = new RepositorioPropietario();
        var lista = repo.ListarPropietarios();
        return View(lista);
    }

    public IActionResult CrearPropietario()
    {
        return View();
    }

    public IActionResult GuardarPropietario(Propietario propietario)
    {
        if (ModelState.IsValid)//Asegurarse q es valido el modelo
        {
            RepositorioPropietario repo = new RepositorioPropietario();
            repo.GuardarNuevo(propietario);
            return RedirectToAction(nameof(ListadoPropietarios));
        }
        return View("CrearPropietario", propietario);
    }

    public IActionResult EditarPropietario(int id)
    {
        if (id > 0)
        {
            RepositorioPropietario repo = new RepositorioPropietario();
            var propietario = repo.ObtenerPropietario(id);
            return View(propietario);
        }
        else
        {
            return View();
        }
    }

    public IActionResult ModificarPropietario(Propietario propietario)
    {
         if (ModelState.IsValid)//Asegurarse q es valido el modelo
        {
            RepositorioPropietario repo = new RepositorioPropietario();
            repo.ActualizarPropietario(propietario);
            return RedirectToAction(nameof(ListadoPropietarios));
        }
        return View("EditarPropietario",propietario);
    }

    
    public IActionResult EliminarPropietario(int id)
    {
            RepositorioPropietario repo = new RepositorioPropietario();
            repo.EliminarPropietario(id);
            return RedirectToAction(nameof(ListadoPropietarios));
    }
}