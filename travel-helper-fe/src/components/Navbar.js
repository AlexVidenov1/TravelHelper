import { Link, useNavigate } from 'react-router-dom';

export default function NavBar({ isAuth }) {
    const nav = useNavigate();
    const logout = () => {
        localStorage.removeItem('jwt');
        nav('/login', { replace: true });
        window.location.reload();
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">
                <Link className="navbar-brand" to="/">Travel Helper</Link>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav ms-auto">
                        {isAuth ? (
                            <>
                                <li className="nav-item"><Link className="nav-link" to="/favorites">My favorites</Link></li>
                                <li className="nav-item"><button className="btn btn-link nav-link" onClick={logout}>Logout</button></li>
                            </>
                        ) : (
                            <>
                                <li className="nav-item"><Link className="nav-link" to="/login">Login</Link></li>
                                <li className="nav-item"><Link className="nav-link" to="/register">Register</Link></li>
                            </>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}