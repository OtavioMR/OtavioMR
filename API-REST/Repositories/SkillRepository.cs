using API_REST.Models;
using API_REST.Repositories.Interfaces;
using API_REST.Repositories;

namespace API_REST.Repositories
{
    public class SkillRepository

    {
        private readonly FirebaseClient _firebaseClient;

        public SkillRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }





        //Adiconar uma nova skill
        public async Task<SkillModel> AddSkill(SkillModel skill)
        {
            var result = await _firebaseClient
                .Child("Skills")
                .PostAsync(skill);

            skill.Id = result.Key; //Armazena o ID gerado automaticamente pelo Firebase
            return skill;
        }

        //Método para listar todas as skills
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



        //Método para obter uma skill por ID
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

            skill.Id = id; //Garante que o ID é mantido no retorno
            return skill;
        }



        //Método para atualizar uma skill existente 
        public async Task<SkillModel> UpdateSkill(SkillModel skill, string id)
        {
            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade com o ID: {id} não foi encontrada no Firebase.")
            }

            skill.Id = id;
            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .PutAsync(skill);

            return skill;
        }



        //Método para deletar uma skiil 
        public async Task<bool> DeleteSkill (string id)
        {
            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade com o ID: {id} não foi encontrada no Firebase.");
            }

            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .DeleteAsync();

            return true;
        }





    }
}
