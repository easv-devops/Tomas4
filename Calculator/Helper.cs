﻿using Npgsql;


public class Helper
{
    public 
        readonly NpgsqlDataSource DataSource;

     public Helper()
    {
        var rawConnectionString = Environment.GetEnvironmentVariable("pgconn"); //Her testes om vi kan få secret til at virke for Drone
        
        try
        {
            if (string.IsNullOrEmpty(rawConnectionString))
            {
                throw new Exception("PostgreSQL connection string is empty or not set.");
            }

            var uri = new Uri(rawConnectionString);
            var ProperlyFormattedConnectionString = string.Format(
            "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;",
            uri.Host,
            uri.AbsolutePath.Trim('/'),
            uri.UserInfo.Split(':')[0],
            uri.UserInfo.Split(':')[1],
            uri.Port > 0 ? uri.Port : 5432);

            DataSource = new NpgsqlDataSourceBuilder(ProperlyFormattedConnectionString).Build();
            DataSource.OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to initialize Helper class. See exception details for more information", e);
        }
    }
}