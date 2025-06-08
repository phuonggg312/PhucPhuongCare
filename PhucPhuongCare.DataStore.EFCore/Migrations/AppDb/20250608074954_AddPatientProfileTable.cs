using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhucPhuongCare.DataStore.EFCore.Migrations.AppDb
{
    public partial class AddPatientProfileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ApplicationUserId", "Bio", "Degree", "FullName", "ProfileImageUrl", "SpecialtyId" },
                values: new object[] { 1, "c4198e01-de6c-4929-9cb7-626568c62714", "Hơn 10 năm kinh nghiệm trong lĩnh vực nội tim mạch. Tận tâm và chuyên nghiệp.", "Bác sĩ Chuyên khoa I", "BS.CKI Nguyễn Thị An", null, 1 });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ApplicationUserId", "Bio", "Degree", "FullName", "ProfileImageUrl", "SpecialtyId" },
                values: new object[] { 2, "a585d425-0465-46cd-a4a7-42df57c2ae21", "Chuyên gia về các bệnh lý mạch vành và cao huyết áp. Từng tu nghiệp tại Singapore.", "Thạc sĩ, Bác sĩ", "ThS.BS Trần Văn Bình", null, 1 });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ApplicationUserId", "Bio", "Degree", "FullName", "ProfileImageUrl", "SpecialtyId" },
                values: new object[] { 3, "cbb24d8e-8c6d-4d90-97bd-60027fe2ba77", "Rất có kinh nghiệm với các bệnh lý ở trẻ sơ sinh và trẻ nhỏ.", "Bác sĩ Chuyên khoa II", "BS.CKII Lê Thị Cúc", null, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientProfiles");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
