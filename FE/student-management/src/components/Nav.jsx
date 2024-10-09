import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSchool, faUsers, faChild, faChartLine, faClipboardCheck, faPrint, faChartBar, faTools } from '@fortawesome/free-solid-svg-icons';
import 'bootstrap/dist/css/bootstrap.min.css';
// import '../styles/Nav.css';
import { Link } from 'react-router-dom';

function Nav() {
  return (
    <nav className="d-flex align-items-center p-2 bg-light border-bottom">
      <Link to="/School" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faSchool} className="me-1" />1. Trường học</Link>
      <Link to="/class" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faUsers} className="me-1" />2. Lớp học</Link>
      <Link to="/staff" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faUsers} className="me-1" />3. Nhân sự</Link>
      <Link to="/students" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChild} className="me-1" />4. Học sinh</Link>
      <Link to="/data-entry" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faClipboardCheck} className="me-1" />5. Nhập liệu</Link>
      <Link to="/summary" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartLine} className="me-1" />6. Tổng kết</Link>
      <Link to="/print" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faPrint} className="me-1" />7. In ấn</Link>
      <Link to="/statistics" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartBar} className="me-1" />8. Thống kê</Link>
      <Link to="/reports" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartBar} className="me-1" />9. Báo cáo</Link>
      <Link to="/tools" className="text-dark text-decoration-none"><FontAwesomeIcon icon={faTools} className="me-1" />10. Công cụ hỗ trợ</Link>
    </nav>
  );
}

export default Nav;