import React, { useState } from 'react';
import CustomButton from '../components/CustomButton';

function GradeEntry() {
  const [students, setStudents] = useState([
    { id: 1, name: 'Nguyễn Báo Ân', code: '79529819756' },
    { id: 2, name: 'Trương Tố Nhi', code: '79532687373' },
    { id: 3, name: 'Phạm Thảo Chi', code: '79529405349' },
    { id: 4, name: 'Cao Cẩm Doanh', code: '79537668898' },
    { id: 5, name: 'Nguyễn Ngọc Duy', code: '7953766698' },
    // Add more student data here
  ]);

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-12">
          <div className="d-flex justify-content-between align-items-center my-3">
          <h5 className="fw-bold" style={{ color: '#2874f0'}}>Nhập điểm môn học</h5>
            <div>
                <CustomButton label="Cập nhật" onClick={() => { /* handle update click */ }} />
              
                <CustomButton label="Nhập điểm từ excel" onClick={() => { /* handle import click */ }} />
               
                <CustomButton label="Xuất excel" onClick={() => { /* handle export click */ }} />
            </div>
          </div>
          <div className="d-flex align-items-center mb-3 flex-wrap">
            <label className="me-2">Khối:</label>
            <select className="form-select me-2 w-auto" aria-label="Chọn khối">
              <option>Khối 10</option>
              <option>Khối 11</option>
              <option>Khối 12</option>
            </select>
            <label className="me-2">Lớp:</label>
            <select className="form-select me-2 w-auto" aria-label="Chọn lớp">
              <option>10A7</option>
              <option>10A8</option>
              <option>11A1</option>
            </select>
            <label className="me-2">Môn học:</label>
            <select className="form-select me-2 w-auto" aria-label="Chọn môn học">
              <option>Địa lí</option>
              <option>Toán</option>
              <option>Văn</option>
            </select>
            <label className="me-2">Học kỳ:</label>
            <select className="form-select me-2 w-auto" aria-label="Chọn học kỳ">
              <option>Học kỳ 1</option>
              <option>Học kỳ 2</option>
            </select>
            <div className="form-check form-check-inline ms-2">
              <input className="form-check-input" type="checkbox" id="showInfo" />
              <label className="form-check-label" htmlFor="showInfo">Hiển thị ngày sinh, giới tính</label>
            </div>
            <div className="form-check form-check-inline ms-3">
              <input className="form-check-input" type="radio" name="displayOption" id="jumpColumn" />
              <label className="form-check-label" htmlFor="jumpColumn">Nhảy cột</label>
            </div>
            <div className="form-check form-check-inline">
              <input className="form-check-input" type="radio" name="displayOption" id="jumpRow" defaultChecked />
              <label className="form-check-label" htmlFor="jumpRow">Nhảy dòng</label>
            </div>
          </div>
          <table className="table table-bordered table-hover mt-3">
            <thead className="table-primary">
              <tr>
                <th className="text-center">STT</th>
                <th className="text-center">Mã định danh Bộ GDĐT</th>
                <th className="text-center">Họ tên</th>
                <th className="text-center">1</th>
                <th className="text-center">2</th>
                <th className="text-center">3</th>
                <th className="text-center">4</th>
                <th className="text-center">DDGtx</th>
                <th className="text-center">DDGk</th>
                <th className="text-center">DTBmhk1</th>
              </tr>
            </thead>
            <tbody>
              {students.map((student, index) => (
                <tr key={student.id}>
                  <td>{index + 1}</td>
                  <td>{student.code}</td>
                  <td>{student.name}</td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                  <td><input type="text" className="form-control p-2" style={{ backgroundColor: '#e6f7ff', borderRadius: '10px', width: '80px', borderWidth: '1px', borderColor: '#b3d9ff', margin: '0 auto', display: 'block' }} /></td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default GradeEntry;