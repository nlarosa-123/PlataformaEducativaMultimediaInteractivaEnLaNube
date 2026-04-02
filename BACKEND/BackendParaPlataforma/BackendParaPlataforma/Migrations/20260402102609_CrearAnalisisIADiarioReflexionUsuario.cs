using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendParaPlataforma.Migrations
{
    /// <inheritdoc />
    public partial class CrearAnalisisIADiarioReflexionUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diarios_emocionales",
                columns: table => new
                {
                    Id_Diario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Emocion_Usuario = table.Column<int>(type: "int", nullable: false),
                    Texto_Usuario = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Audio_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
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
                });

            migrationBuilder.CreateTable(
                name: "progreso_usuario",
                columns: table => new
                {
                    Id_Progreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Modulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Porcentaje = table.Column<int>(type: "int", nullable: false),
                    Ultima_Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "analisis_ia",
                columns: table => new
                {
                    Id_Analisis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Diario = table.Column<int>(type: "int", nullable: false),
                    Emocion_Detectada_IA = table.Column<int>(type: "int", nullable: false),
                    Tono_Detectado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Confianza = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    Coincide_Usuario = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_Analisis = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "reflexiones_mejora",
                columns: table => new
                {
                    Id_Reflexion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Diario = table.Column<int>(type: "int", nullable: false),
                    Texto_Reflexion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

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
                name: "IX_progreso_usuario_Id_Usuario",
                table: "progreso_usuario",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_reflexiones_mejora_Id_Diario",
                table: "reflexiones_mejora",
                column: "Id_Diario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analisis_ia");

            migrationBuilder.DropTable(
                name: "progreso_usuario");

            migrationBuilder.DropTable(
                name: "reflexiones_mejora");

            migrationBuilder.DropTable(
                name: "diarios_emocionales");
        }
    }
}
