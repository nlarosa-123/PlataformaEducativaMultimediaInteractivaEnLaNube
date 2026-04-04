using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendParaPlataforma.Migrations
{
    /// <inheritdoc />
    public partial class MigracionMYSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Emociones",
                columns: table => new
                {
                    IdEmocion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Emoji = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emociones", x => x.IdEmocion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modulos",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulos", x => x.IdModulo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ultimo_login = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    estado_actual = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    configuracion_voz = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoContenido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContenidoTxt = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlVideo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlAudio = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "diarios_emocionales",
                columns: table => new
                {
                    Id_Diario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id_Emocion_Usuario = table.Column<int>(type: "int", nullable: false),
                    Texto_Usuario = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Audio_Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diarios_emocionales", x => x.Id_Diario);
                    table.ForeignKey(
                        name: "FK_diarios_emocionales_Emociones_Id_Emocion_Usuario",
                        column: x => x.Id_Emocion_Usuario,
                        principalTable: "Emociones",
                        principalColumn: "IdEmocion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diarios_emocionales_usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estadisticas_usuario",
                columns: table => new
                {
                    IdEstadistica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    PorcentajeCoincidenciaIA = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    EmocionMasFrecuente = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RachaDiasRegistrados = table.Column<int>(type: "int", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "progreso_usuario",
                columns: table => new
                {
                    Id_Progreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Modulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Porcentaje = table.Column<int>(type: "int", nullable: false),
                    Ultima_Actualizacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progreso_usuario", x => x.Id_Progreso);
                    table.ForeignKey(
                        name: "FK_progreso_usuario_usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProgresoModuloUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Completado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UltimaLeccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoModuloUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresoModuloUsuario_modulos_IdModulo",
                        column: x => x.IdModulo,
                        principalTable: "modulos",
                        principalColumn: "IdModulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgresoModuloUsuario_usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "progreso_lecciones_usuario",
                columns: table => new
                {
                    Id_Progreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Id_Leccion = table.Column<int>(type: "int", nullable: false),
                    Completado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha_Completado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    IdQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdLeccion = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "analisis_ia",
                columns: table => new
                {
                    Id_Analisis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Diario = table.Column<int>(type: "int", nullable: false),
                    Emocion_Detectada_IA = table.Column<int>(type: "int", nullable: false),
                    Tono_Detectado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confianza = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    Coincide_Usuario = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha_Analisis = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analisis_ia", x => x.Id_Analisis);
                    table.ForeignKey(
                        name: "FK_analisis_ia_Emociones_Emocion_Detectada_IA",
                        column: x => x.Emocion_Detectada_IA,
                        principalTable: "Emociones",
                        principalColumn: "IdEmocion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analisis_ia_diarios_emocionales_Id_Diario",
                        column: x => x.Id_Diario,
                        principalTable: "diarios_emocionales",
                        principalColumn: "Id_Diario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reflexiones_mejora",
                columns: table => new
                {
                    Id_Reflexion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Diario = table.Column<int>(type: "int", nullable: false),
                    Texto_Reflexion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reflexiones_mejora", x => x.Id_Reflexion);
                    table.ForeignKey(
                        name: "FK_reflexiones_mejora_diarios_emocionales_Id_Diario",
                        column: x => x.Id_Diario,
                        principalTable: "diarios_emocionales",
                        principalColumn: "Id_Diario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preguntas_quiz",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdQuiz = table.Column<int>(type: "int", nullable: false),
                    Pregunta = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "opciones_respuesta",
                columns: table => new
                {
                    IdOpcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    TextoOpcion = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EsCorrecta = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "respuestas_usuario_quiz",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    IdOpcionElegida = table.Column<int>(type: "int", nullable: false),
                    Correcta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_analisis_ia_Emocion_Detectada_IA",
                table: "analisis_ia",
                column: "Emocion_Detectada_IA");

            migrationBuilder.CreateIndex(
                name: "IX_analisis_ia_Id_Diario",
                table: "analisis_ia",
                column: "Id_Diario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_diarios_emocionales_Id_Emocion_Usuario",
                table: "diarios_emocionales",
                column: "Id_Emocion_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_diarios_emocionales_Id_Usuario",
                table: "diarios_emocionales",
                column: "Id_Usuario");

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
                name: "IX_opciones_respuesta_IdPregunta",
                table: "opciones_respuesta",
                column: "IdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_preguntas_quiz_IdQuiz",
                table: "preguntas_quiz",
                column: "IdQuiz");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_lecciones_usuario_Id_Leccion",
                table: "progreso_lecciones_usuario",
                column: "Id_Leccion");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_lecciones_usuario_Id_Usuario",
                table: "progreso_lecciones_usuario",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_usuario_Id_Usuario",
                table: "progreso_usuario",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoModuloUsuario_IdModulo",
                table: "ProgresoModuloUsuario",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoModuloUsuario_IdUsuario",
                table: "ProgresoModuloUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_IdLeccion",
                table: "Quizzes",
                column: "IdLeccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reflexiones_mejora_Id_Diario",
                table: "reflexiones_mejora",
                column: "Id_Diario",
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

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analisis_ia");

            migrationBuilder.DropTable(
                name: "estadisticas_usuario");

            migrationBuilder.DropTable(
                name: "progreso_lecciones_usuario");

            migrationBuilder.DropTable(
                name: "progreso_usuario");

            migrationBuilder.DropTable(
                name: "ProgresoModuloUsuario");

            migrationBuilder.DropTable(
                name: "reflexiones_mejora");

            migrationBuilder.DropTable(
                name: "respuestas_usuario_quiz");

            migrationBuilder.DropTable(
                name: "diarios_emocionales");

            migrationBuilder.DropTable(
                name: "opciones_respuesta");

            migrationBuilder.DropTable(
                name: "Emociones");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "preguntas_quiz");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "lecciones");

            migrationBuilder.DropTable(
                name: "modulos");
        }
    }
}
