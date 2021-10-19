using Microsoft.EntityFrameworkCore.Migrations;

namespace library_api.Migrations
{
    public partial class InitialChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:my_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .Annotation("Npgsql:CollationDefinition:my_collation_deterministic", "en-u-ks-primary,en-u-ks-primary,icu,True")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:CollationDefinition:my_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "text",
                nullable: true,
                collation: "my_collation_deterministic",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "my_collation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:my_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:CollationDefinition:my_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .OldAnnotation("Npgsql:CollationDefinition:my_collation_deterministic", "en-u-ks-primary,en-u-ks-primary,icu,True")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "text",
                nullable: true,
                collation: "my_collation",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "my_collation_deterministic");
        }
    }
}
