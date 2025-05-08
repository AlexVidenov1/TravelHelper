import React from 'react';
import ReactDOM from 'react-dom/client';
import 'bootstrap/dist/css/bootstrap.min.css';   // your current line
import App from './App';                         // <- add this

// create root and render the application
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);