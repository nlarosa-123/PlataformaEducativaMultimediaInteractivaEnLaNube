using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendParaPlataforma.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaEmocionesQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgresoModuloUsuario_IdUsuario_IdModulo",
                table: "ProgresoModuloUsuario");

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    IdQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLeccion = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.IdQuiz);
                    table.ForeignKey(
                        name: "FK_Quizzes_lecciones_IdLeccion",
                        column: x => x.IdLeccion,
                        principalTable: "lecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preguntas_quiz",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuiz = table.Column<int>(type: "int", nullable: false),
                    Pregunta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas_quiz", x => x.IdPregunta);
                    table.ForeignKey(
                        name: "FK_preguntas_quiz_Quizzes_IdQuiz",
                        column: x => x.IdQuiz,
                        principalTable: "Quizzes",
                        principalColumn: "IdQuiz",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opciones_respuesta",
                columns: table => new
                {
                    IdOpcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    TextoOpcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EsCorrecta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opciones_respuesta", x => x.IdOpcion);
                    table.ForeignKey(
                        name: "FK_opciones_respuesta_preguntas_quiz_IdPregunta",
                        column: x => x.IdPregunta,
                        principalTable: "preguntas_quiz",
                        principalColumn: "IdPregunta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "respuestas_usuario_quiz",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    IdOpcionElegida = table.Column<int>(type: "int", nullable: false),
                    Correcta = table.Column<bool>(type: "bit", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_usuario_quiz", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK_respuestas_usuario_quiz_opciones_respuesta_IdOpcionElegida",
                        column: x => x.IdOpcionElegida,
                        principalTable: "opciones_respuesta",
                        principalColumn: "IdOpcion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_respuestas_usuario_quiz_preguntas_quiz_IdPregunta",
                        column: x => x.IdPregunta,
                        principalTable: "preguntas_quiz",
                        principalColumn: "IdPregunta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_respuestas_usuario_quiz_usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoModuloUsuario_IdModulo",
                table: "ProgresoModuloUsuario",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoModuloUsuario_IdUsuario",
                table: "ProgresoModuloUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_opciones_respuesta_IdPregunta",
                table: "opciones_respuesta",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_preguntas_quiz_IdQuiz",
                table: "preguntas_quiz",
                column: "IdQuiz");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_IdLeccion",
                table: "Quizzes",
                column: "IdLeccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_usuario_quiz_IdOpcionElegida",
                table: "respuestas_usuario_quiz",
                column: "IdOpcionElegida");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_usuario_quiz_IdPregunta",
                table: "respuestas_usuario_quiz",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_usuario_quiz_IdUsuario",
                table: "respuestas_usuario_quiz",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgresoModuloUsuario_modulos_IdModulo",
                table: "ProgresoModuloUsuario",
                column: "IdModulo",
                principalTable: "modulos",
                principalColumn: "IdModulo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgresoModuloUsuario_modulos_IdModulo",
                table: "ProgresoModuloUsuario");

            migrationBuilder.DropTable(
                name: "respuestas_usuario_quiz");

            migrationBuilder.DropTable(
                name: "opciones_respuesta");

            migrationBuilder.DropTable(
                name: "preguntas_quiz");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_ProgresoModuloUsuario_IdModulo",
                table: "ProgresoModuloUsuario");

            migrationBuilder.DropIndex(
                name: "IX_ProgresoModuloUsuario_IdUsuario",
                table: "ProgresoModuloUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoModuloUsuario_IdUsuario_IdModulo",
                table: "ProgresoModuloUsuario",
                columns: new[] { "IdUsuario", "IdModulo" },
                unique: true);
        }
    }
}
