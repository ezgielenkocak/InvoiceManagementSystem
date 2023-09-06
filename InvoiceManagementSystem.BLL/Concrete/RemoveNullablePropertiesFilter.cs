using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    //public class RemoveNullablePropertiesFilter : ISchemaFilter
    //{
    //    public void Apply(OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
    //    {
    //        //nullable özelliği true olan propertyleri swaggerda görmek istemediğim için bulduğum çözüm
    //        if (schema.Properties != null)
    //        {
    //            var nullableProperties = schema.Properties.Where(p => p.Value.Nullable == true).ToList();

    //            foreach (var nullableProperty in nullableProperties)
    //            {
    //                schema.Properties.Remove(nullableProperty.Key);
    //            }
    //        }
    //    }
    //}
}
