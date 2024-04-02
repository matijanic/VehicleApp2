using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VehicleAppConsl2.Models;

namespace VehicleAppConsl2.Util
{
    public static class GlobalVehicleOwnerList
    {
        private static List<VehicleOwnerRabbit> vehicleOwner = new List<VehicleOwnerRabbit>();

        public static List<VehicleOwnerRabbit> VehicleOwnerAll
        {
            get
            {
                return vehicleOwner;
            }
        }

        public static void FetchAndLoadData(List<VehicleOwnerRabbit> vehicleModelNew)
        {
            vehicleOwner.Clear();
            vehicleOwner.AddRange(vehicleModelNew);
        }

        public static void Details()
        {
            foreach(var vehicle in vehicleOwner)
            {
                Console.WriteLine("Make: {0}/ Model: {1}/ First Name: {2}/ Last Name: {3}/ Price: {4}/ IsActive: {5}",
                    vehicle.MakeName,vehicle.ModelName,vehicle.FirstName, vehicle.LastName, vehicle.Price, vehicle.IsActice);

            }
        }

        public static void Details(IEnumerable<VehicleOwnerRabbit> vehicleModelNew)
        {
            foreach (var vehicle in vehicleModelNew)
            {
                Console.WriteLine("Make: {0}/ Model: {1}/ First Name: {2}/ Last Name: {3}/ Price: {4}/ IsActive: {5}",
                    vehicle.MakeName, vehicle.ModelName, vehicle.FirstName, vehicle.LastName, vehicle.Price, vehicle.IsActice);

            }
        }


    }
}
