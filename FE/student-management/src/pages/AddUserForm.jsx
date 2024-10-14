import React, { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";

function AddUserForm({ show, handleClose, handleSubmit }) {
  const [newUser, setNewUser] = useState({
    name: "",
    gender: "",
    address: "",
    phone: "",
    mail: "",
    birthday: ""
  });

  const handleAddUserSubmit = () => {
    handleSubmit(newUser); // Gọi hàm handleSubmit từ props để đăng ký người dùng
    handleClose();
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Thêm người dùng mới</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group className="mb-3">
            <Form.Label>Họ tên</Form.Label>
            <Form.Control
              type="text"
              placeholder="Nhập họ tên"
              value={newUser.name}
              onChange={(e) => setNewUser({ ...newUser, name: e.target.value })}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Giới tính</Form.Label>
            <Form.Control
              type="text"
              placeholder="Nhập giới tính"
              value={newUser.gender}
              onChange={(e) => setNewUser({ ...newUser, gender: e.target.value })}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Ngày sinh</Form.Label>
            <Form.Control
              type="date"
              value={newUser.birthday}
              onChange={(e) => setNewUser({ ...newUser, birthday: e.target.value })}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Địa chỉ</Form.Label>
            <Form.Control
              type="text"
              placeholder="Nhập địa chỉ"
              value={newUser.address}
              onChange={(e) => setNewUser({ ...newUser, address: e.target.value })}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Số điện thoại</Form.Label>
            <Form.Control
              type="text"
              placeholder="Nhập số điện thoại"
              value={newUser.phone}
              onChange={(e) => setNewUser({ ...newUser, phone: e.target.value })}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              placeholder="Nhập email"
              value={newUser.mail}
              onChange={(e) => setNewUser({ ...newUser, mail: e.target.value })}
            />
          </Form.Group>
          
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Đóng
        </Button>
        <Button variant="primary" onClick={handleAddUserSubmit}>
          Thêm
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddUserForm;
