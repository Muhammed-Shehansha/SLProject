import React, { useEffect, useState } from "react";
import axios from "axios";
import Sidebar from '../components/Sidebar';
import Navbar from '../components/Navbar';

function AssignedTasks() {
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selectedTask, setSelectedTask] = useState(null);
    const [newStatus, setNewStatus] = useState("");

    useEffect(() => {
        fetchTasks();
    }, []);

    // Function to fetch the tasks
    const fetchTasks = async () => {
        try {
            const token = sessionStorage.getItem("token");
            const response = await axios.get("http://localhost:5268/api/Task/GetAssignedTasks", {
                headers: { Authorization: `Bearer ${token}` }
            });

            setTasks(response.data);
        } catch (error) {
            console.error("Error fetching tasks:", error.response?.data || error.message);
        } finally {
            setLoading(false);
        }
    };

    // Function to handle opening the modal
    const openModal = (task) => {
        setSelectedTask(task);
        setNewStatus(task.status); // Preselect current status
    };

    // Function to handle status update
    const updateStatus = async () => {
        if (!selectedTask) return;

        try {
            const token = sessionStorage.getItem("token");
            await axios.patch(`http://localhost:5268/api/Task/UpdateTaskStatus/${selectedTask.id}`, 
                newStatus, 
                { headers: { 
                    "Authorization": `Bearer ${token}`, 
                    "Content-Type": "application/json"
                } 
            });

            alert("Status updated successfully!");
            setSelectedTask(null); // Close modal
            fetchTasks(); // Refresh task list
        } catch (error) {
            console.error("Error updating task status:", error.response?.data || error.message);
            alert("Failed to update status.");
        }
    };

    return (
        <div>
            <Sidebar />
            <main className="main-content position-relative max-height-vh-100 h-100 border-radius-lg ">
                <Navbar />
                <div className="container-fluid py-4">
                    <div className="row">
                        <div className="col-12">
                            <div className="card my-4">
                                <div className="card-header p-0 position-relative mt-n4 mx-3 z-index-2">
                                    <div className="bg-gradient-primary shadow-primary border-radius-lg pt-4 pb-3">
                                        <h6 className="text-white text-capitalize ps-3">Task List</h6>
                                    </div>
                                </div>
                                <div className="card-body px-4 pb-4">
                                    <div className="table-responsive p-0">
                                        {loading ? (
                                            <p>Loading tasks...</p>
                                        ) : tasks.length > 0 ? (
                                            <table className="table align-items-center mb-0">
                                                <thead>
                                                    <tr>
                                                        <th className="text-uppercase text-secondary font-weight-bolder opacity-7 ps-1">Title</th>
                                                        <th className="text-uppercase text-secondary font-weight-bolder opacity-7 ps-1">Description</th>
                                                        <th className="text-center text-uppercase text-secondary font-weight-bolder opacity-7">Status</th>
                                                        <th className="text-center text-uppercase text-secondary font-weight-bolder opacity-7">Due Date</th>
                                                        <th className="text-secondary opacity-7 ps-1">Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {tasks.map((task) => (
                                                        <tr key={task.id}>
                                                            <td><span className="text-secondary text-sm font-weight-bold">{task.title}</span></td>
                                                            <td><span className="text-secondary text-sm font-weight-bold">{task.description}</span></td>
                                                            <td className="align-middle text-center text-sm">
                                                                <span className={`badge badge-sm ${task.status === "Completed" ? "bg-gradient-success" : "bg-gradient-secondary"}`}>
                                                                    {task.status}
                                                                </span>
                                                            </td>
                                                            <td className="align-middle text-center">{new Date(task.dueDate).toLocaleDateString()}</td>
                                                            <td className="align-middle">
                                                                <a href="#" className="text-secondary font-weight-bold text-sm" data-toggle="tooltip" title="Update Status" onClick={() => openModal(task)}>
                                                                    <i className="fas fa-edit"></i>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        ) : (
                                            <p>No assigned tasks available.</p>
                                        )}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                {/* Status Update Modal */}
                {selectedTask && (
                    <div className="modal fade show d-block" tabIndex="-1" role="dialog" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
                        <div className="modal-dialog modal-dialog-centered" role="document">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Update Task Status</h5>
                                    <button type="button" className="close" onClick={() => setSelectedTask(null)}>
                                        <span>&times;</span>
                                    </button>
                                </div>
                                <div className="modal-body">
                                    <label>Status</label>
                                    <select className="form-control p-2" style={{ border: "1px solid black" }} value={newStatus} onChange={(e) => setNewStatus(e.target.value)}>
                                        <option value="Pending">Pending</option>
                                        <option value="Completed">Completed</option>
                                    </select>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" onClick={() => setSelectedTask(null)}>Cancel</button>
                                    <button type="button" className="btn btn-primary" onClick={updateStatus}>Update</button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </main>
        </div>
    );
}

export default AssignedTasks;
