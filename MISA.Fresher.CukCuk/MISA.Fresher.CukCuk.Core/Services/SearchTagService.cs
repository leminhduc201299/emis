using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Services
{
    public class SearchTagService : BaseService<SearchTag>, ISearchTagService
    {
        ISearchTagRepository _searchTagRepository;

        public SearchTagService(IBaseRepository<SearchTag> baseRepository, ISearchTagRepository searchTagRepository) : base(baseRepository)
        {
            _searchTagRepository = searchTagRepository;
        }

        public async Task<ServiceResult> filterSearchTag(string gradeId, string subjectId)
        {
            // Thực hiện
            _serviceResult.Data = await _searchTagRepository.filterSearchTag(gradeId, subjectId);

            return _serviceResult;
        }
    }
}
