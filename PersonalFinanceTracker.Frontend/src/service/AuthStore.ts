import {defineStore} from "pinia";
import {ref} from "vue";

export interface AuthData {
    token : string
    role : string
    userName : string
}

export const useAuthStore = defineStore('auth', () => {
    const data = ref<AuthData | null>(null)

    return { data }
}, {
    persist: {
        key: 'auth',
        storage: localStorage,
    }
})
