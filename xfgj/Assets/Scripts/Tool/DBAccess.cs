using UnityEngine;
using System;
using System.Collections;
using System.Text;
using Mono.Data.Sqlite;

public class DBAccess{

    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader reader;

    public DBAccess (string connectionString) {
        OpenDB (connectionString);
    }
    
    public DBAccess (){
    }

    public void OpenDB (string connectionString) {
        try {
            dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            //Debug.Log("Connected to db");
        }
        catch(Exception e) {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }
    }

    public void CloseSqlConnection () {
        if (dbCommand != null) {
            dbCommand.Dispose();
        }
        dbCommand = null;
        if (reader != null) {
            reader.Dispose();
        }
        reader = null;
        if (dbConnection != null) {
            dbConnection.Close();
        }
        dbConnection = null;
        //Debug.Log("Disconnected from db.");
    }

    public SqliteDataReader ExecuteQuery (string sqlQuery) {
        if (sqlQuery == null) {
            throw new SqliteException("");
        }
        //Debug.Log(sqlQuery);
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }
    
    public SqliteDataReader CreateTable (string sql) {
        if (sql == null) {
            throw new SqliteException("param sql is null");
        }
        return ExecuteQuery(sql);
    }
    
    public SqliteDataReader Insert (string tableName, string[] cols, object[] values) {
        if (tableName == null || cols == null || values == null || cols.Length != values.Length) {
            throw new SqliteException("param error");
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO ");
        sb.Append(tableName);
        sb.Append("(");
        for (int i = 0; i < cols.Length; ++i) {
            if (i > 0) {
                sb.Append(", ");
            }
            sb.Append(cols[i]);
        }
        sb.Append(") VALUES (");
        for (int i = 0; i < values.Length; ++i) {
            if (i > 0) {
                sb.Append(", ");
            }
            if (values[i] is String) {
                sb.Append("'");
                sb.Append(values[i]);
                sb.Append("'");
            }
            else {
                sb.Append(values[i]);
            }
        }
        sb.Append(")");
        return ExecuteQuery(sb.ToString());
    }

    public SqliteDataReader Update (string tableName, string[] cols, object[] values, string whereArgs) {
        if (tableName == null || cols == null || values == null || cols.Length != values.Length) {
            throw new SqliteException("param error");
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE ");
        sb.Append(tableName);
        sb.Append(" SET ");
        for (int i = 0; i < cols.Length; ++i) {
            if (i > 0) {
                sb.Append(", ");
            }
            sb.Append(cols[i]);
            sb.Append("=");
            if (values[i] is String) {
                sb.Append("'");
                sb.Append(values[i]);
                sb.Append("'");
            }
            else {
                sb.Append(values[i]);
            }
        }
        if (whereArgs != null) {
            sb.Append(" ");
            sb.Append(whereArgs);
        }
        return ExecuteQuery(sb.ToString());
    }

    public SqliteDataReader ReplaceInBatch (string tableName, string[] cols, object[,] values) {
        if (tableName == null || cols == null || values == null || values.Length % cols.Length != 0) {
            throw new SqliteException("param error");
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("REPLACE INTO ");
        sb.Append(tableName);
        sb.Append("(");
        for (int i = 0; i < cols.Length; ++i) {
            if (i > 0) {
                sb.Append(", ");
            }
            sb.Append(cols[i]);
        }
        sb.Append(") VALUES ");
        for (int i = 0; i < values.Length / cols.Length; ++i) {
            if (i > 0) {
                sb.Append(",");
            }
            sb.Append("(");
            for (int j = 0; j < cols.Length; ++j) {
                if (j > 0) {
                    sb.Append(", ");
                }
                if (values[i, j] is String) {
                    sb.Append("'");
                    sb.Append(values[i, j]);
                    sb.Append("'");
                }
                else {
                    sb.Append(values[i, j]);
                }
            }
            sb.Append(")");
        }
        return ExecuteQuery(sb.ToString());
    }

    public SqliteDataReader Delete (string tableName, string whereArgs) {
        if (tableName == null) {
            throw new SqliteException("param error");
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM ");
        sb.Append(tableName);
        if (whereArgs != null) {
            sb.Append(" ");
            sb.Append(whereArgs);
        }
        return ExecuteQuery(sb.ToString());
    }

    public SqliteDataReader ClearTable (string tableName) {
        return Delete(tableName, null);
    }

    public SqliteDataReader Query (string tableName, string selectArgs, string whereArgs) {
        return Query(tableName, selectArgs, whereArgs, null, null);
    }
    
    public SqliteDataReader Query (string tableName, string selectArgs, string whereArgs, string orderArgs, string limitArgs) {
        if (tableName == null || selectArgs == null) {
            throw new SqliteException("param tableName and selectArgs can't be null");
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(selectArgs);
        sb.Append(" FROM ");
        sb.Append(tableName);
        if (whereArgs != null) {
            sb.Append(" ");
            sb.Append(whereArgs);
        }
        if (orderArgs != null) {
            sb.Append(" ");
            sb.Append(orderArgs);
        }
        if (limitArgs != null) {
            sb.Append(" ");
            sb.Append(limitArgs);
        }
        return ExecuteQuery(sb.ToString());
    }

}