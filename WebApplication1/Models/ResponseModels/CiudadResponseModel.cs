﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.API.Models.ResponseModels
{
    public class CiudadResponseModel : GenericResponseModel
    {
        [StringLength(3)]
        public string ciud_ID { get; set; } = null!;

        [StringLength(50)]
        public string ciud_nombre { get; set; } = null!;

        [StringLength(3)]
        public string? pais_ID { get; set; }

    }

    public class CiudadModel
    {
        [StringLength(3)]
        public string ciud_ID { get; set; } = null!;

        [StringLength(50)]
        public string ciud_nombre { get; set; } = null!;

        [StringLength(3)]
        public string? pais_ID { get; set; }

    }

    public class CiudadModelAlone
    {
        [StringLength(3)]
        public string ciud_ID { get; set; } = null!;

        [StringLength(50)]
        public string ciud_nombre { get; set; } = null!;
    }

    public class CiudadesResponseModel : GenericResponseModel
    {
        public IEnumerable<CiudadModel> Ciudades { get; set; }
    }

    public class Ciudades
    {
        public IEnumerable<CiudadModel> Ciudadess { get; set; }
    }
}
