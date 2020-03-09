using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using EmploymentInsights.Models;

namespace EmploymentInsights.Controllers
{
    public class VacatureController : ApiController
    {
        public IList<Vacature> Get(int page, int requestedResults) {
            AdzunaConnection adConn = new AdzunaConnection();
            IList<Vacature> results = adConn.GetVacatureAsync(page, requestedResults).Result;
            return results;
        }
    }
}
