using System;

public class Recommend {

    public int recommendId;
    public string pictureId;
    public string url;
    public DateTime startTime;
    public DateTime endTime;
    public string position;

    public static readonly string TABLE_NAME = "recommend";
    public static readonly string FIELD_RECOMMEND_ID = "recommend_id";
    public static readonly string FIELD_PICTURE_ID = "picture_id";
    public static readonly string FIELD_URL = "url";
    public static readonly string FIELD_START_TIME = "start_time";
    public static readonly string FIELD_END_TIME = "end_time";
    public static readonly string FIELD_POSITION = "position";
    public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_RECOMMEND_ID + " INTEGER PRIMARY KEY,"
                                               + FIELD_PICTURE_ID + " TEXT,"
                                               + FIELD_URL + " TEXT,"
                                               + FIELD_START_TIME + " TEXT,"
                                               + FIELD_END_TIME + " TEXT,"
                                               + FIELD_POSITION + " INTEGER)";

    public Recommend (int recommendId, string pictureId, string url,
                      DateTime startTime, DateTime endTime, string position) {
        this.recommendId = recommendId;
        this.pictureId = pictureId;
        this.url = url;
        this.startTime = startTime;
        this.endTime = endTime;
        this.position = position;
    }

}

