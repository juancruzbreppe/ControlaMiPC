var builder = WebApplication.CreateBuilder(args);

// Agregamos controladores
builder.Services.AddControllers();

// Swagger para probar desde navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(44318);  // El puerto debe coincidir con el que usas
});

var app = builder.Build();

// Activar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Habilitar HTTPS si querés
// app.UseHttpsRedirection();

// Mapeo de controladores
app.MapControllers();

app.Run();
