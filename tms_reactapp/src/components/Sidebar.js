import React from 'react';
import { Link } from 'react-router-dom';

function Sidebar() {
    return (
            <aside className="sidenav navbar navbar-vertical navbar-expand-xs border-0 border-radius-xl my-3 fixed-start ms-3 bg-gradient-dark" id="sidenav-main">
                <div className="sidenav-header text-white text-center d-flex align-items-center justify-content-center">
                    <i className="material-icons opacity-10 me-1">view_in_ar</i>
                    <Link className="navbar-brand p-1 m-0" to="/Dashboard">
                        <span className="ms-1 font-weight-bold text-white">Task Management System</span>
                    </Link>
                </div>
                <hr className="horizontal light mt-0 mb-2" />
                <div className="collapse navbar-collapse w-auto max-height-vh-100" id="sidenav-collapse-main">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <Link className="nav-link text-white active bg-gradient-primary" to="/Dashboard">
                                <div className="text-white text-center me-2 d-flex align-items-center justify-content-center">
                                    <i className="material-icons opacity-10">dashboard</i>
                                </div>
                                <span className="nav-link-text ms-1">Dashboard</span>
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-white" to="/AssignedTasks">
                                <div className="text-white text-center me-2 d-flex align-items-center justify-content-center">
                                    <i className="material-icons opacity-10">table_view</i>
                                </div>
                                <span className="nav-link-text ms-1">Assigned Tasks</span>
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-white" to="/Notifications">
                                <div className="text-white text-center me-2 d-flex align-items-center justify-content-center">
                                    <i className="material-icons opacity-10">notifications</i>
                                </div>
                                <span className="nav-link-text ms-1">Notifications</span>
                            </Link>
                        </li>
                    </ul>
                </div>
            </aside>
    );
}

export default Sidebar;
