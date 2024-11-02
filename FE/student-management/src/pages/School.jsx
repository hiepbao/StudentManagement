import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Notification from '../components/Notification';

function School() {
  const [notificationMessage, setNotificationMessage] = useState('');
  const [showNotification, setShowNotification] = useState(true);
  useEffect(() => {
    setNotificationMessage('Đăng nhập thành công');
    setShowNotification(true);
  }, []);
  return (
    <div className="container mt-5">
      <h1>Trường học</h1>
      <p>Xin chào bạn tới trang trường học</p>
      <Notification message={notificationMessage} show={showNotification} />
    
    </div>
  );
}

export default School;