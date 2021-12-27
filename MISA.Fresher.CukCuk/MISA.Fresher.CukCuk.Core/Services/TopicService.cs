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
    public class TopicService : BaseService<Topic>, ITopicService
    {
        ITopicRepository _topicRepository;

        public TopicService(IBaseRepository<Topic> baseRepository, ITopicRepository topicRepository) : base(baseRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<ServiceResult> filterTopic(string gradeId, string subjectId)
        {
            // Thực hiện
            _serviceResult.Data = await _topicRepository.filterTopic(gradeId, subjectId);

            return _serviceResult;
        }
    }
}
