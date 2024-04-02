using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using VehicleAppConsl2.Models;
using VehicleAppConsl2.Util;
using VwhicleAppConsl2.Util;

namespace VehicleAppConsl2.Repository
{
    public class VehicleOwnerRepository
    {
        
        public async Task GetAllVehicleOwner()
        {
            
            var listAll = new List<VehicleOwnerRabbit>();
            using (var connection = new NpgsqlConnection(ConstantsBla.connectionString))
            {

                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("select * from \"VehicleModelView\" vmv", connection))

                //using (var command = new NpgsqlCommand("select vk.\"Name\" as \"MakeName\" ,vm.\"Name\"  as \"ModelName\", o.\"FirstName\" ,o.\"LastName\" ,vmo.\"Price\" , vmo.\"IsActive\"" +
                //    " \r\nfrom \"VehicleModelOwner\" vmo " +
                //    "\r\njoin \"VehicleModel\" vm on vm.\"Id\" = vmo.\"VehicleModel_id\"" +
                //    " \r\njoin \"VehicleMake\" vk on vk.\"Id\" = vm.\"MakeId\"" +
                //    " \r\njoin \"Owner\" o on o.\"Owner_id\" = vmo.\"Owner_id\"; ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleOwner = new VehicleOwnerRabbit()
                            {
                                MakeName = reader.GetString(reader.GetOrdinal("MakeName")),
                                ModelName = reader.GetString(reader.GetOrdinal("ModelName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                IsActice = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            };

                            listAll.Add(vehicleOwner);
                           
                            
                        }
                    }
                }
            }

            GlobalVehicleOwnerList.FetchAndLoadData(listAll);
            

        }

        

    }
}
