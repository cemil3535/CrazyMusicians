using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        private static readonly List<CrazyMusician> _crazyMusicians = new List<CrazyMusician>()
        {
            new CrazyMusician{Id = 1, Name = "Ahmet Çalgı" , Job = "Ünlü Çalgı Çalar" , Description = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new CrazyMusician{Id = 2, Name = "Zeynep Melodi" , Job = "Popüler Melodi Yazarı" , Description = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new CrazyMusician{Id = 3, Name = "Cemil Akor" , Job = "Çılgın Akorist" , Description = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new CrazyMusician{Id = 4, Name = "Fatma Nota" , Job = "Sürpriz Nota Üreticisi" , Description = "Nota üretirken sürekli sürprizler hazırlar" },
            new CrazyMusician{Id = 5, Name = "Hasan Ritim" , Job = "Ritim Canavarı" , Description = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new CrazyMusician{Id = 6, Name = "Elif Armoni" , Job = "Armoni Ustası" , Description = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new CrazyMusician{Id = 7, Name = "Ali Perde" , Job = "Perde Uygulayıcı" , Description = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new CrazyMusician{Id = 8, Name = "Ayşe Rezonans" , Job = "Rezonans Uzmanı" , Description = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new CrazyMusician{Id = 9, Name = "Murat Ton" , Job = "Tonlama Meraklısı" , Description = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
            new CrazyMusician{Id = 10, Name = "Selin Akor" , Job = "Akor Sihirbazı" , Description = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        };



        // GET: api/CrazyMusicians
        [HttpGet]
        public ActionResult<IEnumerable<CrazyMusician>> GetAll()
        {
            return Ok(_crazyMusicians);
        }


        // GET: api/CrazyMusicians/5
        [HttpGet("{id:int:min(1)}")]
        public ActionResult<CrazyMusician> GetMusician(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound($"Musician Id {id} bulunamadi");

            return Ok(musician);
        }


        [HttpGet("musician/{musicianName:alpha}")]
        public ActionResult<IEnumerable<CrazyMusician>> GetMusicianByName(string musicianName)
        {
            var musicianNames = _crazyMusicians.Where(x => x.Name.Equals(musicianName, StringComparison.OrdinalIgnoreCase));

            if (musicianNames.Any())
            {
                return NotFound($"{musicianName} icin muzisyen bulunamadi");
            }

            return Ok(musicianNames);
        }



        // POST: api/CrazyMusicians
        [HttpPost]
        public ActionResult<CrazyMusician> Post([FromBody] CrazyMusician musician)
        {
            var id = _crazyMusicians.Max(x => x.Id) + 1;
            musician.Id = id;

            _crazyMusicians.Add(musician);

            return CreatedAtAction(nameof(GetMusician), new { id = musician.Id }, musician);
        }


        // PUT: api/CrazyMusicians/5
        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] CrazyMusician updatedMusician)
        {
            var musician = _crazyMusicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();


            musician.Name = updatedMusician.Name;
            musician.Job = updatedMusician.Job;
            musician.Description = updatedMusician.Description;
            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult PatchMusician(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();

            musician.Job = musician.Job;
            return Ok(musician);
        }


        // GET: api/CrazyMusicians/search?name=cemil
        [HttpGet("search")]
        public ActionResult<IEnumerable<CrazyMusician>> Search([FromQuery] string name)
        {
            var query = _crazyMusicians.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(query.ToList());
        }


        // DELETE: api/CrazyMusicians/5
        // DELETE: api/CrazyMusicians/musicianName
        [HttpDelete("{id:int:min(1)}")]
        [HttpDelete("cancel/{musicianName}")]
        public IActionResult Delete(int? id, string musicianName)
        {

            CrazyMusician musicianToRomove;

            if (id.HasValue)
            {
                musicianToRomove = _crazyMusicians.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                musicianToRomove = _crazyMusicians.FirstOrDefault(x => x.Name.Equals(musicianName, StringComparison.OrdinalIgnoreCase));
            }


            if (musicianToRomove is null)
                return NotFound("Belirtilen Musician bulunamadi");

            _crazyMusicians.Remove(musicianToRomove);
            return NoContent();
        }

    }
}










