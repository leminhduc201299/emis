using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Entities
{
    public class Exercise
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        /// <summary>
        /// Id bài tập
        /// </summary>
        public string? ExerciseId { get; set; }

        /// <summary>
        /// Tên bài tập
        /// </summary>
        public string ExerciseName { get; set; }

        /// <summary>
        /// Id môn học
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? SubjectId { get; set; }

        /// <summary>
        /// Id khối lớp
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? GradeId { get; set; }

        /// <summary>
        /// Id chủ đề
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? TopicId { get; set; }

        /// <summary>
        /// Thẻ tìm kiếm
        /// </summary>
        public String SearchTagNames { get; set; }

        /// <summary>
        /// Url của ảnh bài tập
        /// </summary>
        public string ExerciseImg { get; set; }

        /// <summary>
        /// Trạng thái của bài tập
        /// </summary>
        public bool ExerciseStatus { get; set; }

        public List<Question> Questions { get; set; }

        /// <summary>
        /// Danh sách câu hỏi
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
