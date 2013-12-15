using System;
using System.Collections;

public class Scene {

    public readonly int sceneId;
    public readonly string name;
    public readonly int typeId;
    public readonly string pictureId;
    public readonly string details;
    public readonly DateTime modified;
    public readonly int assetId;
    public readonly string products;

    public static readonly string TABLE_NAME = "scene";
    public static readonly string FIELD_SCENE_ID = "scene_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_TYPE_ID = "type_id";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string FIELD_ASSET_ID = "asset_id";
    public static readonly string FIELD_PRODUCTS = "products";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_SCENE_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_TYPE_ID + " INTEGER,"
                                               + FIELD_PICTURE_ID + " TEXT,"
                                               + FIELD_DETAILS + " TEXT,"
                                               + FIELD_MODIFIED + " TEXT,"
                                               + FIELD_ASSET_ID + " INTEGER,"
                                               + FIELD_PRODUCTS + " TEXT)";
    
    public Scene (int sceneId, string name, int typeId, string pictureId,
                  string details, DateTime modified, int assetId, string products) {
        this.sceneId = sceneId;
        this.name = name;
        this.typeId = typeId;
        this.pictureId = pictureId;
        this.details = details;
        this.modified = modified;
        this.assetId = assetId;
        this.products = products;
    }
    
}
