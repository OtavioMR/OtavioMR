document.addEventListener('DOMContentLoaded', function () {
    // Função para carregar as habilidades
    async function loadskills() {
        try {
            console.log('Tentando carregar habilidades...');
            const response = await axios.get('https://localhost:44313/api/Skill');
            console.log(response.data); // Verifique a estrutura aqui

            // Se response.data for o array diretamente
            const skills = response.data; // Ajuste aqui se necessário

            // Verifica se skills existe e se não está vazio
            if (!Array.isArray(skills) || skills.length === 0) {
                throw new Error('Nenhuma habilidade encontrada');
            }

            console.log('Habilidades carregadas:', skills);

            const skillsContainer = document.querySelector('.skills-progress');
            skillsContainer.innerHTML = ''; // Limpa o conteúdo existente

            skills.forEach(skill => {
                const skillElement = document.createElement('div');
                skillElement.classList.add('progress');

                skillElement.innerHTML = `
                    <span class="skill">${skill.nameSkill} <i class="val">${skill.percentageSkill || 0}%</i></span>
                    <div class="progress-bar-wrap">
                        <div class="progress-bar" role="progressbar" aria-valuenow="${skill.percentageSkill || 0}" aria-valuemin="0" aria-valuemax="100" style="width: ${skill.percentageSkill || 0}%;"></div>
                    </div>
                `;
                skillsContainer.appendChild(skillElement);
            });
        } catch (error) {
            console.error('Erro ao carregar habilidades:', error);
        }
    }

    // Chama a função para carregar as habilidades
    loadskills();
});