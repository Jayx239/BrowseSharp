using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharpTest.Models
{
    class RandomObject
    {
        public RandomObject()
        {

        }

        public RandomObject(string firstName, byte data, List<int> randomIntegerList)
        {
            FirstName = firstName;
            Data = data;
            RandomInts = randomIntegerList;
        }

        string FirstName { get; set; }
        byte Data { get; set; }
        List<int> RandomInts { get; set; }
    }
}
