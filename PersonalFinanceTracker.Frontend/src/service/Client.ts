import {type AuthData, useAuthStore} from "@/service/AuthStore.ts";

export function login() {
    fetch("/api/auth/login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({username: 'admin', password: 'admin_password'})
    })
    .then(response => response.json())
    .then(json =>
    {
        const data = json as AuthData
        const authStore = useAuthStore()

        authStore.data = data
        console.log(authStore.data)
    })
    .catch(error => {
        if (error.name === 'AbortError') {
            console.log('Запрос был отменен');
        } else {
            console.error('Ошибка:', error);
        }
    })
}
