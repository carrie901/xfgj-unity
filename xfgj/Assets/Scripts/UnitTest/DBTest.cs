using UnityEngine;
using System;
using System.Collections;

public class DBTest : UUnitTestCase {

    [UUnitTest]
    public void CategoryDbTest () {
        LogicController.CreateTable();
        Category category = new Category(1, "test1", 0, true);
        LogicController.InsertCategory(category);
        Category category2 = LogicController.GetCategory(1);
        UUnitAssert.NotNull(category2);
        UUnitAssert.Equals(category.name, category2.name);
        Category category3 = new Category(category.cid, "test2", 1, false);
        LogicController.UpdateCategory(category3);
        category2 = LogicController.GetCategory(category.cid);
        UUnitAssert.NotNull(category2);
        UUnitAssert.Equals("test2", category2.name);
        LogicController.DeleteCategory(category);
        Category category4 = LogicController.GetCategory(1);
        UUnitAssert.Null(category4);
    }
    
    [UUnitTest]
    public void ItemDbTest () {
        LogicController.CreateTable();
        Item item = new Item(1, "title test", "http://www.xfgj.com", 1, "http://www.xfgj.com", 10.0f, DateTime.Now,
                             DateTime.Now, 2, 0);
        LogicController.InsertItem(item);
        Item item2 = LogicController.GetItem(1);
        UUnitAssert.NotNull(item2);
        UUnitAssert.Equals("title test", item2.title);
        Item item3 = new Item(1, "hello world", "http://www.xfgj.com", 1, "http://www.xfgj.com", 10.0f, DateTime.Now,
                             DateTime.Now, 2, 0);
        LogicController.UpdateItem(item3);
        item2 = LogicController.GetItem(1);
        UUnitAssert.NotNull(item2);
        UUnitAssert.Equals("hello world", item2.title);
        LogicController.DeleteItem(item);
        item2 = LogicController.GetItem(1);
        UUnitAssert.Null(item2);
    }
    
    [UUnitTest]
    public void ProducerTest () {
        LogicController.CreateTable();
        Producer producer = new Producer(1, "producer name", "hello world");
        LogicController.InsertProducer(producer);
        Producer producer2 = LogicController.GetProducer(1);
        UUnitAssert.NotNull(producer2);
        Producer producer3 = new Producer(1, "where", "hello world");
        LogicController.UpdateProducer(producer3);
        producer2 = LogicController.GetProducer(1);
        UUnitAssert.NotNull(producer2);
        UUnitAssert.Equals("where", producer2.name);
        LogicController.DeleteProducer(producer);
        producer2 = LogicController.GetProducer(1);
        UUnitAssert.Null(producer2);
    }
    
    [UUnitTest]
    public void ProductTest () {
        LogicController.CreateTable();
        Product product = new Product(1, 2, "hello", 1, "size", "http://www.baidu.com", "details info");
        LogicController.InsertProduct(product);
        Product product2 = LogicController.GetProduct(1);
        UUnitAssert.NotNull(product2);
        Product product3 = new Product(1, 2, "hello world1", 1, "size", "http://www.baidu.com", "details info");
        LogicController.UpdateProduct(product3);
        product2 = LogicController.GetProduct(1);
        UUnitAssert.NotNull(product2);
        UUnitAssert.Equals("hello world1", product2.name);
        LogicController.DeleteProduct(product);
        product2 = LogicController.GetProduct(1);
        UUnitAssert.Null(product2);
    }
    
    [UUnitTest]
    public void SceneTest () {
        LogicController.CreateTable();
        Scene scene = new Scene(1, "scene name", 1, "http://www.xfgj.com", "detail info");
        LogicController.InsertScene(scene);
        Scene scene2 = LogicController.GetScene(1);
        UUnitAssert.NotNull(scene2);
        Scene scene3 = new Scene(1, "scene name", 1, "http://www.xfgj.com", "detail infohaha");
        LogicController.UpdateScene(scene3);
        scene2 = LogicController.GetScene(1);
        UUnitAssert.NotNull(scene2);
        UUnitAssert.Equals("detail infohaha", scene2.details);
        LogicController.DeleteScene(scene);
        scene2 = LogicController.GetScene(1);
        UUnitAssert.Null(scene2);
    }
    
    [UUnitTest]
    public void SceneTypeTest () {
        LogicController.CreateTable();
        SceneType sceneType = new SceneType(1, "type1");
        LogicController.InsertSceneType(sceneType);
        SceneType sceneType2 = LogicController.GetSceneType(1);
        UUnitAssert.NotNull(sceneType2);
        SceneType sceneType3 = new SceneType(1, "type2");
        LogicController.UpdateSceneType(sceneType3);
        sceneType2 = LogicController.GetSceneType(1);
        UUnitAssert.NotNull(sceneType2);
        UUnitAssert.Equals("type2", sceneType2.name);
        LogicController.DeleteSceneType(sceneType);
        sceneType2 = LogicController.GetSceneType(1);
        UUnitAssert.Null(sceneType2);
    }
    
    [UUnitTest]
    public void TraderateTest () {
        LogicController.CreateTable();
        Traderate tr = new Traderate(1, 1, Traderate.ROLE.BUYER, "nickname", Traderate.RATE_RESULT.GOOD, DateTime.Now,
                                     "content hello world", "reply");
        LogicController.InsertTraderate(tr);
        Traderate tr2 = LogicController.GetTraderate(1);
        UUnitAssert.NotNull(tr2);
        Traderate tr3 = new Traderate(1, 2, Traderate.ROLE.SELLER, "nickname", Traderate.RATE_RESULT.BAD, DateTime.Now,
                                     "content hello world", "reply");
        LogicController.UpdateTraderate(tr3);
        tr2 = LogicController.GetTraderate(1);
        UUnitAssert.NotNull(tr2);
        UUnitAssert.Equals(Traderate.ROLE.SELLER.ToString(), tr2.role.ToString());
        LogicController.DeleteTraderate(tr);
        tr2 = LogicController.GetTraderate(1);
        UUnitAssert.Null(tr2);
    }
}
