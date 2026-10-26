import { useNavigate, Link } from "react-router-dom";
import { Form } from "react-bootstrap";
import { useState } from "react";
import { IoIosEye, IoIosEyeOff } from "react-icons/io";
import { useAlert } from "../../componentes/Alert/AlertContext";
import LoginAPI from "../../Services/loginAPI";
import style from "./Login.module.css";
import { salvarSessao } from "../../Services/sessao";


export function Login() { //declarando o componente
    const { mostrarAlerta } = useAlert();

    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [verSenha, setVerSenha] = useState('password');

    const navigate = useNavigate();

    const isFormValid = () => {
        return email.trim().length > 0 && senha.length >= 8;
    };


    function handleSenha() {
        setVerSenha(verSenha === "password" ? "text" : "password");
    }


    // É chamada quando o formulário é enviado
    //Valida, pergunta ao servidor e conforme a resposta, ou leva para a home ou avisa que deu errado 
    async function handleLogin(e) {
        e.preventDefault(); //Impede que a pagina seja recarregada ao enviar 

        if (!isFormValid()) {
            mostrarAlerta("Insira um e-mail válido e uma senha com no minimo 8 caracteres", "warning");
            return;
        }

        try {
            const response = await LoginAPI.loginAsync(email, senha);
            const { token, nome, email: emailUsuario } = response.data; 


            if (!token) {
                mostrarAlerta("Resposta inválida do servidor", "danger");
                return;
            }

            salvarSessao({ token, nome, email: emailUsuario });

            //Responsavel por guardar os dados do usuário no local storage, exibir o alerta de boas vindas e redirecionar para a rota home 
            if (response?.status === 200) {
                mostrarAlerta(`Bem-Vindo, ${response.data.nome}!`, "success", () => {
                    navigate("/home");
                });
            }
        } catch (error) {
            if (error.response?.status === 400) {
                mostrarAlerta("Login inválido", "danger");
                setEmail("");
                setSenha("");
            } else if (error.response?.status === 401) {
                mostrarAlerta("Senha incorreta, por favor tente novamente", "danger")
                setSenha("");
            }
            else {
                mostrarAlerta("Erro ao fazer login", "danger");
            }
        }
    }



    return (
        <div className={style.login_conteudo}>
            <div className={style.login_formulario}>
                <h1 className={style.titulo}>Acesse sua Carteira</h1>
                <Form onSubmit={handleLogin}>
                    <Form.Group controlId="formEmail" className={style.grupo_campo}>
                        <Form.Label>E-mail</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder="Digite seu email"
                            name="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            maxLength={100}
                            required
                        />
                    </Form.Group>
                    <Form.Group controlId="formSenha" className={style.grupo_campo}>
                        <Form.Label>Senha</Form.Label>
                        <div className={style.campo_senha}>
                            <Form.Control
                                type={verSenha}
                                placeholder="******"
                                name="senha"
                                value={senha}
                                onChange={(e) => setSenha(e.target.value)}
                                maxLength={50}
                                required
                            />
                            <button type="button" onClick={handleSenha} className={style.botao_verSenha}>
                                {verSenha === 'password' ? <IoIosEyeOff /> : <IoIosEye />}
                            </button>
                        </div>
                    </Form.Group>
                    <button type="submit" className={style.botao_novo}>Entrar</button>

                    <p className={style.texto_cadastro}>
                        Não tem conta? {" "}
                        <Link to="/cadastro" className={style.Link_cadastro}>Cadastre-se</Link>
                    </p>
                </Form>
            </div>
        </div>
    )

}