using UnityEngine;
using System;
using System.Collections;

public class Item {

    public readonly long numIid;
    public readonly string title;
    public readonly string detailUrl;
    public readonly int cid;
    public readonly string picUrl;
    public readonly float price;
    public readonly DateTime listTime;
    public readonly DateTime delistTime;
    public readonly int productId;
    public readonly int seq;
    
    public static readonly string TABLE_NAME = "item";
    public static readonly string FIELD_NUMIID = "numiid";
    public static readonly string FIELD_TITLE = "title";
    public static readonly string FIELD_DETAIL_URL = "detail_url";
    public static readonly string FIELD_CID = "CID";
    public static readonly string FIELD_PIC_URL = "pic_url";
    public static readonly string FIELD_PRICE = "price";
    public static readonly string FIELD_LIST_TIME = "list_time";
    public static readonly string FIELD_DELIST_TIME = "delist_time";
    public static readonly string FIELD_PRODUCT_ID = "product_id";
    public static readonly string FIELD_SEQ = "seq";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_NUMIID + " INTEGER PRIMARY KEY,"
                                               + FIELD_TITLE + " TEXT,"
                                               + FIELD_DETAIL_URL + " TEXT,"
                                               + FIELD_CID + " INTEGER,"
                                               + FIELD_PIC_URL + " TEXT,"
                                               + FIELD_PRICE + " REAL,"
                                               + FIELD_LIST_TIME + " TEXT,"
                                               + FIELD_DELIST_TIME + " TEXT,"
                                               + FIELD_PRODUCT_ID + " INTEGER,"
                                               + FIELD_SEQ + " INTEGER)";
    
    public Item (long numIid, string title, string detailUrl, int cid, string picUrl, float price, 
                 DateTime listTime, DateTime delistTime, int productId, int seq) {
        this.numIid = numIid;
        this.title = title;
        this.detailUrl = detailUrl;
        this.cid = cid;
        this.picUrl = picUrl;
        this.price = price;
        this.listTime = listTime;
        this.delistTime = delistTime;
        this.productId = productId;
        this.seq = seq;
    }

}
