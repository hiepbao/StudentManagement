import React, { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import CustomButton from "../components/CustomButton";

function UpdateUserModal({ show, user, handleClose, handleUpdate }) {
  const [updatedUser, setUpdatedUser] = useState({
    name: "",
    gender: "",
    birthday: "",
    address: "",
    phone: "",
    mail: ""
  });

  useEffect(() => {
    if (user) {
      setUpdatedUser({
        ...user,
        birthday: user.birthday ? new Date(user.birthday).toISOString().split("T")[0] : ""
      });
    } else {
      setUpdatedUser({
        name: "",
        gender: "",
        birthday: "",
        address: "",
        phone: "",
        mail: ""
      });
    }
  }, [user]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdatedUser((prevUser) => ({ ...prevUser, [name]: value }));
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Cập nhật thông tin người dùng</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="formUserName">
            <Form.Label>Họ tên</Form.Label>
            <Form.Control type="text" name="name" value={updatedUser.name} onChange={handleChange} />
          </Form.Group>
          <Form.Group controlId="formUserGender">
            <Form.Label>Giới tính</Form.Label>
            <Form.Control type="text" name="gender" value={updatedUser.gender} onChange={handleChange} />
          </Form.Group>
          <Form.Group controlId="formUserBirthday">
            <Form.Label>Ngày sinh</Form.Label>
            <Form.Control type="date" name="birthday" value={updatedUser.birthday} onChange={handleChange} />
          </Form.Group>
          <Form.Group controlId="formUserAddress">
            <Form.Label>Địa chỉ</Form.Label>
            <Form.Control type="text" name="address" value={updatedUser.address} onChange={handleChange} />
          </Form.Group>
          <Form.Group controlId="formUserPhone">
            <Form.Label>Số điện thoại</Form.Label>
            <Form.Control type="text" name="phone" value={updatedUser.phone} onChange={handleChange} />
          </Form.Group>
          <Form.Group controlId="formUserEmail">
            <Form.Label>Email</Form.Label>
            <Form.Control type="email" name="mail" value={updatedUser.mail} onChange={handleChange} />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Hủy
        </Button>
        <CustomButton label="Lưu" onClick={() => handleUpdate(updatedUser)} />
      </Modal.Footer>
    </Modal>
  );
}

export default UpdateUserModal;