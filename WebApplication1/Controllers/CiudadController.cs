
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.API.DataAccess;
using RecursosHumanos.API.DataAccess.Models;
using RecursosHumanos.API.Models.CreateModels;
using RecursosHumanos.API.Models.ModifyModel;
using RecursosHumanos.API.Models.ResponseModels;
//using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    //ESTE CONTRLADOR USAR UNA INSTANCIA DE ADO.NET 
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly IAdoRepository _context;
        private readonly RH_Context rh_context;
        public CiudadController(IAdoRepository context, RH_Context _contextR)
        {
            _context = context;
            rh_context = _contextR;
        }

        //metodo para obtener una ciudad determinada por medio del ID
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCiudadById(string Id)
        {
            //usamos el contexto "ado.net" para conectarnos a la base de datos
            CiudadResponseModel ciudadResponse = await _context.GetCiudadById(Id);

            if (!ciudadResponse.Succcess) return NotFound(ciudadResponse);
            else return Ok(ciudadResponse);

        }


        [HttpGet]
        [Route("Pais")]
        public async Task<IActionResult> GetCiudadWithPaisById(string Id)
        {
            //usamos el contexto "ado.net" para conectarnos a la base de datos
            //    CiudadResponseModel ciudadResponse = await _context.GetCiudadById(Id);


            /*    var z = from ciu in rh_context.CIUDAD
                        join pais in rh_context.PAIS on ciu.pais_ID  equals pais.pais_ID
                        where ciu.ciud_ID == Id
                        select new CIUDAD { ciud_ID = ciu.ciud_ID,
                        ciud_nombre = ciu.ciud_nombre,
                        pais_ID = pais.pais_ID,
                        pais = new PAIS { pais_nombre = pais.pais_nombre, pais_ID= pais.pais_ID },

                        };
                */

            var ff = rh_context.CIUDAD.Include(o => o.pais).Where(po => po.ciud_ID == Id).Select(k => new CIUDAD
            {
                ciud_ID = k.ciud_ID,
                ciud_nombre = k.ciud_nombre,
                pais_ID = k.pais_ID,
                pais = new PAIS { pais_nombre = k.pais!.pais_nombre, pais_ID = k.pais.pais_ID },

            });

            //  var g = z.ToList();

            return Ok(ff);



            //  if (!ciudadResponse.Succcess) return NotFound(ciudadResponse);
            // else return Ok(z);

        }

        //metodo para obtener todas las ciudades de la BD
        [HttpGet]
        public async Task<IActionResult> GetCiudades()
        {
            CiudadesResponseModel ciudadesResponse = await _context.GetCiudades();

            if (!ciudadesResponse.Succcess)
            {
                return BadRequest(ciudadesResponse);
            }
            else
            {
                return Ok(ciudadesResponse);
            }
        }

        //Metodo para agregar una ciudad
        [HttpPost]
        public async Task<IActionResult> PostCiudad(CiudadCreateModel createCiudad)
        {

            CiudadResponseModel ciudadResponse = await _context.PostCiudad(new CiudadCreateModel
            {
                ciud_ID = createCiudad.ciud_ID.ToUpper(),
                ciud_nombre = createCiudad.ciud_nombre.ToUpper(),
                pais_ID = createCiudad.pais_ID!.ToUpper()
            });

            if (!ciudadResponse.Succcess)
            {
                return BadRequest(ciudadResponse);
            }
            else
            {
                return Created("api/localizaciones/ciudad/" + ciudadResponse.ciud_ID, ciudadResponse);
            }
        }

        //metodo para modificar una ciudad
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCiudad(string Id, CiudadModifyModel ciudadModify)
        {

            CiudadResponseModel ciudadResponse = await _context.UpdateCiudad(Id, new CiudadModifyModel
            {
                ciud_nombre = ciudadModify.ciud_nombre.ToUpper(),
                pais_ID = ciudadModify.pais_ID!.ToUpper()
            });

            if (!ciudadResponse.Succcess)
            {
                return BadRequest(ciudadResponse);
            }
            else
            {
                return Ok(ciudadResponse);
            }
        }

        //metodo para eliminar una ciudad
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCiudad(string Id)
        {
            CiudadResponseModel ciudadResponse = await _context.DeleteCiudad(Id);

            if (!ciudadResponse.Succcess)
            {
                return NotFound(ciudadResponse);
            }
            else
            {
                return NoContent();
            }

        }

    }
}
