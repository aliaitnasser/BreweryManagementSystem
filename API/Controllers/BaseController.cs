using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BaseController : ControllerBase
	{
		protected ActionResult HandleResult<T>(Result<T> result)
		{
			
			if (result.IsSuccess && result.Value != null)
			{
				if (HttpContext.Request.Method == "POST") return Ok(result.Message);
				if (HttpContext.Request.Method == "GET") return Ok(result.Value);
				if (HttpContext.Request.Method == "PUT") return Ok(result.Message);
				if (HttpContext.Request.Method == "DELETE") return Ok(result.Message);
			}
			if (result.IsSuccess && result.Value == null)
			{
				return NotFound();
			}
			return BadRequest(result.Error);
		}
	}
}
