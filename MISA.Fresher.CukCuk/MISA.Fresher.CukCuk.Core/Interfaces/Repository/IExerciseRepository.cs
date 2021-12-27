using MISA.Fresher.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Repository
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        /// <summary>
        /// Thực hiện lấy và phân trang exercise
        /// </summary>
        /// <param name="gradeId"></param>
        /// <param name="subjectId"></param>
        /// <param name="topicId"></param>
        /// <param name="exerciseStatus"></param>
        /// <param name="searchText">Đoạn text để tì kiếm</param>
        /// <param name="pageSize">Số dòng trên 1 trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <returns>Danh sách teacher đã phân trang</returns>
        /// CreatedBy : LMDuc (22/12/2021)
        public Task<Object> GetExercisePaging(string gradeId, string subjectId, string topicId, bool? exerciseStatus, string searchText, int pageSize, int pageNumber);

        /// <summary>
        /// Thêm 1 Exercise vào CSDL
        /// </summary>
        /// <param name="exercise">Thông tin của exercise</param>
        /// <returns>Exercise thêm mới thành công</returns>
        /// CreatedBy : LMDuc (16/11/2021)
        public Task<Exercise> UpsertAndReturn(Exercise exercise);
    }
}
