using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRental.Data.Migrations
{
    public partial class SeedAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator]) VALUES (N'ceb0e823-01d9-4108-ba9d-2746d70c423d', N'admin', N'ADMIN', N'admin@car.com', N'ADMIN@CAR.COM', 0, N'AQAAAAEAACcQAAAAEJmp/21QPWPCztuQyoUpZtDGvfVVX2U+z6ImJzGIYFhE8r2pXsjImGFVKlrZ62zDUQ==', N'S73VK3T5RJSXRLOIJ3UCGZNPLANWCIP7', N'aa2ceef4-e0e7-4628-b764-9306ef317a9a', N'01200000000', 0, 0, NULL, 1, 0, N'User')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'573bf61a-ad1d-48e6-821c-c8e962d2ab22', N'admin', N'ADMIN', N'7dfb766d-952f-42b0-b69f-b6c4f082c836')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ceb0e823-01d9-4108-ba9d-2746d70c423d', N'573bf61a-ad1d-48e6-821c-c8e962d2ab22')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
