using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.CukCuk.Core;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Api.Api
{
    public class ExercisesController : MISAController<Exercise>
    {
        IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService, IBaseService<Exercise> baseService, IBaseRepository<Exercise> baseRepository) :
            base(baseService, baseRepository)
        {
            _exerciseService = exerciseService;
        }
        public override async Task<IActionResult> Create([FromBody] Exercise exercise)
        {
            try
            {
                //Exercise exerciseObj = JsonConvert.DeserializeObject<Exercise>(exercise);
                var serviceResult = await _exerciseService.UpsertAndReturn(exercise);

                if (serviceResult.Success)
                {
                    return StatusCode(201, serviceResult);
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
        /// Thực hiện phân trang và tìm kiếm nhân viên
        /// </summary>
        /// <param name="searchText">Text để tìm kiếm</param>
        /// <param name="pageSize">Số lượng dòng trên 1 trang</param>
        /// <param name="pageNumber">Trang số bao nhiêu</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (19/11/2021)
        [HttpGet("Filter")]
        public async Task<IActionResult> GetPaging(string gradeId, string subjectId, string topicId, bool? exerciseStatus, string searchText, int pageSize, int pageNumber)
        {
            try
            {
                var serviceResult = await _exerciseService.GetExercisePaging(gradeId, subjectId, topicId, exerciseStatus, searchText, pageSize, pageNumber);

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
        /// Thực hiện xóa 1 bài tập theo Id
        /// </summary>
        /// <param name="exerciseId">Id của bài tập</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (16/11/2021)
        [HttpDelete("{exerciseId}")]
        public async override Task<IActionResult> Delete(string exerciseId)
        {
            try
            {
                var serviceResult = await _exerciseService.Delete(exerciseId);
                serviceResult.UserMsg = Resources.DeleteExerciseSuccess;

                if (serviceResult.Success)
                {
                    return StatusCode(200, serviceResult);
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
        /// Thực hiện upload file
        /// </summary>
        /// <param name="file">Dữ liệu file</param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (12/11/2021)
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            try
            {
                //Exercise exerciseObj = JsonConvert.DeserializeObject<Exercise>(exercise);
                var serviceResult = await _exerciseService.UploadFile(file);

                if (serviceResult.Success)
                {
                    return StatusCode(201, serviceResult);
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

    }
}
