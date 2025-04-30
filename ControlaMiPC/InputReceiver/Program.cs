using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

		builder.WebHost.ConfigureKestrel(options =>
		{
			options.ListenAnyIP(7135, listenOptions =>
			{
				listenOptions.UseHttps(); // O quitá esta línea si querés solo HTTP
			});
		});

		builder.WebHost.ConfigureKestrel(options =>
		{
			options.ListenAnyIP(7140, listenOptions =>
			{
				listenOptions.UseHttps(); // O quitá esta línea si querés solo HTTP
			});
		});

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

public record MouseMoveDto(int DeltaX, int DeltaY);