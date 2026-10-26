
const CHAVE_TOKEN = "token";
const CHAVE_USUARIO = "usuario";


// guarda o token e salva o nome e email do usuario como JSON
export function salvarSessao({ token, nome, email }) {
    localStorage.setItem(CHAVE_TOKEN, token)
    localStorage.setItem(CHAVE_USUARIO, JSON.stringify({ nome, email }));
}

export function obterToken() {
    return localStorage.getItem(CHAVE_TOKEN);
}

// recupera e converte os dados do usuario para objeto
export function obterUsuario() {
    try {
        return JSON.parse(localStorage.getItem(CHAVE_USUARIO));
    } catch {
        return null;
    }
}


export function limparSessao() {
    localStorage.removeItem(CHAVE_TOKEN);
    localStorage.removeItem(CHAVE_USUARIO);
}

export function tokenValido() {
    const token = obterToken();
    if (!token)
        return false

    try {
        const partesDoToken = token.split(".");
        const conteudoCodificado = partesDoToken[1]; //pega o conteudo -  [cabecalho, conteudo, assinatura]
        const conteudoBase64 = conteudoCodificado.replace(/-/g, "+").replace(/_/g, "/");

        // recebe o base64 o atob decodifica e dps transforma em objeto 
        const dadosDoToken = JSON.parse(atob(conteudoBase64));
        const expiracao = dadosDoToken.exp; // consulta expiração do token 

        // senão ter data de expiração ele não é valido
        if (!expiracao) {
            return false;
        }

        const expiracaoEmMilissegundos = expiracao * 1000;
        return expiracaoEmMilissegundos > Date.now(); // Retorna true se o horário de expiração ainda estiver no futuro; se já passou, retorna false.
    } catch {
        return false;
    }
}

