using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Core.Entities
{
    public class Question
    {
        public int QuestionType { get; set; }
        public string QuestionContent { get; set; }
        public List<String> QuestionFiles { get; set; }
        public List<Answer> Answers { get; set; }
        public string SolutionContent { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
