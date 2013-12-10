using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class LogicController {

    public static void CreateTable () {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        dbAccess.CreateTable(Category.CREATE_SQL);
        dbAccess.CreateTable(Item.CREATE_SQL);
        dbAccess.CreateTable(Producer.CREATE_SQL);
        dbAccess.CreateTable(Product.CREATE_SQL);
        dbAccess.CreateTable(Scene.CREATE_SQL);
        dbAccess.CreateTable(SceneType.CREATE_SQL);
        dbAccess.CreateTable(Traderate.CREATE_SQL);
        dbAccess.CloseSqlConnection();
    }
    
    #region category operation
    public static void InsertCategory (Category category) {
        if (category == null) {
            throw new SqliteException("category can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Category.FIELD_CID, Category.FIELD_NAME, 
                                      Category.FIELD_PARENT_CID, Category.FIELD_IS_PARENT,
                                      Category.FIELD_USABLE, Category.FIELD_MODIFIED};
        object[] values = new object[] {category.cid, category.name, category.parentCid,
                                        category.isParent ? 1 : 0, category.usable ? 1 : 0,
                                        StringUtil.DateTimeToString(category.modified)};
        dbAccess.Insert(Category.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateCategory (Category category) {
        if (category == null) {
            throw new SqliteException("category can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Category.FIELD_NAME, Category.FIELD_PARENT_CID,
                                      Category.FIELD_IS_PARENT, Category.FIELD_USABLE,
                                      Category.FIELD_MODIFIED};
        object[] values = new object[] {category.name, category.parentCid, category.isParent ? 1 : 0,
                                        category.usable ? 1 : 0, StringUtil.DateTimeToString(category.modified)};
        string whereArgs = "WHERE " + Category.FIELD_CID + "=" + category.cid;
        dbAccess.Update(Category.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }

    public static void ReplaceCategorys (List<Category> list) {
        if (list == null || list.Count == 0) {
            throw new SqliteException("list can't be empty");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Category.FIELD_CID, Category.FIELD_NAME, Category.FIELD_PARENT_CID,
                                      Category.FIELD_IS_PARENT, Category.FIELD_USABLE, Category.FIELD_MODIFIED};
        object[,] values = new object[list.Count, cols.Length];
        for (int i = 0; i < list.Count; ++i) {
            values[i, 0] = list[i].cid;
            values[i, 1] = list[i].name;
            values[i, 2] = list[i].parentCid;
            values[i, 3] = list[i].isParent ? 1 : 0;
            values[i, 4] = list[i].usable ? 1 : 0;
            values[i, 5] = StringUtil.DateTimeToString(list[i].modified);
        }
        dbAccess.ReplaceInBatch(Category.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteCategory (Category category) {
        if (category == null) {
            throw new SqliteException("category can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Category.FIELD_CID + "=" + category.cid;
        dbAccess.Delete(Category.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static Category GetCategory (int cid) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Category.FIELD_CID + "=" + cid;
        SqliteDataReader reader = dbAccess.Query(Category.TABLE_NAME, "*", whereArgs);
        Category category = null;
        while (reader.Read()) {
            category = new Category(reader.GetInt32(reader.GetOrdinal(Category.FIELD_CID)),
                                    reader.GetString(reader.GetOrdinal(Category.FIELD_NAME)),
                                    reader.GetInt32(reader.GetOrdinal(Category.FIELD_PARENT_CID)),
                                    reader.GetBoolean(reader.GetOrdinal(Category.FIELD_IS_PARENT)),
                                    reader.GetBoolean(reader.GetOrdinal(Category.FIELD_USABLE)),
                                    StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Category.FIELD_MODIFIED))));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return category;
    }
    #endregion
    
    #region Item operation
    public static void InsertItem (Item item) {
        if (item == null) {
            throw new SqliteException("item can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Item.FIELD_NUMIID, Item.FIELD_TITLE, Item.FIELD_DETAIL_URL, Item.FIELD_CID,
                                      Item.FIELD_PIC_URL, Item.FIELD_PRICE, Item.FIELD_LIST_TIME, Item.FIELD_DELIST_TIME,
                                      Item.FIELD_PRODUCT_ID, Item.FIELD_SEQ, Item.FIELD_MODIFIED};
        object[] values = new object[] {item.numIid, item.title, item.detailUrl, item.cid, item.picUrl, item.price,
                                        StringUtil.DateTimeToString(item.listTime), StringUtil.DateTimeToString(item.delistTime),
                                        item.productId, item.seq, StringUtil.DateTimeToString(item.modified)};
        dbAccess.Insert(Item.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateItem (Item item) {
        if (item == null) {
            throw new SqliteException("item can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Item.FIELD_TITLE, Item.FIELD_DETAIL_URL, Item.FIELD_CID,
                                      Item.FIELD_PIC_URL, Item.FIELD_PRICE, Item.FIELD_LIST_TIME, Item.FIELD_DELIST_TIME,
                                      Item.FIELD_PRODUCT_ID, Item.FIELD_SEQ, Item.FIELD_MODIFIED};
        object[] values = new object[] {item.title, item.detailUrl, item.cid, item.picUrl, item.price,
                                        StringUtil.DateTimeToString(item.listTime), StringUtil.DateTimeToString(item.delistTime),  
                                        item.productId, item.seq, StringUtil.DateTimeToString(item.modified)};
        string whereArgs = "WHERE " + Item.FIELD_NUMIID + "=" + item.numIid;
        dbAccess.Update(Item.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteItem (Item item) {
        if (item == null) {
            throw new SqliteException("item can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Item.FIELD_NUMIID + "=" + item.numIid;
        dbAccess.Delete(Item.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static Item GetItem (int numIid) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Item.FIELD_NUMIID + "=" + numIid;
        SqliteDataReader reader = dbAccess.Query(Item.TABLE_NAME, "*", whereArgs);
        Item item = null;
        while (reader.Read()) {
            item = new Item(reader.GetInt64(reader.GetOrdinal(Item.FIELD_NUMIID)),
                            reader.GetString(reader.GetOrdinal(Item.FIELD_TITLE)),
                            reader.GetString(reader.GetOrdinal(Item.FIELD_DETAIL_URL)),
                            reader.GetInt32(reader.GetOrdinal(Item.FIELD_CID)),
                            reader.GetString(reader.GetOrdinal(Item.FIELD_PIC_URL)),
                            reader.GetFloat(reader.GetOrdinal(Item.FIELD_PRICE)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Item.FIELD_LIST_TIME))),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Item.FIELD_DELIST_TIME))),
                            reader.GetInt32(reader.GetOrdinal(Item.FIELD_PRODUCT_ID)),
                            reader.GetInt32(reader.GetOrdinal(Item.FIELD_SEQ)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Item.FIELD_MODIFIED))));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return item;
    }
    #endregion
    
    #region Producer operation
    public static void InsertProducer (Producer producer) {
        if (producer == null) {
            throw new SqliteException("producer can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Producer.FIELD_PRODUCER_ID, Producer.FIELD_NAME,
                                      Producer.FIELD_DETAILS, Producer.FIELD_MODIFIED};
        object[] values = new object[] {producer.producerId, producer.name, producer.details,
                                        StringUtil.DateTimeToString(producer.modified)};
        dbAccess.Insert(Producer.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateProducer (Producer producer) {
        if (producer == null) {
            throw new SqliteException("producer can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Producer.FIELD_NAME, Producer.FIELD_DETAILS,
                                      Producer.FIELD_MODIFIED};
        object[] values = new object[] {producer.name, producer.details,
                                        StringUtil.DateTimeToString(producer.modified)};
        string whereArgs = "WHERE " + Producer.FIELD_PRODUCER_ID + "=" + producer.producerId;
        dbAccess.Update(Producer.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteProducer (Producer producer) {
        if (producer == null) {
            throw new SqliteException("producer can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Producer.FIELD_PRODUCER_ID + "=" + producer.producerId;
        dbAccess.Delete(Producer.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static Producer GetProducer (int producerId) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Producer.FIELD_PRODUCER_ID + "=" + producerId;
        SqliteDataReader reader = dbAccess.Query(Producer.TABLE_NAME, "*", whereArgs);
        Producer producer = null;
        while (reader.Read()) {
            producer = new Producer(reader.GetInt32(reader.GetOrdinal(Producer.FIELD_PRODUCER_ID)),
                            reader.GetString(reader.GetOrdinal(Producer.FIELD_NAME)),
                            reader.GetString(reader.GetOrdinal(Producer.FIELD_DETAILS)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Producer.FIELD_MODIFIED))));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return producer;
    }
    #endregion
    
    #region Product operation
    public static void InsertProduct (Product product) {
        if (product == null) {
            throw new SqliteException("product can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Product.FIELD_PRODUCT_ID, Product.FIELD_PRODUCER_ID,
                                      Product.FIELD_NAME, Product.FIELD_CID, Product.FIELD_DETAILS,
                                      Product.FIELD_PIC_URL, Product.FIELD_SIZE,
                                      Product.FIELD_MODIFIED, Product.FIELD_ASSET_NAME,
                                      Product.FIELD_ASSET_VERSION};
        object[] values = new object[] {product.productId, product.producerId, product.name, product.cid,
                                        product.details, product.picUrl, product.size,
                                        StringUtil.DateTimeToString(product.modified),
                                        product.assetName, product.assetVersion};
        dbAccess.Insert(Product.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateProduct (Product product) {
        if (product == null) {
            throw new SqliteException("product can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Product.FIELD_PRODUCER_ID, Product.FIELD_NAME,
                                      Product.FIELD_CID, Product.FIELD_DETAILS, Product.FIELD_PIC_URL,
                                      Product.FIELD_SIZE, Product.FIELD_MODIFIED,
                                      Product.FIELD_ASSET_NAME, Product.FIELD_ASSET_VERSION};
        object[] values = new object[] {product.producerId, product.name, product.cid,
                                        product.details, product.picUrl, product.size,
                                        StringUtil.DateTimeToString(product.modified),
                                        product.assetName, product.assetVersion};
        string whereArgs = "WHERE " + Product.FIELD_PRODUCT_ID + "=" + product.productId;
        dbAccess.Update(Product.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteProduct (Product product) {
        if (product == null) {
            throw new SqliteException("product can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Product.FIELD_PRODUCT_ID + "=" + product.productId;
        dbAccess.Delete(Product.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static Product GetProduct (int productId) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Product.FIELD_PRODUCT_ID + "=" + productId;
        SqliteDataReader reader = dbAccess.Query(Product.TABLE_NAME, "*", whereArgs);
        Product product = null;
        while (reader.Read()) {
            product = new Product(reader.GetInt32(reader.GetOrdinal(Product.FIELD_PRODUCT_ID)),
                            reader.GetInt32(reader.GetOrdinal(Product.FIELD_PRODUCER_ID)),
                            reader.GetString(reader.GetOrdinal(Product.FIELD_NAME)),
                            reader.GetInt32(reader.GetOrdinal(Product.FIELD_CID)),
                            reader.GetString(reader.GetOrdinal(Product.FIELD_SIZE)),
                            reader.GetString(reader.GetOrdinal(Product.FIELD_PIC_URL)),
                            reader.GetString(reader.GetOrdinal(Product.FIELD_DETAILS)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Product.FIELD_MODIFIED))),
                            reader.GetString(reader.GetOrdinal(Product.FIELD_ASSET_NAME)),
                            reader.GetInt32(reader.GetOrdinal(Product.FIELD_ASSET_VERSION)));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return product;
    }
    #endregion
    
    #region Scene operation
    public static void InsertScene (Scene scene) {
        if (scene == null) {
            throw new SqliteException("scene can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Scene.FIELD_SCENE_ID, Scene.FIELD_NAME, Scene.FIELD_TYPE_ID, 
                                      Scene.FIELD_PIC_URL, Scene.FIELD_DETAILS, Scene.FIELD_MODIFIED,
                                      Scene.FIELD_ASSET_NAME, Scene.FIELD_ASSET_VERSION};
        object[] values = new object[] {scene.sceneId, scene.name, scene.typeId,
                                        scene.picUrl, scene.details, StringUtil.DateTimeToString(scene.modified),
                                        scene.assetName, scene.assetVersion};
        dbAccess.Insert(Scene.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateScene (Scene scene) {
        if (scene == null) {
            throw new SqliteException("scene can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Scene.FIELD_NAME, Scene.FIELD_TYPE_ID,
                                      Scene.FIELD_PIC_URL, Scene.FIELD_DETAILS, Scene.FIELD_MODIFIED,
                                      Scene.FIELD_ASSET_NAME, Scene.FIELD_ASSET_VERSION};
        object[] values = new object[] {scene.name, scene.typeId,
                                        scene.picUrl, scene.details, StringUtil.DateTimeToString(scene.modified),
                                        scene.assetName, scene.assetVersion};
        string whereArgs = "WHERE " + Scene.FIELD_SCENE_ID + "=" + scene.sceneId;
        dbAccess.Update(Scene.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }

    public static void ReplaceScenes (List<Scene> list) {
        if (list == null || list.Count == 0) {
            throw new SqliteException("list can't be empty");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Scene.FIELD_SCENE_ID, Scene.FIELD_NAME, Scene.FIELD_TYPE_ID, 
                                      Scene.FIELD_PIC_URL, Scene.FIELD_DETAILS, Scene.FIELD_MODIFIED,
                                      Scene.FIELD_ASSET_NAME, Scene.FIELD_ASSET_VERSION};
        object[,] values = new object[list.Count, cols.Length];
        for (int i = 0; i < list.Count; ++i) {
            values[i, 0] = list[i].sceneId;
            values[i, 1] = list[i].name;
            values[i, 2] = list[i].typeId;
            values[i, 3] = list[i].picUrl;
            values[i, 4] = list[i].details;
            values[i, 5] = StringUtil.DateTimeToString(list[i].modified);
            values[i, 6] = list[i].assetName;
            values[i, 7] = list[i].assetVersion;
        }
        dbAccess.ReplaceInBatch(Scene.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteScene (Scene scene) {
        if (scene == null) {
            throw new SqliteException("scene can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Scene.FIELD_SCENE_ID + "=" + scene.sceneId;
        dbAccess.Delete(Scene.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }

    public static void DeleteScenes (List<Scene> list) {
        if (list == null || list.Count == 0) {
            throw new SqliteException("scene list can't be empty");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        StringBuilder whereArgs = new StringBuilder();
        whereArgs.Append("WHERE " + Scene.FIELD_SCENE_ID + " in (");
        for (int i = 0; i < list.Count; ++i) {
            whereArgs.Append(list[i].sceneId);
            if (i == list.Count - 1) {
                whereArgs.Append(")");
            }
            else {
                whereArgs.Append(",");
            }
        }
        dbAccess.Delete(Scene.TABLE_NAME, whereArgs.ToString());
        dbAccess.CloseSqlConnection();
    }
    
    public static Scene GetScene (int sceneId) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Scene.FIELD_SCENE_ID + "=" + sceneId;
        SqliteDataReader reader = dbAccess.Query(Scene.TABLE_NAME, "*", whereArgs);
        Scene scene = null;
        while (reader.Read()) {
            scene = new Scene(reader.GetInt32(reader.GetOrdinal(Scene.FIELD_SCENE_ID)),
                            reader.GetString(reader.GetOrdinal(Scene.FIELD_NAME)),
                            reader.GetInt32(reader.GetOrdinal(Scene.FIELD_TYPE_ID)),
                            reader.GetString(reader.GetOrdinal(Scene.FIELD_PIC_URL)),
                            reader.GetString(reader.GetOrdinal(Scene.FIELD_DETAILS)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Scene.FIELD_MODIFIED))),
                            reader.GetString(reader.GetOrdinal(Scene.FIELD_ASSET_NAME)),
                            reader.GetInt32(reader.GetOrdinal(Scene.FIELD_ASSET_VERSION)));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return scene;
    }

    public static List<Scene> GetScenesBySceneType(int sceneTypeId) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Scene.FIELD_TYPE_ID + "=" + sceneTypeId;
        SqliteDataReader reader = dbAccess.Query(Scene.TABLE_NAME, "*", whereArgs);
        List<Scene> sceneList = new List<Scene>();
        while (reader.Read()) {
            sceneList.Add(new Scene(reader.GetInt32(reader.GetOrdinal(Scene.FIELD_SCENE_ID)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_NAME)),
                                    reader.GetInt32(reader.GetOrdinal(Scene.FIELD_TYPE_ID)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_PIC_URL)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_DETAILS)),
                                    StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Scene.FIELD_MODIFIED))),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_ASSET_NAME)),
                                    reader.GetInt32(reader.GetOrdinal(Scene.FIELD_ASSET_VERSION))));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return sceneList;
    }

    public static Scene GetLatestMofifiedScene () {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string orderArgs = "ORDER BY " + Scene.FIELD_MODIFIED + " DESC";
        string limitArgs = "LIMIT 1";
        SqliteDataReader reader = dbAccess.Query(Scene.TABLE_NAME, "*", null, orderArgs, limitArgs);
        Scene scene = null;
        while (reader.Read()) {
            scene = new Scene(reader.GetInt32(reader.GetOrdinal(Scene.FIELD_SCENE_ID)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_NAME)),
                                    reader.GetInt32(reader.GetOrdinal(Scene.FIELD_TYPE_ID)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_PIC_URL)),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_DETAILS)),
                                    StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Scene.FIELD_MODIFIED))),
                                    reader.GetString(reader.GetOrdinal(Scene.FIELD_ASSET_NAME)),
                                    reader.GetInt32(reader.GetOrdinal(Scene.FIELD_ASSET_VERSION)));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return scene;
    }
    #endregion
    
    #region SceneType operation
    public static void InsertSceneType (SceneType sceneType) {
        if (sceneType == null) {
            throw new SqliteException("sceneType can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {SceneType.FIELD_TYPE_ID, SceneType.FIELD_NAME,
                                      SceneType.FIELD_MODIFIED};
        object[] values = new object[] {sceneType.typeId, sceneType.name, StringUtil.DateTimeToString(sceneType.modified)};
        dbAccess.Insert(SceneType.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateSceneType (SceneType sceneType) {
        if (sceneType == null) {
            throw new SqliteException("sceneType can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {SceneType.FIELD_NAME, SceneType.FIELD_MODIFIED};
        object[] values = new object[] {sceneType.name, StringUtil.DateTimeToString(sceneType.modified)};
        string whereArgs = "WHERE " + SceneType.FIELD_TYPE_ID + "=" + sceneType.typeId;
        dbAccess.Update(SceneType.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }

    public static void ReplaceSceneTypes (List<SceneType> list) {
        if (list == null || list.Count == 0) {
            throw new SqliteException("list can't be empty");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {SceneType.FIELD_TYPE_ID, SceneType.FIELD_NAME, SceneType.FIELD_MODIFIED};
        object[,] values = new object[list.Count, cols.Length];
        for (int i = 0; i < list.Count; ++i) {
            values[i, 0] = list[i].typeId;
            values[i, 1] = list[i].name;
            values[i, 2] = StringUtil.DateTimeToString(list[i].modified);
        }
        dbAccess.ReplaceInBatch(SceneType.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteSceneType (SceneType sceneType) {
        if (sceneType == null) {
            throw new SqliteException("sceneType can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + SceneType.FIELD_TYPE_ID + "=" + sceneType.typeId;
        dbAccess.Delete(SceneType.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static SceneType GetSceneType (int typeId) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + SceneType.FIELD_TYPE_ID + "=" + typeId;
        SqliteDataReader reader = dbAccess.Query(SceneType.TABLE_NAME, "*", whereArgs);
        SceneType sceneType = null;
        while (reader.Read()) {
            sceneType = new SceneType(reader.GetInt32(reader.GetOrdinal(SceneType.FIELD_TYPE_ID)),
                            reader.GetString(reader.GetOrdinal(SceneType.FIELD_NAME)),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(SceneType.FIELD_MODIFIED))));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return sceneType;
    }
    #endregion
    
    #region Traderate operation
    public static void InsertTraderate (Traderate traderate) {
        if (traderate == null) {
            throw new SqliteException("traderate can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Traderate.FIELD_TID, Traderate.FIELD_NUMIID, Traderate.FIELD_ROLE,
                                      Traderate.FIELD_NICK, Traderate.FIELD_RATE_RESULT, Traderate.FIELD_CREATED,
                                      Traderate.FIELD_CONTENT, Traderate.FIELD_REPLY};
        object[] values = new object[] {traderate.tid, traderate.numIid, traderate.role.ToString(), traderate.nick,
                                        traderate.result.ToString(), StringUtil.DateTimeToString(traderate.created),
                                        traderate.content, traderate.reply};
        dbAccess.Insert(Traderate.TABLE_NAME, cols, values);
        dbAccess.CloseSqlConnection();
    }
    
    public static void UpdateTraderate (Traderate traderate) {
        if (traderate == null) {
            throw new SqliteException("traderate can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string[] cols = new string[] {Traderate.FIELD_TID, Traderate.FIELD_NUMIID, Traderate.FIELD_ROLE,
                                      Traderate.FIELD_NICK, Traderate.FIELD_RATE_RESULT, Traderate.FIELD_CREATED,
                                      Traderate.FIELD_CONTENT, Traderate.FIELD_REPLY};
        object[] values = new object[] {traderate.tid, traderate.numIid, traderate.role.ToString(), traderate.nick,
                                        traderate.result.ToString(), StringUtil.DateTimeToString(traderate.created),
                                        traderate.content, traderate.reply};
        string whereArgs = "WHERE " + Traderate.FIELD_TID + "=" + traderate.tid;
        dbAccess.Update(Traderate.TABLE_NAME, cols, values, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static void DeleteTraderate (Traderate traderate) {
        if (traderate == null) {
            throw new SqliteException("traderate can't be null");
        }
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Traderate.FIELD_TID + "=" + traderate.tid;
        dbAccess.Delete(Traderate.TABLE_NAME, whereArgs);
        dbAccess.CloseSqlConnection();
    }
    
    public static Traderate GetTraderate (int tid) {
        DBAccess dbAccess = new DBAccess();
        dbAccess.OpenDB(Config.DB_PATH);
        string whereArgs = "WHERE " + Traderate.FIELD_TID + "=" + tid;
        SqliteDataReader reader = dbAccess.Query(Traderate.TABLE_NAME, "*", whereArgs);
        Traderate traderate = null;
        while (reader.Read()) {
            traderate = new Traderate(reader.GetInt32(reader.GetOrdinal(Traderate.FIELD_TID)),
                            reader.GetInt64(reader.GetOrdinal(Traderate.FIELD_NUMIID)),
                            Traderate.StringToROLE(reader.GetString(reader.GetOrdinal(Traderate.FIELD_ROLE))),
                            reader.GetString(reader.GetOrdinal(Traderate.FIELD_NICK)),
                            Traderate.StringToRateResult(reader.GetString(reader.GetOrdinal(Traderate.FIELD_RATE_RESULT))),
                            StringUtil.StringToDateTime(reader.GetString(reader.GetOrdinal(Traderate.FIELD_CREATED))),
                            reader.GetString(reader.GetOrdinal(Traderate.FIELD_CONTENT)),
                            reader.GetString(reader.GetOrdinal(Traderate.FIELD_REPLY)));
        }
        reader.Close();
        dbAccess.CloseSqlConnection();
        return traderate;
    }
    #endregion
}
