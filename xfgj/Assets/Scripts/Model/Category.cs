using UnityEngine;
using System;
using System.Collections;

public class Category {

    public int cid;
    public string name;
    public int parentCid;
    public bool isParent;
    public bool usable;
    public DateTime modified;
    
    public static readonly string TABLE_NAME = "category";
    public static readonly string FIELD_CID = "cid";
    public static readonly string FIELD_NAME = "name";
    public static readonly string FIELD_PARENT_CID = "parent_cid";
    public static readonly string FIELD_IS_PARENT = "is_parent";
    public static readonly string FIELD_USABLE = "usable";
    public static readonly string FIELD_MODIFIED = "modified";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_CID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_PARENT_CID + " INTEGER,"
                                               + FIELD_IS_PARENT + " INTEGER,"
                                               + FIELD_USABLE + " INTEGER,"
                                               + FIELD_MODIFIED + " TEXT)";
    
    public Category (int cid, string name, int parentCid, bool isParent,
                     bool usable, DateTime modified) {
        this.cid = cid;
        this.name = name;
        this.parentCid = parentCid;
        this.isParent = isParent;
        this.usable = usable;
        this.modified = modified;
    }
    
}
