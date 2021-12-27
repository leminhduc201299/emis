using Microsoft.Extensions.Options;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Infrastructure.Repository
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(IOptions<MISAEmisDatabaseSettings> misaEmisDatabaseSettings) : base(misaEmisDatabaseSettings)
        {

        }

        public async Task<List<Topic>> filterTopic(string gradeId, string subjectId)
        {
            return await _entityCollection.Find(entity =>
                    (String.IsNullOrEmpty(gradeId) || gradeId == entity.GradeId) &&
                    (String.IsNullOrEmpty(subjectId) || subjectId == entity.SubjectId))
                .Limit(50)
                .ToListAsync();
        }
    }
}
