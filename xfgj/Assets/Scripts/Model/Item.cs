﻿using UnityEngine;
using System;
using System.Collections;

public class Item {

    public long numIid;
    public string title;
    public string detailUrl;
    public int cid;
    public string picUrl;
    public float price;
    public DateTime listTime;
    public DateTime delistTime;
    public int productId;
    public int seq;
    public DateTime modified;
    
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
    public static readonly string FIELD_MODIFIED = "modified";
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
                                               + FIELD_SEQ + " INTEGER,"
                                               + FIELD_MODIFIED + " TEXT)";
    
    public Item (long numIid, string title, string detailUrl, int cid, string picUrl,
                 float price, DateTime listTime, DateTime delistTime,
                 int productId, int seq, DateTime modified) {
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
        this.modified = modified;
    }

}
