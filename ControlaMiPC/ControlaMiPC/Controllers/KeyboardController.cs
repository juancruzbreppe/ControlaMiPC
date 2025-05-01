using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowsInput.Native;
using WindowsInput;

namespace ControlaMiPC.Controllers
{
	[Route("api/keyboard")]
	[ApiController]
	public class KeyboardController : ControllerBase
	{
		[HttpPost("press")]
		public IActionResult PressKey([FromBody] KeyPressDto key)
		{
			var sim = new InputSimulator();
			// Transforma el nombre de la tecla al código virtual
			sim.Keyboard.KeyPress((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), key.Key));
			return Ok();
		}

		[HttpPost("write")]
		public IActionResult WriteText([FromBody] TextWriteDto txt)
		{
			var sim = new InputSimulator();
			sim.Keyboard.TextEntry(txt.Text);
			return Ok();
		}
	}

	public record KeyPressDto(string Key);       // Ej: "VK_RETURN", "VK_LEFT"
	public record TextWriteDto(string Text);
}
