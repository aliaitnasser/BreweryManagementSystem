using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BaseController : ControllerBase
	{
		protected ActionResult HandleResultWithValue<T>(Result<T> result)
		{
			
			if (result.IsSuccess && result.Value != null)
			{
				return Ok(result.Value);
			}
			if (result.IsSuccess && result.Value == null)
			{
				return NotFound(result.Error);
			}
			return BadRequest(result.Error);
		}

		protected ActionResult HandleResultWithMessage<T>(Result<T> result)
		{

			if (result.IsSuccess && result.Value != null)
			{
				return Ok(result.Message);
			}
			if (result.IsSuccess && result.Value == null)
			{
				return NotFound(result.Error);
			}
			return BadRequest(result.Error);
		}
	}
}
