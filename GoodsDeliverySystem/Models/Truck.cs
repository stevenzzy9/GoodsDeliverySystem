using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Truck
    {
        public int id { get; private set; }
        public string licensePlateNum { get; set; }
        public string licensePlateState { get; set; }
        public int modelId { get; set; }
        public double fuelTankSize { get; set; }
        public double odometerReading { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double maxWeight { get; set; }
        public string remark { get; set; }
        public UtilityClass.TruckStatus status { get; set; }
        public UtilityClass.ShippingSpeed speed { get; set; }
        public List<Shipment> shipments { get; private set; }
        public int pathId { get; set; }
        public bool localTruck { get; set; }

        //new truck
        public Truck(string licensePlateNum, string licensePlateState, int modelId, double fuelTankSize, double odometerReading, 
                     double length, double width, double height, double maxWeight, string remark, bool localTruck)
        {
            this.licensePlateNum = licensePlateNum;
            this.licensePlateState = licensePlateState;
            this.modelId = modelId;
            this.fuelTankSize = fuelTankSize;
            this.odometerReading = odometerReading;
            this.length = length;
            this.width = width;
            this.height = height;
            this.maxWeight = maxWeight;
            this.remark = remark;
            this.status = UtilityClass.TruckStatus.Idle;
            this.localTruck = localTruck;
        }

        //existing truck
        public Truck(int id, string licensePlateNum, string licensePlateState, int modelId, double fuelTankSize, double odometerReading,
                     double length, double width, double height, double maxWeight, string remark, bool localTruck)
        {
            this.id = id;
            this.licensePlateNum = licensePlateNum;
            this.licensePlateState = licensePlateState;
            this.modelId = modelId;
            this.fuelTankSize = fuelTankSize;
            this.odometerReading = odometerReading;
            this.length = length;
            this.width = width;
            this.height = height;
            this.maxWeight = maxWeight;
            this.remark = remark;
            this.status = UtilityClass.TruckStatus.Idle;
            this.localTruck = localTruck;
        }


        /*
         * Pre:  There may not be an existing truck with the same license plate
         * Post: The new truck is added to the database
         * @returns true if the truck is successfully added and false otherwise
         */
        public bool AddTruckToDatabase()
        {
            return DbInterfaceTruck.AddNewTruck(licensePlateNum, licensePlateState, length, width, height, 
                                                maxWeight, modelId, fuelTankSize, odometerReading, remark, localTruck);
        }

        /*
         * Pre:
         * Post: Determines whether or not the truck has room for a shipment of
         *       the input volume and weight
         * @param volume is the volume of the new shipment
         * @param weight is the weight of the new shipment
         */
        public bool TruckHasRoom(double volume, double weight)
        {
            bool hasRoom = true;
            double totalVolume = 0.0, totalWeight = 0.0;

            //load shipments if they haven't already been loaded
            if (shipments == null || shipments.Count == 0)
                shipments = DbInterfaceTruck.GetTruckShipments(id);

            //find total volume and weight of shipments
            foreach (Shipment shipment in shipments)
            {
                totalVolume += (shipment.length * shipment.width * shipment.height);
                totalWeight += shipment.weight;
            }

            //return false if the truck is out of room
            if (totalVolume + volume > (length * width * height) || totalWeight + weight > maxWeight)
                hasRoom = false;

            return hasRoom;
        }

        /*
         * Pre:
         * Post: Determines whether the truck will be passing through the input city
         * @paths is the list of paths in the map
         * @city is the city name
         */
        public bool GoingToLocation(Paths paths, string city)
        {
            bool passingThrough = false;
            Path path = paths.paths[pathId - 1];

            //see if the input city is in the current path
            int i = 0;
            while (!passingThrough && i < path.roads.Count)
            {
                if (path.roads[i].city.Equals(city))
                    passingThrough = true;

                i++;
            }

            return passingThrough;
        }

        /*
         * Pre: 
         * Post: The truck's route is altered if the new city can be accomodated
         * @returnst rue if the route was altered and false otherwise
         */
        public bool AlterRoute(Paths paths, string city)
        {
            bool altered = false;
            List<int> pathsToSwitchTo = new List<int>(); //track paths that can be switched to
            Path currentPath = paths.paths[pathId - 1];

            //look for paths that have all current cities as well as the new city
            for (int i = 0; i < paths.paths.Length; i++)
            {
                bool hasAllCities = true;

                foreach (Road road in currentPath.roads)
                {
                    if (!paths.paths[i].hasCity(road.city))
                        hasAllCities = false;
                }

                //if all cities were found, add to list of possible paths
                if (hasAllCities && paths.paths[i].hasCity(city))
                    pathsToSwitchTo.Add(i + 1);
            }

            //if there is one available path, switch to it
            if (pathsToSwitchTo.Count == 1)
            {
                pathId = pathsToSwitchTo.ElementAt(0);
                altered = true;
            }
            //if there are multiple available paths, switch to the shortest one
            else if (pathsToSwitchTo.Count > 1)
            {
                int pathIdWithShortestTime = -1;
                long shortestTime = long.MaxValue;

                for (int i = 0; i < pathsToSwitchTo.Count; i++)
                {
                    long currTime = paths.paths[pathsToSwitchTo[i] - 1].time();

                    if (currTime < shortestTime)
                    {
                        shortestTime = currTime;
                        pathIdWithShortestTime = pathsToSwitchTo[i];
                    }

                }

                pathId = pathIdWithShortestTime;
                altered = true;
            }
           
            return altered;
        }
    }
}