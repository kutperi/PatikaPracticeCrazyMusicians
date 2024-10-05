using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaPracticeCrazyMusicians.Models;

namespace PatikaPracticeCrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansControllers : ControllerBase
    {
        private static List<Musician> _musicians = new List<Musician>()
        {
            new Musician{ Id = 1, Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar ama çok eğlenceli" },
            new Musician{ Id = 2, Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new Musician{ Id = 3, Name = "Cemil Akor", Job = "Çılgın Akorist", FunFact = "Akorları sık değiştirir ama şaşırtıcı derecede yetenekli" },
            new Musician{ Id = 4, Name = "Fatma Nota", Job = "Sürpriz Nota Üreticisi", FunFact = "Nota üretirken sürekli sürprizler hazırlar" },
            new Musician{ Id = 5, Name = "Hasan Ritim", Job = "Ritim Canavarı", FunFact = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new Musician{ Id = 6, Name = "Elif Armoni", Job = "Armoni Ustası", FunFact = "Armonilerini bazen yanlış çalar ama çok yaratıcıdır" },
            new Musician{ Id = 7, Name = "Ali Perde", Job = "Perde Uygulayıcı", FunFact = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new Musician{ Id = 8, Name = "Aye Rezonans", Job = "Rezonans Uzmanı", FunFact = "Rezonans konusunda uzman ama bazen çok gürültü çıkarır" },
            new Musician{ Id = 9, Name = "Murat Ton", Job = "Tonlama Meraklısı", FunFact = "Tonlamasındaki farklılıklar bazen komik ama olduka ilginç" },
            new Musician{ Id = 10, Name = "Selin Akor", Job = "Akor Sihirbazı", FunFact = "Akorları değiştirdiğindde bazen sinirli bir hava yaratır" },

        };

        [HttpGet]
        public IEnumerable<Musician> GetAll()
        {
            return _musicians;
        }

        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician is null)
                return NotFound();

            return Ok(musician);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            var musicians = _musicians.Where(x => x.Name.Contains(keyword) || x.Job.Contains(keyword) || x.FunFact.Contains(keyword)).ToList();

            if (musicians.Count == 0)
                return NotFound();

            return Ok(musicians);

        }

        [HttpPost]
        public IActionResult Create([FromBody] Musician musician)
        {
            var id  = _musicians.Max(x => x.Id) + 1;
            musician.Id = id;

            _musicians.Add(musician);

            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
        }

        [HttpPatch("{id}")]
        public IActionResult UptadeFunFact(int id, [FromBody] Musician request)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();

            musician.FunFact = request.FunFact;

            return Ok(musician);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] Musician request)
        {
            if (request is null || id != request.Id)
            {
                return BadRequest();
            }

            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();

            musician.Name = request.Name;
            musician.Job = request.Job;
            musician.FunFact = request.FunFact;
            musician.IsDeleted = request.IsDeleted;

            return Ok(musician);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();

            musician.IsDeleted = true;

            return Ok(musician);
        }
    }
}
