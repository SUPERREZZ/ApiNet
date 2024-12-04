using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tugas3_Api_reza.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tugas3_Api_reza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiswaController : ControllerBase
    {
        private readonly SimpleCRUD _context;

        public SiswaController(SimpleCRUD context)
        {
            _context = context;
        }
        // GET: api/<SiswaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Siswa>>> GetSiswa()
        {
            return await _context.Siswas.Include(s => s.AsalSekolah).ToListAsync();
        }

        // GET api/<SiswaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Siswa>> GetSiswaByID(int id)
        {
            var data = await _context.Siswas.Include(s => s.AsalSekolah).FirstOrDefaultAsync(s => s.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        // POST api/<SiswaController>
        [HttpPost]
        public async Task<IActionResult> CreateSiswa([FromBody] SiswaDTO siswaCreate)
        {
            var siswa = new Siswa
            {
                Name = siswaCreate.Name,
                Sex = siswaCreate.Sex,
                AsalSekolahId = siswaCreate.AsalSekolahId
            };
            _context.Siswas.Add(siswa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Terjadi kesalahan saat menyimpan data.");
            }

            return CreatedAtAction(nameof(GetSiswaByID), new { id = siswa.Id }, new {status = "Data Berhasil Dibuat",Siswa = siswa});
        }


        // PUT api/<SiswaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiswa(int id, [FromBody] SiswaDTO siswaUpdate)
        {
            var siswa = await _context.Siswas.FindAsync(id);
            if (siswa == null)
            {
                return NotFound();
            }

            siswa.Name = siswaUpdate.Name;
            siswa.Sex = siswaUpdate.Sex;
            siswa.AsalSekolahId = siswaUpdate.AsalSekolahId;

            _context.Entry(siswa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Siswas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           return Ok(new { Message = "Siswa berhasil diupdate",Siswa = siswa}); ;
        }

        // DELETE api/<SiswaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiswa(int id)
        {
            var siswa = await _context.Siswas.FindAsync(id);
            if (siswa == null)
            {
                return NotFound();
            }

            _context.Siswas.Remove(siswa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Terjadi kesalahan saat menghapus data.");
            }
            return Ok(new { Message = "Siswa berhasil dihapus." });
        }
    }
}
