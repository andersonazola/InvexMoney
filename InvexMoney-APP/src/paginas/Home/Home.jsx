import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { HTTPClient } from "../../Services/Client";
import { obterUsuario, limparSessao } from "../../Services/sessao";

export function Home() {
    const navigate = useNavigate();
    const usuario = obterUsuario();
    const [dadosDoToken, setDadosDoToken] = useState(null);

    useEffect(() => {
        HTTPClient.get("usuario/Eu")
            .then((resposta) => setDadosDoToken(resposta.data))
            .catch(() => {});
    }, []);

    function sair() {
        limparSessao();
        navigate("/", { replace: true });
    }

    return (
        <div style={{ padding: 24 }}>
            <h1>Olá, {usuario?.nome}</h1>
            <pre>{JSON.stringify(dadosDoToken)}</pre>
            <button onClick={sair}>Sair</button>
        </div>
    );
}