using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using WebRetoTecnico.Models;
using WebRetoTecnico.Repositorios.Contrato;
using WebRetoTecnico.Repositorios.Implementacion;

namespace WebRetoTecnico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<ComplejosDeportivos> _complejosDeportivosRepository;
        private readonly IGenericRepository<SedesOlimpicas> _sedesOlimpicasRepository;
        private readonly IUsuarioService _usuarioService;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<ComplejosDeportivos> complejosDeportivosRepository,
            IGenericRepository<SedesOlimpicas> sedesOlimpicasRepository
            , IUsuarioService usuarioService
            )
        {
            _logger = logger;
            _complejosDeportivosRepository = complejosDeportivosRepository;
            _sedesOlimpicasRepository = sedesOlimpicasRepository;
             _usuarioService = usuarioService;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> listaSedesOlimpicas()
        {
            List<SedesOlimpicas> lista = await _sedesOlimpicasRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> listaComplejosDeportivos()
        {
            List<ComplejosDeportivos> lista = await _complejosDeportivosRepository.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> guardarSedesOlimpicas([FromBody] SedesOlimpicas modelo)
        {
            bool result = await _sedesOlimpicasRepository.Guardar(modelo);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> guardarComplejosDeportivos([FromBody] ComplejosDeportivos modelo)
        {
            bool result = await _complejosDeportivosRepository.Guardar(modelo);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> editarComplejosDeportivos([FromBody] ComplejosDeportivos modelo)
        {
            bool result = await _complejosDeportivosRepository.Editar(modelo);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> editarSedesOlimpicas([FromBody] SedesOlimpicas modelo)
        {
            
            bool result = await _sedesOlimpicasRepository.Editar(modelo);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> eliminarComplejosDeportivos(int i_ComplejoId)
        {
            bool result = await _complejosDeportivosRepository.Eliminar(i_ComplejoId);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> eliminarSedesOlimpicas(int i_SedeId)
        {
            bool result = await _sedesOlimpicasRepository.Eliminar(i_SedeId);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = result, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = result, msg = "error" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}