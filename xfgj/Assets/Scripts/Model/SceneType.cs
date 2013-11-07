using UnityEngine;
using System.Collections;

public class SceneType {
    
    public readonly int typeId;
    public readonly string name;
    
	public static readonly string TABLE_NAME = "scene_type";
	public static readonly string FIELD_TYPE_ID = "type_id";
	public static readonly string FIELD_NAME = "name";
	public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_TYPE_ID + " INTEGER PRIMARY KEY,"
			                                   + FIELD_NAME + " TEXT)";
	
    public SceneType (int typeId, string name) {
        this.typeId = typeId;
        this.name = name;
    }
    
}
