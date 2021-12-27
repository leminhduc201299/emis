using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MISA.Fresher.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Services
{
    public class ExerciseService : BaseService<Exercise>, IExerciseService
    {
        IHostingEnvironment _environment;
        IExerciseRepository _exerciseRepository;

        public ExerciseService(IHostingEnvironment environment, IBaseRepository<Exercise> baseRepository, IExerciseRepository exerciseRepository) : base(baseRepository)
        {
            _environment = environment;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<ServiceResult> GetExercisePaging(string gradeId, string subjectId, string topicId, bool? exerciseStatus, string searchText, int pageSize, int pageNumber)
        {
            // Xử lý nghiệp vụ
            if (pageSize <= 0 || pageNumber <= 0)
            {
                _serviceResult.DevMsg = Resources.ParamInvalid;
                _serviceResult.UserMsg = Resources.ParamInvalid;
                _serviceResult.ErrorCode = "006";
                _serviceResult.MoreInfo = "http://google.com";
                _serviceResult.Success = false;
            }
            else
            {
                if (searchText == null)
                {
                    searchText = "";
                }

                // Thực hiện
                _serviceResult.Data = await _exerciseRepository.GetExercisePaging(gradeId, subjectId, topicId, exerciseStatus, searchText, pageSize, pageNumber);
            }

            return _serviceResult;
        }

        public async Task<ServiceResult> UpsertAndReturn(Exercise exercise)
        {
            if (String.IsNullOrEmpty(exercise.TopicId))
            {
                exercise.TopicId = null;
            }

            _serviceResult.Data = await _exerciseRepository.UpsertAndReturn(exercise);

            // Chuyển ảnh từ thư mục Temp sang Upload
            if (!Directory.Exists(_environment.ContentRootPath + "\\wwwroot\\Upload\\"))
            {
                Directory.CreateDirectory(_environment.ContentRootPath + "\\wwwroot\\Upload\\");
            }
            

            if (!String.IsNullOrEmpty(exercise.ExerciseImg))
            {
                MoveFileFormTempToUpload(exercise.ExerciseImg);
            }

            if (exercise.Questions != null && exercise.Questions.Count > 0)
            {
                foreach (var question in exercise.Questions)
                {
                    // Move question files
                    if (question.QuestionFiles != null && question.QuestionFiles.Count > 0)
                    {
                        foreach (var questionFile in question.QuestionFiles)
                        {
                            if (!String.IsNullOrEmpty(questionFile))
                            {
                                MoveFileFormTempToUpload(questionFile);
                            }
                        }
                    }

                    // Move answers file
                    if (question.Answers != null && question.Answers.Count > 0)
                    {
                        foreach (var answer in question.Answers)
                        {
                            var answerImg = answer.AnswerImg;

                            if (!String.IsNullOrEmpty(answerImg))
                            {
                                // Move answers file
                                MoveFileFormTempToUpload(answerImg);
                            }
                        }
                    }
                }
            }

            _serviceResult.UserMsg = Resources.AddExerciseSuccess;

            return _serviceResult;
        }

        public async Task<ServiceResult> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                _serviceResult.Success = false;
                _serviceResult.Data = false;
                _serviceResult.UserMsg = "Upload file thất bại";
            };

            try
            {
                if (!Directory.Exists(_environment.ContentRootPath + "\\wwwroot\\Temp\\"))
                {
                    Directory.CreateDirectory(_environment.ContentRootPath + "\\wwwroot\\Temp\\");
                }

                // Getting FileName
                var fileName = Path.GetFileName(file.FileName);
                // Getting file Extension
                var fileExtension = Path.GetExtension(fileName);

                // Assigning Unique Filename(Guid)
                 var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(myUniqueFileName, fileExtension);


                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Temp\\" + newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _serviceResult.Data = newFileName;

                return _serviceResult;
            }
            catch (Exception ex)
            {
                _serviceResult.Success = false;
                _serviceResult.Data = false;
                _serviceResult.UserMsg = "Upload file thất bại";

                return _serviceResult;
            }
            
        }

        private void MoveFileFormTempToUpload(string fileString)
        {
            string[] fileList = Directory.GetFiles(_environment.ContentRootPath + "\\wwwroot\\Temp\\", fileString);

            if (fileList.Length > 0)
            {
                string fileName = Path.GetFileName(fileList[0]);

                string fileToMove = Path.Combine(_environment.ContentRootPath, "wwwroot\\Temp\\" + fileName);
                string moveTo = Path.Combine(_environment.ContentRootPath, "wwwroot\\Upload\\" + fileName);

                //moving file
                File.Move(fileToMove, moveTo);
            }
        }
    }
}
