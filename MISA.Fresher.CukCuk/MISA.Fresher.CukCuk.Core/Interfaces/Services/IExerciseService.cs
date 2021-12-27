using Microsoft.AspNetCore.Http;
using MISA.Fresher.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Services
{
    public interface IExerciseService : IBaseService<Exercise>
    {
        public Task<ServiceResult> GetExercisePaging(string gradeId, string subjectId, string topicId, bool? exerciseStatus, string searchText, int pageSize, int pageNumber);
        public Task<ServiceResult> UpsertAndReturn(Exercise exercise);
        public Task<ServiceResult> UploadFile(IFormFile file);
    }
}
