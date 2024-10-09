import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Header from './components/Header';
import Nav from './components/Nav';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './pages/login';
import School from './pages/School';

function App() {
  return (
    <Router basename="/">
      <div className="d-flex flex-column vh-100">
      <header>
          <Header />
          <Nav />
        </header>
        <main className="flex-grow-1 overflow-auto" style={{ paddingTop: '20px', paddingBottom: '20px' }}>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/school" element={<School />} />
          </Routes>
        </main>
        <footer className="mt-auto">
          <Footer />
        </footer>
      </div>
    </Router>
  );
}

export default App;