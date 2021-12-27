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
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        #region Contructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region Method
        public async virtual Task<ServiceResult> GetAll()
        {
            _serviceResult.Data = await _baseRepository.GetAll();

            return _serviceResult;
        }

        public async Task<ServiceResult> GetById(string entityId)
        {
            _serviceResult.Data = await _baseRepository.GetById(entityId);

            return _serviceResult;
        }

        public async Task<ServiceResult> Insert(TEntity entity)
        {
            // Xử lý nghiệp vụ 
            var isValid = ValidateObject(entity);
            if (isValid)
            {
                isValid = ValidateObjectCustom(entity);
            }

            if(isValid)
            {
                await _baseRepository.Insert(entity);
            }
            else
            {
                _serviceResult.Success = false;
            }

            return _serviceResult;
        }

        public async Task<ServiceResult> Update(string entityId, TEntity entity)
        {
            // Xử lý nghiệp vụ
            // Thực hiện
            await _baseRepository.Update(entityId, entity);

            return _serviceResult;
        }

        public async Task<ServiceResult> Delete(string entityId)
        {
            await _baseRepository.Delete(entityId);

            return _serviceResult;
        }

        /// <summary>
        /// Hàm thực hiện validate dữ liệu - cho phép override
        /// </summary>
        /// <param name="entity">Thông tin entity</param>
        /// <returns>true - hợp lệ; false - không hợp lệ</returns>
        /// CreatedBy : LMDuc (14/11/2021)
        protected bool ValidateObject(TEntity entity)
        {
            // Validate chung
            // 1. Bắt buộc nhập
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                // value of prop
                var propValue = prop.GetValue(entity);
                var propType = prop.PropertyType;
                var requiredAttributes = prop.GetCustomAttributes(typeof(Required), false);

                if (requiredAttributes.Length > 0)
                {
                    if (propValue == null || propType == typeof(String) && String.IsNullOrEmpty(propValue.ToString())) {
                        var attributeMsg = (requiredAttributes[0] as Required).ErrorMsg;
                        attributeMsg = attributeMsg == null ? Resources.FieldEmpty : attributeMsg;

                        _serviceResult.DevMsg = attributeMsg;
                        _serviceResult.UserMsg = attributeMsg;
                        _serviceResult.ErrorCode = "002";
                        _serviceResult.Success = false;

                        return false;
                    }

                }
            }

            return true;
        }

        /// <summary>
        /// Hàm thực hiện validate custom cho các service khách nhau
        /// </summary>
        /// <param name="entity">Dữ liệu Entity</param>
        /// <returns>true - valid, false - invalid</returns>
        /// CreatedBy : LMDuc (14/11/2021)
        protected virtual bool ValidateObjectCustom(TEntity entity)
        {
            return true;
        }
        #endregion
    }
}
