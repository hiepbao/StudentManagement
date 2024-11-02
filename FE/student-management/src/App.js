import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Header from './components/Header';
import Nav from './components/Nav';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import School from './pages/School';
import Studypoint from './pages/Studypoint';
import User from './pages/User';
import Class from './pages/Class';
import Teacher from './pages/Teacher';
import Parent from './pages/Parent';
import Student from './pages/Student';
import Diligence from './pages/Diligence';
import StudypointDetail from './pages/StudypointDetail';
import DiligenceDetail from './pages/DiligenceDetail';


// Giả sử có một hàm để kiểm tra người dùng đã đăng nhập hay chưa
const isAuthenticated = () => {
  return localStorage.getItem("authToken") !== null;
};

// PrivateRoute component để bảo vệ các route
function PrivateRoute({ element }) {
  return isAuthenticated() ? element : <Navigate to="/" />;
}

function App() {
  return (
    <div>
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/school" element={<PrivateRoute element={<MainLayout><School /></MainLayout>} />} />
          <Route path="/user" element={<PrivateRoute element={<MainLayout><User /></MainLayout>} />} />
          <Route path="/class" element={<PrivateRoute element={<MainLayout><Class /></MainLayout>} />} />
          <Route path="/teacher" element={<PrivateRoute element={<MainLayout><Teacher /></MainLayout>} />} />
          <Route path="/parent" element={<PrivateRoute element={<MainLayout><Parent /></MainLayout>} />} />
          <Route path="/student" element={<PrivateRoute element={<MainLayout><Student /></MainLayout>} />} />
          <Route path="/studypoint" element={<PrivateRoute element={<MainLayout><Studypoint /></MainLayout>} />} />
          <Route path="/diligence" element={<PrivateRoute element={<MainLayout><Diligence /></MainLayout>} />} />
          <Route path="/studypointdetail" element={<PrivateRoute element={<MainLayout><StudypointDetail /></MainLayout>} />} />
          <Route path="/diligencedetail" element={<PrivateRoute element={<MainLayout><DiligenceDetail /></MainLayout>} />} />
        </Routes>
      </Router>
    </div>
  );
}

// Component để bố trí Header, Nav và Footer cho các trang sau khi đăng nhập thành công
function MainLayout({ children }) {
  return (
    <div className="d-flex flex-column vh-100">
      <header>
        <Header />
        <Nav />
      </header>
      <main className="flex-grow-1 overflow-auto" style={{ paddingTop: '20px', paddingBottom: '20px' }}>
        {children}
      </main>
      <footer className="mt-auto">
        <Footer />
      </footer>
    </div>
  );
}

export default App;