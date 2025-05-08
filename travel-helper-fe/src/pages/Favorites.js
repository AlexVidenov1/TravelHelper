import { useEffect, useState } from 'react';
import api from '../api';

export default function Favorites() {
    const [list, setList] = useState([]);
    const [note, setNote] = useState('');
    const [editing, setEditing] = useState(null);

    const load = async () => setList((await api.get('/favorites')).data);
    useEffect(() => { load(); }, []);

    const del = async id => { await api.delete('/favorites/' + id); load(); };
    const startEdit = f => { setEditing(f); setNote(f.note || ''); };
    const save = async () => {
        await api.put('/favorites/' + editing.id, { ...editing, note });
        setEditing(null); load();
    };

    return (
        <>
            <h2>My favourites</h2>
            <ul className="list-group">
                {list.map(f => (
                    <li key={f.id} className="list-group-item d-flex justify-content-between align-items-center">
                        {f.city}  {f.note && <span className="text-muted">– {f.note}</span>}
                        <span>
                            <button className="btn btn-sm btn-outline-primary me-1" onClick={() => startEdit(f)}>Edit</button>
                            <button className="btn btn-sm btn-outline-danger" onClick={() => del(f.id)}>X</button>
                        </span>
                    </li>
                ))}
            </ul>

            {/* very light “modal” */}
            {editing &&
                <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex align-items-center justify-content-center">
                    <div className="bg-white p-4 rounded" style={{ minWidth: 300 }}>
                        <h5>Edit note for {editing.city}</h5>
                        <input className="form-control mb-2" value={note} onChange={e => setNote(e.target.value)} />
                        <button className="btn btn-success me-2" onClick={save}>Save</button>
                        <button className="btn btn-secondary" onClick={() => setEditing(null)}>Cancel</button>
                    </div>
                </div>}
        </>
    );
}