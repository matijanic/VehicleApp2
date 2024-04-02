using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VehicleAppConsl2.Models;
using VehicleAppConsl2.Repository;
using VehicleAppConsl2.Services;

namespace VehicleAppConsl2.Util
{
    public static class Receive
    {
        private static readonly VehicleOwnerService _vehicleOwnerService = new VehicleOwnerService();
        private static readonly VehicleOwnerRepository _vehicleOwnerRespos = new VehicleOwnerRepository();


        public static async Task ListenToUpdate()
        {
            while (true)
            {
                try
                {
                    var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();
                    channel.QueueDeclare(queue: "vehicle",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        
                        var newObject = JsonSerializer.Deserialize<VehicleContainer>(message);

                        var existingModel = GlobalVehicleOwnerList.VehicleOwnerAll.FirstOrDefault(v => v.FirstName.Equals(newObject.vehicleModelOwner.FirstName, StringComparison.OrdinalIgnoreCase)
                        && v.LastName.Equals(newObject.vehicleModelOwner.LastName, StringComparison.OrdinalIgnoreCase));


                        switch (newObject.actionType)
                        {

                            case ActionType.Insert:
                                GlobalVehicleOwnerList.VehicleOwnerAll.Add(newObject.vehicleModelOwner);
                                break;

                            case ActionType.Delete:
                                
                                    GlobalVehicleOwnerList.VehicleOwnerAll.Remove(existingModel);
                                break;

                            case ActionType.Update:

                                existingModel.Price = newObject.vehicleModelOwner.Price;
                                existingModel.IsActice = newObject.vehicleModelOwner.IsActice;
                                
                                break;
                        }

                        

                        
                    };

                    channel.BasicConsume(queue: "vehicle",
                                         autoAck: true,
                                         consumer: consumer);

                    
                    await Task.Delay(Timeout.Infinite);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Trying to reconnect...");
                    await Task.Delay(5000); // Čekaj 5 sekundi prije ponovnog pokušaja
                }
            }
        }
    }
}
