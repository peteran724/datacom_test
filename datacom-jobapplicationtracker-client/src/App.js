import { useState } from 'react';
import ApplicationList from './components/ApplicationList';
import AddApplication from './components/AddApplication';

import './App.css';

function App() {

  const [currentPage, setCurrentPage] = useState(1);

  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const refreshList = () => {
    setCurrentPage(1);
    setRefreshTrigger(prev => prev + 1);
  };

  return (
    <div className="app-container">
      <AddApplication onAdd={refreshList} />
      <ApplicationList
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        refreshTrigger={refreshTrigger} />
    </div>
  );
}

export default App