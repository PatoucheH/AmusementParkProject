namespace Test_AmusementPark;

public class UnitTest1
{
    [Fact]
    public void TestBuySomeBuilding_1_()
    {
        // Arrange
        var park = new AmusementPark.Models.Park("jojo");
        park.BuySomeBuilding();

        // Act
        var resultat = calc.Additionner(2, 3);

        // Assert
        Assert.Equal(5, resultat);
    }
}
