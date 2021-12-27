using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Api.Api
{
    public class GradesController : MISAController<Grade>
    {
        IGradeService _gradeService;

        public GradesController(IBaseService<Grade> baseService, IBaseRepository<Grade> baseRepository) :
            base(baseService, baseRepository)
        {
        }

    }
}
