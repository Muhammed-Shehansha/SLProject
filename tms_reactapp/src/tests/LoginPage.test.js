import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { BrowserRouter } from "react-router";
import axios from "axios";
import MockAdapter from "axios-mock-adapter";
import LoginPage from "../pages/LoginPage";

const mockAxios = new MockAdapter(axios);

describe("LoginPage Component", () => {
    beforeEach(() => {
        sessionStorage.clear();
        mockAxios.reset();
    });

    test("renders login form correctly", () => {
        render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );

        expect(screen.getByText(/Task Management System/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText(/Username/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText(/Password/i)).toBeInTheDocument();
        expect(screen.getByRole("button", { name: /Login/i })).toBeInTheDocument();
    });

    test("allows user to input username and password", () => {
        render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );

        fireEvent.change(screen.getByPlaceholderText(/Username/i), { target: { value: 'testuser' } });
        fireEvent.change(screen.getByPlaceholderText(/Password/i), { target: { value: 'password123' } });

        expect(screen.getByPlaceholderText(/Username/i).value).toBe('testuser');
        expect(screen.getByPlaceholderText(/Password/i).value).toBe('password123');
    });

    test("handles successful login", async () => {
        mockAxios.onPost('http://localhost:5268/api/auth/login').reply(200, {
            token: 'fakeToken',
            username: 'testuser'
        });

        render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );

        fireEvent.change(screen.getByPlaceholderText(/Username/i), { target: { value: 'testuser' } });
        fireEvent.change(screen.getByPlaceholderText(/Password/i), { target: { value: 'password123' } });
        fireEvent.click(screen.getByRole("button", { name: /Login/i }));

        await waitFor(() => {
            expect(sessionStorage.getItem("token")).toBe('fakeToken');
            expect(sessionStorage.getItem("username")).toBe('testuser');
        });
    });

    test("handles failed login", async () => {
        mockAxios.onPost('http://localhost:5268/api/auth/login').reply(401, {
            message: 'Invalid credentials'
        });
    
        // Mock the alert function
        window.alert = jest.fn();
    
        render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );
    
        fireEvent.change(screen.getByPlaceholderText(/Username/i), { target: { value: 'wronguser' } });
        fireEvent.change(screen.getByPlaceholderText(/Password/i), { target: { value: 'wrongpassword' } });
        fireEvent.click(screen.getByRole("button", { name: /Login/i }));
    
        await waitFor(() => {
            // Check if the alert was called with the correct message
            expect(window.alert).toHaveBeenCalledWith('Login Failed. Check your credentials.');
        });
    });

});