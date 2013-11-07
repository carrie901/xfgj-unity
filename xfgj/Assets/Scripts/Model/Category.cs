using UnityEngine;
using System.Collections;

public class Category {

    public readonly int cid;
    public readonly string name;
    public readonly int parentCid;
    public readonly bool isParent;
    
    public static readonly string TABLE_NAME = "category";
	public static readonly string FIELD_CID = "cid";
	public static readonly string FIELD_NAME = "name";
	public static readonly string FIELD_PARENT_CID = "parent_cid";
	public static readonly string FIELD_IS_PARENT = "is_parent";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_CID + " INTEGER PRIMARY KEY,"
                                               + FIELD_NAME + " TEXT,"
                                               + FIELD_PARENT_CID + " INTEGER,"
			                                   + FIELD_IS_PARENT + " INTEGER)";
    
    public Category (int cid, string name, int parentCid, bool isParent) {
        this.cid = cid;
        this.name = name;
        this.parentCid = parentCid;
        this.isParent = isParent;
    }
    
}
