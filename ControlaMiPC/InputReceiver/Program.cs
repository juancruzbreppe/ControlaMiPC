using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using WindowsInput;
using WindowsInput.Native;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddCors(o => o.AddPolicy("allow-all", p => p
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()));


		var app = builder.Build();
		app.UseCors("allow-all");

		var sim = new InputSimulator();

		app.MapPost("/api/mouse/move-touchpad", (MouseMoveDto move) =>
		{
			sim.Mouse.MoveMouseBy(move.DeltaX, move.DeltaY);
			return Results.Ok();
		});

		app.Run("https://0.0.0.0:7140");
	}
}

public record MouseMoveDto(
	[property: JsonPropertyName("DeltaX")] int DeltaX,
	[property: JsonPropertyName("DeltaY")] int DeltaY
);