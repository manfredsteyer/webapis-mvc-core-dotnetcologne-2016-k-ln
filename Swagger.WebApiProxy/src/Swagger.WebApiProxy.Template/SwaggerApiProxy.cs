




















using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

// ReSharper disable All

namespace SwaggerServer{
/// <summary>
/// Web Proxy for Flug
/// </summary>
public class FlugWebProxy : Swagger.WebApiProxy.Template.BaseProxy
{
public FlugWebProxy(Uri baseUrl) : base(baseUrl)
{}

					// helper function for building uris. 
					private string AppendQuery(string currentUrl, string paramName, string value)
					{
						if (currentUrl.Contains("?"))
							currentUrl += string.Format("&{0}={1}", paramName, Uri.EscapeUriString(value));
						else
							currentUrl += string.Format("?{0}={1}", paramName, Uri.EscapeUriString(value));
						return currentUrl;
					}
				/// <summary>
/// 
/// </summary>
public async Task<List<Flug>> ApiFlugGet ()
{
var url = "api/Flug";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<List<Flug>>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="flug"></param>
public async Task<Flug> ApiFlugPost (Flug flug = null)
{
var url = "api/Flug";

using (var client = BuildHttpClient())
{
var response = await client.PostAsJsonAsync(url, flug).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<Flug>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="von"></param>
/// <param name="nach"></param>
public async Task<List<Flug>> ApiFlugByRouteGet (string von = null, string nach = null)
{
var url = "api/Flug/byRoute";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<List<Flug>>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
public async Task<Flug> ApiFlugByIdGet (int id)
{
var url = "api/Flug/{id}"
	.Replace("{id}", id.ToString());

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<Flug>().ConfigureAwait(false);
}
}
}
public class Flug 
{
public int Id { get; set; }
public string AblugOrt { get; set; }
public string ZielOrt { get; set; }
public DateTime Datum { get; set; }
public string FlugNr { get; set; }
public List<Buchung> Buchungen { get; set; }
}
public class Buchung 
{
public int BuchungId { get; set; }
public DateTime Datum { get; set; }
public int FlugId { get; set; }
public Flug Flug { get; set; }
public int PassagierId { get; set; }
}
}
        

    