namespace blue.ink.Domain.Tests;
using blue.ink.Domain.Catalog;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Can_Create_New_Rating()
    {
        // Arrange
        var rating = new Rating(1,"Mike","Great fit!");

        // Act (empty)

        // Assert
        Assert.AreEqual(1,rating.Stars);
        Assert.AreEqual("Mike",rating.UserName);
        Assert.AreEqual("Great fit!",rating.Review);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Cannot_Create_Rating_With_Invalid_Stars()
    {
        // Arrange
        var rating = new Rating(0,"Mike","Great fit!");
    }

    [TestMethod]
    public void Can_Create_New_Item()
    {
        var item = new Item("Name","Description","Brand",10.00m);

        Assert.AreEqual("Name", item.Name);
        Assert.AreEqual("Description", item.Description);
        Assert.AreEqual("Brand", item.Brand);
        Assert.AreEqual(10.00m, item.Price);
    }

    [TestMethod]
    public void Can_Create_Add_Rating()
    {
        // Arrange
        var item = new Item("Name","Description","Brand",10.00m);
        var rating = new Rating(5,"Name","Review");

        // Act
        item.AddRating(rating);

        // Assert
        Assert.AreEqual(rating, item.Ratings[0]);
    }
}