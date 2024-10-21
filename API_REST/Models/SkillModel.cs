namespace API_REST.Models
{
    public class SkillModel
    {
        public string Id { get; set; }

        public string NameSkill { get; set; }

        public string DescriptionSkill { get; set; }

        public int PercentageSkill { get; set; }

        public SkillModel() { }

        public SkillModel(string nameSkill, string descriptionSkill, int percentageSkill)
        {
            NameSkill = nameSkill;
            DescriptionSkill = descriptionSkill;
            PercentageSkill = percentageSkill;
        }
    }
}
