using API_REST.Models;

namespace API_REST.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<SkillModel>> GetAllSkills();
        Task<SkillModel> GetSkillById(string id);
        Task<SkillModel> AddSkill(SkillModel skill);
        Task<SkillModel> UpdateSkill(SkillModel skill, string id);
        Task<SkillModel> DeleteSkill(string id);
    }
}
