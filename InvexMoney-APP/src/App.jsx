import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Cadastro } from './paginas/Cadastro/cadastro';
import {AlertProvider} from './componentes/Alert/AlertContext';
import { Login } from './paginas/Login/Login';
import { RotaProtegida } from './componentes/RotaProtegida/rotaProtegida';
import { Home } from './paginas/Home/Home';

function App() {
  return (
    <AlertProvider>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Login />} />
          <Route path='/cadastro' element={<Cadastro />} />
          <Route path='/home' element={<RotaProtegida><Home /></RotaProtegida>} />
        </Routes>
      </BrowserRouter>
    </AlertProvider>
  );
}

export default App;