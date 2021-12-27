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
    public class GradeService : BaseService<Grade>, IGradeService
    {
        IGradeRepository _gradeRepository;
        public GradeService(IGradeRepository gradeRepository) : base(gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public ServiceResult GetAll()
        {
            _serviceResult.Data = _gradeRepository.GetAll();

            return _serviceResult;
        }
    }
}
