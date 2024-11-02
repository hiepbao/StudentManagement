import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

function UpdateUserForm({ show, handleClose, user, handleUpdate }) {
  const [updatedUser, setUpdatedUser] = useState(user);

  useEffect(() => {
    if (user) {
      setUpdatedUser(user);
    }
  }, [user]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdatedUser((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    handleUpdate(updatedUser);
  };

  return (
    <Modal show={show} onHide={handleClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>Cập nhật thông tin người dùng</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="name" className="form-label">Họ tên</label>
            <input
              type="text"
              className="form-control"
              id="name"
              name="name"
              value={updatedUser.name || ''}
              onChange={handleChange}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="gender" className="form-label">Giới tính</label>
            <select
              className="form-control"
              id="gender"
              name="gender"
              value={updatedUser.gender || ''}
              onChange={handleChange}
            >
              <option value="">Chọn giới tính</option>
              <option value="Nam">Nam</option>
              <option value="Nữ">Nữ</option>
            </select>
          </div>
          <div className="mb-3">
            <label htmlFor="birthday" className="form-label">Ngày sinh</label>
            <input
              type="date"
              className="form-control"
              id="birthday"
              name="birthday"
              value={updatedUser.birthday || ''}
              onChange={handleChange}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="address" className="form-label">Địa chỉ</label>
            <input
              type="text"
              className="form-control"
              id="address"
              name="address"
              value={updatedUser.address || ''}
              onChange={handleChange}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="phone" className="form-label">Số điện thoại</label>
            <input
              type="text"
              className="form-control"
              id="phone"
              name="phone"
              value={updatedUser.phone || ''}
              onChange={handleChange}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="mail" className="form-label">Email</label>
            <input
              type="email"
              className="form-control"
              id="mail"
              name="mail"
              value={updatedUser.mail || ''}
              onChange={handleChange}
            />
          </div>
          <Button variant="primary" type="submit">
            Cập nhật
          </Button>
        </form>
      </Modal.Body>
    </Modal>
  );
}

export default UpdateUserForm;