namespace ControlaMiPC.Modelos
{
	public class MouseMoveRequest
	{
		public int X { get; set; }     // Coordenada X
		public int Y { get; set; }     // Coordenada Y
		public bool Relative { get; set; } = false; // ¿Movimiento relativo?
	}

}
