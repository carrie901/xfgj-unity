using UnityEngine;
using System.Collections;

public class Producer {

    public readonly int producerId;
    public readonly string name;
    public readonly string details;
    
    public static readonly string TABLE_NAME = "producer";
    public static readonly string FIELD_PRODUCER_ID = "producer_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_PRODUCER_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_DETAILS + " TEXT)";
    
    public Producer (int producerId, string name, string details) {
        this.producerId = producerId;
        this.name = name;
        this.details = details;
    }
    
}
