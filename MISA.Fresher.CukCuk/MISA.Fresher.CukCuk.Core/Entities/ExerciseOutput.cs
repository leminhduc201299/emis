using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Entities
{
    public class ExerciseOutput : Exercise
    {
        public Grade[] Grade { get; set; }

        public Subject[] Subject { get; set; }
    }
}
