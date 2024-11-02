import { useEffect, useState } from 'react';
import { HubConnectionBuilder } from "@microsoft/signalr";
import Notification from '../components/Notification';

function NotificationListener() {
  const [notificationMessage, setNotificationMessage] = useState('');
  const [showNotification, setShowNotification] = useState(false);

    useEffect(() => {
        // Tạo kết nối với SignalR Hub
        const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:7220/notificationHub") // Địa chỉ Hub của bạn
            .build();

        connection.start()
            .then(() => {
                console.log("Kết nối thành công!");

                // Lắng nghe sự kiện UsersUpdated từ server
                connection.on("UsersUpdated", () => {
                    setNotificationMessage("Danh sách người dùng đã được cập nhật!");
                    setShowNotification(true);
                });
            })
            .catch(error => console.error("Kết nối thất bại: ", error));

        // Dọn dẹp khi component bị unmount
        return () => {
            connection.stop();
        };
    }, []);

    return (
      <>
        {/* Notification component để thay thế cho alert */}
        <Notification message={notificationMessage} show={showNotification} />
      </>
    );
}

export default NotificationListener;