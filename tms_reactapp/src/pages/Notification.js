import React, { useEffect, useState } from "react";
import axios from "axios";
import Sidebar from "../components/Sidebar";
import Navbar from "../components/Navbar";

function Notifications() {
    const [notifications, setNotifications] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selectedNotification, setSelectedNotification] = useState(null);

    useEffect(() => {
        fetchNotifications();
    }, []);

    // Function to fetch the Notification
    const fetchNotifications = async () => {
        try {
            const token = sessionStorage.getItem("token");
            const response = await axios.get("http://localhost:5268/api/Notification/GetNotifications", {
                headers: { Authorization: `Bearer ${token}` }
            });

            setNotifications(response.data);
        } catch (error) {
            console.error("Error fetching notifications:", error.response?.data || error.message);
        } finally {
            setLoading(false);
        }
    };

    // Function to mark notification as read.
    const markAsRead = async (id) => {
        try {
            const token = sessionStorage.getItem("token");
            await axios.patch(`http://localhost:5268/api/Notification/MarkAsRead/${id}`, {}, {
                headers: { Authorization: `Bearer ${token}` }
            });

            setNotifications(notifications.map(n => n.id === id ? { ...n, isRead: true } : n));
            setSelectedNotification(null);
        } catch (error) {
            console.error("Error marking notification as read:", error.response?.data || error.message);
        }
    };

    // Function to delete Notification
    const deleteNotification = async (id) => {
        try {
            const token = sessionStorage.getItem("token");
            await axios.delete(`http://localhost:5268/api/Notification/DeleteNotification/${id}`, {
                headers: { Authorization: `Bearer ${token}` }
            });

            setNotifications(notifications.filter(n => n.id !== id));
            setSelectedNotification(null);
        } catch (error) {
            console.error("Error deleting notification:", error.response?.data || error.message);
        }
    };

    return (
        <div>
            <Sidebar />
            <main className="main-content position-relative max-height-vh-100 h-100 border-radius-lg">
                <Navbar />
                <div className="container-fluid py-4">
                    <div className="row">
                        <div className="col-lg-10 col-md-10 mx-auto">
                            <div className="card mt-4">
                                <div className="card-header p-3">
                                    <h5 className="mb-0">Notifications</h5>
                                </div>
                                <div className="card-body p-3 pb-0">
                                    {loading ? (
                                        <p>Loading notifications...</p>
                                    ) : notifications.length > 0 ? (
                                        notifications.map((notification) => (
                                            <div key={notification.id} 
                                                className={`alert ${notification.isRead ? "alert-secondary" : "alert-primary"} alert-dismissible text-white`} 
                                                role="alert"
                                                onClick={() => setSelectedNotification(notification)}
                                                style={{ cursor: "pointer" }}>
                                                <span className="text-sm">
                                                    {notification.message}
                                                </span>
                                                <button type="button" className="btn-close text-lg py-3 opacity-10" data-bs-dismiss="alert" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                        ))
                                    ) : (
                                        <p>No notifications available.</p>
                                    )}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>

            {/* MODAL */}
            {selectedNotification && (
                <div className="modal fade show d-block" style={{ background: "rgba(0, 0, 0, 0.5)" }}>
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">Notification Actions</h5>
                                <button type="button" className="btn-close" style={{ backgroundColor: "black" }} onClick={() => setSelectedNotification(null)}></button>
                            </div>
                            <div className="modal-body">
                                <p>{selectedNotification.message}</p>
                                <p>Status: {selectedNotification.isRead ? "Read" : "Unread"}</p>
                            </div>
                            <div className="modal-footer">
                                <button className="btn btn-success" onClick={() => markAsRead(selectedNotification.id)}>Mark as Read</button>
                                <button className="btn btn-danger" onClick={() => deleteNotification(selectedNotification.id)}>Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default Notifications;
