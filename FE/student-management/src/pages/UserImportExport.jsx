import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import { exportExcel, importExcel } from '../services/UserApi';
import { toast } from 'react-toastify';
import CustomButton from "../components/CustomButton";

const ExportExcelButton = () => {
  const handleExport = () => {
    exportExcel()
      .then(() => {
        toast.success('File exported successfully');
      })
      .catch(() => {
        toast.error('Error exporting Excel file');
      });
  };

  return (
    <CustomButton label="Xuất Excel" onClick={handleExport} />
  );
};

const ImportExcelButton = ({ onImportSuccess }) => {
  const onDrop = useCallback((acceptedFiles) => {
    importExcel(acceptedFiles[0])
      .then((response) => {
        // Gọi hàm callback onImportSuccess để cập nhật danh sách người dùng
        if (onImportSuccess) {
          onImportSuccess();
        }
      })
      .catch((error) => {
        if (error.response && error.response.data && error.response.data.errors) {
          alert('Errors: \n' + error.response.data.errors.join('\n'));
        } else {
          alert('Thêm file thất bại');
        }
      });
  }, [onImportSuccess]);

  const { getRootProps, getInputProps } = useDropzone({ onDrop });

  return (
    <div {...getRootProps()} className="dropzone">
      <input {...getInputProps()} />
      <CustomButton label="Nhập từ Excel" />
    </div>
  );
};

export { ExportExcelButton, ImportExcelButton };