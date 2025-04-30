namespace ControlaMiPC.Modelos
{
	public class MouseClickRequest
	{
		public string Button { get; set; } = "left"; // "left", "right", "middle"
		public bool DoubleClick { get; set; } = false;
	}

}
