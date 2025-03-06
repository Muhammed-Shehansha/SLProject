import React from 'react';
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';

function Navbar() {

    const [User, setUser] = useState("");

    useEffect(() => {
        setUser(sessionStorage.getItem("username"));
    }, []);

    const navigate = useNavigate();

    const handleLogout = (e) => {
        e.preventDefault();
        e.stopPropagation();

        sessionStorage.removeItem("token");

        setTimeout(() => {
            navigate("/");
        }, 100);
    };

    return (
        <nav className="navbar navbar-main navbar-expand-lg px-0 mx-4 shadow-none border-radius-xl" id="navbarBlur"
            navbar-scroll="true">
            <div className="mt-2 container-fluid py-1 px-3">
                <nav aria-label="breadcrumb">
                    <h4 className="font-weight-bolder mb-0">Technician Dashboard</h4>
                </nav>
                <div className="collapse navbar-collapse mt-sm-0 mt-2 me-md-0 me-sm-4" id="navbar">
                    <div className="ms-md-auto pe-md-3 d-flex align-items-center">
                    </div>
                    <ul className="navbar-nav justify-content-end">
                        <li class="nav-item d-flex align-items-center me-4 text-dark">
                            <span><b>Welcome, {User ? User : "Guest"}</b></span>
                        </li>
                        <li className="nav-item d-flex align-items-center">
                            <button onClick={handleLogout} className="btn btn-danger d-flex align-items-center mb-0" style={{ backgroundColor: "#27272a" }}>
                                <i className="material-icons opacity-10 me-1">logout</i>
                                Logout
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;