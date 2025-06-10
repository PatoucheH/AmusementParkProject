using AmusementPark.Models;
using Spectre.Console.Testing;

namespace Test_AmusementPark;

public class Test_Park
{
    /// <summary>
    /// Tests the ability of a park to purchase a roller coaster and add it to its inventory.
    /// </summary>
    [Fact]
    public void Test_BuyRollerCoaster()
    {
        var park = new Park("ooo");
        var buildings = new List<(string type, string name)>
        {
            ("RollerCoaster", "eee")
        };

        park.BuyBuilding(buildings);

        Assert.Contains(park.InventoryBuildings, b => b is RollerCoaster && b.Name == "eee");

    }

    /// <summary>
    /// Tests the ability of a park to purchase a gift shop and add it to its inventory.
    /// </summary>
    [Fact]
    public void Test_BuyGiftShop()
    {
        var park = new Park("ooo");
        var buildings = new List<(string type, string name)>
        {
            ("GiftShop", "ggg")
        };
        park.BuyBuilding(buildings);

        Assert.Contains(park.InventoryBuildings, b => b is GiftShop && b.Name == "ggg");
    }
    
    /// <summary>
    /// Tests the successful placement of a building in the park grid.
    /// </summary>
    [Fact]
    public void Test_Placement_Success_0_0()
    {
        var park = new Park("eee");
        park.InventoryBuildings.Add(new RollerCoaster("eee"));

        var result = park.TryPlaceBuilding("eee", 1, 1, out string message);

        Assert.True(result);
        Assert.Contains(park.PlacedBuilding, b => b.Name == "eee");
        Assert.DoesNotContain(park.InventoryBuildings, b => b.Name == "eee");
        Assert.Equal(":roller_coaster:", park.GridPark[0, 0]);
    }

    /// <summary>
    /// Tests the successful placement of a building in the park grid.
    /// </summary>
    [Fact]
    public void Test_Placement_Success_3_4()
    {
        var park = new Park("rrr");
        park.InventoryBuildings.Add(new GiftShop("eee"));

        var result = park.TryPlaceBuilding("eee", 3, 4, out string message);

        Assert.True(result);
        Assert.Contains(park.PlacedBuilding, b => b.Name == "eee");
        Assert.DoesNotContain(park.InventoryBuildings, b => b.Name == "eee");
        Assert.Equal(":wrapped_gift:", park.GridPark[2, 3]);
    }



    [Fact]
    public void Test_Remove_Success()
    {
        var park = new Park("rrr");
        park.PlacedBuilding.Add(new FoodShop("eee"));

        var result = park.TryRemoveBuilding("eee", out string message);

        Assert.True(result);
        Assert.Contains(park.InventoryBuildings, b => b.Name == "eee");
        Assert.DoesNotContain(park.PlacedBuilding, b => b.Name == "eee");  
    }

    [Fact]
    public void Test_Remove_Echec()
    {
        var park = new Park("rrr");

        var result = park.TryRemoveBuilding("eee", out string message);

        Assert.False(result);
    }
}
