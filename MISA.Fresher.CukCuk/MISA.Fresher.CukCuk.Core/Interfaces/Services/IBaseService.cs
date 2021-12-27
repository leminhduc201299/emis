using MISA.Fresher.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        /// /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: LMDuc (12/11/2021)
        public Task<ServiceResult> GetAll();

        /// <summary>
        /// Lấy Entity theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy : LMDuc (18/11/2021)
        public Task<ServiceResult> GetById(string entityId);

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: LMDuc (12/11/2021)
        public Task<ServiceResult> Insert(TEntity entity);

        /// <summary>
        /// Update Entity theo Id
        /// </summary>
        /// <param name="entityId">Id của Entity</param>
        /// <param name="entity">Dữ liệu Entity muốn update</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy : LMDuc (18/11/2021)
        public Task<ServiceResult> Update(string entityId, TEntity entity);

        /// <summary>
        /// Xóa Entity theo Id
        /// </summary>
        /// <param name="entityId">Id của Entity</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy : LMDuc (18/11/2021)
        public Task<ServiceResult> Delete(string entityId);
    }
}
