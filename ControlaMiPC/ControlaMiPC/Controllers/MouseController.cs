using ControlaMiPC.Helpers;
using ControlaMiPC.Modelos;
using ControlaMiPC.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowsInput;

namespace ControlaMiPC.Controllers
{

	[ApiController]
	[Route("api/mouse")]
	public class MouseController : ControllerBase
	{
		[HttpPost("move-touchpad")]
		public IActionResult MoveMouseTouchpad([FromBody] MouseMoveDto move)
		{
			var sim = new InputSimulator();
			sim.Mouse.MoveMouseBy(move.deltaX, move.deltaY);
			return Ok();
		}

		[HttpPost("move")]
		public IActionResult MoveMouse([FromBody] MouseMoveRequest request)
		{
			MouseHelper.MoveMouse(request.X, request.Y, request.Relative);
			return Ok(new { success = true });
		}

		[HttpPost("click")]
		public IActionResult ClickMouse([FromQuery] string button = "left", [FromQuery] bool doubleClick = false)
		{
			MouseHelper.ClickMouse(button, doubleClick);
			return Ok(new { success = true });
		}

		[HttpPost("scroll")]
		public IActionResult Scroll([FromQuery] int amount, [FromQuery] bool horizontal = false)
		{
			MouseHelper.Scroll(amount, horizontal);
			return Ok(new { success = true });
		}

		[HttpGet("position")]
		public IActionResult GetMousePosition()
		{
			var (x, y) = MouseHelper.GetPosition();
			return Ok(new { x, y });
		}
	}

}
