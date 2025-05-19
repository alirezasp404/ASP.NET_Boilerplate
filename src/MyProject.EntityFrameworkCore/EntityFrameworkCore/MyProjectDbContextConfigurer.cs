using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MyProject.EntityFrameworkCore;

public static class MyProjectDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<MyProjectDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<MyProjectDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
