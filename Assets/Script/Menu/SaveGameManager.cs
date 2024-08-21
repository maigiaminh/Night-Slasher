using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Linq;
using System.Globalization;

public class SaveGame
{
  public string timeSaved;
  public float xPosition;
  public float yPosition;
  public float hpAmount;
  public string map;

  public SaveGame(string timeSaved, float xPos, float yPos, float hpAmount, string map)
  {
    this.timeSaved = timeSaved;
    xPosition = xPos;
    yPosition = yPos;
    this.hpAmount = hpAmount;
    this.map = map;
  }
}

public class SaveGameManager : MonoBehaviour
{
  public string timeSaved;
  public float xPosition;
  public float yPosition;
  public float hpAmount;
  public string map;

  public List<SaveGame> saveGames = new();

  public void OnSaveGame(SaveGame data)
  {
    IDbConnection dbConnection = CreateAndOpenDatabase();
    IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
    dbCommandInsertValue.CommandText = "INSERT INTO SaveGameTable (id, time, xPos, yPos, hpAmount, map) VALUES ('"
      + Guid.NewGuid().ToString("N") + "', '"
      + DateTime.Now.ToString() + "', "
      + data.xPosition.ToString("G", CultureInfo.InvariantCulture) + ", "
      + data.yPosition.ToString("G", CultureInfo.InvariantCulture) + ", "
      + data.hpAmount.ToString("G", CultureInfo.InvariantCulture) + ", '"
      + data.map + "'"
      + ")";

    Debug.Log(dbCommandInsertValue.CommandText);
    dbCommandInsertValue.ExecuteNonQuery();

    dbConnection.Close();
  }

  public List<SaveGame> OnRetriveSaveGame()
  {
    IDbConnection dbConnection = CreateAndOpenDatabase();
    IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
    dbCommandReadValues.CommandText = "SELECT * FROM SaveGameTable";
    IDataReader dataReader = dbCommandReadValues.ExecuteReader();

    while (dataReader.Read())
    {
      timeSaved = dataReader.GetString(1);
      xPosition = dataReader.GetFloat(2);
      yPosition = dataReader.GetFloat(3);
      hpAmount = dataReader.GetFloat(4);
      map = dataReader.GetString(5);
      saveGames.Add(new SaveGame(timeSaved, xPosition, yPosition, hpAmount, map));
    }
    dbConnection.Close();
    return saveGames;
  }


  private IDbConnection CreateAndOpenDatabase()
  {
    string dbUri = "URI=file:GameDatabase.sqlite";
    IDbConnection dbConnection = new SqliteConnection(dbUri);
    dbConnection.Open();

    IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
    dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS SaveGameTable (id TEXT PRIMARY KEY, time TEXT, xPos REAL, yPos REAL, hpAmount REAL, map TEXT);";
    dbCommandCreateTable.ExecuteReader();

    return dbConnection;
  }
}
