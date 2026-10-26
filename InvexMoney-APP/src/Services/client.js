import axios from "axios";
import { obterToken, limparSessao } from "./sessao";

export const HTTPClient = axios.create({
    baseURL: "http://localhost:5038",
    // headers: {
    //     "Access-Control-Allow-Origin": "*",
    //     "Access-Control-Allow-Headers": "Authorization",
    //     "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
    //     "Content-Type": "application/json;charset-UTF-8",
    // }
})

// Antes de cada requisição ele anexa o token no cabecalho, se existir
HTTPClient.interceptors.request.use((config) => {
    const token = obterToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});


// Depois de cada resposta se a sessão venceu, apaga e volta ao login
HTTPClient.interceptors.response.use(
    (resposta) => resposta,
    (erro) => {
        const url = erro.config?.url?.toLowerCase() ?? "";
        const ehLogin = url.includes("usuario/login");

        if (erro.response?.status === 401 && !ehLogin) {
            limparSessao();
            window.location.href = "/";
        }
        return Promise.reject(erro);
    }
);