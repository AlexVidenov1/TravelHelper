import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import NavBar from './components/Navbar';
import Login from './pages/Login';
import Register from './pages/Register';
import Search from './pages/Search';
import Favorites from './pages/Favorites';

export default function App() {
    const isAuth = !!localStorage.getItem('jwt');

    return (
        <BrowserRouter>
            <NavBar isAuth={isAuth} />
            <div className="container mt-3">
                <Routes>
                    <Route path="/" element={<Search />} />
                    <Route path="/login" element={isAuth ? <Navigate to="/" /> : <Login />} />
                    <Route path="/register" element={isAuth ? <Navigate to="/" /> : <Register />} />
                    <Route path="/favorites" element={isAuth ? <Favorites /> : <Navigate to="/login" />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}