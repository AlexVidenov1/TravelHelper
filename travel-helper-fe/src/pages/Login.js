import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api';

export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [err, setErr] = useState('');
    const nav = useNavigate();

    const submit = async e => {
        e.preventDefault();
        try {
            const { data } = await api.post('/auth/login', { email, password });
            localStorage.setItem('jwt', data.token);
            nav('/');
            window.location.reload();
        } catch (ex) {
            setErr('Invalid credentials');
        }
    };

    return (
        <div className="row justify-content-center">
            <div className="col-md-4">
                <h2>Login</h2>
                {err && <div className="alert alert-danger">{err}</div>}
                <form onSubmit={submit}>
                    <input className="form-control mb-2" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
                    <input className="form-control mb-2" type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
                    <button className="btn btn-primary w-100">Login</button>
                </form>
            </div>
        </div>
    );
}