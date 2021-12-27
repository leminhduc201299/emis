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
    public class TopicsController : MISAController<Topic>
    {
        ITopicService _topicService;

        public TopicsController(ITopicService topicService, IBaseService<Topic> baseService, IBaseRepository<Topic> baseRepository) :
            base(baseService, baseRepository)
        {
            _topicService = topicService;
        }

        /// <summary>
        /// Thực hiện tìm kiếm topic
        /// </summary>
        /// <param name="gradeId"></param>
        /// <param name="subjectId"></param>
        /// <returns>
        /// 200: 'The server successfully returned the requested data.',
        /// 400: 'The request was made with an error and the server did not perform any new or modified data operations.',
        /// 404: 'The request is made for a record that does not exist and the server does not operate.',
        /// 500: 'An error occurred on the server. Please check the server.',
        /// </returns>
        /// CreatedBy: LMDuc (24/12/2021)
        [HttpGet("Filter")]
        public async Task<IActionResult> filterTopic(string gradeId, string subjectId)
        {
            try
            {
                var serviceResult = await _topicService.filterTopic(gradeId, subjectId);

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

    }
}
