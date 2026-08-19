import {create} from "zustand/react";
import {persist} from "zustand/middleware";

export interface AuthData {
    token : string
    role : string
    userName : string
}

type AuthStore = {
    data: AuthData | null,
    setData: (newData : AuthData) => void
}

export const useAuthStore = create<AuthStore>()(
    persist(
        (set) => ({
            data: null,
            setData: (newData : AuthData) => {
                set({data: newData})
            }
        }),
        {
            name: 'bear-storage'
        }
    )
)