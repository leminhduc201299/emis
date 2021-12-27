using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả Entity trong CSDL
        /// </summary>
        /// <returns>Danh sách Entity</returns>
        /// CreatedBy : LMDuc (16/11/2021)
        public Task<List<TEntity>> GetAll();

        /// <summary>
        /// Lấy Entity theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Entity</returns>
        /// CreatedBy : LMDuc (16/11/2021)
        public Task<TEntity?> GetById(string entityId);

        /// <summary>
        /// Thêm 1 entity vào CSDL
        /// </summary>
        /// <param name="entity">Thông tin của entity</param>
        /// <returns>Số dòng thêm mới thành công</returns>
        /// CreatedBy : LMDuc (16/11/2021)
        public Task Insert(TEntity entity);

        /// <summary>
        /// Sửa entity trong CSDL
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <param name="entity">Thông tin của entity</param>
        /// <returns>Số dòng sửa thành công</returns>
        /// CreatedBy : LMDuc (16/11/2021)
        public Task Update(string entityId, TEntity entity);

        /// <summary>
        /// Xóa Entity theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Số dòng xóa thành công</returns>
        /// CreatedBy : LMDuc (17/11/2021)
        public Task Delete(string entityId);
    }
}
