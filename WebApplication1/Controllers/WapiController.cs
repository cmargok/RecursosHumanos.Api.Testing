using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WapiController : ControllerBase
    {
        private readonly HttpClient _HttpClient;


        public WapiController()
        {
            _HttpClient = new HttpClient();
            _HttpClient.BaseAddress = new Uri("https://wapiv1.azurewebsites.net/api/v1/");
            _HttpClient.DefaultRequestHeaders.Accept.Clear();
            _HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var respo = await GetRequerimientoAsync();
            if(respo != null)
            {
                return Ok(respo);
            }
            return BadRequest();

           
        }


     
           
      

        private async Task<List<RequerimientoDTO>> GetRequerimientoAsync()
        {
            HttpResponseMessage response = await _HttpClient.GetAsync("Requerimiento");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var reqs = JsonConvert.DeserializeObject<List<RequerimientoDTO>>(content);
                if (reqs != null)
                {
                    return reqs;
                }
                else
                {
                    return null!;
                }
            }
            else
            {
                return null!;
            }


        }



    }








    public class RequerimientoDTO
    {
        public int Requerimiento_ID { get; set; }

        [Required]
        public int NumeroRequerimiento { get; set; }

        [Required]
        [MaxLength]
        public string Descripcion { get; set; }

        [Required]
        public short DuracionDias { get; set; }

        [Required]
        public short DuracionMeses { get; set; }

        [Required]
        public short Version { get; set; }

        public short MesEstimadoinicioSel { get; set; }

        public short MesEstimadoPres { get; set; }

        public short MesEstimadoInicioEjec { get; set; }


        public decimal Honorarios { get; set; }

        [Required]
        public int CantidadContratos { get; set; }

        [StringLength(20)]
        public string? Numerocontrato { get; set; }

        public int Proyecto_ID { get; set; }

        public int Modalidad_Sel_ID { get; set; }

        public int Actuacion_ID { get; set; }

        public int? Perfil_ID { get; set; }

        public int Estado_ID { get; set; }


        public int Dependencia_destino_ID { get; set; }

        public int Tipo_Contrato_ID { get; set; }

        public int Modificacion_numero { get; set; }
    }
}
