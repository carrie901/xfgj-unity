using System;
using System.Collections;

public class Scene {

    public readonly int sceneId;
    public readonly string name;
    public readonly int typeId;
    public readonly string picUrl;
    public readonly string details;
    public readonly DateTime modified;
    public readonly string assetName;
    public readonly int assetVersion;
    
    public static readonly string TABLE_NAME = "scene";
    public static readonly string FIELD_SCENE_ID = "scene_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_TYPE_ID = "type_id";
    public static readonly string FIELD_PIC_URL = "pic_url";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string FIELD_ASSET_NAME = "asset_name";
    public static readonly string FIELD_ASSET_VERSION = "asset_version";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_SCENE_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_TYPE_ID + " INTEGER,"
                                               + FIELD_PIC_URL + " TEXT,"
                                               + FIELD_DETAILS + " TEXT,"
                                               + FIELD_MODIFIED + " TEXT,"
                                               + FIELD_ASSET_NAME + " TEXT,"
                                               + FIELD_ASSET_VERSION + " INTEGER)";
    
    public Scene (int sceneId, string name, int typeId, string picUrl,
                  string details, DateTime modified, string assetName, int assetVersion) {
        this.sceneId = sceneId;
        this.name = name;
        this.typeId = typeId;
        this.picUrl = picUrl;
        this.details = details;
        this.modified = modified;
        this.assetName = assetName;
        this.assetVersion = assetVersion;
    }
    
}
