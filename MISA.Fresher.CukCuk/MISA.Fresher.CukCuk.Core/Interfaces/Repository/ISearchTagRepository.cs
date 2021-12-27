using MISA.Fresher.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Repository
{
    public interface ISearchTagRepository : IBaseRepository<SearchTag>
    {
        public Task<List<SearchTag>> filterSearchTag(string gradeId, string subjectId);
    }
}
