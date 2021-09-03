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

        [HttpGet("{traversal}")]
        public ActionResult GetByRecorrido([FromRoute] string traversal)
        {
            try
            {
                List<Pelicula> result = Singleton.Instance.ABPeliculas.recorrido(traversal);
                if (result.Count == 0) return NotFound();
                return Ok(result);
            }
            
            catch (Exception)
            {
                return BadRequest();
            }
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
        public IActionResult PostBody([FromBody] IEnumerable<Pelicula> pelicula)
        {
            try
            {
                foreach (var nuevo in pelicula)
                {
                    Singleton.Instance.ABPeliculas.Insertar(nuevo);
                }
                // Singleton.Instance.ABPeliculas.Insertar(pelicula);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("import")]
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

        //DELETE api/<Pelicula>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            try
            {
                bool StatusCode = false;              
               StatusCode = Singleton.Instance.ABPeliculas.eliminar(id);
                if(StatusCode == true) {return Ok();}
                else{return NotFound();}
                
                
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
