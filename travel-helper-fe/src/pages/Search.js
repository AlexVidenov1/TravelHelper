import { useState } from 'react';
import api from '../api';

export default function Search() {
    const [city, setCity] = useState('');
    const [data, setData] = useState(null);
    const [err, setErr] = useState('');
    const isAuth = !!localStorage.getItem('jwt');

    const search = async e => {
        e.preventDefault();
        if (!city) return;
        try {
            const res = await api.get('/destination', { params: { city } });
            setData(res.data); setErr('');
        } catch {
            setErr('Error fetching data'); setData(null);
        }
    };

    const save = async () => {
        try {
            await api.post('/favorites', { city });
            alert('Saved!');
        } catch {
            alert('Error while saving');
        }
    };

    return (
        <>
            <form className="input-group mb-3" onSubmit={search}>
                <input className="form-control" placeholder="Enter city..." value={city} onChange={e => setCity(e.target.value)} />
                <button className="btn btn-primary">Search</button>
            </form>

            {err && <div className="alert alert-danger">{err}</div>}

            {data && !data.error && (
                <div className="card">
                    <div className="card-body">
                        <h3>{city}</h3>
                        <p>{data.weather.description}, {data.weather.temp}°C</p>
                        <img src={data.weather.icon} alt="" />
                        <p><img src={data.country.flagPng} width="32" alt="" /> {data.country.name}</p>
                        <p>1 EUR = <span className="fw-bold">{data.rate.eur_to_local}</span> {data.country.currency}</p>
                        {isAuth && <button className="btn btn-success" onClick={save}>Save to favourites</button>}
                    </div>
                </div>
            )}

            {data && data.error && <div className="alert alert-warning">{data.error}</div>}
        </>
    );
}