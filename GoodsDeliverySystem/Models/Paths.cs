using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodsDeliverySystem.DatabaseAccess;

namespace GoodsDeliverySystem.Models
{
    public class Paths
    {
        public Path[] paths { get; private set; }

        public Paths()
        {
            paths = DbInterfaceShipment.GetPaths();
        }

        /*
         * Pre:
         * Post: returns the id of the shortest path containing all 
         *       of the input cities
         */
        public int GetIdOfShortestPath(List<string> cities)
        {
            int pathId = -1;
            long shortestTime = Int32.MaxValue;

            for (int i = 0; i < paths.Length; i++)
            {
                bool hasAllCities = true;

                //make sure the path has all cities
                foreach (string city in cities)
                {
                    if (!paths[i].hasCity(city))
                        hasAllCities = false;
                }

                //save the path id if the path has all cities and is the shortest
                if (hasAllCities && paths[i].time() < shortestTime)
                {
                    shortestTime = paths[i].time();
                    pathId = i + 1; //path id is array index + 1
                }
            }
            

            return pathId;
        }

        /*public Path GetShortestPath(List<string> cities)
        {
            Path shortestPath = null, currPath;
            HashSet<Path> paths = GetAllPaths(cities);
            HashSet<Path> removalSet = new HashSet<Path>();

            //copy all paths to removal set
            foreach (Path path in paths)
                removalSet.Add(path);

            //find shortest path
            while (removalSet.Count > 0)
            {
                currPath = removalSet.ElementAt(0);
                removalSet.Remove(currPath);

                if (shortestPath == null || shortestPath.time() > currPath.time())
                    shortestPath = currPath;
            }

            return shortestPath;
        }*/

        /*
         * Pre:  All of the input paths must exist in the current list of paths
         * Post: The shortest route between the input paths is returned
         * @cities is the list of cities that must be passed through
         */
        /*private HashSet<Path> GetAllPaths(List<string> cities)
        {
            Queue<Path> partialPaths = new Queue<Path>();
            List<Path> tempPaths = getStartingPaths(UtilityClass.START_CITY);
            List<Road> connectingRoads, tempRoads;
            HashSet<Path> allPaths = new HashSet<Path>();
            Path currPath, tempPath;
            string endingCity;

            //put all paths going between all cities in the allPaths set, otherwise put in list of partial paths
            foreach (Path path in tempPaths)
            {
                if (path.containsAllCities(cities))
                    allPaths.Add(path);
                else
                    partialPaths.Enqueue(path);
            }

            //find all paths using the initial partial paths to build off of
            while (partialPaths.Count > 0)
            {
                currPath = partialPaths.Dequeue();
                endingCity = getEndingCity(currPath, UtilityClass.START_CITY);
                connectingRoads = getCityNeighbors(endingCity);

                foreach (Road currRoad in connectingRoads)
                {
                    //copy list of roads and add current one
                    tempRoads = new List<Road>();
                    foreach (Road rd in currPath.roads)
                        tempRoads.Add(rd);
                    tempRoads.Add(currRoad);
                    tempPath = new Path(tempRoads);

                    //if the current road would reach all needed cities, add to set of paths
                    if (tempPath.containsAllCities(cities))
                    {
                        allPaths.Add(tempPath);
                    }
                    //if adding the road would make the path go through the same city more than twice, ignore it
                    //otherwise add the new path to the queue
                    else
                    {
                        tempRoads = new List<Road>();
                        foreach (Road rd in currPath.roads)
                            tempRoads.Add(rd);
                        tempRoads.Add(currRoad);

                        tempPath = new Path(tempRoads);

                        if (!tempPath.hasACityMoreThanTwice(cities))
                        {
                            partialPaths.Enqueue(tempPath);
                        }
                        
                    }
                }
            }

            return allPaths;
        }*/

        /*
         * Pre:
         * Post: Helper for retrieving all paths between two cities.  Creates
         *       a list containing Paths connecting the start city to other cities
         */
        /*private List<Path> getStartingPaths(string fromCity)
        {
            List<Path> startingPaths = new List<Path>();
            List<Road> connectingRoads = getCityNeighbors(fromCity);

            foreach (Road r in connectingRoads)
            {
                List<Road> temp = new List<Road>();
                temp.Add(r);

                Path path = new Path(temp);
                startingPaths.Add(path);
            }

            return startingPaths;
        }*/

        /*
         * Pre:
         * Post: Retrieves a list of all paths connecting the input city  to another city
         */
        /*private List<Road> getCityNeighbors(string fromCity)
        {
            List<Road> neighbors = new List<Road>();
            HashSet<Road> removalSet = new HashSet<Road>();
            Road path, temp;
            int idx;

            //add paths to temporary set
            foreach (Road p in paths)
            {
                temp = new Road(p.city1, p.city2, p.time);
                removalSet.Add(temp);
            }

            while (removalSet.Count > 0)
            {
                path = removalSet.ElementAt(0);
                removalSet.Remove(path);

                if (path.city1.Equals(fromCity) || path.city2.Equals(fromCity))
                    neighbors.Add(path);
            }

            return neighbors;
        }*/

        /*
         * Pre:
         * Post: Retrieves the city that is at the end of the input path
         */
       /* private string getEndingCity(Path path, string fromCity)
        {
            string endingCity;

            //if there is only one road, the ending city is the one attached to the input city
            if (path.roads.Count == 1)
            {
                if (path.roads[0].city1.Equals(fromCity))
                    endingCity = path.roads[0].city2;
                else
                    endingCity = path.roads[0].city1;
            }
            //get the city in the last road of the path that is not in the previous road
            else
            {
                Road lastRoad = path.roads[path.roads.Count - 1];
                Road secondToLastRoad = path.roads[path.roads.Count - 2];

                if (lastRoad.city1.Equals(secondToLastRoad.city1) || lastRoad.city1.Equals(secondToLastRoad.city2))
                    endingCity = lastRoad.city2;
                else
                    endingCity = lastRoad.city1;
            }

            return endingCity;
        }*/
    }
}