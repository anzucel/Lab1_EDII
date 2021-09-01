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
        /*public static IWebHostEnvironment WebEnvironment; 
        public PeliculasController(IWebHostEnvironment web_environment)
        {
            WebEnvironment = web_environment;
        }

        public class SubirArchivo
        {
            public IFormFile File { get; set; } //interface IformFile representa archivo enviado con HttpRequest
        }

        [HttpPost]
        public async Task<string> Post(SubirArchivo archivo_nuevo) //Task, operación que no devuelve un valor, se ejecuta de manera asincrónica
        { 
            try
            {
                if (archivo_nuevo.File.Length > 0) //archivo > 0 bytes
                {
                    if (!Directory.Exists(WebEnvironment.WebRootPath + "\\JsonFile\\"))  //ruta se refiere a directorio existente en disco
                    {
                        Directory.CreateDirectory(WebEnvironment.WebRootPath + "\\JsonFile\\"); //crea nueva ruta 
                    }
                    using (FileStream fileStream = System.IO.File.Create(WebEnvironment.WebRootPath + "\\JsonFile\\" + archivo_nuevo.File.FileName))
                    {
                        archivo_nuevo.File.CopyTo(fileStream);
                        fileStream.Flush(); //datos en memoria volatil los pone en el archivo
                        return "\\JsonFile\\" + archivo_nuevo.File.FileName;
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }*/


        // GET: api/<Pelicula> 
        /*[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1" };
        }

        // GET api/<Pelicula>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

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
        public IActionResult PostGrado([FromForm] IFormFile File)
        {
            using var archivo = new MemoryStream();
            try
            {
                File.CopyToAsync(archivo);
                var cadena = Encoding.ASCII.GetString(archivo.ToArray());
                var pelicula = JsonConvert.DeserializeObject<ListaDobleEnlace.ListaDoble<Pelicula>>(cadena);
                foreach (var nuevo in pelicula)
                {
                    Singleton.Instance.ABPeliculas.Insertar(nuevo);
                }
                return Ok();
            }
            catch (Exception e)
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
