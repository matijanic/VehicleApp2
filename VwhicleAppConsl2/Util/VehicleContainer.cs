using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAppConsl2.Models;

namespace VehicleAppConsl2.Util
{
    public class VehicleContainer
    {

           public VehicleOwnerRabbit vehicleModelOwner { get; set; }

            public ActionType actionType { get; set; }

    }

    public enum ActionType
       
    {
            Insert,
            Update,
            Delete
    }

}

