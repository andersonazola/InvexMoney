import { useNavigate, Link } from "react-router-dom";
import { Form } from "react-bootstrap";
import { useState } from "react";
import { IoIosEye, IoIosEyeOff } from "react-icons/io";
import { useAlert } from "../../componentes/Alert/AlertContext";
import UsuarioAPI from "../../Services/usuarioAPI";
import style from "./Cadastro.module.css";

export function Cadastro() { // declarando o componente
    const { mostrarAlerta } = useAlert();

    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [confirmarSenha, setConfirmarSenha] = useState('');
    const [verSenha, setVerSenha] = useState('password');

    const navigate = useNavigate();

    const isFormValid = () => {
        return nome.trim().length > 0 && email.length > 0 && senha.length >= 8;
    };

    function handleSenha() {
        setVerSenha(verSenha === "password" ? "text" : "password");
    }


    async function handleCadastro(e) {
        e.preventDefault();

        if (!isFormValid()) {
            mostrarAlerta("Preencha todos os campos e use uma senha com no mínimo 8 caracteres", "warning");
            return;
        }

        if (senha !== confirmarSenha) {
            mostrarAlerta("As senhas não coincidem", "warning");
            return;
        }

        try {
            const response = await UsuarioAPI.cadastrarAsync(nome, email, senha);

            if (response?.status === 201) {
                mostrarAlerta(`Cadastro realizado com sucesso!`, () => {
                    navigate("/");
                })
            }
        } catch (error) {
            if (error.response?.status === 400) {
                mostrarAlerta("Dados inválidos, confira as informações e tente novamente", "danger");
            } else if (error.response?.status === 409) {
                mostrarAlerta("Este e-mail já está cadastrado", "danger");
            } else {
                mostrarAlerta("Erro ao realizar o cadastro", "danger");
            }
        }
    }


    return (
        <div className={style.cadastro_conteudo}>
            <div className={style.cadastro_formulario}>
                <h1 className={style.titulo}>Faça seu cadastro</h1>
                <Form onSubmit={handleCadastro}>
                    <Form.Group controlId="formNome" className={style.grupo_campo}>
                        <Form.Label>Nome</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Digite seu nome"
                            name="nome"
                            value={nome}
                            onChange={(e) => setNome(e.target.value)}
                            maxLength={100}
                            required
                        />
                    </Form.Group>

                    <Form.Group controlId="formEmail" className={style.grupo_campo}>
                        <Form.Label>E-mail</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder="Digite seu e-mail"
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

                    <Form.Group controlId="formConfirmarSenha" className={style.grupo_campo}>
                        <Form.Label>Confirmar senha</Form.Label>
                        <Form.Control
                            type={verSenha}
                            placeholder="******"
                            name="confirmarSenha"
                            value={confirmarSenha}
                            onChange={(e) => setConfirmarSenha(e.target.value)}
                            maxLength={50}
                            required
                        />
                    </Form.Group>

                    <button type="submit" className={style.botao_novo}>Criar conta</button>

                    <p className={style.texto_cadastro}>
                        Já tem conta?{" "}
                        <Link to="/" className={style.link_cadastro}>Entrar</Link>
                    </p>
                </Form>
            </div>
        </div>
    );
}