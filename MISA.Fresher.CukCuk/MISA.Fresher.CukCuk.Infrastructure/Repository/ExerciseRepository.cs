using Microsoft.Extensions.Options;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Infrastructure.Repository
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(IOptions<MISAEmisDatabaseSettings> misaEmisDatabaseSettings) : base(misaEmisDatabaseSettings)
        {

        }

        public async Task<Object> GetExercisePaging(string gradeId, string subjectId, string topicId, bool? exerciseStatus, string searchText, int pageSize, int pageNumber)
        {
            var data = await _entityCollection.Aggregate()
                .Match(entity => 
                    (String.IsNullOrEmpty(gradeId) || gradeId == entity.GradeId) && 
                    (String.IsNullOrEmpty(subjectId) || subjectId == entity.SubjectId) &&
                    (String.IsNullOrEmpty(topicId) || topicId == entity.TopicId) &&
                    (exerciseStatus == null || exerciseStatus == entity.ExerciseStatus) &&
                    (String.IsNullOrEmpty(searchText) || entity.SearchTagNames.ToLower().Contains(searchText) || entity.ExerciseName.ToLower().Contains(searchText))
                )
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                //.Sort(Builders<Exercise>.Sort.Ascending("CreatedDate"))
                //.Sort("{CreatedDate: 1}")
                .SortByDescending(entity => entity.CreatedDate)
                .Lookup("Grade", "GradeId", "_id", "Grade")
                .Lookup("Subject", "SubjectId", "_id", "Subject")
                .As<ExerciseOutput>()
                .ToListAsync();

            var totalRecord = await _entityCollection.CountDocumentsAsync(new BsonDocument());

            return new
            {
                Data = data,
                TotalRecord = totalRecord,
            };
        }

        public async Task<Exercise> UpsertAndReturn(Exercise exercise)
        {
            if (String.IsNullOrEmpty(exercise.ExerciseId))
            {
                exercise.ExerciseId = ObjectId.GenerateNewId().ToString();
            }

            if (exercise.CreatedDate == null)
            {
                exercise.CreatedDate = DateTime.Now;
            }

            await _entityCollection.ReplaceOneAsync(entity => entity.ExerciseId == exercise.ExerciseId, exercise, new ReplaceOptions { IsUpsert = true });

            return exercise;
        }
    }
}
