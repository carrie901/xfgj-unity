using System;
using System.Collections;

public class Scene {

    public int sceneId;
    public string name;
    public int typeId;
    public string pictureId;
    public string details;
    public DateTime modified;
    public int assetId;
    public string products;
    public bool favourite;

    public static readonly string TABLE_NAME = "scene";
    public static readonly string FIELD_SCENE_ID = "scene_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_TYPE_ID = "type_id";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string FIELD_ASSET_ID = "asset_id";
    public static readonly string FIELD_PRODUCTS = "products";
    public static readonly string FIELD_FAVOURITE = "favourite";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_SCENE_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_TYPE_ID + " INTEGER,"
                                               + FIELD_PICTURE_ID + " TEXT,"
                                               + FIELD_DETAILS + " TEXT,"
                                               + FIELD_MODIFIED + " TEXT,"
                                               + FIELD_ASSET_ID + " INTEGER,"
                                               + FIELD_PRODUCTS + " TEXT,"
                                               + FIELD_FAVOURITE + " INTEGER DEFAULT 0)";
    
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
        this.favourite = false;
    }

    public Scene (int sceneId, string name, int typeId, string pictureId,
                  string details, DateTime modified, int assetId, string products,
                  bool favourite) {
        this.sceneId = sceneId;
        this.name = name;
        this.typeId = typeId;
        this.pictureId = pictureId;
        this.details = details;
        this.modified = modified;
        this.assetId = assetId;
        this.products = products;
        this.favourite = favourite;
    }
    
}
