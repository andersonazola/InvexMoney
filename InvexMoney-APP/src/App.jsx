import { BrowserRouter, Route, Routes } from 'react-router-dom';
// import { Cadastro } from './paginas/Cadastro/cadastro';
import {AlertProvider} from './componentes/Alert/AlertContext';
import { Login } from './paginas/Login/Login';

function App() {
  return (
    <AlertProvider>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Login />} />
          {/* <Route path='/cadastro' element={<Cadastro />} /> */}
        </Routes>
      </BrowserRouter>
    </AlertProvider>
  );
}

export default App;