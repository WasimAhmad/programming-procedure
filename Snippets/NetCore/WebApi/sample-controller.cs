/// There can be at most one parameter per action decorated with [FromBody]. 
/// The ASP.NET Core MVC run-time delegates the responsibility of reading 
/// the request stream to the formatter. 
/// Once the request stream is read for a parameter, 
/// it's generally not possible to read the request stream again for binding other [FromBody] parameters.
	

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace anaxisoft.Controllers.WebAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiagramController : ControllerBase
	{
		BLL.AppSettings _config { get; }
		public DiagramController(BLL.AppSettings config)
		{
			_config = config;

		}

		// GET: api/Diagram
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/Diagram/5
		[HttpGet("{companyId}/{diagramId}", Name = "GetDiagrams")]
		public string Get(Guid companyId, Guid diagramId)
		{
			var diagrams = new BLL.Services.Diagram.DiagramElement(companyId, diagramId, _config);
			var json = diagrams.ListForDrawingCanvas();
			return Newtonsoft.Json.JsonConvert.SerializeObject(json);
		}
		
		
		// https://stackoverflow.com/questions/42360139/asp-net-core-return-json-with-status-code
		
		[HttpGet]
		public ActionResult<string> Get()
		{
			var customer = new BLL.Services.Customer.Customer(975433);
			return Ok(customer);
		}
		
		
		[HttpGet("{id}")]
		public ActionResult Get(int id)
		{
			var customer = new BLL.Services.Customer.Customer(975433);
			return Ok(customer);
			//return Newtonsoft.Json.JsonConvert.SerializeObject(customer);
		}
		
		
		
		
		[HttpPost]
		// NOT VALID public IEnumerable<string> Auth([FromForm] string email, string password)
		public IEnumerable<string> Auth([FromForm] ObjectHere obj)
		{
			var auth = new BLL.Services.Authentication.Login(obj.email,obj.password);

			int userId = auth.GetUser();

			return new string[] { "userId", userId.ToStringOrDefault() };

		}
		
		[HttpPost]
		public IEnumerable<string> Auth([FromForm] BLL.ViewModels.Common.Auth data)
		{
			var auth = new BLL.Services.Authentication.Login(data.email,data.password);

			int userId = auth.GetUser();

			return new string[] { "userId", userId.ToStringOrDefault() };

		}
		

		// POST: api/Diagram
		[HttpPost]
		public string Post([FromBody] BLL.PostModels.Page.DiagramEditor data)
		{
			var diagramelement = new BLL.Services.Diagram.DiagramElement(data);
			return JsonConvert.SerializeObject(diagramelement.Dispatcher());
		}
		// DELETE: api/ApiWithActions/5
		[HttpDelete]
		public void Delete([FromBody] BLL.PostModels.Page.DiagramEditor data)
		{
			var diagramelement = new BLL.Services.Diagram.DiagramElement(data);
			diagramelement.Delete();
		}

	}
}
