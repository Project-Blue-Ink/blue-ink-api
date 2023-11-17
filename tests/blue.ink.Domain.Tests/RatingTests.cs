namespace blue.ink.Domain.Tests;
using blue.ink.Domain;
using blue.ink.Domain.Catalog;

[TestClass]
public class RatingTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Cannot_Created_Rating_With_Invalid_Stars()
    {
        var rating = new Rating(0, "Mike", "Great fit!");
    }
    public void Can_Create_New_Rating()
    {
        var rating = new Rating(1, "Mike", "Great fit!");

        Assert.AreEqual(1,rating.Stars);
        Assert.AreEqual("Mike",rating.UserName);
        Assert.AreEqual("Great fit!", rating.Review);

    }

   
}