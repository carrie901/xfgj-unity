using System;

public class Product {
    
    public readonly int productId;
    public readonly int producerId;
    public readonly string name;
    public readonly int cid;
    public readonly string size;
    public readonly string picUrl;
    public readonly string details;
    public readonly DateTime modified;
    public readonly string assetName;
    public readonly int assetVersion;
    
    public static readonly string TABLE_NAME = "product";
    public static readonly string FIELD_PRODUCT_ID = "product_id";
    public static readonly string FIELD_PRODUCER_ID = "producer_id";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_CID = "cid";
    public static readonly string FIELD_SIZE = "size";
    public static readonly string FIELD_PIC_URL = "pic_url";
    public static readonly string FIELD_DETAILS = "details";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string FIELD_ASSET_NAME = "asset_name";
    public static readonly string FIELD_ASSET_VERSION = "asset_version";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_PRODUCT_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_PRODUCER_ID + " INTEGER,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_CID + " INTEGER,"
                                               + FIELD_SIZE + " TEXT,"
                                               + FIELD_PIC_URL + " TEXT,"
                                               + FIELD_DETAILS + " TEXT,"
                                               + FIELD_MODIFIED + " TEXT,"
                                               + FIELD_ASSET_NAME + " TEXT,"
                                               + FIELD_ASSET_VERSION + " INTEGER)";
    
    public Product (int productId, int producerId, string name, int cid,
                    string size, string picUrl, string details, DateTime modified,
                    string assetName, int assetVersion) {
        this.productId = productId;
        this.producerId = producerId;
        this.name = name;
        this.cid = cid;
        this.size = size;
        this.picUrl = picUrl;
        this.details = details;
        this.modified = modified;
        this.assetName = assetName;
        this.assetVersion = assetVersion;
    }

}
