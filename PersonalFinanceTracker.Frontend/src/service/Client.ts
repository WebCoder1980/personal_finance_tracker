import {type AuthData, useAuthStore} from "@/service/AuthStore.ts";

export async function login(login : string, password : string) {
    const response = await fetch("/api/auth/login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({username: login, password: password})
    })
    const data = await response.json() as AuthData
    if (!response.ok) {
        throw new Error(`Ошибка при попытке входа. Статус: ${response.status}. Тело ответа: ${JSON.stringify(data)}`)
    }
    const authStore = useAuthStore()
    authStore.data = data
}

export async function register(login : string, password : string) {
    const response = await fetch("/api/auth/register", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({username: login, password: password})
    })
    const data = await response.json()
    if (!response.ok) {
        throw new Error(`Ошибка при попытке регистрации. Статус: ${response.status}. Тело ответа: ${JSON.stringify(data)}`)
    }
}
