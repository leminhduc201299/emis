using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Infrastructure.Repository
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(IOptions<MISAEmisDatabaseSettings> misaEmisDatabaseSettings) : base(misaEmisDatabaseSettings)
        {
        }

        public List<Grade> GetAll()
        {
            // Lấy dữ liệu từ database
            var res = new List<Grade>();

            return res.ToList();
        }
    }
}
