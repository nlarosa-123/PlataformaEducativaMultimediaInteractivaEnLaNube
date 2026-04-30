using BackendParaPlataforma.FuncionesAux;
using BackendParaPlataforma.Infraestructure.Mappings;
using BackendParaPlataforma.Infraestructure.Persistence;
using BackendParaPlataforma.Infraestructure.Repositories;
using BackendParaPlataforma.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<JwtService>();

var key = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key!)
        )
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmocionesRepository, EmocionesRepository>();
builder.Services.AddScoped<IProgresoUsuarioRepository, ProgresoUsuarioRepository>();
builder.Services.AddScoped<IDiarioEmocionalRepository, DiarioEmocionalRepository>();
builder.Services.AddScoped<IAnalisisIARepository, AnalisisIARepository>();
builder.Services.AddScoped<IReflexionMejoraRepository, ReflexionMejoraRepository>();
builder.Services.AddScoped<IEstadisticaUsuarioRepository, EstadisticaUsuarioRepository>();
builder.Services.AddScoped<IModulosRepository, ModulosRepository>();
builder.Services.AddScoped<ILeccionesRepository, LeccionesRepository>();
builder.Services.AddScoped<IPreguntaQuizRepository, PreguntaQuizRepository>();
builder.Services.AddScoped<IRespuestaUsuarioQuizRepository, RespuestaUsuarioQuizRepository>();
builder.Services.AddScoped<IProgresoModuloUsuarioRepository, ProgresoModuloUsuarioRepository>();
builder.Services.AddScoped<IProgresoLeccionUsuarioRepository, ProgresoLeccionUsuarioRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IOpcionRespuestaRepository, OpcionRespuestaRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<BackendParaPlataforma.Azure.MétodosAzure>();
builder.Services.AddScoped<MetodosAux>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();

app.UseCors("PermitirAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();