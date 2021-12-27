using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Services
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
        public Task<List<Topic>> filterTopic(string gradeId, string subjectId);
    }
}
