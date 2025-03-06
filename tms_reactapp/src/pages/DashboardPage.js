import React, { useEffect, useState } from 'react';
import Sidebar from '../components/Sidebar';
import Navbar from '../components/Navbar';

function DashboardPage() {
  const [taskSummary, setTaskSummary] = useState({
    totalTasks: 0,
    pendingTasks: 0,
    completedTasks: 0
  });

  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchTaskSummary = async () => {
      try {
        const token = sessionStorage.getItem("token"); // ✅ Fetch JWT token
        if (!token) {
          setError("User is not authenticated.");
          return;
        }

        const response = await fetch("http://localhost:5268/api/Task/GetTaskSummary", {
          method: "GET",
          headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
          }
        });

        if (!response.ok) {
          throw new Error(`API request failed: ${response.status}`);
        }

        const data = await response.json();
        setTaskSummary(data); // ✅ Update state with task summary
      } catch (error) {
        console.error("Error fetching task summary:", error);
        setError("Failed to load task summary.");
      }
    };

    fetchTaskSummary();
  }, []);

  return (
    <div>
      <Sidebar />
      <main className="main-content position-relative max-height-vh-100 h-100 border-radius-lg">
        <Navbar />
        <div className="container-fluid py-4">
          {error && <p className="text-danger text-center">{error}</p>}
          <div className="row">
            <div className="row mt-2">
              <div className="col-lg-4 col-md-6 mt-4 mb-4">
                <div className="card z-index-2">
                  <div className="card-header p-0 position-relative mt-n4 mx-3 z-index-2 bg-transparent">
                    <img src="assets/img/1.png" className="navbar-brand-img" alt="main_logo" />
                  </div>
                  <div className="card-body">
                    <h6 className="mb-0">Total Tasks</h6>
                    <p className="text-lg mb-0">{taskSummary.totalTasks}</p>
                  </div>
                </div>
              </div>
              <div className="col-lg-4 col-md-6 mt-4 mb-4">
                <div className="card z-index-2">
                  <div className="card-header p-0 position-relative mt-n4 mx-3 z-index-2 bg-transparent">
                    <img src="assets/img/2.png" className="navbar-brand-img" alt="main_logo" />
                  </div>
                  <div className="card-body">
                    <h6 className="mb-0">Pending Tasks</h6>
                    <p className="text-lg mb-0">{taskSummary.pendingTasks}</p>
                  </div>
                </div>
              </div>
              <div className="col-lg-4 mt-4 mb-3">
                <div className="card z-index-2">
                  <div className="card-header p-0 position-relative mt-n4 mx-3 z-index-2 bg-transparent">
                    <img src="assets/img/3.jpg" className="navbar-brand-img" alt="main_logo" />
                  </div>
                  <div className="card-body">
                    <h6 className="mb-0">Completed Tasks</h6>
                    <p className="text-lg mb-0">{taskSummary.completedTasks}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}

export default DashboardPage;
