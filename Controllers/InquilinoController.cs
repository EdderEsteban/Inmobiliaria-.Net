using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_.Net.Models;
using Inmobiliaria_.Net.Repositorios;

namespace Inmobiliaria_.Net.Controllers;

public class InquilinoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InquilinoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult ListadoInquilinos()
    {
        RepositorioInquilino repo = new RepositorioInquilino();
        var lista = repo.Listar();
        return View(lista);
    }

    public IActionResult CrearInquilino()
    {
        return View();
    }

    public IActionResult GuardarInquilino(Inquilino inquilino)
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
        Console.WriteLine(id);
        if (id > 0)
        {
            Console.WriteLine("entro por Si");
            RepositorioInquilino repo = new RepositorioInquilino();
            var inquilino = repo.ObtenerInquilino(id);
            return View(inquilino);
        }
        else
        {
            Console.WriteLine("entro por No");
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

    public IActionResult BorrarInquilino(int id)
    {
        return View();
    }
}