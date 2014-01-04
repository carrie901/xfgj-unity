using System;
using System.Collections;

public class SceneType {
    
    public int typeId;
    public string name;
    public string pictureId;
    public DateTime modified;
    
    public static readonly string TABLE_NAME = "scene_type";
    public static readonly string FIELD_TYPE_ID = "type_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_TYPE_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_PICTURE_ID + " TEXT,"
                                               + FIELD_MODIFIED + " TEXT)";
    
    public SceneType (int typeId, string name, string pictureId, DateTime modified) {
        this.typeId = typeId;
        this.name = name;
        this.pictureId = pictureId;
        this.modified = modified;
    }
    
}
