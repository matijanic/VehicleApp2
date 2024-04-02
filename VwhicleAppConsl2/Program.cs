
using DbUp;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Reflection;
using System.Runtime.InteropServices;
using VehicleAppConsl2.Services;
using VehicleAppConsl2.Util;
using System.Text;
using VwhicleAppConsl2.Util;



var upgrader =
        DeployChanges.To
            .PostgresqlDatabase(ConstantsBla.connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

if (upgrader.IsUpgradeRequired())
{
    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Error);
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
    }

}


var _vehicleOwnerService = new VehicleOwnerService();
await _vehicleOwnerService.InitialFatch();


//_ = Task.Run(() => 
Receive.ListenToUpdate();




int userInput;
Console.WriteLine();
Console.WriteLine("1. List All with Pagination");
Console.WriteLine("2. Search ");
Console.WriteLine("3. Active / NonActive VehicleOwner");
Console.WriteLine("4. Sort by price");
Console.WriteLine("5. Search by First Name and Last Name");


do
{

    Console.WriteLine("Enter choice");

    while (!int.TryParse(Console.ReadLine(), out userInput))
    {
        Console.WriteLine("Wrong choice");
    }

    switch (userInput)
    {
        case 1: Console.WriteLine("Enter page number:");
            int pageNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter page size:");
            int pageSize = int.Parse(Console.ReadLine());

            await _vehicleOwnerService.GetAllVehicleOwnerWithPagination(pageNumber,pageSize); 
            break;
        case 2: await _vehicleOwnerService.GetOwnerPrice();
            break;
        case 3: await _vehicleOwnerService.ActiveVehicleOwner();
            break;
        case 4: await _vehicleOwnerService.SortByPrice();
            break;
        case 5: await _vehicleOwnerService.SearchByName();
            break;
        default:Console.WriteLine("Wrong input");
            break;

    }




} while (userInput != 0);
