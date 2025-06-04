using AmusementPark.Models;
using Microsoft.Data.Sqlite;
using System;

public class ParkRepository
{
    private readonly string _connectionString;

    public ParkRepository(string connectionString)
    {
        _connectionString = connectionString;
        EnsureTable();
    }

    private void EnsureTable()
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Parks (
                ParkName TEXT PRIMARY KEY,
                Budget REAL,
                VisitorsEntry INTEGER,
                VisitorsOut INTEGER,
                TotalVisitors INTEGER,
                InventoryBuildingsJson TEXT,
                PlacedBuildingJson TEXT
            );
        ";
        cmd.ExecuteNonQuery();
    }

    public void SavePark(Park park)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Parks (ParkName, Budget, VisitorsEntry, VisitorsOut, TotalVisitors, InventoryBuildingsJson, PlacedBuildingJson)
            VALUES ($name, $budget, $visitorsEntry, $visitorsOut, $totalVisitors, $inventoryJson, $placedJson)
            ON CONFLICT(ParkName) DO UPDATE SET
                Budget = excluded.Budget,
                VisitorsEntry = excluded.VisitorsEntry,
                VisitorsOut = excluded.VisitorsOut,
                TotalVisitors = excluded.TotalVisitors,
                InventoryBuildingsJson = excluded.InventoryBuildingsJson,
                PlacedBuildingJson = excluded.PlacedBuildingJson;
        ";

        cmd.Parameters.AddWithValue("$name", park.ParkName);
        cmd.Parameters.AddWithValue("$budget", park.Budget);
        cmd.Parameters.AddWithValue("$visitorsEntry", park.VisitorsEntry);
        cmd.Parameters.AddWithValue("$visitorsOut", park.VisitorsOut);
        cmd.Parameters.AddWithValue("$totalVisitors", park.TotalVisitors);
        cmd.Parameters.AddWithValue("$inventoryJson", park.InventoryBuildingsJson);
        cmd.Parameters.AddWithValue("$placedJson", park.PlacedBuildingJson);

        cmd.ExecuteNonQuery();
    }

    public Park? LoadPark(string parkName)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Parks WHERE ParkName = $name";
        cmd.Parameters.AddWithValue("$name", parkName);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        var park = new Park(reader.GetString(reader.GetOrdinal("ParkName")))
        {
            Budget = reader.GetDouble(reader.GetOrdinal("Budget")),
            VisitorsEntry = reader.GetInt32(reader.GetOrdinal("VisitorsEntry")),
            VisitorsOut = reader.GetInt32(reader.GetOrdinal("VisitorsOut")),
            TotalVisitors = reader.GetInt32(reader.GetOrdinal("TotalVisitors")),
        };

        string inventoryJson = reader.GetString(reader.GetOrdinal("InventoryBuildingsJson"));
        string placedJson = reader.GetString(reader.GetOrdinal("PlacedBuildingJson"));

        park.LoadFromJson(inventoryJson, placedJson);

        return park;
    }
}
