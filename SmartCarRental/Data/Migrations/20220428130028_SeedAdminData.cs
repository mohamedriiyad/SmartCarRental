using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRental.Data.Migrations
{
    public partial class SeedAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator]) VALUES (N'2a457923-8d8d-4504-a588-6d67a66d6b80', N'admin', N'ADMIN', N'admin@car.com', N'ADMIN@CAR.COM', 0, N'AQAAAAEAACcQAAAAEOss4F/wW2XnyAXRaAZsI2O2mxZXwOI8G0meQ7VdGf5wjvsrYkatrypGBD+XVTtgmw==', N'WOHRNCWIGKOI45OYOKVC7UGGUPGAO67D', N'92c56fdd-adca-4a83-817e-c36b0cc0c78d', N'01014581574', 0, 0, NULL, 1, 0, N'User')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8e70bb3a-133a-460e-8da9-48b00fa4bc8e', N'admin', N'ADMIN', N'0e0de339-55f7-4054-ae37-72c05955dd44')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2a457923-8d8d-4504-a588-6d67a66d6b80', N'8e70bb3a-133a-460e-8da9-48b00fa4bc8e')

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
