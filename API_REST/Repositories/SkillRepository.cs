using API_REST.Data;
using API_REST.Models;
using API_REST.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;

namespace API_REST.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public SkillRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }

        // Método para adicionar uma nova skill
        public async Task<SkillModel> AddSkill(SkillModel skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill), "O objeto de habilidade não pode ser nulo.");
            }

            var result = await _firebaseClient
                .Child("Skills")
                .PostAsync(skill);

            skill.Id = result.Key;
            return skill;
        }

        // Método para listar todas as habilidades
        public async Task<List<SkillModel>> GetAllSkills()
        {
            var skills = await _firebaseClient
                .Child("Skills")
                .OnceAsync<SkillModel>();

            return skills.Select(s => new SkillModel
            {
                Id = s.Key,
                NameSkill = s.Object.NameSkill,
                DescriptionSkill = s.Object.DescriptionSkill,
                PercentageSkill = s.Object.PercentageSkill,
            }).ToList();
        }

        // Método para listar a habilidade pelo Id
        public async Task<SkillModel> GetSkillById(string id)
        {
            var skill = await _firebaseClient
                .Child("Skills")
                .Child(id)
                .OnceSingleAsync<SkillModel>();

            if (skill == null)
            {
                return null;
            }

            skill.Id = id;
            return skill;
        }

        // Método para atualizar uma skill pelo Id
        public async Task<SkillModel> UpdateSkill(SkillModel skill, string id)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill), "O objeto de habilidade não pode ser nulo.");
            }

            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade de id: {id} não consta no Firebase.");
            }

            skill.Id = id;
            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .PutAsync(skill);

            return skill;
        }

        // Método para deletar uma habilidade 
        public async Task<bool> DeleteSkill(string id)
        {
            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade com o ID: {id} não consta no banco de dados.");
            }

            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .DeleteAsync();

            return true;
        }
    }
}
