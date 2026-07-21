import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import {createPinia} from "pinia";
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import {createMemoryHistory, createRouter} from "vue-router";
import HomePage from "@/components/HomePage.vue";

const app = createApp(App)
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)
const routes = [
    { path: "/", component: HomePage }
]
app.use(createRouter({
    history: createMemoryHistory(),
    routes
}))
app.mount('#app')
