using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tugas3_Api_reza.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tugas3_Api_reza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SekolahController : ControllerBase
    {
        private readonly SimpleCRUD _context;

        public SekolahController(SimpleCRUD context)
        {
            _context = context;
        }
        // GET: api/<SekolahController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsalSekolah>>> GetSekolah()
        {
            return await _context.AsalSekolahs.ToListAsync();
        }

        // GET api/<SekolahController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AsalSekolah>> GetSekolahByID(int id)
        {
            var data = await _context.AsalSekolahs.FirstOrDefaultAsync(a => a.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        // GET api/<SekolahController>/JumlahSiswa/5
        [HttpGet("/JumlahSiswa/{id}")]
        public async Task<ActionResult<JumlahSiswaDTO>> GetJumlahSiswaByIDSekolah(int id)
        {
            var jumlahSiswa = await _context.Siswas.CountAsync(a => a.AsalSekolahId == id);
            return Ok(new JumlahSiswaDTO { JumlahSiswa = jumlahSiswa});
        }

        // POST api/<SekolahController>
        [HttpPost]
        public async Task<IActionResult> CreateSekolah([FromBody] SekolahDTO sekolahCreate)
        {
            var sekolah = new AsalSekolah
            {
                Name = sekolahCreate.Name
            };
            _context.AsalSekolahs.Add(sekolah);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Terjadi kesalahan saat menyimpan data.");
            }

            return CreatedAtAction(nameof(GetSekolahByID), new { id = sekolah.Id }, new { status = "Data Berhasil Dibuat", Sekolah = sekolah });
        }

        // PUT api/<SekolahController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSekolah(int id, [FromBody] SekolahDTO sekolahUpdate)
        {
            var sekolah = await _context.AsalSekolahs.FindAsync(id);
            if (sekolah == null)
            {
                return NotFound();
            }

            sekolah.Name = sekolahUpdate.Name;
            _context.Entry(sekolah).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.AsalSekolahs.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { Message = "Sekolah berhasil diupdate", Sekolah = sekolah });
        }

        // DELETE api/<SekolahController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSekolah(int id)
        {
            var sekolah = await _context.AsalSekolahs.FindAsync(id);
            if (sekolah == null)
            {
                return NotFound();
            }

            _context.AsalSekolahs.Remove(sekolah);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Terjadi kesalahan saat menghapus data.");
            }
            return Ok(new { Message = "sekolah berhasil dihapus." });
        }
    }
}
