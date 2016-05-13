using FlugDemo.Models;
using Microsoft.AspNet.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlugDemo.Components
{
    public class CsvOutputFormatter : IOutputFormatter
    {
        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            var fluege = context.Object as IEnumerable<Flug>;

            if (fluege == null) throw new NotSupportedException("IEnumerable<Flug> erwartet!");

            var response = context.HttpContext.Response;

            using (var stream = response.Body)
            using (var writer = context.WriterFactory(stream, Encoding.UTF8)) {
                foreach(var flug in fluege) {
                    await writer.WriteLineAsync($"{flug.Id};{flug.AblugOrt};{flug.ZielOrt};{flug.Datum}");
                }
            }
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.Object is IEnumerable<Flug> 
                    && context.ContentType.MediaType == "text/csv";
        }

    }
}
