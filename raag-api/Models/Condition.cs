using System.ComponentModel.DataAnnotations;

namespace raag_api.Models
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string ImpactedCondition { get; set; }
        public int? Year { get; set; }
        public string CurrentCondition { get; set; }
        public string TargetCond2027 { get; set; }
        public string TargetCondLongTerm { get; set; }
    }
}
