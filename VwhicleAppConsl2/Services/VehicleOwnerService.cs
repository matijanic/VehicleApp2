using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAppConsl2.Models;
using VehicleAppConsl2.Repository;
using VehicleAppConsl2.Util;

namespace VehicleAppConsl2.Services
{
    
    public class VehicleOwnerService
    {
        private VehicleOwnerRepository repository = new VehicleOwnerRepository();

        public async Task InitialFatch()
        {
            await repository.GetAllVehicleOwner();
        }
       
        public async Task GetAllVehicleOwnerWithPagination(int pageNumber, int pageSize)
        {

            //Receive.ListenToUpdate();

            var skip = (pageNumber-1) * pageSize;

            var paginationList = GlobalVehicleOwnerList.VehicleOwnerAll
                .Skip(skip).
                Take(pageSize);

            
            Console.WriteLine("List All");
            Console.WriteLine("----------------------");


            GlobalVehicleOwnerList.Details(paginationList);

        }

        public async Task GetOwnerPrice()
        {
            Console.WriteLine("Enter price to list all Vehicle Owner greater then");
            decimal inputPrice;

            while(!decimal.TryParse(Console.ReadLine(), out inputPrice))
            {
                Console.WriteLine("Wrong input! Enter price again: ");
                
            }

            var priceList = GlobalVehicleOwnerList.VehicleOwnerAll.Where(v => v.Price > inputPrice);

            GlobalVehicleOwnerList.Details(priceList);

        }

        public async Task SortByPrice()
        {
            Console.WriteLine("Sort VehicleOwner by price:");
            Console.WriteLine("-------------------------");

            var listPrice = GlobalVehicleOwnerList.VehicleOwnerAll.OrderByDescending(v => v.Price);
            GlobalVehicleOwnerList.Details(listPrice);
        }

        public async Task SearchByName()
        {
            Console.WriteLine("Enter first name:");
            string firstName= Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            var firstAndLastName = GlobalVehicleOwnerList.VehicleOwnerAll.Where(v=>v.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase) 
            || v.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));

            if(firstAndLastName.Any() )
            {
                GlobalVehicleOwnerList.Details(firstAndLastName);
            }
            else
            {
                Console.WriteLine("Not found!");
            }
            
        }

        public async Task ActiveVehicleOwner()
        {
            Console.WriteLine("Enter choice:");
            Console.WriteLine("1. Active");
            Console.WriteLine("2. No Active");
            Console.WriteLine("Sort by price");


            int inputActive;

            while(!int.TryParse(Console.ReadLine(), out inputActive))
            {
                Console.WriteLine("Enter valid input");
            }

            switch (inputActive)
            {
                case 1:
                    var isActive = GlobalVehicleOwnerList.VehicleOwnerAll.Where(v => v.IsActice == true);
                    GlobalVehicleOwnerList.Details(isActive);
                    break;
                case 2:
                    var noActive = GlobalVehicleOwnerList.VehicleOwnerAll.Where(v => v.IsActice == false);
                    GlobalVehicleOwnerList.Details(noActive);
                    break;
               
                default:
                    Console.WriteLine("Wrong input! Enter 1. for Active, 2. No Active VehicleOwner");
                    break;
            }
            

        }

       
    }
}
