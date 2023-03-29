using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExcelHelper
{
    public class mapTo
    {

        private static readonly List<Type> supportedValues = new()
        {
            typeof(byte),typeof(int),typeof(bool),typeof(short),typeof(long),typeof(float),typeof(double),typeof(decimal),typeof(string),typeof(DateTime)
        };


        private static int columns = 0;


        public static List<T> Map<T>(List<List<string>> list, int excelColumns) where T : new()
        {
            T entityModel = new T();         

            var entityProperties = getTypeProperties(entityModel);

            if(entityProperties.Length> excelColumns || entityProperties.Length < excelColumns)
            {
                throw new IndexOutOfRangeException("model is not acceptable to fetch excel data");
            }
            var distincTypes = GetTypesFromObject<T>(entityModel);


            List<T> modelosOUT = new();
            for (int i = 0; i < list.Count; i++)
            {
                foreach (var field in entityProperties)
                {

                    field.SetValue(entityModel, ParseDataType(field, list[i][columns]));
                    columns++;
                    if (columns == excelColumns) columns = 0;

                }
                modelosOUT.Add(entityModel);
                entityModel = new T();
            }
            Console.WriteLine("finishing");
            return modelosOUT;
        }

        private static PropertyInfo[] getTypeProperties<T>(T entity)
                       
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();

            return properties;
        }

        public static IEnumerable<Type> GetTypesFromObject<T>(T entity)
        {
            IEnumerable<Type> distincTypes = entity.GetType().GetProperties().Select(p => p.PropertyType).Distinct();

            ValidateSupportedValues(distincTypes);
            
            return distincTypes;
        }


       private static void ValidateSupportedValues(IEnumerable<Type> typesFromObject)
        {
           var diference = typesFromObject.Except(supportedValues);

            if (diference.Count() > 0)
            {
                throw new InvalidDataException("Type properties is not supported");
            
            }           

        }



        private static dynamic ParseDataType(PropertyInfo propertyInfo, string value)
        {
            var type = propertyInfo.PropertyType.ToString();

            if (value == "" || value == string.Empty || value == null)
            {
                return null;
            }


            switch (type)
            {
                case "System.Byte":
                    return Byte.Parse(value);

                case "System.Int32":
                    return Int32.Parse(value);

                case "System.Boolean":
                    return Boolean.Parse(value);

                case "System.Int16":
                    return Int16.Parse(value);

                case "System.Int64":
                    return Int64.Parse(value);

                case "System.Single":
                    return Single.Parse(value);                

                case "System.Double":
                    return Double.Parse(value);

                case "System.Decimal":                    
                    return Decimal.Parse(value);

                case "System.DateTime":
                    return Convert.ToDateTime(value);

                case "System.String":
                    return value;

                default: 
                    return null;
            }

        }





    }

    public class UnspscDTO
    {        

        [Required]
        [MaxLength(50)]
        public string CodigoUNSPSC { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;
    }

    public class ppp 
    {
        public string texto { get; set; }
        public int entero { get; set; }
        public string texto2 { get; set; }
        public decimal decimales { get; set; }
        public bool boleano { get; set; }
        public DateTime fecha { get; set; }

        public short smallint { get; set; }

       

        public void ooo()
        {
            Console.WriteLine("- "+texto +"    - "+entero+"    - "+texto2+ "      - "+decimales+"    -" +boleano+ "  - "+ fecha.ToString() +"   - "+ smallint);
        }

    }
}