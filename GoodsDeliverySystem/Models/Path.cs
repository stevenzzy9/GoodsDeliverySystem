using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class Path
    {
        public List<Road> roads { get; private set; }

        public Path(List<Road> roads)
        {
            this.roads = roads;
        }

        /*
         * Pre:
         * Post: Determines whether the path contains the input city
         */
        public bool hasCity(string city)
        {
            bool has = false;

            foreach (Road road in roads)
            {
                if (road.city.Equals(city))
                {
                    has = true;
                    break;
                }
            }

            return has;
        }

        /*
         * Pre:
         * Post: Determines whether all of the input cities are in the path
         */
        /*public bool containsAllCities(List<string> cities)
        {
            bool containsAll = true;

            foreach (string city in cities)
            {
                bool cityFound = false;

                //search for city
                foreach (Road road in roads)
                {
                    if (road.city.Equals(city))
                        cityFound = true;
                }

                //if not found, not all were found, so break
                if (!cityFound)
                {
                    containsAll = false;
                    break;
                }
            }

            return containsAll;
        }*/

        /*
         * Pre:
         * Post: Determines whether or not a single city appears in the
         *       current path more than 2 times
         */
        /*public bool hasACityMoreThanTwice(List<string> cities)
        {
            bool moreThanTwice = false;

            foreach (string city in cities)
            {
                int count = 0;

                foreach (Road road in roads)
                {
                    if (road.city1.Equals(city) || road.city2.Equals(city))
                        count++;

                    if (count > 4) //4 instead of 2 because the city will appear in 2 roads to show a connection
                    {
                        moreThanTwice = true;
                        break;
                    }

                    if (count > 4) break;
                }
            }

            return moreThanTwice;
        }*/

        /*
         * Pre:
         * Post: Computes the travel time for the current path
         */
        public long time()
        {
            long time = 0;

            foreach (Road road in roads)
                time += road.time;

            return time;
        }
    }
}