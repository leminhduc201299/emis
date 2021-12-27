using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.CukCuk.Core;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Api.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISAController<TEntity> : ControllerBase
    {
        #region Field
        IBaseService<TEntity> _baseService;
        IBaseRepository<TEntity> _baseRepository;
        private ServiceResult service = new ServiceResult();
        #endregion

        #region Contructor
        public MISAController(IBaseService<TEntity> baseService, IBaseRepository<TEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Thực hiện lấy toàn bộ entity
        /// </summary>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (16/11/2021)
        [HttpGet]
        public async virtual Task<IActionResult> Get()
        {
            try
            {
                var serviceResult = await _baseService.GetAll();

                if (serviceResult.Success)
                {
                    return Ok(serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                return handleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện lấy entity theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (16/11/2021)
        [HttpGet("{entityId}")]
        public async virtual Task<IActionResult> GetById(string entityId)
        {
            try
            {
                var serviceResult = await _baseService.GetById(entityId);

                if (serviceResult.Success)
                {
                    return Ok(serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                return handleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện thêm mới entity
        /// </summary>
        /// <param name="entity">Dữ liệu entity</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (12/11/2021)
        [HttpPost]
        public async virtual Task<IActionResult> Create([FromBody] TEntity entity)
        {
            try
            {
                var serviceResult = await _baseService.Insert(entity);

                if (serviceResult.Success)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                return handleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện sửa 1 entity trong csdl
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <param name="entity">Dữ liệu của entity</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (16/11/2021)
        [HttpPut("{entityId}")]
        public async virtual Task<IActionResult> Update([FromRoute] string entityId, [FromBody] TEntity entity)
        {
            try
            {
                var serviceResult = await _baseService.Update(entityId, entity);

                if (serviceResult.Success)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                return handleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện xóa 1 entity theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (16/11/2021)
        [HttpDelete("{entityId}")]
        public async virtual Task<IActionResult> Delete(string entityId)
        {
            try
            {
                var serviceResult = await _baseService.Delete(entityId);

                if (serviceResult.Success)
                {
                    return Ok(serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                return handleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện xử lý khi có exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// <returns>ObjectResult</returns>
        /// CreatedBy: LMDuc (16/11/2021)
        protected ObjectResult handleException(Exception ex, string errorCode = null)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Success = false;
            serviceResult.DevMsg = ex.Message;
            serviceResult.UserMsg = Resources.ServiceError;
            serviceResult.ErrorCode = "001";
            serviceResult.MoreInfo = "http://google.com";

            return StatusCode(500, serviceResult);
        }
        #endregion
    }
}
