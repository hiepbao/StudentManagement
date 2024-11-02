import React, { useEffect, useState } from "react";
import { searchUsers, updateUser, addRole } from "../services/UserApi";
import CustomButton from "../components/CustomButton";
import Notification from "../components/Notification";
import { ExportExcelButton, ImportExcelButton } from "./UserImportExport";
import UpdateUserModal from "./UserUpdateForm";
import AddRoleModal from "./AddRoll";

function UserAccountsPage() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showAddRoleModal, setShowAddRoleModal] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);
  const [notification, setNotification] = useState({ message: "", show: false });

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

  useEffect(() => {
    getUsers();
  }, [searchTerm]);

  useEffect(() => {
    if (notification.show) {
      const timer = setTimeout(() => {
        setNotification({ ...notification, show: false });
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [notification]);

  const handleUsersUpdated = () => {
    getUsers(); 
  };

  const handleShowUpdateModal = (user) => {
    setSelectedUser(user);
    setShowUpdateModal(true);
  };

  const handleCloseUpdateModal = () => {
    setShowUpdateModal(false);
    setSelectedUser(null);
  };

  const handleUpdateUser = async (updatedUser) => {
    try {
      await updateUser(updatedUser);
      setUsers((prevUsers) => prevUsers.map((user) => (user.userId === updatedUser.userId ? updatedUser : user)));
      setNotification({ message: "Cập nhật người dùng thành công", show: true });
      setShowUpdateModal(false);
    } catch (error) {
      console.error("Error updating user:", error);
      setNotification({ message: "Lỗi khi cập nhật người dùng", show: true });
    }
  };

  const handleShowAddRoleModal = (user) => {
    setSelectedUser(user);
    setShowAddRoleModal(true);
  };

  const handleCloseAddRoleModal = () => {
    setShowAddRoleModal(false);
    setSelectedUser(null);
  };

  const handleAddRole = async (userId, roleId) => {
    try {
      await addRole(userId, roleId);
      setNotification({ message: "Thêm vai trò thành công", show: true });
      setShowAddRoleModal(false);
      handleUsersUpdated();
    } catch (error) {
      console.error("Error adding role:", error);
      setNotification({ message: "Lỗi khi thêm vai trò", show: true });
    }
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
              <ImportExcelButton onImportSuccess={() => { handleUsersUpdated(); setNotification({ message: 'Thêm người dùng thành công', show: true }); }} />
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
                    <td className="text-center">{new Date(user.birthday).toLocaleDateString('en-GB')}</td>
                    <td className="text-center">{user.address}</td>
                    <td className="text-center">{user.phone}</td>
                    <td className="text-center">{user.mail}</td>
                    <td className="text-center">
                      {user.userRoles && user.userRoles.length > 0 ? 
                        user.userRoles.map(userRole => userRole.role ? userRole.role.roleName : "N/A").join(", ") 
                        : "N/A"}
                    </td>
                    <td>
                      <CustomButton label="Cập nhật" onClick={() => handleShowUpdateModal(user)} />
                      <CustomButton label="Thêm Roll" onClick={() => handleShowAddRoleModal(user)} />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
        
      <UpdateUserModal 
        show={showUpdateModal} 
        user={selectedUser} 
        handleClose={handleCloseUpdateModal} 
        handleUpdate={handleUpdateUser} 
      /> 

      <AddRoleModal 
        show={showAddRoleModal} 
        user={selectedUser} 
        handleClose={handleCloseAddRoleModal} 
        handleAddRole={handleAddRole} 
      />

      <Notification message={notification.message} show={notification.show} />      
    </div>
  );
}

export default UserAccountsPage;