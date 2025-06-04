using AmusementPark.Models;
using Spectre.Console.Testing;

namespace Test_AmusementPark;

public class UnitTest1
{

    [Fact]

    public void Test_BuySomeBuilding()
    {
        Park YourPark = new Park("jojo");

        YourPark.BuySomeBuilding();

        TestConsole console = new();
        console.Interactive();

        console.Input.PushKey(ConsoleKey.Spacebar);
        console.Input.PushKey(ConsoleKey.Enter);

        console.Input.PushTextWithEnter("eee");

        Assert.Contains(YourPark.InventoryBuildings, b => b is RollerCoaster && b.Name == "eee");

    }



    [Fact]
    public void TestBuySomeBuilding_1_()
    {
        // Given
        TestConsole console = new();
        console.Interactive();

        // Your mocked inputs must always end with "Enter" for each prompt!

        console.Input.PushTextWithEnter("martinland");

        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.Enter);

        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.Spacebar);
        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.Spacebar);
        console.Input.PushKey(ConsoleKey.DownArrow);
        console.Input.PushKey(ConsoleKey.Spacebar);
        console.Input.PushKey(ConsoleKey.Enter);

        console.Input.PushTextWithEnter("martinhouse");
        console.Input.PushTextWithEnter("martinfood");
        console.Input.PushTextWithEnter("martincoin");


        console.Input.PushKey(ConsoleKey.Enter);

        
    }
}
