import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSchool, faUsers, faChalkboardTeacher, faUserFriends, faChild, faChartLine, faClipboardCheck, faPrint, faChartBar, faTools, faListCheck, faClipboardList } from '@fortawesome/free-solid-svg-icons';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';

function Nav() {
  return (
    <nav className="d-flex align-items-center p-2 bg-light border-bottom">
      <Link to="/school" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faSchool} className="me-1" />Trường học</Link>
      <Link to="/user" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faUserFriends} className="me-1" />Tài khoản</Link>
      <Link to="/class" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faSchool} className="me-1" />Lớp học</Link>
      <Link to="/teacher" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChalkboardTeacher} className="me-1" />Giáo viên</Link>
      <Link to="/parent" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faUsers} className="me-1" />Phụ huynh</Link>
      <Link to="/student" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChild} className="me-1" />Học sinh</Link>
      <Link to="/studypoint" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faClipboardCheck} className="me-1" />Nhập điểm</Link>
      <Link to="/diligence" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faListCheck} className="me-1" />Nhập chuyên cần</Link>
      <Link to="/studypointdetail" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faClipboardList} className="me-1" />Xem điểm</Link>
      <Link to="/diligencedetail" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faClipboardList} className="me-1" />Xem chuyên cần</Link>
      <Link to="/school" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartLine} className="me-1" />Tổng kết</Link>
      <Link to="/school" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faPrint} className="me-1" />In ấn</Link>
      <Link to="/school" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartBar} className="me-1" />Thống kê</Link>
      <Link to="/school" className="text-dark text-decoration-none me-4"><FontAwesomeIcon icon={faChartBar} className="me-1" />Báo cáo</Link>
    </nav>
  );
}

export default Nav;