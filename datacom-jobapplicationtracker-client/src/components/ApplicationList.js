import { useEffect, useState } from 'react';
import { getApplications, updateApplication } from '../services/api';

const ApplicationList = (
  {
    currentPage,
    setCurrentPage,
    refreshTrigger
  }) => {

  const [currentPageSize, setCurrentPageSize] = useState(2);
  const [applications, setApplications] = useState([]);
  const [totalPages, setTotalPages] = useState(0);
  const [totalRecords, setTotalRecords] = useState(0);

  const [errorMessage, setErrorMessage] = useState('');

  const [showUpdateSuccess, setShowUpdateSuccess] = useState(false);

  const getPagedList = async () => {
    let res;
    try {
      res = await getApplications(currentPage, currentPageSize);
    }
    catch (error) {
      setErrorMessage(error.message || 'Failed to get application list');
      return;
    }
    setApplications(res.data.data);
    setTotalRecords(res.data.totalRecords);
    setTotalPages(res.data.totalPages);
    setErrorMessage('');
  };

  useEffect(() => { getPagedList() }, [currentPage, currentPageSize, refreshTrigger]);

  const handleSelectChange = (id, newStatus) => {
    setApplications(apps =>
      apps.map(app =>
        app.id === id ? { ...app, status: newStatus } : app
      )
    );
  };

  const handleStatusChange = async (id, newStatus) => {
    const res = await updateApplication(id, newStatus);
    const success = res.data.success;

    if (success === true) {
      await getPagedList();
      setShowUpdateSuccess(true);
      setTimeout(() => setShowUpdateSuccess(false), 5000);
    }

  };


  return (
    <div><br />
      {errorMessage && (<div className="error-message">
        <span>{errorMessage}</span>
        <button
          className="close-button"
          onClick={() => setErrorMessage('')}
        >
          ×
        </button>
      </div>)}
      {showUpdateSuccess && (<div className="update-message">
        <span>Update Status Successfully</span>
        <button
          className="close-button"
          onClick={() => setShowUpdateSuccess(false)}
        >
          ×
        </button>
      </div>)}

      <table className="applications-table">
        <thead>
          <tr>
            <th>Company Name</th>
            <th>Position</th>
            <th>Status</th>
            <th>Date Applied</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {applications.map(app => (
            <tr key={app.id}>
              <td>{app.companyName}</td>
              <td>{app.position}</td>
              <td>
                <select
                  value={app.status}
                  onChange={(e) => handleSelectChange(app.id, e.target.value)}
                  className="status-select"
                >
                  <option value="Applied">Applied</option>
                  <option value="Interview">Interview</option>
                  <option value="Offer">Offer</option>
                  <option value="Rejected">Rejected</option>
                </select>
              </td>
              <td>{new Date(app.dateApplied).toLocaleDateString()}</td>
              <td>
                <button
                  onClick={() => handleStatusChange(app.id, app.status)}
                  className="save-button"
                >
                  Change Status
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="pagination-controls">
        <button
          onClick={() => setCurrentPage(currentPage => Number(currentPage) - 1)}
          disabled={Number(currentPage) <= 1}
        >
          Previous
        </button>

        <span>Page {currentPage} of {totalPages}</span>

        <button
          onClick={() => setCurrentPage(currentPage => Number(currentPage) + 1)}
          disabled={Number(currentPage) >= Number(totalPages)}
        >
          Next
        </button>

        <div className="page-jump-size">
          jump to:
          <input
            type="number"
            min="1"
            max={totalPages}
            placeholder={currentPage}
            onBlur={(e) => { setCurrentPage(Number(e.target.value) <= 0 ? Number(currentPage) : Number(e.target.value)) }}
            onKeyDown={(e) => e.preventDefault()}
            onPaste={(e) => e.preventDefault()}
          />
        </div>

        <div className="page-jump-size">
          page size:
          <input
            type="number"
            min="1"
            max={totalRecords}
            placeholder={currentPageSize}
            onBlur={(e) => {
              setCurrentPageSize(Number(e.target.value) <= 0 ? Number(currentPageSize) : Number(e.target.value));
              setCurrentPage(1);
            }}
            onKeyDown={(e) => e.preventDefault()}
            onPaste={(e) => e.preventDefault()}
          />
        </div>

        <div className="total-records">
          Total: {totalRecords} applications
        </div>
      </div>
    </div>
  );
};

export default ApplicationList
