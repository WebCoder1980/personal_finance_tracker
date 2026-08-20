import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {BrowserRouter, Navigate, Outlet, Route, Routes} from "react-router";
import {LoginPage} from "@/components/pages/LoginPage.tsx";
import {RegisterPage} from "@/components/pages/RegisterPage.tsx";
import {useAuthStore} from "@/service/AuthStore.ts";

function ProtectedRoute() {
    const {data} = useAuthStore()
    const hasToken = data != null

    if (!hasToken) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}

function PublicRoute() {
    const {data} = useAuthStore()
    const hasToken = data != null

    if (hasToken) {
        return <Navigate to="/" replace />;
    }

    return <Outlet />;
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
        <Routes>
            <Route element={<PublicRoute />}>
                <Route path="/login" element={<LoginPage/>}></Route>
                <Route path="/register" element={<RegisterPage/>}></Route>
            </Route>
            <Route element={<ProtectedRoute />}>
                <Route path="/" element={<App/>}></Route>
            </Route>
        </Routes>
    </BrowserRouter>
  </StrictMode>,
)
