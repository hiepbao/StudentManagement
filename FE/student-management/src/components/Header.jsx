import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faBell, faQuestionCircle, faTh } from '@fortawesome/free-solid-svg-icons';
import 'bootstrap/dist/css/bootstrap.min.css';
// import '../styles/Header.css';

function Header() {
  return (
    <header className="d-flex align-items-center p-2 bg-primary text-white">
      <div className="d-flex align-items-center me-auto">
        <img src="/assets/logo.png" alt="Logo" className="me-2" style={{ width: '40px', height: '40px' }} />
        <h5 className="mb-0">HCM-EDU</h5>
      </div>
      <span className="me-auto">TT GDN-GDTX QUẬN PHÚ NHUẬN</span>
      <div className="d-flex align-items-center">
        <span className="me-3">Quản lý giáo dục GDTX</span>
        <FontAwesomeIcon icon={faUser} className="me-3" />
        <a href="#" className="text-white text-decoration-none me-3">[Đào Hoàng Xuân]</a>
        <a href="#" className="text-white text-decoration-none me-3">Học kỳ | 2024-2025</a>
        <FontAwesomeIcon icon={faBell} className="me-3" />
        <FontAwesomeIcon icon={faQuestionCircle} className="me-3" />
        <FontAwesomeIcon icon={faTh} />
      </div>
    </header>
  );
}

export default Header;