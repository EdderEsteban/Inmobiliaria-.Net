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

    public IActionResult Listado()
    {
        RepositorioInquilino repo = new RepositorioInquilino();
        var lista = repo.Listar();
        return View(lista);
    }

}