using AmusementPark.Models;
using Spectre.Console.Testing;

namespace Test_AmusementPark;

public class UnitTest1
{

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
