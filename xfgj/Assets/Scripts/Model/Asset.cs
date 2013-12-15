using System;

public class Asset {

    public int assetId;
    public string name;
    public int version;

    public static readonly string TABLE_NAME = "asset";
    public static readonly string FIELD_ASSET_ID = "asset_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_VERSION = "version";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_ASSET_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_VERSION + " INTEGER)";

    public Asset (int assetId, string name, int version) {
        this.assetId = assetId;
        this.name = name;
        this.version = version;
    }

}

