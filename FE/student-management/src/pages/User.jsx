import React, { useEffect, useState } from "react";
import { searchUsers, registerUser } from "../services/UserApi";
import CustomButton from "../components/CustomButton";
import { ExportExcelButton, ImportExcelButton } from "./UserImportExport";
import NotificationListener from "../services/RealtimeApi";

function UserAccountsPage() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");
  const [showAddUserModal, setShowAddUserModal] = useState(false);

  const getUsers = async () => {
    try {
      const response = await searchUsers(searchTerm);
      setUsers(response.data);
    } catch (error) {
      console.error("Error fetching user accounts:", error);
    } finally {
      setLoading(false);
    }
  };

  // Sử dụng useEffect để lấy danh sách người dùng khi component mount
  useEffect(() => {
    getUsers();
  }, [searchTerm]);

  // Hàm này sẽ được gọi khi có sự kiện cập nhật từ SignalR
  const handleUsersUpdated = () => {
    getUsers(); // Cập nhật lại danh sách người dùng
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-12">
          <div className="d-flex justify-content-between align-items-center my-3">
            <h5 className="fw-bold" style={{ color: '#2874f0' }}>Quản lý người dùng</h5>
            <input
              type="text"
              className="form-control w-25 mx-3"
              placeholder="Tìm kiếm người dùng..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
            <div className="d-flex align-items-center">
              <ImportExcelButton onImportSuccess={handleUsersUpdated}/>
              <ExportExcelButton />
            </div>
          </div>
          <div className="form-horizontal" style={{ backgroundColor: "white", padding: "30px", borderRadius: "1rem" }}>
            <table className="table table-bordered table-hover mt-3">
              <thead className="table-primary">
                <tr>
                  <th className="text-center">STT</th>
                  <th className="text-center">Họ tên</th>
                  <th className="text-center">Giới tính</th>
                  <th className="text-center">Ngày sinh</th>
                  <th className="text-center">Địa chỉ</th>
                  <th className="text-center">Số điện thoại</th>
                  <th className="text-center">Email</th>
                  <th className="text-center">Loại tài khoản</th>
                  <th className="text-center">Actions</th>
                </tr>
              </thead>
              <tbody>
                {users.map((user, index) => (
                  <tr key={user.userId}>
                    <td>{index + 1}</td>
                    <td className="text-center">{user.name}</td>
                    <td className="text-center">{user.gender}</td>
                    <td className="text-center">{user.birthday}</td>
                    <td className="text-center">{user.address}</td>
                    <td className="text-center">{user.phone}</td>
                    <td className="text-center">{user.mail}</td>
                    <td className="text-center">
                      {user.userRoles && user.userRoles.length > 0 ? 
                        user.userRoles.map(userRole => userRole.role ? userRole.role.roleName : "N/A").join(", ") 
                        : "N/A"}
                    </td>
                    <td>
                      <CustomButton label="Cập nhật" onClick={() => { /* handle update click */ }} />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      
      <NotificationListener onUsersUpdated={handleUsersUpdated} />          
    </div>
  );
}

export default UserAccountsPage;