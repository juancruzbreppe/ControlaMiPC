var builder = WebApplication.CreateBuilder(args);

// Agregamos controladores
builder.Services.AddControllers();

// Swagger para probar desde navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowGitHubPages", policy =>
	{
		policy.WithOrigins("https://juancruzbreppe.github.io")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});


builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(44318);  // El puerto debe coincidir con el que usas
});

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
app.UseCors("AllowGitHubPages");

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
