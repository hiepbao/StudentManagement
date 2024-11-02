import React, { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";

function AddRoleModal({ show, user, handleClose, handleAddRole }) {
  const [roleId, setRoleId] = useState("");

  useEffect(() => {
    if (user) {
      setRoleId("");
    }
  }, [user]);

  const handleChange = (e) => {
    setRoleId(e.target.value);
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Thêm vai trò cho người dùng</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="formRoleId">
            <Form.Label>Role ID</Form.Label>
            <Form.Control type="text" value={roleId} onChange={handleChange} />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Hủy
        </Button>
        <Button variant="primary" onClick={() => handleAddRole(user.userId, roleId)}>
          Thêm vai trò
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddRoleModal;