using UnityEngine;
using System;
using System.Collections;

public class Traderate {
    
    public enum ROLE {
        BUYER, SELLER
    };
    
    public enum RATE_RESULT {
        GOOD, NEUTRAL, BAD
    };
    
	public static ROLE StringToROLE (string s) {
		if (s.Equals(ROLE.BUYER.ToString())) {
			return ROLE.BUYER;
		}
		else if (!s.Equals(ROLE.SELLER.ToString())) {
			throw new Exception("param s error");
		}
		return ROLE.SELLER;
	}
	
	public static RATE_RESULT StringToRateResult (string s) {
		if (s.Equals(RATE_RESULT.GOOD.ToString())) {
			return RATE_RESULT.GOOD;
		}
		else if (s.Equals(RATE_RESULT.NEUTRAL.ToString())) {
			return RATE_RESULT.NEUTRAL;
		}
		else if (!s.Equals(RATE_RESULT.BAD.ToString())){
			throw new Exception("param s error");
		}
		return RATE_RESULT.BAD;
	}
	
    public readonly int tid;
    public readonly long numIid;
    public readonly ROLE role;
    public readonly string nick;
    public readonly RATE_RESULT result;
    public readonly DateTime created;
    public readonly string content;
    public readonly string reply;
    
	public static readonly string TABLE_NAME = "traderate";
	public static readonly string FIELD_TID = "tid";
	public static readonly string FIELD_NUMIID = "numiid";
	public static readonly string FIELD_ROLE = "role";
	public static readonly string FIELD_NICK = "nick";
	public static readonly string FIELD_RATE_RESULT = "rate_result";
	public static readonly string FIELD_CREATED = "created";
	public static readonly string FIELD_CONTENT = "content";
	public static readonly string FIELD_REPLY = "reply";
	
	public static readonly string CREATE_SQL = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + "("
                                               + FIELD_TID + " INTEGER PRIMARY KEY,"
			                                   + FIELD_NUMIID + " INTEGER,"
			                                   + FIELD_ROLE + " TEXT,"
						                       + FIELD_NICK + " TEXT,"
						                       + FIELD_RATE_RESULT + " TEXT,"
						                       + FIELD_CREATED + " TEXT,"
						                       + FIELD_CONTENT + " TEXT,"
			                                   + FIELD_REPLY + " TEXT)";
	
    public Traderate (int tid, long numIid, ROLE role, string nick, RATE_RESULT result, DateTime created, 
                      string content, string reply) {
        this.tid = tid;
        this.numIid = numIid;
        this.role = role;
        this.nick = nick;
        this.result = result;
        this.created = created;
        this.content = content;
        this.reply = reply;
    }
    
}
