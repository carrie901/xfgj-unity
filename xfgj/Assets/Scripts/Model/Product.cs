using System;

public class Product {
    
    public int productId;
    public int producerId;
    public string name;
    public int cid;
    public string size;
    public string pictureId;
    public string details;
    public int assetId;
    public DateTime modified;

    public static readonly string TABLE_NAME = "product";
    public static readonly string FIELD_PRODUCT_ID = "product_id";
    public static readonly string FIELD_PRODUCER_ID = "producer_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_CID = "cid";
    public static readonly string FIELD_SIZE = "size";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string FIELD_ASSET_ID = "asset_id";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_PRODUCT_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_PRODUCER_ID + " INTEGER,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_CID + " INTEGER,"
                                               + FIELD_SIZE + " TEXT,"
                                               + FIELD_PICTURE_ID + " TEXT,"
                                               + FIELD_DETAILS + " TEXT,"
                                               + FIELD_ASSET_ID + " INTEGER,"
                                               + FIELD_MODIFIED + " TEXT)";

    public Product (int productId, int producerId, string name, int cid, string size,
                    string pictureId, string details, int assetId, DateTime modified) {
        this.productId = productId;
        this.producerId = producerId;
        this.name = name;
        this.cid = cid;
        this.size = size;
        this.pictureId = pictureId;
        this.details = details;
        this.assetId = assetId;
        this.modified = modified;
    }

}
