using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace consumiWAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            cliente cliente = new cliente();

            var reqqs = await cliente.GetRequerimientoAsync();

            foreach (var req in reqqs)
            {
                Console.WriteLine(req.Proyecto_ID+ "   "+ req.NumeroRequerimiento);
            }


            Console.ReadLine();
        }
    }
    public class cliente
    {
        private readonly HttpClient HttpClient;

        public cliente()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://wapiv1.azurewebsites.net/api/v1/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<RequerimientoDTO>> GetRequerimientoAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Requerimiento");

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