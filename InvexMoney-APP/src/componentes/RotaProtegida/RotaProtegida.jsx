import {Navigate} from "react-router-dom";
import {tokenValido} from "../../Services/sessao";

export function RotaProtegida({children}) {
    return tokenValido() ? children : <Navigate to="/" replace />;
}