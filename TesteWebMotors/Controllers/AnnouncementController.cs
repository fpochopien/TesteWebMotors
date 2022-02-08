using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using TesteWebMotors.Entities;
using TesteWebMotors.Models;
using TesteWebMotors.Repositories;

namespace TesteWebMotors.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ILogger<AnnouncementController> _logger;

        public AnnouncementController(ILogger<AnnouncementController> logger)
        {
            _logger = logger;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("[controller]/autoinsert")]
        public async Task<IActionResult> AutoInsert()
        {
            var autoInsert = new AutoInsertRepository();
            autoInsert.GetVehicles();
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement announcement)
        {
            var announcementRet = new AnnouncementRepository().Create(announcement);
            return (announcementRet > 0) ? Ok("Anuncio inserido com sucesso") : BadRequest("Problemas ao inserir o anuncio");
        }

        public IActionResult Update()
        {
            return View();
        }
        
        [HttpPut]        
        public async Task<IActionResult> Update([FromBody] Announcement announcement)
        {
            var announcementRet = new AnnouncementRepository().Update(announcement);
            return (announcementRet > 0) ? Ok("Anuncio alterado com sucesso") : BadRequest("Problemas ao alterar o anuncio");
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpDelete("[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var announcementRet = new AnnouncementRepository().Delete(id);
            return (announcementRet > 0) ? Ok("Anuncio deletado com sucesso") : BadRequest("Problemas ao deletar o anuncio");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var announcementRet = new AnnouncementRepository().Read(0);
            return Ok(announcementRet);
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