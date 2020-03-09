using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmploymentInsights.Models;
using System.Text;
using Newtonsoft.Json.Linq;

namespace EmploymentInsights
{
    public class AdzunaConnection
    {
        public async Task<IList<Vacature>> GetVacatureAsync(int page, int resultsPerPage) {
            string url = String.Format("https://api.adzuna.com/v1/api/jobs/nl/search/{0}?app_id=6fb5ae2c&app_key=aab8bcc32c35ec1e6abe93c22606c780&results_per_page={1}", page, resultsPerPage);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            IList<Vacature> vacatures = new List<Vacature>();
            using (HttpResponseMessage httpResponseMessage =
                await new HttpClient().SendAsync(httpRequestMessage).ConfigureAwait(false))
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var buffer = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                    String response = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    JObject jsonObject = JObject.Parse(response);
                    IList<JToken> results = jsonObject["results"].Children().ToList();
                    foreach (JToken result in results)
                    {
                        Vacature vacature = result.ToObject<Vacature>();
                        vacatures.Add(vacature);
                    }
                    return vacatures;
                }
                else {
                    return vacatures;
                }
            }
        }
        
    }
}