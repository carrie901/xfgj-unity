using System;

public class Picture
{

    public string pictureId;
    public string atlasName;
    public int assetId;

    public static readonly string TABLE_NAME = "picture";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_ATLAS_NAME = "atlas_name";
    public static readonly string FIELD_ASSET_ID = "asset_id";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_PICTURE_ID + " TEXT PRIMARY KEY,"
                                               + FIELD_ATLAS_NAME + " TEXT,"
                                               + FIELD_ASSET_ID + " INTEGER)";

    public Picture (string pictureId, string atlasName, int assetId) {
        this.pictureId = pictureId;
        this.atlasName = atlasName;
        this.assetId = assetId;
    }

}

