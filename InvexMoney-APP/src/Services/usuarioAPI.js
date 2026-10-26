import { HTTPClient } from "./Client";

const UsuarioAPI = {
    async cadastrarAsync(nome, email, senha) {
        try {
            const usuarioCadastrar = {
                Nome: nome,
                Email: email,
                Senha: senha
            };
            const response = await HTTPClient.post(`usuario/Cadastro`, usuarioCadastrar);
            return response.data;
        } catch (error) {
            console.error("Erro ao cadastrar Usuário");
            throw error;
        }
    }
}
export default UsuarioAPI;