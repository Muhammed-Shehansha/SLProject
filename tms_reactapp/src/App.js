/*
Title : Task Management System - React
Author: Muhammed Shehansha
Created at : 28/12/2025
Updated at : 30/01/2025
Reviewed by : -
Reviewed at : -
*/

import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import AssignedTasks from './pages/AssignedTasks';
import Notifications from './pages/Notification';

function App() {
  return (
    
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/Dashboard" element={<DashboardPage/>} />
        <Route path="/AssignedTasks" element={<AssignedTasks/>} />
        <Route path="/Notifications" element={<Notifications/>} />
      </Routes>
    </Router>
  );
}

export default App;
