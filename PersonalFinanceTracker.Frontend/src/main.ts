import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import {createPinia} from "pinia";
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import {createRouter, createWebHistory} from "vue-router";
import TransactionsPage from "@/components/TransactionsPage.vue";
import LoginPage from "@/components/LoginPage.vue";
import DefaultPage from "@/components/DefaultPage.vue";
import RegisterPage from "@/components/RegisterPage.vue";

const app = createApp(App)
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)
const routes = [
    { path: "/", component: DefaultPage },
    { path: "/transactions", component: TransactionsPage },
    { path: "/login", component: LoginPage },
    { path: "/register", component: RegisterPage }
]
app.use(createRouter({
    history: createWebHistory(),
    routes
}))
app.mount('#app')
