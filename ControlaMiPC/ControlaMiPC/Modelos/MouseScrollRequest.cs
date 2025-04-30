namespace ControlaMiPC.Modelos
{
	public class MouseScrollRequest
	{
		public int Amount { get; set; } // Ej: +120 o -120
		public bool Horizontal { get; set; } = false;
	}

}
