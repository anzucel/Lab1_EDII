using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoAPI.Extra;
using ProyectoAPI.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        // GET: api/<Pelicula> 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1" };
        }

        // GET api/<Pelicula>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Pelicula>
        [HttpPost]
        public IActionResult Post([FromBody] Models.GradoArbol gradoArbol)
        {
            try
            {
                int grado = gradoArbol.grado;
                Singleton.Instance.ABPeliculas = new ArbolB.ArbolB<Models.Pelicula>(grado);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route ("populate")]
        public IActionResult PostBody([FromBody] Models.Pelicula pelicula)
        {
            try
            {
                Pelicula nuevo = pelicula;
                Singleton.Instance.ABPeliculas.Insertar(nuevo);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route ("import")]
        public IActionResult PostFile([FromForm] IFormFile File)
        {
            using var archivo = new MemoryStream();
            try
            {
                File.CopyToAsync(archivo);
                var coleccion = Encoding.ASCII.GetString(archivo.ToArray());
                var listaPeliculas = JsonConvert.DeserializeObject<List<Pelicula>>(coleccion);
                foreach (var nuevo in listaPeliculas)
                {
                    Singleton.Instance.ABPeliculas.Insertar(nuevo);
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public void Delete()
        {
            Singleton.Instance.ABPeliculas = null;
        }

        // DELETE api/<Pelicula>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
