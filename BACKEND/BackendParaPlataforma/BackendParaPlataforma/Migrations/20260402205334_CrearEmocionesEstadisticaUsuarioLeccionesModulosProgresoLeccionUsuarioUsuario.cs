using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendParaPlataforma.Migrations
{
    /// <inheritdoc />
    public partial class CrearEmocionesEstadisticaUsuarioLeccionesModulosProgresoLeccionUsuarioUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Creacion",
                table: "diarios_emocionales",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "estadisticas_usuario",
                columns: table => new
                {
                    IdEstadistica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    PorcentajeCoincidenciaIA = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    EmocionMasFrecuente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RachaDiasRegistrados = table.Column<int>(type: "int", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadisticas_usuario", x => x.IdEstadistica);
                    table.ForeignKey(
                        name: "FK_estadisticas_usuario_usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "modulos",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulos", x => x.IdModulo);
                });

            migrationBuilder.CreateTable(
                name: "lecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TipoContenido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContenidoTxt = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UrlVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UrlAudio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lecciones_modulos_IdModulo",
                        column: x => x.IdModulo,
                        principalTable: "modulos",
                        principalColumn: "IdModulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "progreso_lecciones_usuario",
                columns: table => new
                {
                    Id_Progreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Id_Leccion = table.Column<int>(type: "int", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_Completado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tiempo_Visualizado = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progreso_lecciones_usuario", x => x.Id_Progreso);
                    table.ForeignKey(
                        name: "FK_progreso_lecciones_usuario_lecciones_Id_Leccion",
                        column: x => x.Id_Leccion,
                        principalTable: "lecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_progreso_lecciones_usuario_usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_estadisticas_usuario_IdUsuario",
                table: "estadisticas_usuario",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lecciones_IdModulo",
                table: "lecciones",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_lecciones_usuario_Id_Leccion",
                table: "progreso_lecciones_usuario",
                column: "Id_Leccion");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_lecciones_usuario_Id_Usuario",
                table: "progreso_lecciones_usuario",
                column: "Id_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estadisticas_usuario");

            migrationBuilder.DropTable(
                name: "progreso_lecciones_usuario");

            migrationBuilder.DropTable(
                name: "lecciones");

            migrationBuilder.DropTable(
                name: "modulos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Creacion",
                table: "diarios_emocionales",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
